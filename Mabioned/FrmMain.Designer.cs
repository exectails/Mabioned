namespace Mabioned
{
	partial class FrmMain
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
			this.MainMenu = new System.Windows.Forms.MainMenu(this.components);
			this.MnuFile = new System.Windows.Forms.MenuItem();
			this.MnuOpen = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.MnuNew = new System.Windows.Forms.MenuItem();
			this.MnuSave = new System.Windows.Forms.MenuItem();
			this.MnuSaveAs = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.MnuRecent = new System.Windows.Forms.MenuItem();
			this.MnuRecentNone = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.MnuExit = new System.Windows.Forms.MenuItem();
			this.MnuEdit = new System.Windows.Forms.MenuItem();
			this.MnuEditSettings = new System.Windows.Forms.MenuItem();
			this.MnuView = new System.Windows.Forms.MenuItem();
			this.MnuShowProps = new System.Windows.Forms.MenuItem();
			this.MnuShowEvents = new System.Windows.Forms.MenuItem();
			this.MnuShowEventsAll = new System.Windows.Forms.MenuItem();
			this.MnuShowEventsUndefined = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.MnuShowAreas = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.MnuScaleToFit = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.MnuExpand = new System.Windows.Forms.MenuItem();
			this.MnuCollapse = new System.Windows.Forms.MenuItem();
			this.MnuHelp = new System.Windows.Forms.MenuItem();
			this.MnuAbout = new System.Windows.Forms.MenuItem();
			this.ToolStrip = new System.Windows.Forms.ToolStrip();
			this.BtnOpen = new System.Windows.Forms.ToolStripButton();
			this.BtnNew = new System.Windows.Forms.ToolStripButton();
			this.BtnSave = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.BtnScrollTool = new System.Windows.Forms.ToolStripButton();
			this.BtnMoveTool = new System.Windows.Forms.ToolStripButton();
			this.BtnRotateTool = new System.Windows.Forms.ToolStripButton();
			this.StatusStrip = new System.Windows.Forms.StatusStrip();
			this.LblCurrentPosition = new System.Windows.Forms.ToolStripStatusLabel();
			this.LblScale = new System.Windows.Forms.ToolStripStatusLabel();
			this.SplMain = new System.Windows.Forms.SplitContainer();
			this.SplSidebar = new System.Windows.Forms.SplitContainer();
			this.TreeRegion = new System.Windows.Forms.TreeView();
			this.ImgsTree = new System.Windows.Forms.ImageList(this.components);
			this.PropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.ImgMap = new System.Windows.Forms.PictureBox();
			this.OfdRegion = new System.Windows.Forms.OpenFileDialog();
			this.CtxMap = new System.Windows.Forms.ContextMenu();
			this.MnuCopyCoordinates = new System.Windows.Forms.MenuItem();
			this.MnuCopyAuraWarp = new System.Windows.Forms.MenuItem();
			this.SfdRegion = new System.Windows.Forms.SaveFileDialog();
			this.MnuShowHeightmap = new System.Windows.Forms.MenuItem();
			this.ToolStrip.SuspendLayout();
			this.StatusStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SplMain)).BeginInit();
			this.SplMain.Panel1.SuspendLayout();
			this.SplMain.Panel2.SuspendLayout();
			this.SplMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SplSidebar)).BeginInit();
			this.SplSidebar.Panel1.SuspendLayout();
			this.SplSidebar.Panel2.SuspendLayout();
			this.SplSidebar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImgMap)).BeginInit();
			this.SuspendLayout();
			// 
			// MainMenu
			// 
			this.MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
			this.MnuFile,
			this.MnuEdit,
			this.MnuView,
			this.MnuHelp});
			// 
			// MnuFile
			// 
			this.MnuFile.Index = 0;
			this.MnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
			this.MnuOpen,
			this.menuItem5,
			this.MnuNew,
			this.MnuSave,
			this.MnuSaveAs,
			this.menuItem4,
			this.MnuRecent,
			this.menuItem7,
			this.MnuExit});
			this.MnuFile.Text = "File";
			// 
			// MnuOpen
			// 
			this.MnuOpen.Index = 0;
			this.MnuOpen.Text = "Open...";
			this.MnuOpen.Click += new System.EventHandler(this.MnuOpen_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 1;
			this.menuItem5.Text = "-";
			// 
			// MnuNew
			// 
			this.MnuNew.Enabled = false;
			this.MnuNew.Index = 2;
			this.MnuNew.Text = "New...";
			// 
			// MnuSave
			// 
			this.MnuSave.Enabled = false;
			this.MnuSave.Index = 3;
			this.MnuSave.Text = "Save";
			this.MnuSave.Click += new System.EventHandler(this.BtnSave_Click);
			// 
			// MnuSaveAs
			// 
			this.MnuSaveAs.Enabled = false;
			this.MnuSaveAs.Index = 4;
			this.MnuSaveAs.Text = "Save as...";
			this.MnuSaveAs.Click += new System.EventHandler(this.MnuSaveAs_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 5;
			this.menuItem4.Text = "-";
			// 
			// MnuRecent
			// 
			this.MnuRecent.Index = 6;
			this.MnuRecent.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
			this.MnuRecentNone});
			this.MnuRecent.Text = "Recent Files";
			// 
			// MnuRecentNone
			// 
			this.MnuRecentNone.Enabled = false;
			this.MnuRecentNone.Index = 0;
			this.MnuRecentNone.Text = "None";
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 7;
			this.menuItem7.Text = "-";
			// 
			// MnuExit
			// 
			this.MnuExit.Index = 8;
			this.MnuExit.Text = "Exit";
			this.MnuExit.Click += new System.EventHandler(this.MnuExit_Click);
			// 
			// MnuEdit
			// 
			this.MnuEdit.Index = 1;
			this.MnuEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
			this.MnuEditSettings});
			this.MnuEdit.Text = "Edit";
			// 
			// MnuEditSettings
			// 
			this.MnuEditSettings.Index = 0;
			this.MnuEditSettings.Text = "Settings";
			this.MnuEditSettings.Click += new System.EventHandler(this.MnuEditSettings_Click);
			// 
			// MnuView
			// 
			this.MnuView.Index = 2;
			this.MnuView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
			this.MnuShowProps,
			this.MnuShowEvents,
			this.MnuShowAreas,
			this.MnuShowHeightmap,
			this.menuItem1,
			this.MnuScaleToFit,
			this.menuItem2,
			this.MnuExpand,
			this.MnuCollapse});
			this.MnuView.Text = "View";
			// 
			// MnuShowProps
			// 
			this.MnuShowProps.Checked = true;
			this.MnuShowProps.Index = 0;
			this.MnuShowProps.Text = "Show Props";
			this.MnuShowProps.Click += new System.EventHandler(this.MnuShowToggle_Click);
			// 
			// MnuShowEvents
			// 
			this.MnuShowEvents.Index = 1;
			this.MnuShowEvents.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
			this.MnuShowEventsAll,
			this.MnuShowEventsUndefined,
			this.menuItem3});
			this.MnuShowEvents.Text = "Show Events";
			// 
			// MnuShowEventsAll
			// 
			this.MnuShowEventsAll.Checked = true;
			this.MnuShowEventsAll.Index = 0;
			this.MnuShowEventsAll.Text = "All";
			this.MnuShowEventsAll.Click += new System.EventHandler(this.MnuShowEventsAll_Click);
			// 
			// MnuShowEventsUndefined
			// 
			this.MnuShowEventsUndefined.Checked = true;
			this.MnuShowEventsUndefined.Index = 1;
			this.MnuShowEventsUndefined.Text = "Undefined";
			this.MnuShowEventsUndefined.Click += new System.EventHandler(this.MnuShowEventsToggle_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "-";
			// 
			// MnuShowAreas
			// 
			this.MnuShowAreas.Index = 2;
			this.MnuShowAreas.Text = "Show Areas";
			this.MnuShowAreas.Click += new System.EventHandler(this.MnuShowToggle_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 4;
			this.menuItem1.Text = "-";
			// 
			// MnuScaleToFit
			// 
			this.MnuScaleToFit.Index = 5;
			this.MnuScaleToFit.Text = "Scale to fit";
			this.MnuScaleToFit.Click += new System.EventHandler(this.MnuScaleToFit_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 6;
			this.menuItem2.Text = "-";
			// 
			// MnuExpand
			// 
			this.MnuExpand.Index = 7;
			this.MnuExpand.Text = "Expand tree";
			this.MnuExpand.Click += new System.EventHandler(this.MnuExpand_Click);
			// 
			// MnuCollapse
			// 
			this.MnuCollapse.Index = 8;
			this.MnuCollapse.Text = "Collapse tree";
			this.MnuCollapse.Click += new System.EventHandler(this.MnuCollapse_Click);
			// 
			// MnuHelp
			// 
			this.MnuHelp.Index = 3;
			this.MnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
			this.MnuAbout});
			this.MnuHelp.Text = "?";
			// 
			// MnuAbout
			// 
			this.MnuAbout.Index = 0;
			this.MnuAbout.Text = "About";
			this.MnuAbout.Click += new System.EventHandler(this.MnuAbout_Click);
			// 
			// ToolStrip
			// 
			this.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.BtnOpen,
			this.BtnNew,
			this.BtnSave,
			this.toolStripSeparator1,
			this.BtnScrollTool,
			this.BtnMoveTool,
			this.BtnRotateTool});
			this.ToolStrip.Location = new System.Drawing.Point(0, 0);
			this.ToolStrip.Name = "ToolStrip";
			this.ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.ToolStrip.Size = new System.Drawing.Size(1034, 25);
			this.ToolStrip.TabIndex = 2;
			this.ToolStrip.Text = "toolStrip1";
			// 
			// BtnOpen
			// 
			this.BtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.BtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("BtnOpen.Image")));
			this.BtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.BtnOpen.Name = "BtnOpen";
			this.BtnOpen.Size = new System.Drawing.Size(23, 22);
			this.BtnOpen.Text = "Open...";
			this.BtnOpen.Click += new System.EventHandler(this.MnuOpen_Click);
			// 
			// BtnNew
			// 
			this.BtnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.BtnNew.Enabled = false;
			this.BtnNew.Image = ((System.Drawing.Image)(resources.GetObject("BtnNew.Image")));
			this.BtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.BtnNew.Name = "BtnNew";
			this.BtnNew.Size = new System.Drawing.Size(23, 22);
			this.BtnNew.Text = "New...";
			// 
			// BtnSave
			// 
			this.BtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.BtnSave.Enabled = false;
			this.BtnSave.Image = ((System.Drawing.Image)(resources.GetObject("BtnSave.Image")));
			this.BtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.BtnSave.Name = "BtnSave";
			this.BtnSave.Size = new System.Drawing.Size(23, 22);
			this.BtnSave.Text = "Save";
			this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// BtnScrollTool
			// 
			this.BtnScrollTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.BtnScrollTool.Enabled = false;
			this.BtnScrollTool.Image = ((System.Drawing.Image)(resources.GetObject("BtnScrollTool.Image")));
			this.BtnScrollTool.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.BtnScrollTool.Name = "BtnScrollTool";
			this.BtnScrollTool.Size = new System.Drawing.Size(23, 22);
			this.BtnScrollTool.Text = "Hand Tool";
			// 
			// BtnMoveTool
			// 
			this.BtnMoveTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.BtnMoveTool.Enabled = false;
			this.BtnMoveTool.Image = ((System.Drawing.Image)(resources.GetObject("BtnMoveTool.Image")));
			this.BtnMoveTool.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.BtnMoveTool.Name = "BtnMoveTool";
			this.BtnMoveTool.Size = new System.Drawing.Size(23, 22);
			this.BtnMoveTool.Text = "Move Tool";
			// 
			// BtnRotateTool
			// 
			this.BtnRotateTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.BtnRotateTool.Enabled = false;
			this.BtnRotateTool.Image = ((System.Drawing.Image)(resources.GetObject("BtnRotateTool.Image")));
			this.BtnRotateTool.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.BtnRotateTool.Name = "BtnRotateTool";
			this.BtnRotateTool.Size = new System.Drawing.Size(23, 22);
			this.BtnRotateTool.Text = "Rotate Tool";
			// 
			// StatusStrip
			// 
			this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.LblCurrentPosition,
			this.LblScale});
			this.StatusStrip.Location = new System.Drawing.Point(0, 636);
			this.StatusStrip.Name = "StatusStrip";
			this.StatusStrip.Size = new System.Drawing.Size(1034, 24);
			this.StatusStrip.TabIndex = 3;
			this.StatusStrip.Text = "statusStrip1";
			// 
			// LblCurrentPosition
			// 
			this.LblCurrentPosition.AutoSize = false;
			this.LblCurrentPosition.Name = "LblCurrentPosition";
			this.LblCurrentPosition.Size = new System.Drawing.Size(110, 19);
			this.LblCurrentPosition.Text = "0 x 0";
			this.LblCurrentPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LblScale
			// 
			this.LblScale.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
			this.LblScale.Name = "LblScale";
			this.LblScale.Size = new System.Drawing.Size(62, 19);
			this.LblScale.Text = "Scale 1:50";
			// 
			// SplMain
			// 
			this.SplMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SplMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.SplMain.Location = new System.Drawing.Point(0, 25);
			this.SplMain.Name = "SplMain";
			// 
			// SplMain.Panel1
			// 
			this.SplMain.Panel1.Controls.Add(this.SplSidebar);
			// 
			// SplMain.Panel2
			// 
			this.SplMain.Panel2.AutoScroll = true;
			this.SplMain.Panel2.BackColor = System.Drawing.SystemColors.ControlDark;
			this.SplMain.Panel2.Controls.Add(this.ImgMap);
			this.SplMain.Size = new System.Drawing.Size(1034, 611);
			this.SplMain.SplitterDistance = 320;
			this.SplMain.TabIndex = 4;
			// 
			// SplSidebar
			// 
			this.SplSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SplSidebar.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.SplSidebar.Location = new System.Drawing.Point(0, 0);
			this.SplSidebar.Name = "SplSidebar";
			this.SplSidebar.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// SplSidebar.Panel1
			// 
			this.SplSidebar.Panel1.Controls.Add(this.TreeRegion);
			// 
			// SplSidebar.Panel2
			// 
			this.SplSidebar.Panel2.Controls.Add(this.PropertyGrid);
			this.SplSidebar.Size = new System.Drawing.Size(320, 611);
			this.SplSidebar.SplitterDistance = 305;
			this.SplSidebar.TabIndex = 2;
			// 
			// TreeRegion
			// 
			this.TreeRegion.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TreeRegion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TreeRegion.HideSelection = false;
			this.TreeRegion.ImageIndex = 0;
			this.TreeRegion.ImageList = this.ImgsTree;
			this.TreeRegion.Location = new System.Drawing.Point(0, 0);
			this.TreeRegion.Name = "TreeRegion";
			this.TreeRegion.SelectedImageIndex = 0;
			this.TreeRegion.Size = new System.Drawing.Size(320, 305);
			this.TreeRegion.TabIndex = 2;
			this.TreeRegion.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeRegion_AfterSelect);
			this.TreeRegion.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeRegion_NodeMouseClick);
			this.TreeRegion.DoubleClick += new System.EventHandler(this.TreeRegion_DoubleClick);
			this.TreeRegion.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TreeRegion_KeyUp);
			// 
			// ImgsTree
			// 
			this.ImgsTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImgsTree.ImageStream")));
			this.ImgsTree.TransparentColor = System.Drawing.Color.Transparent;
			this.ImgsTree.Images.SetKeyName(0, "blank");
			this.ImgsTree.Images.SetKeyName(1, "region");
			this.ImgsTree.Images.SetKeyName(2, "area");
			this.ImgsTree.Images.SetKeyName(3, "prop");
			this.ImgsTree.Images.SetKeyName(4, "event");
			// 
			// PropertyGrid
			// 
			this.PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PropertyGrid.HelpBorderColor = System.Drawing.SystemColors.Control;
			this.PropertyGrid.HelpVisible = false;
			this.PropertyGrid.Location = new System.Drawing.Point(0, 0);
			this.PropertyGrid.Name = "PropertyGrid";
			this.PropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
			this.PropertyGrid.Size = new System.Drawing.Size(320, 302);
			this.PropertyGrid.TabIndex = 4;
			this.PropertyGrid.ViewBorderColor = System.Drawing.Color.White;
			this.PropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGrid_PropertyValueChanged);
			this.PropertyGrid.PropertySortChanged += new System.EventHandler(this.PropertyGrid_PropertySortChanged);
			// 
			// ImgMap
			// 
			this.ImgMap.Location = new System.Drawing.Point(0, 0);
			this.ImgMap.Name = "ImgMap";
			this.ImgMap.Size = new System.Drawing.Size(200, 200);
			this.ImgMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.ImgMap.TabIndex = 0;
			this.ImgMap.TabStop = false;
			this.ImgMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ImgMap_MouseClick);
			this.ImgMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImgMap_MouseDown);
			this.ImgMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImgMap_MouseMove);
			this.ImgMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImgMap_MouseUp);
			// 
			// OfdRegion
			// 
			this.OfdRegion.Filter = "Region File|*.rgn|Area File|*.area";
			// 
			// CtxMap
			// 
			this.CtxMap.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
			this.MnuCopyCoordinates,
			this.MnuCopyAuraWarp});
			// 
			// MnuCopyCoordinates
			// 
			this.MnuCopyCoordinates.Index = 0;
			this.MnuCopyCoordinates.Text = "Copy Coordinates";
			this.MnuCopyCoordinates.Click += new System.EventHandler(this.MnuCopyCoordinates_Click);
			// 
			// MnuCopyAuraWarp
			// 
			this.MnuCopyAuraWarp.Index = 1;
			this.MnuCopyAuraWarp.Text = "Copy Aura Warp Command";
			this.MnuCopyAuraWarp.Click += new System.EventHandler(this.MnuCopyAuraWarp_Click);
			// 
			// MnuShowHeightmap
			// 
			this.MnuShowHeightmap.Enabled = false;
			this.MnuShowHeightmap.Index = 3;
			this.MnuShowHeightmap.Text = "Show Heightmap";
			// 
			// FrmMain
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1034, 660);
			this.Controls.Add(this.SplMain);
			this.Controls.Add(this.StatusStrip);
			this.Controls.Add(this.ToolStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.MainMenu;
			this.Name = "FrmMain";
			this.Text = "Mabioned";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
			this.Load += new System.EventHandler(this.FrmMain_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FrmMain_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FrmMain_DragEnter);
			this.ToolStrip.ResumeLayout(false);
			this.ToolStrip.PerformLayout();
			this.StatusStrip.ResumeLayout(false);
			this.StatusStrip.PerformLayout();
			this.SplMain.Panel1.ResumeLayout(false);
			this.SplMain.Panel2.ResumeLayout(false);
			this.SplMain.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.SplMain)).EndInit();
			this.SplMain.ResumeLayout(false);
			this.SplSidebar.Panel1.ResumeLayout(false);
			this.SplSidebar.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SplSidebar)).EndInit();
			this.SplSidebar.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ImgMap)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.MainMenu MainMenu;
		private System.Windows.Forms.MenuItem MnuFile;
		private System.Windows.Forms.MenuItem MnuOpen;
		private System.Windows.Forms.MenuItem MnuExit;
		private System.Windows.Forms.MenuItem MnuHelp;
		private System.Windows.Forms.MenuItem MnuAbout;
		private System.Windows.Forms.ToolStrip ToolStrip;
		private System.Windows.Forms.ToolStripButton BtnOpen;
		private System.Windows.Forms.StatusStrip StatusStrip;
		private System.Windows.Forms.SplitContainer SplMain;
		private System.Windows.Forms.OpenFileDialog OfdRegion;
		private System.Windows.Forms.SplitContainer SplSidebar;
		private System.Windows.Forms.TreeView TreeRegion;
		private System.Windows.Forms.PropertyGrid PropertyGrid;
		private System.Windows.Forms.PictureBox ImgMap;
		private System.Windows.Forms.ToolStripStatusLabel LblCurrentPosition;
		private System.Windows.Forms.ToolStripStatusLabel LblScale;
		private System.Windows.Forms.MenuItem MnuView;
		private System.Windows.Forms.MenuItem MnuShowProps;
		private System.Windows.Forms.MenuItem MnuShowEvents;
		private System.Windows.Forms.MenuItem MnuShowAreas;
		private System.Windows.Forms.MenuItem MnuShowEventsAll;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem MnuShowEventsUndefined;
		private System.Windows.Forms.MenuItem MnuNew;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem MnuSave;
		private System.Windows.Forms.MenuItem MnuSaveAs;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.ToolStripButton BtnNew;
		private System.Windows.Forms.ToolStripButton BtnSave;
		private System.Windows.Forms.ContextMenu CtxMap;
		private System.Windows.Forms.MenuItem MnuCopyCoordinates;
		private System.Windows.Forms.MenuItem MnuCopyAuraWarp;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem MnuScaleToFit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton BtnScrollTool;
		private System.Windows.Forms.ToolStripButton BtnMoveTool;
		private System.Windows.Forms.ToolStripButton BtnRotateTool;
		private System.Windows.Forms.ImageList ImgsTree;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem MnuExpand;
		private System.Windows.Forms.MenuItem MnuCollapse;
		private System.Windows.Forms.SaveFileDialog SfdRegion;
		private System.Windows.Forms.MenuItem MnuRecent;
		private System.Windows.Forms.MenuItem MnuRecentNone;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem MnuEdit;
		private System.Windows.Forms.MenuItem MnuEditSettings;
		private System.Windows.Forms.MenuItem MnuShowHeightmap;
	}
}

