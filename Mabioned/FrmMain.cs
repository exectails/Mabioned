using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using MabiWorld;
using MabiWorld.Data;
using MabiWorld.PropertyEditing;
using PrimitiveCanvas.Extensions;
using PrimitiveCanvas.Interactions;
using PrimitiveCanvas.Objects;
using PrimitiveCanvas.Primitives;

namespace Mabioned
{
	/// <summary>
	/// Application's main form.
	/// </summary>
	public partial class FrmMain : Form
	{
		public const string Title = "Mabioned";
		public const int LastOpenedMax = 10;

		private const float DefaultScale = 50;
		private const float MinScale = 1;
		private const float MaxScale = 200;
		private const float ScaleStep = 5;
		private const int MaxMapSize = 1024 * 8;
		private const int NoPropShapesSize = 100;

		private MabiWorld.Region _region;
		private List<Area> _areas;
		private PointF _lowerLeft;
		private PointF _topRight;

		private ulong _selectedEntityId;
		private IEntity _selectedEntity;
		private IEntity _copyEntity;
		private Dictionary<ulong, TreeNode> _entityNodes = new Dictionary<ulong, TreeNode>();
		private Dictionary<Area, TreeNode> _areaNodes = new Dictionary<Area, TreeNode>();
		private Dictionary<EventType, MenuItem> _eventTypeMenuItems = new Dictionary<EventType, MenuItem>();

		private string _openFilePath;
		private bool _modifiedOpenFile;

		private Point _mapRightClickLocation;

		private DrawStyle _areaStyle = new DrawStyle() { OutlineColor = Settings.Default.AreasColor, SelectedOutlineColor = Settings.Default.SelectionColor };
		private DrawStyle _propStyle = new DrawStyle() { OutlineColor = Settings.Default.PropsColor, SelectedOutlineColor = Settings.Default.SelectionColor };
		private DrawStyle _eventStyle = new DrawStyle() { OutlineColor = Settings.Default.EventsColor, SelectedOutlineColor = Settings.Default.SelectionColor };
		private Font _areaNameFont = new Font("Arial", 200);
		private StringFormat _areaNameFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
		private CanvasObject _miniMapObject;

		/// <summary>
		/// Returns true if a single area is open.
		/// </summary>
		private bool AreaOnly => (_region == null && _areas.Count == 1);

		/// <summary>
		/// Returns true if areas should be displayed.
		/// </summary>
		private bool ShowAreas => this.MnuShowAreas.Checked;

		/// <summary>
		/// Returns true if props should be displayed.
		/// </summary>
		private bool ShowProps => this.MnuShowProps.Checked;

		/// <summary>
		/// Returns true if any events should be displayed.
		/// </summary>
		private bool ShowEvents => (_eventTypeMenuItems.Values.Any(a => a.Checked) || this.MnuShowEventsUndefined.Checked);

		/// <summary>
		/// Returns true if any files are currently open.
		/// </summary>
		private bool IsFileOpen => (_openFilePath != null);

		/// <summary>
		/// Returns true if any files are currently open.
		/// </summary>
		private bool IsFileModified => (_modifiedOpenFile);

		/// <summary>
		/// Creates new instance.
		/// </summary>
		public FrmMain()
		{
			this.InitializeComponent();

			NotifyingCollectionEditor.CollectionChanged += this.OnCollectionChanged;
			NotifyingCollectionEditor.CollectionPropertyChanged += this.OnCollectionPropertyChanged;
			NotifyingCollectionEditor.FormClosed += this.OnCollectionEditorFormClosed;
		}

		/// <summary>
		/// Called when the form is being loaded.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmMain_Load(object sender, EventArgs e)
		{
			this.ToolStrip.Renderer = new ToolStripRendererNL();

			this.LoadViewOptions();
			this.LoadData();
			this.SetGlobalPropertyEditors();

			var args = Environment.GetCommandLineArgs();
			if (args.Length > 1)
				this.OpenFile(args[1]);

			this.LoadRecentFilesList();

			this.UpdateWindow();

			if (this.IsFileOpen)
			{
				this.RegionCanvas.ScaleToFitCenter();
				this.RegionCanvas_ScaleChanged(this, new ScaleChangedEventArgs(0, this.RegionCanvas.ScaleCurrent));
			}
		}

		/// <summary>
		/// Loads data if data folder setting is valid.
		/// </summary>
		private void LoadData()
		{
			try
			{
				var path = Settings.Default.DataFolder;
				if (!Directory.Exists(path))
					return;

				var localPath = Path.Combine(path, "local");
				if (Directory.Exists(localPath))
					Local.Load(localPath);

				var featuresPath = Path.Combine(path, "features.xml.compiled");
				if (File.Exists(featuresPath))
				{
					Features.Load(featuresPath);
					Features.SelectSetting("USA", false, false);
				}

				var propDbPath = Path.Combine(path, "db", "propdb.xml");
				if (File.Exists(propDbPath))
					PropDb.Load(propDbPath);

				var propPalettePath = Path.Combine(path, "world", "proppalette.plt");
				if (File.Exists(propPalettePath))
					PropPalette.Load(propPalettePath);

				var miniMapInfoPath = Path.Combine(path, "db", "minimapinfo.xml");
				if (File.Exists(miniMapInfoPath))
					MiniMapInfo.Load(miniMapInfoPath);
			}
			catch (Exception ex)
			{
				MessageBox.Show("An error ocurred while loading data: " + ex, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Called when the form is closing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.IsFileOpen && this.IsFileModified)
			{
				var result = MessageBox.Show(this, "There are unsaved modifications, do you want to save before closing?", Title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (result == DialogResult.Cancel)
				{
					e.Cancel = true;
					return;
				}

				if (result == DialogResult.Yes)
				{
					if (!this.SaveFile(_openFilePath))
					{
						e.Cancel = true;
						return;
					}
				}
			}

			this.SaveSettings();
		}

		/// <summary>
		/// Updates window size and location based on loaded settings.
		/// </summary>
		private void UpdateWindow()
		{
			if (Settings.Default.WindowMaximized)
			{
				this.WindowState = FormWindowState.Maximized;
			}
			else if (Settings.Default.WindowPositionSet)
			{
				this.WindowState = FormWindowState.Normal;
				this.Location = Settings.Default.WindowPosition;
				this.Size = Settings.Default.WindowSize;
			}

			if (Settings.Default.SplitterMain != -1)
				this.SplMain.SplitterDistance = Settings.Default.SplitterMain;

			if (Settings.Default.SplitterSidebar != -1)
				this.SplSidebar.SplitterDistance = Settings.Default.SplitterSidebar;

			this.SelectTool((Tool)Settings.Default.Tool);
		}

		/// <summary>
		/// Sets view options based on loaded settings.
		/// </summary>
		private void LoadViewOptions()
		{
			this.MnuShowPropsNormal.Checked = Settings.Default.ShowPropsNormal;
			this.MnuShowPropsDisabled.Checked = Settings.Default.ShowPropsDisabled;
			this.MnuShowPropsEvent.Checked = Settings.Default.ShowPropsEvent;
			this.MnuShowPropsTerrain.Checked = Settings.Default.ShowPropsTerrain;
			this.MnuShowAreas.Checked = Settings.Default.ShowAreas;
			this.MnuShowMiniMap.Checked = Settings.Default.ShowMiniMap;

			var eventTypeType = typeof(EventType);

			this.MnuShowEventsUndefined.Checked = Settings.Default.ShowEvents[-1];
			foreach (var eventType in Enum.GetValues(eventTypeType).Cast<EventType>())
			{
				var intValue = (int)eventType;

				var menuItem = new MenuItem(string.Format("{0} ({1})", eventType, intValue));
				menuItem.Checked = Settings.Default.ShowEvents[intValue];
				menuItem.Click += this.MnuShowEventsToggle_Click;
				menuItem.Tag = intValue;

				this.MnuShowEvents.MenuItems.Add(menuItem);
				_eventTypeMenuItems[eventType] = menuItem;
			}

			_areaStyle = new DrawStyle() { OutlineColor = Settings.Default.AreasColor, SelectedOutlineColor = Settings.Default.SelectionColor };
			_propStyle = new DrawStyle() { OutlineColor = Settings.Default.PropsColor, SelectedOutlineColor = Settings.Default.SelectionColor };
			_eventStyle = new DrawStyle() { OutlineColor = Settings.Default.EventsColor, SelectedOutlineColor = Settings.Default.SelectionColor };

			this.UpdateShowPropsAll();
			this.UpdateShowEventsAll();
		}

		/// <summary>
		/// Updates and saves settings.
		/// </summary>
		private void SaveSettings()
		{
			Settings.Default.ShowPropsNormal = this.MnuShowPropsNormal.Checked;
			Settings.Default.ShowPropsDisabled = this.MnuShowPropsDisabled.Checked;
			Settings.Default.ShowPropsEvent = this.MnuShowPropsEvent.Checked;
			Settings.Default.ShowPropsTerrain = this.MnuShowPropsTerrain.Checked;
			Settings.Default.ShowAreas = this.MnuShowAreas.Checked;
			Settings.Default.ShowMiniMap = this.MnuShowMiniMap.Checked;

			Settings.Default.ShowEvents[-1] = this.MnuShowEventsUndefined.Checked;
			foreach (var menuItem in _eventTypeMenuItems.Values)
				Settings.Default.ShowEvents[(int)menuItem.Tag] = menuItem.Checked;

			Settings.Default.WindowMaximized = (this.WindowState == FormWindowState.Maximized);
			if (this.WindowState == FormWindowState.Normal)
			{
				Settings.Default.WindowPosition = this.Location;
				Settings.Default.WindowSize = this.Size;
			}

			Settings.Default.SplitterMain = this.SplMain.SplitterDistance;
			Settings.Default.SplitterSidebar = this.SplSidebar.SplitterDistance;

			Settings.Default.Tool = (int)this.RegionCanvas.SelectedTool;

			Settings.Default.Save();
		}

		/// <summary>
		/// Updates list of recent files from settings.
		/// </summary>
		private void LoadRecentFilesList()
		{
			var list = Settings.Default.LastOpenedFiles;

			this.MnuRecent.MenuItems.Clear();

			if (list.Count > 0)
			{
				for (int i = list.Count - 1, j = 1; i >= 0; --i)
				{
					var filePath = list[i];
					var menuItem = new MenuItem(j++ + " " + filePath);
					menuItem.Click += this.MnuRecentItem_Click;
					menuItem.Tag = filePath;

					this.MnuRecent.MenuItems.Add(menuItem);
				}
			}
			else
			{
				this.MnuRecent.MenuItems.Add(new MenuItem("None") { Enabled = false });
			}
		}

		/// <summary>
		/// Opens a recently opened file.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuRecentItem_Click(object sender, EventArgs e)
		{
			var menuItem = (sender as MenuItem);
			if (menuItem?.Tag is string filePath)
				this.OpenFile(filePath);
		}

		/// <summary>
		/// Sets global editors for the property grid.
		/// </summary>
		private void SetGlobalPropertyEditors()
		{
			TypeDescriptor.AddAttributes(typeof(Color),
				new EditorAttribute(typeof(ColorEditor), typeof(UITypeEditor)),
				new TypeConverterAttribute(typeof(MyColorConverter)));
		}

		/// <summary>
		/// Called when the Open menu option is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuOpen_Click(object sender, EventArgs e)
		{
			var result = this.OfdRegion.ShowDialog();

			if (result != DialogResult.OK)
				return;

			this.OpenFile(this.OfdRegion.FileName);
		}

		/// <summary>
		/// Opens given file.
		/// </summary>
		/// <param name="filePath"></param>
		public void OpenFile(string filePath)
		{
			// Ensure that we don't run another opening while the message
			// box for saving modifications is still open if user repeatedly
			// opens a new file.
			//if (_openOnUnsavedChanges)
			//	return;

			if (!File.Exists(filePath))
			{
				MessageBox.Show("File not found.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var ext = Path.GetExtension(filePath);
			if (ext != ".rgn" && ext != ".area")
			{
				MessageBox.Show("Unsupported file format.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (this.IsFileOpen && this.IsFileModified)
			{
				var result = MessageBox.Show(this, "There are unsaved modifications, do you want to save before opening a new file?", Title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (result == DialogResult.Cancel)
					return;

				if (result == DialogResult.Yes)
				{
					if (!this.SaveFile(_openFilePath))
						return;
				}
			}

			try
			{
				if (ext == ".rgn")
				{
					_region = MabiWorld.Region.ReadFromFile(filePath);
					_areas = _region.Areas;
				}
				else if (ext == ".area")
				{
					_region = null;
					_areas = new List<Area>();
					_areas.Add(Area.ReadFromFile(filePath));
				}

				var minX = _areas.Min(a => a.BottomLeft.X);
				var maxX = _areas.Max(a => a.BottomRight.X);
				var minY = _areas.Min(a => a.BottomLeft.Y);
				var maxY = _areas.Max(a => a.TopLeft.Y);
				_lowerLeft = new PointF(minX, minY);
				_topRight = new PointF(maxX, maxY);

				this.CreateTree();
				this.InitCanvas();

				_openFilePath = filePath;
				this.AddLastOpenedFile(filePath);
				this.SetModified(false);
				this.RegionCanvas_ScaleChanged(this, new ScaleChangedEventArgs(0, this.RegionCanvas.ScaleCurrent));
			}
			catch (UnsupportedVersionException)
			{
				MessageBox.Show(this, "This file's version is not supported yet.", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to open file." + Environment.NewLine + ex, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			this.MnuEditRemoveAllProps.Enabled = true;
			this.MnuEditRemoveEvents.Enabled = true;
			this.MnuFlattenTerrain.Enabled = true;
		}

		/// <summary>
		/// Initializes canvas for current region and areas.
		/// </summary>
		private void InitCanvas()
		{
			var canvas = this.RegionCanvas;

			canvas.BeginUpdate();
			canvas.ClearObjects();

			var region = _region;
			var areas = _areas;
			if (areas == null || !areas.Any())
			{
				canvas.SetCanvasArea(0, 0);
				return;
			}

			canvas.SetCanvasArea(_topRight.X, _topRight.Y);
			canvas.ScaleToFitCenter();

			canvas.CanvasBackColor = Settings.Default.BackgroundColor;

			if (region != null && MiniMapInfo.TryGetEntry(region.Id, out var entry))
			{
				var miniMapImage = this.GetMiniMapImage(entry);
				if (miniMapImage != null)
				{
					var x = 0;
					var y = 0;
					//var width = _topRight.X;
					//var height = _topRight.Y;
					var width = entry.MapWidth;
					var height = entry.MapHeight;
					var imgWidth = miniMapImage.Width;
					var imgHeight = miniMapImage.Height;

					if (entry.MapOffsetX != 0)
					{
						x += entry.MapOffsetX;
						//width -= entry.MapOffsetX;
					}

					if (entry.MapOffsetY != 0)
					{
						y += entry.MapOffsetY;
						//height -= entry.MapOffsetY;
					}

					// Keep aspect ratio? Didn't work well on the test case
					// "dugald_aisle_keep", where the mini map is way too
					// small if not stretched, but if streched it does kinda
					// match.
					//var xs = (width / imgWidth);
					//var ys = (height / imgHeight);
					//if (xs < ys)
					//{
					//	width = imgWidth * xs;
					//	height = imgHeight * xs;
					//}
					//else if (ys < xs)
					//{
					//	width = imgWidth * ys;
					//	height = imgHeight * ys;
					//}

					var pic = new Picture(miniMapImage, new RectangleF(0, 0, imgWidth, imgHeight), new RectangleF(x, y, width, height));
					var rect = new FlatRect(new RectangleF(x, y, width, height), Color.FromArgb(127, 255, 255, 255));

					var obj = new CanvasObject(0, 0, pic, rect);
					obj.Interactions = ObjectInteractions.None;
					obj.DrawOrder = 1;
					obj.Visible = this.MnuShowMiniMap.Checked;

					canvas.Add(obj);
					_miniMapObject = obj;
				}
			}

			for (var j = 0; j < areas.Count; ++j)
			{
				var area = areas[j];

				var w = (area.BottomRight.X - area.BottomLeft.X);
				var h = (area.TopLeft.Y - area.BottomLeft.Y);
				var x = (area.BottomLeft.X + w / 2);
				var y = (area.TopLeft.Y + h / 2);
				y -= h;

				var areaObj = new CanvasObject(x, y);
				areaObj.Add(new Rect(x, y, w, h));
				areaObj.Add(new TextString(new PointF(x, y), _areaNameFont, area.Name) { StringFormat = _areaNameFormat });
				areaObj.Interactions = ObjectInteractions.None;
				areaObj.Visible = this.ShowAreas;
				areaObj.DrawOrder = 100;
				areaObj.Tag = area;
				areaObj.Style = _areaStyle;
				canvas.Add(areaObj);

				area.Tag = areaObj;

				for (var i = 0; i < area.Events.Count; ++i)
				{
					var evnt = area.Events[i];
					var evntObj = this.GetEventCanvasObject(evnt);

					evnt.Tag = evntObj;
					canvas.Add(evntObj);
				}

				for (var i = 0; i < area.Props.Count; ++i)
				{
					var prop = area.Props[i];
					var propObj = this.GetPropCanvasObject(prop);

					prop.Tag = propObj;
					canvas.Add(propObj);
				}
			}

			canvas.EndUpdate();
		}

		/// <summary>
		/// Creates canvas object from prop.
		/// </summary>
		/// <param name="prop"></param>
		/// <returns></returns>
		private CanvasObject GetPropCanvasObject(Prop prop)
		{
			var obj = new CanvasObject(prop.Position.X, prop.Position.Y);

			// Use a circle if prop doesn't have a shape, so it's still
			// selectable on the map.
			if (prop.Shapes.Any())
			{
				foreach (var shape in prop.Shapes)
					obj.Add(new Polygon(shape.GetPoints()));
			}
			else
			{
				obj.Add(new Circle(prop.Position.X, prop.Position.Y, 50));
			}

			obj.Visible = this.DisplayProp(prop);
			obj.DrawOrder = 200;
			obj.Priority = 100;
			obj.Tag = prop;
			obj.Style = _propStyle;

			return obj;
		}

		/// <summary>
		/// Creates canvas object from event.
		/// </summary>
		/// <param name="evnt"></param>
		/// <returns></returns>
		private CanvasObject GetEventCanvasObject(Event evnt)
		{
			var obj = new CanvasObject(evnt.Position.X, evnt.Position.Y);

			// Use a circle if event doesn't have a shape, so it's still
			// selectable on the map.
			if (evnt.Shapes.Any())
			{
				foreach (var shape in evnt.Shapes)
					obj.Add(new Polygon(shape.GetPoints()));
			}
			else
			{
				obj.Add(new Circle(evnt.Position.X, evnt.Position.Y, 50));
			}

			obj.Visible = this.DisplayEventType(evnt.Type);
			obj.DrawOrder = 100;
			obj.Priority = 200;
			obj.Tag = evnt;
			obj.Style = _eventStyle;

			return obj;
		}

		/// <summary>
		/// Returns the mini map for the given region if it exists in the
		/// data folder.
		/// </summary>
		/// <param name="entry"></param>
		/// <returns></returns>
		private Image GetMiniMapImage(MiniMapInfoEntry entry)
		{
			var filePath = entry.MapFile;
			if (filePath.StartsWith("data/") || filePath.StartsWith("data\\"))
				filePath = filePath.Substring(5);

			filePath = Path.Combine(Settings.Default.DataFolder, filePath);

			if (!File.Exists(filePath))
				return null;

			return new Bitmap(filePath);
		}

		/// <summary>
		/// Adds file path to list of last opened files.
		/// </summary>
		/// <param name="filePath"></param>
		private void AddLastOpenedFile(string filePath)
		{
			var list = Settings.Default.LastOpenedFiles;

			if (list.Contains(filePath))
				list.Remove(filePath);
			else if (list.Count >= LastOpenedMax)
				list.RemoveAt(0);

			list.Add(filePath);
			this.LoadRecentFilesList();
		}

		/// <summary>
		/// Toggles save buttons and options based on the application's
		/// current state.
		/// </summary>
		private void UpdateSavingControls()
		{
			var isFileOpen = this.IsFileOpen;
			var isFileModified = this.IsFileModified;

			this.BtnSave.Enabled = isFileModified;
			this.MnuSave.Enabled = isFileModified;
			this.MnuSaveAs.Enabled = isFileOpen;
		}

		/// <summary>
		/// Updates the form's title based on the application's current
		/// state.
		/// </summary>
		private void UpdateTitle()
		{
			if (!this.IsFileOpen)
			{
				this.Text = Title;
				return;
			}

			var fileName = _openFilePath;
			var modified = this.IsFileModified;
			var modifiedMarker = (modified ? "*" : "");

			this.Text = string.Format("{2}{0} - {1}", fileName, Title, modifiedMarker);
		}

		/// <summary>
		/// Saves currently open file(s).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSave_Click(object sender, EventArgs e)
		{
			if (!this.IsFileOpen)
				return;

			this.SaveFile(_openFilePath);
		}

		/// <summary>
		/// Saves currently open file(s) at a specific location.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuSaveAs_Click(object sender, EventArgs e)
		{
			if (!this.IsFileOpen)
				return;

			var openFilePath = _openFilePath;

			// Set filter based on open file
			var ext = Path.GetExtension(openFilePath);
			if (ext == ".rgn")
				this.SfdRegion.Filter = "Region File|*.rgn";
			else
				this.SfdRegion.Filter = "Area File|*.area";

			// Set name and location to that of the open file
			var fileName = Path.GetFileName(openFilePath);
			var dirPath = Path.GetDirectoryName(openFilePath);

			this.SfdRegion.FileName = fileName;
			this.SfdRegion.InitialDirectory = dirPath;

			// Get new file path
			var result = this.SfdRegion.ShowDialog();
			if (result != DialogResult.OK)
				return;

			var filePath = this.SfdRegion.FileName;

			// Ask for confirmation if some area files already exist.
			if (ext == ".rgn")
			{
				dirPath = Path.GetDirectoryName(filePath);
				var areasExist = _areas.Any(a => File.Exists(Path.Combine(dirPath, a.Name + ".area")));
				if (areasExist)
				{
					var confirmAreaWrite = MessageBox.Show("One or more of the area files already exist, do you want to save the region and overwrite them?", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (confirmAreaWrite != DialogResult.Yes)
						return;
				}
			}

			this.SaveFile(filePath);
		}

		/// <summary>
		/// Writes the currently open file(s) to the given location.
		/// Returns false if saving any file failed, even if some were saved.
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		private bool SaveFile(string filePath)
		{
			if (!this.AreaOnly)
			{
				var dirPath = Path.GetDirectoryName(filePath);

				try
				{
					// Set area file names based on currently loaded
					// areas' names.
					_region.AreaFileNames.Clear();
					_region.AreaFileNames.AddRange(_areas.Select(a => a.Name));

					using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
						_region.WriteTo(fs);

					_openFilePath = filePath;
					this.AddLastOpenedFile(filePath);
					this.SetModified(false);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Failed to save region. Error: " + ex.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}

				foreach (var area in _areas)
				{
					var fileName = area.Name.ToLowerInvariant() + ".area";
					filePath = Path.Combine(dirPath, fileName);
					if (!saveArea(area, filePath))
						return false;
				}
			}
			else
			{
				var area = _areas[0];
				if (!saveArea(area, filePath))
					return false;

				_openFilePath = filePath;
				this.AddLastOpenedFile(filePath);
				this.SetModified(false);

			}

			return true;

			// Saves area file, returns false if saving failed.
			bool saveArea(Area area, string areaFilePath)
			{
				try
				{
					using (var fs = new FileStream(areaFilePath, FileMode.Create, FileAccess.Write))
						area.WriteTo(fs);

					return true;
				}
				catch (Exception ex)
				{
					MessageBox.Show(string.Format("Failed to save area '{0}'. Error: {1}", area.Name, ex.Message), Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				return false;
			}
		}

		/// <summary>
		/// Generates tree based on current region and areas.
		/// </summary>
		private void CreateTree()
		{
			_areaNodes.Clear();
			_entityNodes.Clear();

			this.TreeRegion.BeginUpdate();
			{
				this.TreeRegion.Nodes.Clear();

				if (!this.AreaOnly)
				{
					var regionNode = new TreeNode(GetRegionNodeName(_region.Name));
					regionNode.Tag = _region;
					regionNode.ImageKey = regionNode.SelectedImageKey = "region";

					foreach (var area in _areas)
					{
						var node = this.CreateAreaNode(area);
						regionNode.Nodes.Add(node);
						_areaNodes[area] = node;
					}

					this.TreeRegion.Nodes.Add(regionNode);
				}
				else
				{
					var area = _areas[0];
					var node = this.CreateAreaNode(area);
					this.TreeRegion.Nodes.Add(node);
					_areaNodes[area] = node;
				}

				if (this.TreeRegion.Nodes.Count == 1)
				{
					this.TreeRegion.Nodes[0].Expand();
					if (this.TreeRegion.Nodes[0].Nodes.Count == 1)
						this.TreeRegion.Nodes[0].Nodes[0].Expand();
					this.TreeRegion.SelectedNode = this.TreeRegion.Nodes[0];
				}
			}
			this.TreeRegion.EndUpdate();
		}

		/// <summary>
		/// Creates and returns tree node for area.
		/// </summary>
		/// <param name="area"></param>
		/// <returns></returns>
		private TreeNode CreateAreaNode(Area area)
		{
			// Create base node
			var areaNode = new TreeNode(GetAreaNodeName(area));
			areaNode.Tag = area;
			areaNode.ImageKey = areaNode.SelectedImageKey = "area";

			// Create entity nodes
			var propsNode = areaNode.Nodes.Add(string.Format("Props ({0})", area.Props.Count));
			var eventsNode = areaNode.Nodes.Add(string.Format("Events ({0})", area.Events.Count));

			propsNode.ImageKey = propsNode.SelectedImageKey = "prop";
			eventsNode.ImageKey = eventsNode.SelectedImageKey = "event";

			// Add props
			foreach (var prop in area.Props)
			{
				var propNode = this.GetPropNode(prop);
				propsNode.Nodes.Add(propNode);

				_entityNodes[prop.EntityId] = propNode;
			}

			// Add events
			foreach (var evnt in area.Events)
			{
				var eventNode = this.GetEventNode(evnt);
				eventsNode.Nodes.Add(eventNode);

				_entityNodes[evnt.EntityId] = eventNode;
			}

			return areaNode;
		}

		/// <summary>
		/// Creates new tree node for prop.
		/// </summary>
		/// <param name="prop"></param>
		/// <returns></returns>
		private TreeNode GetPropNode(Prop prop)
		{
			var node = new TreeNode(string.Format("0x{0:X16}", prop.EntityId));
			node.ImageKey = node.SelectedImageKey = "prop";
			node.Tag = prop;

			return node;
		}

		/// <summary>
		/// Creates new tree node for event.
		/// </summary>
		/// <param name="evnt"></param>
		/// <returns></returns>
		private TreeNode GetEventNode(Event evnt)
		{
			var node = new TreeNode(string.Format("0x{0:X16}", evnt.EntityId));
			node.ImageKey = node.SelectedImageKey = "event";
			node.Tag = evnt;

			return node;
		}

		/// <summary>
		/// Returns name for area tree node.
		/// </summary>
		/// <param name="area"></param>
		/// <returns></returns>
		private static string GetAreaNodeName(Area area)
		{
			return string.Format("Area: {0}  ({1}, {2})", area.Name, area.Props.Count, area.Events.Count);
		}

		/// <summary>
		/// Returns name for region tree node.
		/// </summary>
		/// <param name="regionName"></param>
		/// <returns></returns>
		private static string GetRegionNodeName(string regionName)
		{
			return string.Format("Region: {0}", regionName);
		}

		/// <summary>
		/// Returns the height at the given position on the map.
		/// </summary>
		/// <param name="regionPos"></param>
		/// <returns></returns>
		private float ProbeHeight(PointF regionPos)
		{
			// Not sure if this is quite right yet.

			for (var i = 0; i < _areas.Count; ++i)
			{
				var area = _areas[i];

				var areaPlanes = area.AreaPlanes;
				var areaPlaneWidth = (area.BottomRight.X - area.BottomLeft.X) / area.PlaneX;
				var areaPlaneHeight = (area.TopLeft.Y - area.BottomLeft.Y) / area.PlaneY;

				// Area planes and planes go up from bottom left to top right.

				for (var apx = 0; apx < area.PlaneX; ++apx)
				{
					for (var apy = 0; apy < area.PlaneY; ++apy)
					{
						var areaPlane = areaPlanes[apx * area.PlaneY + apy];
						var planes = areaPlane.Planes;

						var planeWidth = areaPlaneWidth / areaPlane.Size;
						var planeHeight = areaPlaneHeight / areaPlane.Size;

						for (var px = 0; px < areaPlane.Size; ++px)
						{
							for (var py = 0; py < areaPlane.Size; ++py)
							{
								var plane = planes[px * areaPlane.Size + py];

								var x = (area.BottomLeft.X + apx * areaPlaneWidth + px * planeHeight);
								var y = (area.BottomLeft.Y + apy * areaPlaneHeight + py * planeWidth);
								var w = (planeWidth);
								var h = (planeHeight);

								if (regionPos.X >= x && regionPos.X <= x + w && regionPos.Y >= y && regionPos.Y <= y + h)
									return plane.Height;
							}
						}
					}
				}
			}

			return -1;
		}

		/// <summary>
		/// Called when a node on the tree is selected.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TreeRegion_AfterSelect(object sender, TreeViewEventArgs e)
		{
			var selectedNode = e.Node;
			var tag = e.Node?.Tag;

			// Select entity from node
			if (tag != null && tag is IEntity entity)
			{
				this.SetSelectedEntity(entity);
			}
			// Unselect entity if node is not an entity
			else if (_selectedEntityId != 0)
			{
				this.SetSelectedEntity(null);
				this.TreeRegion.SelectedNode = selectedNode;
			}

			this.PropertyGrid.SelectedObject = tag;

			// Redraw map if selection was changed by the user.
			if (e.Action != TreeViewAction.Unknown)
				this.RegionCanvas.Invalidate();
		}

		/// <summary>
		/// Called when a node in the tree is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TreeRegion_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				// Select clicked node on right-click
				if (e.Node?.Tag is IEntity entity)
				{
					this.SetSelectedEntity(entity);
				}
				else
				{
					this.SetSelectedEntity(null);
					this.TreeRegion.SelectedNode = e.Node;
				}

				//ShowPropertyEditor("Parameters");

				// Show area context menu
				if (e.Node?.Tag is Area area)
				{
					this.CtxTreeArea.Show(this.TreeRegion, e.Location);
				}
			}
		}

		/// <summary>
		/// Called when a node in the tree is double clicked,
		/// focuses map on clicked entity.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TreeRegion_DoubleClick(object sender, EventArgs e)
		{
			var tag = this.TreeRegion.SelectedNode?.Tag;

			if (tag is IEntity entity)
				this.RegionCanvas.ScrollToWorldPosition(entity.Position);
		}

		/// <summary>
		/// Called when something is dragged onto the form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmMain_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = (e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None);
		}

		/// <summary>
		/// Called when something is dropped onto the form, loads dropped
		/// text file.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmMain_DragDrop(object sender, DragEventArgs e)
		{
			var filePaths = e.Data.GetData(DataFormats.FileDrop) as string[];
			if (filePaths.Length == 0)
				return;

			this.OpenFile(filePaths[0]);
		}

		/// <summary>
		/// Called when the Exit menu option is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Toggles the visibility of all props.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuShowPropsAll_Click(object sender, EventArgs e)
		{
			this.MnuShowPropsAll.Checked = !this.MnuShowPropsAll.Checked;

			foreach (MenuItem menuItem in this.MnuShowProps.MenuItems)
				menuItem.Checked = this.MnuShowPropsAll.Checked;

			this.UpdatePropVisibility();
		}

		/// <summary>
		/// Called when the "Show Props" menu option is clicked,
		/// toggles prop visibility.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuShowPropsToggle_Click(object sender, EventArgs e)
		{
			var menuItem = (sender as MenuItem);
			var visible = (menuItem.Checked = !menuItem.Checked);

			this.UpdateShowPropsAll();
			this.UpdatePropVisibility();
		}

		/// <summary>
		/// Toggles "Show Props > All" option based on the other selected
		/// options.
		/// </summary>
		private void UpdateShowPropsAll()
		{
			this.MnuShowPropsAll.Checked = this.MnuShowProps.MenuItems.Cast<MenuItem>().Where(a => a.Tag == null).All(a => a.Checked);
		}

		/// <summary>
		/// Updates visibility of props on canvas.
		/// </summary>
		private void UpdatePropVisibility()
		{
			if (!this.IsFileOpen)
				return;

			for (var i = 0; i < _areas.Count; ++i)
			{
				var area = _areas[i];
				var props = area.Props;

				for (var j = 0; j < props.Count; ++j)
				{
					var prop = props[j];
					((CanvasObject)prop.Tag).Visible = this.DisplayProp(prop);
				}
			}

			this.RegionCanvas.Invalidate();
		}

		/// <summary>
		/// Called when the "Show Areas" menu option is clicked,
		/// toggles area visibility.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuShowAreas_Click(object sender, EventArgs e)
		{
			var menuItem = (sender as MenuItem);
			var visible = (menuItem.Checked = !menuItem.Checked);

			if (!this.IsFileOpen)
				return;

			for (var i = 0; i < _areas.Count; ++i)
			{
				var area = _areas[i];
				((CanvasObject)area.Tag).Visible = visible;
			}

			this.RegionCanvas.Invalidate();
		}

		/// <summary>
		/// Sets the currently selected entity, highlightning it.
		/// </summary>
		/// <param name="entity"></param>
		private void SetSelectedEntity(IEntity entity)
		{
			this.SetSelectedEntity(entity?.EntityId ?? 0);
		}

		/// <summary>
		/// Sets the currently selected entity by id, highlightning it.
		/// </summary>
		/// <param name="entityId"></param>
		private void SetSelectedEntity(ulong entityId)
		{
			_selectedEntityId = entityId;

			if (_entityNodes.TryGetValue(entityId, out var node))
			{
				this.TreeRegion.SelectedNode = node;
				_selectedEntity = node.Tag as IEntity;
				this.RegionCanvas.SelectObject((CanvasObject)_selectedEntity.Tag, false);
			}
			else
			{
				this.TreeRegion.SelectedNode = null;
				this.PropertyGrid.SelectedObject = null;
				_selectedEntity = null;
				this.RegionCanvas.ClearSelection();
			}
		}

		/// <summary>
		/// Called when the property grid's sort order changed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PropertyGrid_PropertySortChanged(object sender, EventArgs e)
		{
			// Use non-alphabetical categorized order to keep the order
			// from the files, if someone wants alphabetical ordering they
			// can get it without categories.
			if (this.PropertyGrid.PropertySort == PropertySort.CategorizedAlphabetical)
				this.PropertyGrid.PropertySort = PropertySort.Categorized;
		}

		/// <summary>
		/// Called when the "Show Events > All" menu option is clicked,
		/// toggles between displaying all or no events and triggers
		/// a map redraw.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuShowEventsAll_Click(object sender, EventArgs e)
		{
			this.MnuShowEventsAll.Checked = !this.MnuShowEventsAll.Checked;

			foreach (MenuItem menuItem in this.MnuShowEvents.MenuItems)
				menuItem.Checked = this.MnuShowEventsAll.Checked;

			this.UpdateCanvasEventVisibility();
		}

		/// <summary>
		/// Called when a "Show Events > ..." menu option is clicked.
		/// Toggles the option and triggers a map redraw.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuShowEventsToggle_Click(object sender, EventArgs e)
		{
			var menuItem = (sender as MenuItem);
			menuItem.Checked = !menuItem.Checked;

			this.UpdateShowEventsAll();
			this.UpdateCanvasEventVisibility();
		}

		/// <summary>
		/// Toggles "Show Events > All" option based on the other selected
		/// options.
		/// </summary>
		private void UpdateShowEventsAll()
		{
			this.MnuShowEventsAll.Checked = this.MnuShowEvents.MenuItems.Cast<MenuItem>().Where(a => a.Tag is int).All(a => a.Checked);
		}

		/// <summary>
		/// Updates visibility of all event canvas objects based on their
		/// type's visibility setting.
		/// </summary>
		private void UpdateCanvasEventVisibility()
		{
			if (!this.IsFileOpen)
				return;

			for (var i = 0; i < _areas.Count; ++i)
			{
				var area = _areas[i];
				var events = area.Events;

				for (var j = 0; j < events.Count; ++j)
				{
					var evnt = events[j];
					((CanvasObject)evnt.Tag).Visible = this.DisplayEventType(evnt.Type);
				}
			}

			this.RegionCanvas.Invalidate();
		}

		/// <summary>
		/// Returns true if the given prop should be displayed.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		private bool DisplayProp(Prop prop)
		{
			// Directly return true if the option "All" is checked or when
			// no data could be found for a prop.
			if (this.MnuShowPropsAll.Checked || !PropDb.TryGetEntry(prop.Id, out var data))
				return true;

			var isEventProp = (data.StringID.Value.Contains("/event/") && data.UsedServer);
			var isDisabledProp = (!string.IsNullOrWhiteSpace(data.Feature) && !Features.IsEnabled(data.Feature));
			var isTerrainProp = (data.IsTerrainBlock);
			var isNormalProp = (!isEventProp && !isDisabledProp && !isTerrainProp);

			// Check for event props.
			if (!this.MnuShowPropsEvent.Checked && isEventProp)
				return false;

			// Check for props that are disabled based on the current
			// feature settings.
			if (!this.MnuShowPropsDisabled.Checked && isDisabledProp)
				return false;

			// Check terrain props
			if (!this.MnuShowPropsTerrain.Checked && isTerrainProp)
				return false;

			// Check props that didn't fit another category
			if (!this.MnuShowPropsNormal.Checked && isNormalProp)
				return false;

			// Return true by default of none of the checks matched.
			return true;
		}

		/// <summary>
		/// Returns true if the given event type should be displayed.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		private bool DisplayEventType(EventType type)
		{
			if (!_eventTypeMenuItems.TryGetValue(type, out var menuItem))
				return this.MnuShowEventsUndefined.Checked;

			return menuItem.Checked;
		}

		/// <summary>
		/// Called when the map context menu's coordinates copy option was
		/// clicked. Copies the current coordinates into clipboard.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuCopyCoordinates_Click(object sender, EventArgs e)
		{
			var pos = this.RegionCanvas.GetWorldPosition(_mapRightClickLocation);
			Clipboard.SetText(string.Format("{0:0}; {1:0}", pos.X, pos.Y));
		}

		/// <summary>
		/// Called when the map context menu's warp command copy option was
		/// clicked. Copies a warp command to clipboard to get to the
		/// location on Aura.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuCopyAuraWarp_Click(object sender, EventArgs e)
		{
			var regionId = (_region?.Id ?? _areas?.First().RegionId) ?? 0;
			var pos = this.RegionCanvas.GetWorldPosition(_mapRightClickLocation);
			Clipboard.SetText(string.Format(">warp {0} {1:0} {2:0}", regionId, pos.X, pos.Y));
		}

		/// <summary>
		/// Called when a property changes.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="e"></param>
		private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			var propertyName = e.ChangedItem.PropertyDescriptor?.Name;
			var updateEntityNodes = false;

			if (this.PropertyGrid.SelectedObject is IEntity entity)
			{
				if (propertyName == "Position")
				{
					var oldValue = (Vector3F)e.OldValue;
					var newValue = (Vector3F)e.ChangedItem.Value;
					var diff = (SizeF)(newValue - oldValue);

					foreach (var shape in entity.Shapes)
					{
						shape.Position += diff;
						shape.BottomLeft += diff;
						shape.TopRight += diff;
					}

					if (entity.Tag is CanvasObject obj)
						obj.MoveBy(diff.Width, diff.Height);
				}
				else if (propertyName == "Rotation")
				{
					var oldValue = (float)e.OldValue;
					var newValue = (float)e.ChangedItem.Value;
					var diff = (newValue - oldValue);

					foreach (var shape in entity.Shapes)
					{
						var points = shape.GetPoints();
						for (var i = 0; i < points.Length; ++i)
							points[i] = points[i].RotatePoint(entity.Position, diff);

						shape.SetRotationFromPoints(points);
					}

					if (entity.Tag is CanvasObject obj)
						obj.Rotate(diff);
				}
				else if (propertyName == "Scale")
				{
					var oldValue = (float)e.OldValue;
					var newValue = (float)e.ChangedItem.Value;
					var multiplier = (1.0 / oldValue * newValue);

					foreach (var shape in entity.Shapes)
					{
						var lenX = shape.LenX;
						var lenY = shape.LenX;

						lenX = lenX / oldValue * newValue;
						lenY = lenY / oldValue * newValue;

						shape.LenX = lenX;
						shape.LenY = lenY;
					}

					if (entity.Tag is CanvasObject obj)
						obj.Resize(multiplier);
				}
			}
			else if (this.PropertyGrid.SelectedObject is MabiWorld.Region region)
			{
				if (propertyName == "Id")
				{
					var newId = (ushort)(int)e.ChangedItem.Value;
					var clearMask = 0x0000_FFFF_0000_0000UL;
					var newMask = ((ulong)newId << 32);

					foreach (var area in _areas)
					{
						area.RegionId = newId;
						foreach (var prop in area.Props)
						{
							prop.EntityId &= ~clearMask;
							prop.EntityId |= newMask;
						}
						foreach (var evnt in area.Events)
						{
							evnt.EntityId &= ~clearMask;
							evnt.EntityId |= newMask;
						}
					}

					updateEntityNodes = true;
				}
				else if (propertyName == "Name" && this.TreeRegion.SelectedNode?.Tag == region)
				{
					this.TreeRegion.SelectedNode.Text = GetRegionNodeName(region.Name);
				}
			}
			else if (this.PropertyGrid.SelectedObject is MabiWorld.Area area)
			{
				if (propertyName == "Id")
				{
					var newId = (ushort)e.ChangedItem.Value;
					var clearMask = 0x0000_0000_FFFF_0000UL;
					var newMask = ((ulong)newId << 16);

					foreach (var prop in area.Props)
					{
						prop.EntityId &= ~clearMask;
						prop.EntityId |= newMask;
					}
					foreach (var evnt in area.Events)
					{
						evnt.EntityId &= ~clearMask;
						evnt.EntityId |= newMask;
					}

					updateEntityNodes = true;
				}
				else if (propertyName == "Name" && this.TreeRegion.SelectedNode?.Tag == area)
				{
					if (_areas.Count(a => a.Name == area.Name) > 1)
					{
						MessageBox.Show("Area names must be unique.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);

						area.Name = (string)e.OldValue;
						this.PropertyGrid.Refresh();
						return;
					}

					this.TreeRegion.SelectedNode.Text = GetAreaNodeName(area);
				}
			}

			if (updateEntityNodes)
			{
				this.TreeRegion.BeginUpdate();
				foreach (var node in _entityNodes.Values)
				{
					if (node.Tag is IEntity entiti)
						node.Text = "0x" + entiti.EntityId.ToString("X16");
				}
				this.TreeRegion.EndUpdate();
			}

			this.SetModified(true);
			this.RegionCanvas.Invalidate();
		}

		/// <summary>
		/// Called when a collection editor form is closed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnCollectionEditorFormClosed(object sender, FormClosedEventArgs e)
		{
			if (this.PropertyGrid.SelectedObject is IEntity entity && this.PropertyGrid.SelectedGridItem.PropertyDescriptor.Name == "Shapes")
				this.UpdateCanvasShapes(entity);

			//this.SetModified(true);
			this.RegionCanvas.Invalidate();
		}

		/// <summary>
		/// Called when the collection in a collection editor changed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnCollectionChanged(object sender, CollectionChangedEventArgs e)
		{
			if (this.PropertyGrid.SelectedObject is IEntity entity && this.PropertyGrid.SelectedGridItem.PropertyDescriptor.Name == "Shapes")
			{
				// Set default value for new shapes.
				if (e.Type == CollectionChangeType.Add)
				{
					var shape = entity.Shapes.Last();

					shape.DirX1 = -1;
					shape.DirX2 = 0;
					shape.DirY1 = 0;
					shape.DirY2 = 1;
					shape.LenX = 100;
					shape.LenY = 100;
					shape.Position = entity.Position;
					shape.BottomLeft = (shape.Position - new SizeF(50, 50));
					shape.TopRight = (shape.Position + new SizeF(50, 50));
				}

				this.UpdateCanvasShapes(entity);
			}

			this.SetModified(true);
			this.RegionCanvas.Invalidate();
		}

		/// <summary>
		/// Called when a property of an item in a collection editor changed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnCollectionPropertyChanged(object sender, PropertyValueChangedEventArgs e)
		{
			if (this.PropertyGrid.SelectedObject is IEntity entity && this.PropertyGrid.SelectedGridItem.PropertyDescriptor.Name == "Shapes")
				this.UpdateCanvasShapes(entity);

			this.SetModified(true);
			this.RegionCanvas.Invalidate();
		}

		/// <summary>
		/// Updates shapes of entity on canvas.
		/// </summary>
		/// <param name="entity"></param>
		private void UpdateCanvasShapes(IEntity entity)
		{
			var obj = (entity.Tag as CanvasObject);
			obj.Primitives.Clear();
			if (entity.Shapes.Any())
			{
				foreach (var shape in entity.Shapes)
					obj.Add(new Polygon(shape.GetPoints()));
			}
			else
			{
				obj.Add(new Circle(entity.Position.X, entity.Position.Y, 50));
			}
		}

		/// <summary>
		/// Called when the "Scale to fit" menu option was clicked,
		/// scales the map to fit into the panel.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuScaleToFit_Click(object sender, EventArgs e)
		{
			this.RegionCanvas.ScaleToFitCenter();
		}

		/// <summary>
		/// Expands all nodes in tree.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuExpand_Click(object sender, EventArgs e)
		{
			var selectedNode = this.TreeRegion.SelectedNode;
			this.TreeRegion.ExpandAll();
			this.TreeRegion.SelectedNode = selectedNode;
			if (selectedNode != null)
				this.TreeRegion.SelectedNode.EnsureVisible();
		}

		/// <summary>
		/// Collapses all nodes in tree.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuCollapse_Click(object sender, EventArgs e)
		{
			this.TreeRegion.CollapseAll();
		}

		/// <summary>
		/// Opens About form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuAbout_Click(object sender, EventArgs e)
		{
			new FrmAbout().ShowDialog();
		}

		/// <summary>
		/// Sets whether the open file was modified or not, updating
		/// the UI in turn.
		/// </summary>
		/// <param name="val"></param>
		private void SetModified(bool val)
		{
			_modifiedOpenFile = val;
			this.UpdateSavingControls();
			this.UpdateTitle();
		}

		/// <summary>
		/// Opens settings dialog.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuEditSettings_Click(object sender, EventArgs e)
		{
			var form = new FrmSettings();
			if (form.ShowDialog() != DialogResult.OK)
				return;

			_areaStyle.OutlineColor = Settings.Default.AreasColor;
			_propStyle.OutlineColor = Settings.Default.PropsColor;
			_eventStyle.OutlineColor = Settings.Default.EventsColor;
			_propStyle.SelectedOutlineColor = _eventStyle.SelectedOutlineColor = Settings.Default.SelectionColor;

			this.RegionCanvas.CanvasBackColor = Settings.Default.BackgroundColor;

			this.RegionCanvas.Invalidate();
		}

		/// <summary>
		/// Changes the selected tool.
		/// </summary>
		/// <param name="tool"></param>
		private void SelectTool(Tool tool)
		{
			this.BtnScrollTool.Checked = (tool == Tool.Scroll);
			this.BtnMoveTool.Checked = (tool == Tool.Move);
			this.BtnRotateTool.Checked = (tool == Tool.Rotate);
			this.BtnFreeTool.Checked = (tool == Tool.Free);

			this.RegionCanvas.SelectedTool = tool;
		}

		/// <summary>
		/// Selects the scroll tool.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnScrollTool_Click(object sender, EventArgs e)
		{
			this.SelectTool(Tool.Scroll);
		}

		/// <summary>
		/// Selects the move tool.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnMoveTool_Click(object sender, EventArgs e)
		{
			this.SelectTool(Tool.Move);
		}

		/// <summary>
		/// Selects the rotate tool.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnRotateTool_Click(object sender, EventArgs e)
		{
			this.SelectTool(Tool.Rotate);
		}

		/// <summary>
		/// Selects the free tool.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnFreeTool_Click(object sender, EventArgs e)
		{
			this.SelectTool(Tool.Free);
		}

		/// <summary>
		/// Called when a key is released.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmMain_KeyUp(object sender, KeyEventArgs e)
		{
			// Only check key if the properties aren't currently being edited.
			if (this.PropertyGrid.ContainsFocus)
				return;

			switch (e.KeyCode)
			{
				// Switch tools
				case Keys.D0:
				case Keys.NumPad0:
					this.SelectTool(Tool.Free);
					e.SuppressKeyPress = true;
					break;

				case Keys.D1:
				case Keys.NumPad1:
					this.SelectTool(Tool.Scroll);
					e.SuppressKeyPress = true;
					break;

				case Keys.D2:
				case Keys.NumPad2:
					this.SelectTool(Tool.Move);
					e.SuppressKeyPress = true;
					break;

				case Keys.D3:
				case Keys.NumPad3:
					this.SelectTool(Tool.Rotate);
					e.SuppressKeyPress = true;
					break;

				// Unselect entity
				case Keys.Escape:
					this.SetSelectedEntity(null);
					break;

				// Remove selected entity
				case Keys.Delete:
					var node = this.TreeRegion.SelectedNode;
					if (node?.Tag is IEntity entity)
					{
						var area = entity.Area;

						switch (entity)
						{
							case Prop prop: area.Props.Remove(prop); break;
							case Event evnt: area.Events.Remove(evnt); break;
						}

						if (entity.Tag is CanvasObject obj)
							this.RegionCanvas.Remove(obj);

						node.Remove();
						this.SetSelectedEntity(null);
						this.SetModified(true);
						this.RegionCanvas.Invalidate();
					}
					break;

				// Copy entity
				case Keys.C:
					if (ModifierKeys == Keys.Control)
						this.CopySelectedEntity();
					break;

				// Paste entity
				case Keys.V:
					if (ModifierKeys == Keys.Control)
						this.PasteSelectedEntity();
					break;
			}
		}

		/// <summary>
		/// Marks the selected entity to be copied.
		/// </summary>
		private void CopySelectedEntity()
		{
			if (_selectedEntity == null)
			{
				MessageBox.Show("No entity selected.", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			if (_selectedEntity is Prop prop)
			{
				prop = prop.Copy();

				using (var ms = new MemoryStream())
				using (var bw = new BinaryWriter(ms))
				{
					prop.WriteTo(bw);
					Clipboard.SetData("MabionedProp", ms.ToArray());
				}
			}
			else if (_selectedEntity is Event evnt)
			{
				evnt = evnt.Copy();

				using (var ms = new MemoryStream())
				using (var bw = new BinaryWriter(ms))
				{
					evnt.WriteTo(bw);
					Clipboard.SetData("MabionedEvent", ms.ToArray());
				}
			}
		}

		/// <summary>
		/// Marks the selected entity to be copied.
		/// </summary>
		private void PasteSelectedEntity()
		{
			IEntity entity = null;

			if (Clipboard.ContainsData("MabionedProp"))
			{
				var data = (byte[])Clipboard.GetData("MabionedProp");
				using (var ms = new MemoryStream(data))
				using (var br = new BinaryReader(ms))
					entity = Prop.ReadFrom(null, br);
			}
			else if (Clipboard.ContainsData("MabionedEvent"))
			{
				var data = (byte[])Clipboard.GetData("MabionedEvent");
				using (var ms = new MemoryStream(data))
				using (var br = new BinaryReader(ms))
					entity = Event.ReadFrom(null, br);
			}

			if (entity == null)
				return;

			var mousePosition = this.RegionCanvas.PointToClient(MousePosition);

			// Check if canvas has the focus
			if (!this.RegionCanvas.Focused || !this.RegionCanvas.ClientRectangle.Contains(mousePosition))
				return;

			// Check position, don't paste out bounds
			var pos = this.RegionCanvas.GetWorldPosition(mousePosition);
			if (pos.X < 0 || pos.Y < 0 || pos.X > _topRight.X || pos.Y > _topRight.Y)
				return;

			switch (entity)
			{
				case Prop prop:
				{
					var copy = prop.Copy();
					copy.MoveTo(pos);

					try
					{
						this.AddProp(copy);
					}
					catch (NoEntityIdException)
					{
						MessageBox.Show("Failed to acquire a new prop entity id.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}

					this.SetModified(true);
				}
				break;

				case Event evnt:
				{
					var copy = evnt.Copy();
					copy.MoveTo(pos);

					try
					{
						this.AddEvent(copy);
					}
					catch (NoEntityIdException)
					{
						MessageBox.Show("Failed to acquire a new event entity id.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}

					this.SetModified(true);
				}
				break;
			}
		}

		/// <summary>
		/// Selects entity in tree after it was selected on the canvas.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RegionCanvas_ObjectSelected(object sender, ObjectSelectedEventArgs e)
		{
			if (e.Object == null)
			{
				this.SetSelectedEntity(null);
				return;
			}

			if (!(e.Object.Tag is IEntity entity))
				return;

			if (!_entityNodes.TryGetValue(entity.EntityId, out var node))
				return;

			this.SetSelectedEntity(entity);
		}

		/// <summary>
		/// Updates scale in status bar if the canvas' scale changed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RegionCanvas_ScaleChanged(object sender, ScaleChangedEventArgs e)
		{
			this.LblScale.Text = string.Format("Scale 1:{0:0.##}", e.Scale);
		}

		/// <summary>
		/// Updates current location in status bar if the mouse is moved
		/// over the canvas.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RegionCanvas_MouseMove(object sender, MouseEventArgs e)
		{
			if (!this.IsFileOpen)
				return;

			var pos = this.RegionCanvas.GetWorldPosition(e.Location);
			this.LblCurrentPosition.Text = string.Format("{0:0} x {1:0}", pos.X, pos.Y);
		}

		/// <summary>
		/// Focuses the canvas if it's clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RegionCanvas_MouseDown(object sender, MouseEventArgs e)
		{
			this.RegionCanvas.Select();
		}

		/// <summary>
		/// Updates object's position based on changes on canvas.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RegionCanvas_ObjectMoved(object sender, ObjectMovedEventArgs e)
		{
			if (!(e.Object.Tag is IEntity entity))
				return;

			var delta = new SizeF(e.Delta);

			entity.Position += delta;

			foreach (var shape in entity.Shapes)
			{
				shape.Position += delta;
				shape.BottomLeft += delta;
				shape.TopRight += delta;
			}

			this.SetModified(true);
			this.PropertyGrid.Refresh();
		}

		/// <summary>
		/// Updates object's rotation based on changes on canvas.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RegionCanvas_ObjectRotated(object sender, ObjectRotatedEventArgs e)
		{
			if (!(e.Object.Tag is IEntity entity))
				return;

			var delta = e.Radians;

			if (entity is Prop prop)
				prop.Rotation += (float)delta;

			foreach (var shape in entity.Shapes)
			{
				var points = shape.GetPoints();
				for (var i = 0; i < points.Length; ++i)
					points[i] = points[i].RotatePoint(entity.Position, delta);

				shape.SetRotationFromPoints(points);
			}

			this.SetModified(true);
			this.PropertyGrid.Refresh();
		}

		/// <summary>
		/// Called when the canvas is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RegionCanvas_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				// Open context menu on right click
				if (this.IsFileOpen && !this.RegionCanvas.IsDragging)
				{
					this.CtxMap.Show(this.RegionCanvas, e.Location);
					_mapRightClickLocation = e.Location;
				}
			}

			//Console.WriteLine(this.ProbeHeight(this.RegionCanvas.GetWorldPosition(e.Location)));
		}

		/// <summary>
		/// Updates area's node, updating its text, those of its children,
		/// and clearing their nodes if they should be empty.
		/// </summary>
		/// <param name="area"></param>
		private void UpdateAreaNode(Area area)
		{
			if (!_areaNodes.TryGetValue(area, out var node))
				return;

			node.Text = GetAreaNodeName(area);
			node.FirstNode.Text = string.Format("Props ({0})", area.Props.Count);
			node.LastNode.Text = string.Format("Events ({0})", area.Events.Count);

			if (area.Props.Count == 0) node.FirstNode.Nodes.Clear();
			if (area.Events.Count == 0) node.LastNode.Nodes.Clear();
		}

		/// <summary>
		/// Removes all props from selected area.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuAreaRemoveProps_Click(object sender, EventArgs e)
		{
			if (!(this.TreeRegion.SelectedNode?.Tag is Area area))
				return;

			// Get filter
			var filter = new FrmFilterProps();
			if (filter.ShowDialog() != DialogResult.OK)
				return;

			// Find and remove props from area, tree, and canvas.
			this.RegionCanvas.BeginUpdate();
			this.TreeRegion.BeginUpdate();

			this.RemoveProps(area.Props, filter);
			this.UpdateAreaNode(area);

			this.TreeRegion.EndUpdate();
			this.RegionCanvas.EndUpdate();

			this.UpdateAreaNode(area);
			this.SetModified(true);
		}

		/// <summary>
		/// Removes all events from selected area.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuAreaRemoveEvents_Click(object sender, EventArgs e)
		{
			if (!(this.TreeRegion.SelectedNode?.Tag is Area area))
				return;

			// Get filter
			var filter = new FrmFilterEvents();
			if (filter.ShowDialog() != DialogResult.OK)
				return;

			// Find and remove props from area, tree, and canvas.
			this.RegionCanvas.BeginUpdate();
			this.TreeRegion.BeginUpdate();

			this.RemoveEvents(area.Events, filter);
			this.UpdateAreaNode(area);

			this.TreeRegion.EndUpdate();
			this.RegionCanvas.EndUpdate();

			this.UpdateAreaNode(area);
			this.SetModified(true);
		}

		/// <summary>
		/// Removes props in all areas.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuEditRemoveProps_Click(object sender, EventArgs e)
		{
			// Get filter
			var filter = new FrmFilterProps();
			if (filter.ShowDialog() != DialogResult.OK)
				return;

			// Find and remove props from area, tree, and canvas.
			this.RegionCanvas.BeginUpdate();
			this.TreeRegion.BeginUpdate();
			for (var i = 0; i < _areas.Count; ++i)
			{
				var area = _areas[i];

				this.RemoveProps(area.Props, filter);
				this.UpdateAreaNode(area);
			}
			this.TreeRegion.EndUpdate();
			this.RegionCanvas.EndUpdate();

			this.TreeRegion.SelectedNode = this.TreeRegion.Nodes[0];

			this.SetModified(true);
		}

		/// <summary>
		/// Removes entities from canvas, tree, and the given list based
		/// on the filter.
		/// </summary>
		/// <param name="props"></param>
		/// <param name="filter"></param>
		private void RemoveProps(IList<Prop> props, FrmFilterProps filter)
		{
			var toRemove = new List<Prop>();

			// Find props to remove
			for (var j = 0; j < props.Count; ++j)
			{
				var prop = props[j];
				if (filter.Matches(prop, false))
					toRemove.Add(prop);
			}

			// Remove props from canvas, tree, and the given list.
			for (var j = 0; j < toRemove.Count; ++j)
			{
				var prop = toRemove[j];

				// Remove from canvas
				if (prop.Tag is CanvasObject obj)
					this.RegionCanvas.Remove(obj);

				// Remove from tree
				if (_entityNodes.TryGetValue(prop.EntityId, out var node))
					node.Remove();

				// Remove from area
				props.Remove(prop);
			}
		}

		/// <summary>
		/// Removes all events in all areas.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuEditRemoveEvents_Click(object sender, EventArgs e)
		{
			// Get filter
			var filter = new FrmFilterEvents();
			if (filter.ShowDialog() != DialogResult.OK)
				return;

			// Find and remove events from area, tree, and canvas.
			this.RegionCanvas.BeginUpdate();
			this.TreeRegion.BeginUpdate();
			for (var i = 0; i < _areas.Count; ++i)
			{
				var area = _areas[i];

				this.RemoveEvents(area.Events, filter);
				this.UpdateAreaNode(area);
			}
			this.TreeRegion.EndUpdate();
			this.RegionCanvas.EndUpdate();

			this.TreeRegion.SelectedNode = this.TreeRegion.Nodes[0];

			this.SetModified(true);
		}

		/// <summary>
		/// Removes entities from canvas, tree, and the given list based
		/// on the filter.
		/// </summary>
		/// <param name="events"></param>
		/// <param name="filter"></param>
		private void RemoveEvents(IList<Event> events, FrmFilterEvents filter)
		{
			var toRemove = new List<Event>();

			// Find props to remove
			for (var j = 0; j < events.Count; ++j)
			{
				var evnt = events[j];
				if (filter.Matches(evnt))
					toRemove.Add(evnt);
			}

			// Remove props from canvas, tree, and the given list.
			for (var j = 0; j < toRemove.Count; ++j)
			{
				var prop = toRemove[j];

				// Remove from canvas
				if (prop.Tag is CanvasObject obj)
					this.RegionCanvas.Remove(obj);

				// Remove from tree
				if (_entityNodes.TryGetValue(prop.EntityId, out var node))
					node.Remove();

				// Remove from area
				events.Remove(prop);
			}
		}

		/// <summary>
		/// Flattens entire terrain.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuFlattenTerrain_Click(object sender, EventArgs e)
		{
			var areas = _areas;
			var minHeight = areas.SelectMany(a => a.AreaPlanes).Min(a => a.MinHeight);
			var maxHeight = areas.SelectMany(a => a.AreaPlanes).Max(a => a.MaxHeight);
			var averageHeight = (minHeight + maxHeight) / 2;

			var heightForm = new FrmFlattenHeight(averageHeight, averageHeight);
			if (heightForm.ShowDialog() == DialogResult.Cancel)
				return;

			var newHeight = heightForm.Value;

			var result = MessageBox.Show("Adjust props' positions to place them on the floor?", Title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
			if (result == DialogResult.Cancel)
				return;

			var adjustProps = (result == DialogResult.Yes);

			for (var i = 0; i < areas.Count; ++i)
				this.FlattenArea(areas[i], adjustProps, newHeight);

			MessageBox.Show($"Flattened all areas to height {newHeight}.", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);

			this.SetModified(true);
		}

		/// <summary>
		/// Flattens the selected area.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuAreaFlattenTerrain_Click(object sender, EventArgs e)
		{
			if (!(this.TreeRegion.SelectedNode?.Tag is Area area))
				return;

			var minHeight = area.AreaPlanes.Min(a => a.MinHeight);
			var maxHeight = area.AreaPlanes.Max(a => a.MaxHeight);
			var averageHeight = (minHeight + maxHeight) / 2;

			var heightForm = new FrmFlattenHeight(averageHeight, averageHeight);
			if (heightForm.ShowDialog() == DialogResult.Cancel)
				return;

			var newHeight = heightForm.Value;

			var result = MessageBox.Show("Adjust props' positions to place them on the floor?", Title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
			if (result == DialogResult.Cancel)
				return;

			var adjustProps = (result == DialogResult.Yes);

			this.FlattenArea(area, adjustProps, newHeight);

			MessageBox.Show($"Flattened area to height {newHeight}.", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);

			this.SetModified(true);
		}

		/// <summary>
		/// Flattens the given area.
		/// </summary>
		/// <param name="area"></param>
		/// <param name="adjustProps"></param>
		private void FlattenArea(Area area, bool adjustProps, float newHeight)
		{
			for (var j = 0; j < area.AreaPlanes.Count; ++j)
			{
				var areaPlane = area.AreaPlanes[j];

				areaPlane.MinHeight = newHeight;
				areaPlane.MaxHeight = newHeight;

				for (var k = 0; k < areaPlane.Planes.Count; ++k)
				{
					var plane = areaPlane.Planes[k];
					plane.Height = newHeight;
				}
			}

			if (adjustProps)
			{
				for (var j = 0; j < area.Props.Count; ++j)
				{
					var prop = area.Props[j];
					var pos = prop.Position;
					pos.Z = newHeight;

					prop.Position = pos;
				}
			}
		}

		/// <summary>
		/// Toggles mini map canvas object's visibility.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuShowMiniMap_Click(object sender, EventArgs e)
		{
			var visible = (this.MnuShowMiniMap.Checked = !this.MnuShowMiniMap.Checked);

			if (_miniMapObject != null)
			{
				this.RegionCanvas.BeginUpdate();
				_miniMapObject.Visible = visible;
				this.RegionCanvas.EndUpdate();
			}
		}

		/// <summary>
		/// Creates new prop.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuMapAddProp_Click(object sender, EventArgs e)
		{
			var worldPos = this.RegionCanvas.GetWorldPosition(_mapRightClickLocation);
			var z = this.ProbeHeight(worldPos);
			var vector3 = new Vector3F(worldPos, z);

			// Get info about new prop
			var form = new FrmNewProp(vector3);
			if (form.ShowDialog() != DialogResult.OK)
				return;

			// Get and check entity id
			var prop = form.Prop;

			try
			{
				this.AddProp(prop);
			}
			catch (NoEntityIdException)
			{
				MessageBox.Show("Failed to acquire a new prop entity id.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			this.SetModified(true);
		}

		/// <summary>
		/// Creates new event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuAddEvent_Click(object sender, EventArgs e)
		{
			var worldPos = this.RegionCanvas.GetWorldPosition(_mapRightClickLocation);
			var z = this.ProbeHeight(worldPos);
			var vector3 = new Vector3F(worldPos, z);

			// Get info about new prop
			var form = new FrmNewEvent(vector3);
			if (form.ShowDialog() != DialogResult.OK)
				return;

			// Get and check entity id
			var evnt = form.Event;

			try
			{
				this.AddEvent(evnt);
			}
			catch (NoEntityIdException)
			{
				MessageBox.Show("Failed to acquire a new event entity id.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			this.SetModified(true);
		}

		/// <summary>
		/// Adds prop to the correct area, the canvas, and the tree.
		/// </summary>
		/// <param name="prop"></param>
		private void AddProp(Prop prop)
		{
			var area = this.GetAreaAt(prop.Position);

			prop.Area = area;
			prop.EntityId = area.GetNewPropId();

			if (prop.EntityId == 0)
				throw new NoEntityIdException("Failed to acquire a new prop entity id.");

			// Add prop to area, canvas, and tree, selecting it at the end.
			area.Props.Add(prop);

			this.RegionCanvas.BeginUpdate();

			var propObj = this.GetPropCanvasObject(prop);
			prop.Tag = propObj;

			this.RegionCanvas.Add(propObj);
			this.RegionCanvas.EndUpdate();

			if (_areaNodes.TryGetValue(area, out var node))
			{
				var propNode = this.GetPropNode(prop);
				node.FirstNode.Nodes.Add(propNode);
				_entityNodes[prop.EntityId] = propNode;

				this.UpdateAreaNode(area);
				this.SetSelectedEntity(prop.EntityId);
			}
		}

		/// <summary>
		/// Adds event to the correct area, the canvas, and the tree.
		/// </summary>
		/// <param name="evnt"></param>
		private void AddEvent(Event evnt)
		{
			var area = this.GetAreaAt(evnt.Position);

			evnt.Area = area;
			evnt.EntityId = area.GetNewEventId();

			if (evnt.EntityId == 0)
				throw new NoEntityIdException("Failed to acquire a new event entity id.");

			// Add event to area, canvas, and tree, selecting it at the end.
			area.Events.Add(evnt);

			this.RegionCanvas.BeginUpdate();

			var eventObj = this.GetEventCanvasObject(evnt);
			evnt.Tag = eventObj;

			this.RegionCanvas.Add(eventObj);
			this.RegionCanvas.EndUpdate();

			if (_areaNodes.TryGetValue(area, out var node))
			{
				var eventNode = this.GetEventNode(evnt);
				node.LastNode.Nodes.Add(eventNode);
				_entityNodes[evnt.EntityId] = eventNode;

				this.UpdateAreaNode(area);
				this.SetSelectedEntity(evnt.EntityId);
			}
		}

		/// <summary>
		/// Returns the area at the given world position, or null if no
		/// area was found.
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private Area GetAreaAt(PointF pos)
		{
			var areas = _areas;

			for (var i = 0; i < areas.Count; ++i)
			{
				var area = areas[i];
				if (area.IsInside(pos))
					return area;
			}

			return null;
		}
		/// <summary>
		/// Remove props similar to the selected prop
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuMapRemoveSimilarProps_Click(object sender, EventArgs e)
		{
			if (_selectedEntity == null || !(_selectedEntity is Prop prop))
				return;

			// Open form to filter props, prefilled with information to
			// find similar props.
			var filter = new FrmFilterProps(prop);
			if (filter.ShowDialog() != DialogResult.OK)
				return;

			// Find and remove props from area, tree, and canvas.
			this.RegionCanvas.BeginUpdate();
			this.TreeRegion.BeginUpdate();
			for (var i = 0; i < _areas.Count; ++i)
			{
				var area = _areas[i];

				this.RemoveProps(area.Props, filter);
				this.UpdateAreaNode(area);
			}
			this.TreeRegion.EndUpdate();
			this.RegionCanvas.EndUpdate();

			this.TreeRegion.SelectedNode = this.TreeRegion.Nodes[0];

			this.SetModified(true);
		}
	}
}
