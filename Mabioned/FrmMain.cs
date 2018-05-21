using MabiWorld;
using MabiWorld.PropertyEditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
		private PointF _drawTansform;

		private float _scale = DefaultScale;
		private ulong _selectedEntityId;
		private IEntity _selectedEntity;
		private Dictionary<ulong, TreeNode> _entityNodes = new Dictionary<ulong, TreeNode>();
		private Dictionary<EventType, MenuItem> _eventTypeMenuItems = new Dictionary<EventType, MenuItem>();

		private string _openFilePath;
		private bool _modifiedOpenFile;
		private bool _openOnUnsavedChanges;

		private bool _draggingMap;
		private Point _mapDragStart;
		private Point _mapRightClickLocation;

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
			InitializeComponent();

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
			Settings.Default.Load();

			this.UpdateWindow();
			this.ToolStrip.Renderer = new ToolStripRendererNL();
			this.ImgMap.Size = Size.Empty;
			this.ImgMap.MouseWheel += this.OnMouseWheel;

			this.LoadViewOptions();
			this.SetGlobalPropertyEditors();

			var args = Environment.GetCommandLineArgs();
			if (args.Length > 1)
				this.OpenFile(args[1]);

			this.LoadRecentFilesList();
		}

		/// <summary>
		/// Called when the form is closing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
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
		}

		/// <summary>
		/// Sets view options based on loaded settings.
		/// </summary>
		private void LoadViewOptions()
		{
			this.MnuShowProps.Checked = Settings.Default.ShowProps;
			this.MnuShowAreas.Checked = Settings.Default.ShowAreas;

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

			this.UpdateShowEventsAll();
		}

		/// <summary>
		/// Updates and saves settings.
		/// </summary>
		private void SaveSettings()
		{
			Settings.Default.ShowProps = this.MnuShowProps.Checked;
			Settings.Default.ShowAreas = this.MnuShowAreas.Checked;

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
				for (int i = list.Count - 1, j = 1; i >= 0; --i, ++j)
				{
					var filepath = list[i];
					var menuItem = new MenuItem(j + " " + filepath);
					menuItem.Click += this.MnuRecentItem_Click;
					menuItem.Tag = filepath;

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
				_openOnUnsavedChanges = true;

				var result = MessageBox.Show(this, "There are unsaved modifications, do you want to save before opening a new file?", Title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (result == DialogResult.Cancel)
					return;

				_openOnUnsavedChanges = false;

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
				this.SetScale(this.GetFitScale(), false);
				this.DrawMap();

				_openFilePath = filePath;
				this.AddLastOpenedFile(filePath);
				this.SetModified(false);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to open file." + Environment.NewLine + ex);
			}
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
					using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
						_region.WriteTo(fs);

					_openFilePath = filePath;
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
			_entityNodes.Clear();

			this.TreeRegion.BeginUpdate();
			{
				this.TreeRegion.Nodes.Clear();

				if (!this.AreaOnly)
				{
					var regionNode = new TreeNode("Region: " + (_region?.Name ?? "?"));
					regionNode.Tag = _region;
					regionNode.ImageKey = regionNode.SelectedImageKey = "region";

					foreach (var area in _areas)
						regionNode.Nodes.Add(this.CreateAreaNode(area));

					this.TreeRegion.Nodes.Add(regionNode);
				}
				else
				{
					this.TreeRegion.Nodes.Add(this.CreateAreaNode(_areas[0]));
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
			var areaNode = new TreeNode(string.Format("Area: {0}  ({1}, {2})", area.Name, area.Props.Count, area.Events.Count));
			areaNode.Tag = area;
			areaNode.ImageKey = areaNode.SelectedImageKey = "area";

			var propsNode = areaNode.Nodes.Add(string.Format("Props ({0})", area.Props.Count));
			var eventsNode = areaNode.Nodes.Add(string.Format("Events ({0})", area.Events.Count));

			propsNode.ImageKey = propsNode.SelectedImageKey = "prop";
			eventsNode.ImageKey = eventsNode.SelectedImageKey = "event";

			foreach (var prop in area.Props)
			{
				var propNode = propsNode.Nodes.Add(string.Format("0x{0:X16}", prop.EntityId));
				propNode.Tag = prop;
				propNode.ImageKey = propNode.SelectedImageKey = "prop";

				_entityNodes[prop.EntityId] = propNode;
			}

			foreach (var evnt in area.Events)
			{
				var eventNode = eventsNode.Nodes.Add(string.Format("0x{0:X16}", evnt.EntityId));
				eventNode.Tag = evnt;
				eventNode.ImageKey = eventNode.SelectedImageKey = "event";

				_entityNodes[evnt.EntityId] = eventNode;
			}

			return areaNode;
		}

		/// <summary>
		/// (Re)draws map.
		/// </summary>
		private void DrawMap()
		{
			var region = _region;
			var areas = _areas;
			if (areas == null || !areas.Any())
				return;

			var backBrush = new SolidBrush(Settings.Default.BackgroundColor);
			var propPen = new Pen(Settings.Default.PropsColor);
			var eventPen = new Pen(Settings.Default.EventsColor);
			var areaPen = new Pen(Settings.Default.AreasColor);
			var selectedPen = new Pen(Settings.Default.SelectionColor, 3);
			var boundingShapePen = Pens.DarkRed;

			var minX = _lowerLeft.X;
			var minY = _lowerLeft.Y;
			var maxX = _topRight.X;
			var maxY = _topRight.Y;

			var scale = _scale;

			var width = (int)maxX;
			var height = (int)maxY;
			var scaledWidth = (int)(maxX / scale);
			var scaledHeight = (int)(maxY / scale);
			var imgWidth = (int)(maxX / scale);
			var imgHeight = (int)(maxY / scale);
			var regionWidth = (maxX - minX) / scale;
			var regionHeight = (maxY - minY) / scale;
			var offsetX = minX / scale;
			var offsetY = minY / scale;

			var transformX = 0;
			var transformY = 0;
			if (region == null && areas.Count == 1)
			{
				var area = areas[0];
				var prevWidth = imgWidth;
				var prevHeight = imgHeight;

				imgWidth = (int)((area.BottomRight.X - area.BottomLeft.X) / scale);
				imgHeight = (int)((area.TopLeft.Y - area.BottomLeft.Y) / scale);
				transformX = (imgWidth - prevWidth);
				transformY = 0;
			}

			var showProps = this.ShowProps;
			var showEvents = this.ShowEvents;
			var showAreas = this.ShowAreas;

			var bmp = new Bitmap(imgWidth, imgHeight);
			var g = Graphics.FromImage(bmp);

			g.TranslateTransform(transformX, transformY);
			_drawTansform = new PointF(transformX, transformY);

			{
				g.Clear(Color.White);
				g.FillRectangle(backBrush, offsetX, scaledHeight - offsetY - regionHeight, regionWidth, regionHeight);

				if (showAreas)
				{
					for (var i = 0; i < areas.Count; ++i)
					{
						var area = areas[i];

						var x = (int)(area.BottomLeft.X / scale);
						var y = (int)((maxY - area.TopLeft.Y) / scale);
						var w = (int)((area.BottomRight.X - area.BottomLeft.X) / scale);
						var h = (int)((area.TopLeft.Y - area.BottomLeft.Y) / scale);

						g.DrawRectangle(areaPen, new Rectangle(x, y, w, h));
					}
				}

				if (showEvents)
				{
					for (var i = 0; i < areas.Count; ++i)
					{
						var area = areas[i];

						for (var j = 0; j < area.Events.Count; ++j)
						{
							var evnt = area.Events[j];

							if (!this.DisplayEventType(evnt.Type))
								continue;

							var pen = (evnt.EntityId == _selectedEntityId ? selectedPen : eventPen);

							foreach (var shape in evnt.Shapes)
							{
								var points = shape.GetPoints(scale, height);
								g.DrawPolygon(pen, points);

								//if (evnt.EntityId == _selectedEntityId)
								//	g.DrawRectangle(boundingShapePen, shape.GetBoundingBox(scale, height));
							}
						}
					}
				}

				if (showProps)
				{
					for (var i = 0; i < areas.Count; ++i)
					{
						var area = areas[i];

						for (var j = 0; j < area.Props.Count; ++j)
						{
							var prop = area.Props[j];
							var pen = (prop.EntityId == _selectedEntityId ? selectedPen : propPen);

							if (prop.Shapes.Any())
							{
								foreach (var shape in prop.Shapes)
								{
									var points = shape.GetPoints(scale, height);
									g.DrawPolygon(pen, points);
								}
							}
							else
							{
								var point = prop.GetPoint(scale, height);
								var size = NoPropShapesSize / scale;
								g.DrawEllipse(pen, point.X - size / 2, point.Y - size / 2, size, size);
							}
						}
					}
				}
			}

			this.ImgMap.Image = bmp;
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

				for (var apy = 0; apy < area.PlaneY; ++apy)
				{
					for (var apx = 0; apx < area.PlaneX; ++apx)
					{
						var areaPlane = areaPlanes[apy * area.PlaneX + apx];
						var planes = areaPlane.Planes;

						var planeWidth = areaPlaneWidth / areaPlane.Size;
						var planeHeight = areaPlaneHeight / areaPlane.Size;
						for (var py = 0; py < areaPlane.Size; ++py)
						{
							for (var px = 0; px < areaPlane.Size; ++px)
							{
								var plane = planes[py * areaPlane.Size + px];

								// Do they go from bottom left to top right
								// in vertical lines?

								var x = (area.BottomLeft.X + apy * areaPlaneWidth + py * planeHeight);
								var y = (area.BottomLeft.Y + apx * areaPlaneHeight + px * planeWidth);
								var w = (planeWidth);
								var h = (planeHeight);

								if (regionPos.X > x && regionPos.X <= x + w && regionPos.Y > y && regionPos.Y <= y + h)
									return plane.Height;
							}
						}
					}
				}
			}

			return -1;
		}

		/// <summary>
		/// Changes map's position so it's displayed in the middle of the
		/// panel.
		/// </summary>
		private void PositionMap()
		{
			var location = Point.Empty;

			var panelSize = this.SplMain.Panel2.Size;
			var mapSize = this.ImgMap.Size;

			if (panelSize.Width >= mapSize.Width)
				location.X = (panelSize.Width / 2 - mapSize.Width / 2);

			if (panelSize.Height >= mapSize.Height)
				location.Y = (panelSize.Height / 2 - mapSize.Height / 2);

			this.ImgMap.Location = location;
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
			}

			this.PropertyGrid.SelectedObject = tag;

			// Redraw map if selection was changed by the user.
			if (e.Action != TreeViewAction.Unknown)
				this.DrawMap();
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
				if (e.Node?.Tag is IEntity entity)
					this.SetSelectedEntity(entity);

				//ShowPropertyEditor("Parameters");
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
				this.ScrollToRegionPosition(new PointF(entity.Position.X, entity.Position.Y));
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
		/// Called when the mouse is pressed down on the map.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ImgMap_MouseDown(object sender, MouseEventArgs e)
		{
			_mapDragStart = e.Location;
		}

		/// <summary>
		/// Called when the mouse is released over the map.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ImgMap_MouseUp(object sender, MouseEventArgs e)
		{
			this.Cursor = Cursors.Default;
			_draggingMap = false;
		}

		/// <summary>
		/// Called when the mouse moves over the map.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ImgMap_MouseMove(object sender, MouseEventArgs e)
		{
			var pos = ToRegionPosition(e.Location);
			this.LblCurrentPosition.Text = string.Format("{0:0} x {1:0}", pos.X, pos.Y);

			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
			{
				this.Cursor = Cursors.SizeAll;
				_draggingMap = true;

				var deltaTotal = new Point(e.Location.X - _mapDragStart.X, e.Location.Y - _mapDragStart.Y);
				this.SplMain.Panel2.AutoScrollPosition = new Point(-this.SplMain.Panel2.AutoScrollPosition.X - deltaTotal.X, -this.SplMain.Panel2.AutoScrollPosition.Y - deltaTotal.Y);

				//_mapDragStart = e.Location;
				//if (_selectedEntity != null)
				//{
				//	var delta = new Vector3F(deltaTotal.X * _scale, -deltaTotal.Y * _scale, 0);

				//	var oldPos = _selectedEntity.Position;
				//	(_selectedEntity as Event).Position += delta;
				//	foreach (var shape in _selectedEntity.Shapes)
				//	{
				//		shape.PosX += delta.X;
				//		shape.PosY += delta.Y;
				//	}

				//	this.PropertyGrid.SelectedObject = _selectedEntity;
				//	this.DrawMap();
				//}
			}
		}

		/// <summary>
		/// Called when the map is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ImgMap_MouseClick(object sender, MouseEventArgs e)
		{
			var isLeft = (e.Button == MouseButtons.Left);
			var isRight = (e.Button == MouseButtons.Right);

			if (!isLeft && !isRight)
				return;

			if (!_draggingMap)
			{
				var getNextEntity = isLeft;

				var entity = this.GetEntityAtMapPosition(e.Location, getNextEntity);
				this.SetSelectedEntity(entity);
				this.DrawMap();
			}

			if (isRight)
			{
				this.CtxMap.Show(this.ImgMap, e.Location);
				_mapRightClickLocation = e.Location;
			}

			//Console.WriteLine(this.ProbeHeight(this.ToRegionPosition(e.Location)));
		}

		/// <summary>
		/// Returns entity at the given position on the displayed map.
		/// Returns null if there is no prop or event there.
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="getNextEntity">
		/// If true, and there's multiple entities at the position, the next
		/// entity after the currently selected one is returned, allowing
		/// to iterate through all available ones.
		/// </param>
		/// <returns></returns>
		private IEntity GetEntityAtMapPosition(PointF pos, bool getNextEntity)
		{
			var regionPos = this.ToRegionPosition(pos);
			IEntity entity = null;

			var eventsAtPos = new List<Event>();

			for (var i = 0; i < _areas.Count && entity == null; ++i)
			{
				var area = _areas[i];
				var props = area.Props;
				var events = area.Events;

				// Search for props first, as they're usually small and
				// the most likely target
				if (this.ShowProps)
				{
					for (var j = 0; j < props.Count && entity == null; ++j)
					{
						var prop = props[j];
						var shapes = prop.Shapes;

						for (var k = 0; k < shapes.Count && entity == null; ++k)
						{
							if (shapes[k].IsInside(regionPos))
							{
								entity = prop;
								break;
							}
						}
					}
				}

				// If no props were found under the cursor, search for all
				// events under it and save them for later.
				if (entity == null && this.ShowEvents)
				{
					for (var j = 0; j < events.Count; ++j)
					{
						var evnt = events[j];
						if (!this.DisplayEventType(evnt.Type))
							continue;

						var shapes = evnt.Shapes;

						for (var k = 0; k < shapes.Count; ++k)
						{
							var shape = shapes[k];
							if (shape.IsInside(regionPos))
							{
								eventsAtPos.Add(evnt);
								break;
							}
						}
					}
				}
			}

			// If no entity was found in all props in all regions we fall
			// back to the list of events from all areas and cycle through
			// them, since they're usually layered on top of each other.
			if (entity == null && eventsAtPos.Any())
			{
				// Get the index of the currently selected event if any.
				var index = -1;
				for (var j = 0; j < eventsAtPos.Count; ++j)
				{
					if (eventsAtPos[j].EntityId == _selectedEntityId)
					{
						index = j;
						break;
					}
				}

				// Return the first entity if the currently selected one
				// is not at this position.
				if (index == -1)
				{
					entity = eventsAtPos[0];
				}
				// Return the next entity in the list of available ones
				// if one is currently selected.
				else if (getNextEntity)
				{
					if (index == eventsAtPos.Count - 1)
						entity = eventsAtPos[0];
					else
						entity = eventsAtPos[index + 1];
				}
				// Return the currently selected entity if iterating is
				// disabled.
				else if (!getNextEntity)
				{
					entity = eventsAtPos[index];
				}
			}

			return entity;
		}

		/// <summary>
		/// Returns position in region from position on the displayed map.
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private PointF ToRegionPosition(PointF pos)
		{
			var x = pos.X;
			var y = pos.Y;

			x -= _drawTansform.X;
			y -= _drawTansform.Y;

			x = (x * _scale);
			y = _topRight.Y - (y * _scale);

			return new PointF(x, y);
			//return new PointF((pos.X * _scale), ((this.ImgMap.Height - pos.Y) * _scale));
		}

		/// <summary>
		/// Returns position in displayed map from position in region.
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private PointF ToMapPosition(PointF pos)
		{
			return new PointF((pos.X / _scale), ((_topRight.Y - pos.Y) / _scale));
		}

		/// <summary>
		/// Called when a "Show ..." menu option is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuShowToggle_Click(object sender, EventArgs e)
		{
			var menuItem = (sender as MenuItem);
			menuItem.Checked = !menuItem.Checked;

			this.DrawMap();
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
			}
			else
			{
				this.TreeRegion.SelectedNode = null;
				this.PropertyGrid.SelectedObject = null;
				_selectedEntity = null;
			}
		}

		/// <summary>
		/// Called when the mouse wheel is used over the form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnMouseWheel(object sender, MouseEventArgs e)
		{
			var regionPos = this.ToRegionPosition(e.Location);
			var prevScale = _scale;

			if (e.Delta > 0)
			{
				this.ModifyScale(-ScaleStep);
			}
			else if (e.Delta < 0)
			{
				this.ModifyScale(+ScaleStep);
			}

			if (prevScale > _scale)
				this.ScrollToRegionPosition(regionPos);

			// Don't let event bubble up, so the panel doesn't scroll
			((HandledMouseEventArgs)e).Handled = true;
		}

		/// <summary>
		/// Modifies scale and redraws map.
		/// </summary>
		/// <param name="modifier"></param>
		private void ModifyScale(float modifier)
		{
			var scale = _scale + modifier;
			this.SetScale(scale, true);
		}

		/// <summary>
		/// Sets the scale and optionally redraws map.
		/// </summary>
		/// <param name="scale"></param>
		/// <param name="redraw"></param>
		private void SetScale(float scale, bool redraw)
		{
			var scaleBefore = _scale;

			var maxX = _topRight.X;
			var maxY = _topRight.Y;

			if (maxX / scale > MaxMapSize)
				scale = (maxX / MaxMapSize);
			else if (maxY / scale > MaxMapSize)
				scale = (maxY / MaxMapSize);

			scale = Math.Max(MinScale, Math.Min(MaxScale, scale));

			if (scaleBefore != scale)
			{
				_scale = scale;
				this.LblScale.Text = string.Format("Scale 1:{0:0}", _scale);
			}

			if (redraw)
				this.DrawMap();
		}

		/// <summary>
		/// Returns a scale that allows the map to fit just into the panel.
		/// </summary>
		/// <returns></returns>
		private float GetFitScale()
		{
			var panelSize = this.SplMain.Panel2.ClientSize;
			var fitX = (int)Math.Ceiling(_topRight.X / (panelSize.Width - 30));
			var fitY = (int)Math.Ceiling(_topRight.Y / (panelSize.Height - 30));

			return (fitX > fitY ? fitX : fitY);
		}

		/// <summary>
		/// Scrolls map panel to show given region's position in the center.
		/// </summary>
		/// <param name="centerPos"></param>
		private void ScrollToRegionPosition(PointF centerPos)
		{
			var panelSize = this.SplMain.Panel2.Size;
			var mapPos = this.ToMapPosition(centerPos);

			var scrollPos = Point.Empty;
			scrollPos.X = (int)mapPos.X - panelSize.Width / 2;
			scrollPos.Y = (int)mapPos.Y - panelSize.Height / 2;

			this.SplMain.Panel2.AutoScrollPosition = scrollPos;
		}

		/// <summary>
		/// Returns the position in the middle of the currently displayed
		/// map.
		/// </summary>
		/// <returns></returns>
		private PointF GetMapDisplayCenter()
		{
			var point = PointF.Empty;

			var mapSize = this.ImgMap.Size;
			var panel = this.SplMain.Panel2;
			var panelSize = panel.Size;

			if (mapSize.Width > panelSize.Width)
				point.X = ((panelSize.Width / 2f) + Math.Abs(panel.AutoScrollPosition.X));

			if (mapSize.Height > panelSize.Height)
				point.Y = ((panelSize.Height / 2f) + Math.Abs(panel.AutoScrollPosition.Y));

			return point;
		}

		/// <summary>
		/// Returns the position in the middle of the currently displayed
		/// region.
		/// </summary>
		/// <returns></returns>
		private PointF GetRegionDisplayCenter()
		{
			return this.ToRegionPosition(this.GetMapDisplayCenter());
		}

		/// <summary>
		/// Called when the property grid's sort order changed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PropertyGrid_PropertySortChanged(object sender, EventArgs e)
		{
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

			this.DrawMap();
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
			this.DrawMap();
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
			var pos = this.ToRegionPosition(_mapRightClickLocation);
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
			var pos = this.ToRegionPosition(_mapRightClickLocation);
			Clipboard.SetText(string.Format(">warp {0} {1:0} {2:0}", regionId, pos.X, pos.Y));
		}

		/// <summary>
		/// Called when a property changes.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="e"></param>
		private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			if (this.PropertyGrid.SelectedObject is IEntity entity)
			{
				if (e.ChangedItem.PropertyDescriptor?.Name == "Position")
				{
					var oldValue = (Vector3F)e.OldValue;
					var newValue = (Vector3F)e.ChangedItem.Value;
					var diff = (newValue - oldValue);

					foreach (var shape in entity.Shapes)
					{
						var pos = shape.Position;
						pos.X += diff.X;
						pos.Y += diff.Y;
						shape.Position = pos;

						var bl = shape.BottomLeft;
						bl.X += diff.X;
						bl.Y += diff.Y;
						shape.BottomLeft = bl;

						var tr = shape.TopRight;
						tr.X += diff.X;
						tr.Y += diff.Y;
						shape.TopRight = tr;
					}
				}
			}
			else if (this.PropertyGrid.SelectedObject is MabiWorld.Region region)
			{
				if (e.ChangedItem.PropertyDescriptor?.Name == "Id")
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

					this.CreateTree();
				}
			}
			else if (this.PropertyGrid.SelectedObject is MabiWorld.Area area)
			{
				if (e.ChangedItem.PropertyDescriptor?.Name == "Id")
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

					this.CreateTree();
				}
			}

			this.SetModified(true);
			this.DrawMap();
		}

		/// <summary>
		/// Called when a collection editor form is closed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnCollectionEditorFormClosed(object sender, FormClosedEventArgs e)
		{
			//this.SetModified(true);
			this.DrawMap();
		}

		/// <summary>
		/// Called when the collection in a collection editor changed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnCollectionChanged(object sender, EventArgs e)
		{
			this.SetModified(true);
			this.DrawMap();
		}

		/// <summary>
		/// Called when a property of an item in a collection editor changed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnCollectionPropertyChanged(object sender, PropertyValueChangedEventArgs e)
		{
			this.SetModified(true);
			this.DrawMap();
		}

		/// <summary>
		/// Called when the "Scale to fit" menu option was clicked,
		/// scales the map to fit into the panel.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MnuScaleToFit_Click(object sender, EventArgs e)
		{
			var scale = this.GetFitScale();
			this.SetScale(scale, true);
		}

		/// <summary>
		/// Called when a key is let go on the tree.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TreeRegion_KeyUp(object sender, KeyEventArgs e)
		{
			// Remove selected entity
			if (e.KeyCode == Keys.Delete)
			{
				var node = this.TreeRegion.SelectedNode;
				if (node?.Tag is IEntity entity)
				{
					var area = entity.Area;

					switch (entity)
					{
						case Prop prop: area.Props.Remove(prop); break;
						case Event evnt: area.Events.Remove(evnt); break;
					}

					node.Remove();
					this.SetSelectedEntity(null);
					this.SetModified(true);
					this.DrawMap();
				}
			}
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
			if (form.ShowDialog() == DialogResult.OK)
				this.DrawMap();
		}
	}
}
