namespace Mabioned
{
	partial class FrmSettings
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettings));
			this.BtnOK = new System.Windows.Forms.Button();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.TabColors = new System.Windows.Forms.TabPage();
			this.LblRightClickResetHint = new System.Windows.Forms.Label();
			this.LblSelectionColorLabel = new System.Windows.Forms.Label();
			this.LblSelectionColor = new System.Windows.Forms.Label();
			this.LblBackgroundColorName = new System.Windows.Forms.Label();
			this.LblBackgroundColor = new System.Windows.Forms.Label();
			this.LblPropColorLabel = new System.Windows.Forms.Label();
			this.LblAreasColor = new System.Windows.Forms.Label();
			this.LblEventsColor = new System.Windows.Forms.Label();
			this.LblPropsColor = new System.Windows.Forms.Label();
			this.LblAreaColorLabel = new System.Windows.Forms.Label();
			this.LblEventColorLabel = new System.Windows.Forms.Label();
			this.TabsMain = new System.Windows.Forms.TabControl();
			this.TabFolders = new System.Windows.Forms.TabPage();
			this.LblRestartWarning = new System.Windows.Forms.Label();
			this.BtnSelectDataFolder = new System.Windows.Forms.Button();
			this.LblDataFolder = new System.Windows.Forms.Label();
			this.TxtDataFolder = new System.Windows.Forms.TextBox();
			this.DlgFolder = new System.Windows.Forms.FolderBrowserDialog();
			this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.TabGeneral = new System.Windows.Forms.TabPage();
			this.ChkSingleInstance = new System.Windows.Forms.CheckBox();
			this.TabColors.SuspendLayout();
			this.TabsMain.SuspendLayout();
			this.TabFolders.SuspendLayout();
			this.TabGeneral.SuspendLayout();
			this.SuspendLayout();
			// 
			// BtnOK
			// 
			this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnOK.Location = new System.Drawing.Point(157, 190);
			this.BtnOK.Name = "BtnOK";
			this.BtnOK.Size = new System.Drawing.Size(75, 23);
			this.BtnOK.TabIndex = 1;
			this.BtnOK.Text = "OK";
			this.BtnOK.UseVisualStyleBackColor = true;
			this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
			// 
			// BtnCancel
			// 
			this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnCancel.Location = new System.Drawing.Point(238, 190);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(75, 23);
			this.BtnCancel.TabIndex = 2;
			this.BtnCancel.Text = "Cancel";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// TabColors
			// 
			this.TabColors.Controls.Add(this.LblRightClickResetHint);
			this.TabColors.Controls.Add(this.LblSelectionColorLabel);
			this.TabColors.Controls.Add(this.LblSelectionColor);
			this.TabColors.Controls.Add(this.LblBackgroundColorName);
			this.TabColors.Controls.Add(this.LblBackgroundColor);
			this.TabColors.Controls.Add(this.LblPropColorLabel);
			this.TabColors.Controls.Add(this.LblAreasColor);
			this.TabColors.Controls.Add(this.LblEventsColor);
			this.TabColors.Controls.Add(this.LblPropsColor);
			this.TabColors.Controls.Add(this.LblAreaColorLabel);
			this.TabColors.Controls.Add(this.LblEventColorLabel);
			this.TabColors.Location = new System.Drawing.Point(4, 22);
			this.TabColors.Name = "TabColors";
			this.TabColors.Padding = new System.Windows.Forms.Padding(3);
			this.TabColors.Size = new System.Drawing.Size(293, 146);
			this.TabColors.TabIndex = 0;
			this.TabColors.Text = "Colors";
			this.TabColors.UseVisualStyleBackColor = true;
			// 
			// LblRightClickResetHint
			// 
			this.LblRightClickResetHint.AutoSize = true;
			this.LblRightClickResetHint.ForeColor = System.Drawing.Color.Gray;
			this.LblRightClickResetHint.Location = new System.Drawing.Point(16, 120);
			this.LblRightClickResetHint.Name = "LblRightClickResetHint";
			this.LblRightClickResetHint.Size = new System.Drawing.Size(232, 13);
			this.LblRightClickResetHint.TabIndex = 11;
			this.LblRightClickResetHint.Text = "Right-click a color to reset it to the default color.";
			// 
			// LblSelectionColorLabel
			// 
			this.LblSelectionColorLabel.Location = new System.Drawing.Point(16, 89);
			this.LblSelectionColorLabel.Name = "LblSelectionColorLabel";
			this.LblSelectionColorLabel.Size = new System.Drawing.Size(235, 13);
			this.LblSelectionColorLabel.TabIndex = 9;
			this.LblSelectionColorLabel.Text = "Selection";
			this.ToolTip.SetToolTip(this.LblSelectionColorLabel, "Color of selected entities.");
			// 
			// LblSelectionColor
			// 
			this.LblSelectionColor.BackColor = System.Drawing.Color.Silver;
			this.LblSelectionColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.LblSelectionColor.Location = new System.Drawing.Point(261, 86);
			this.LblSelectionColor.Name = "LblSelectionColor";
			this.LblSelectionColor.Size = new System.Drawing.Size(16, 16);
			this.LblSelectionColor.TabIndex = 10;
			this.LblSelectionColor.Tag = "SelectionColor";
			this.LblSelectionColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LblColor_MouseClick);
			// 
			// LblBackgroundColorName
			// 
			this.LblBackgroundColorName.Location = new System.Drawing.Point(16, 13);
			this.LblBackgroundColorName.Name = "LblBackgroundColorName";
			this.LblBackgroundColorName.Size = new System.Drawing.Size(235, 13);
			this.LblBackgroundColorName.TabIndex = 7;
			this.LblBackgroundColorName.Text = "Background";
			this.ToolTip.SetToolTip(this.LblBackgroundColorName, "Map background color.");
			// 
			// LblBackgroundColor
			// 
			this.LblBackgroundColor.BackColor = System.Drawing.Color.Silver;
			this.LblBackgroundColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.LblBackgroundColor.Location = new System.Drawing.Point(261, 10);
			this.LblBackgroundColor.Name = "LblBackgroundColor";
			this.LblBackgroundColor.Size = new System.Drawing.Size(16, 16);
			this.LblBackgroundColor.TabIndex = 8;
			this.LblBackgroundColor.Tag = "BackgroundColor";
			this.LblBackgroundColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LblColor_MouseClick);
			// 
			// LblPropColorLabel
			// 
			this.LblPropColorLabel.Location = new System.Drawing.Point(16, 32);
			this.LblPropColorLabel.Name = "LblPropColorLabel";
			this.LblPropColorLabel.Size = new System.Drawing.Size(235, 13);
			this.LblPropColorLabel.TabIndex = 1;
			this.LblPropColorLabel.Text = "Props";
			this.ToolTip.SetToolTip(this.LblPropColorLabel, "Prop shape color.");
			// 
			// LblAreasColor
			// 
			this.LblAreasColor.BackColor = System.Drawing.Color.Silver;
			this.LblAreasColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.LblAreasColor.Location = new System.Drawing.Point(261, 67);
			this.LblAreasColor.Name = "LblAreasColor";
			this.LblAreasColor.Size = new System.Drawing.Size(16, 16);
			this.LblAreasColor.TabIndex = 6;
			this.LblAreasColor.Tag = "AreasColor";
			this.LblAreasColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LblColor_MouseClick);
			// 
			// LblEventsColor
			// 
			this.LblEventsColor.BackColor = System.Drawing.Color.Silver;
			this.LblEventsColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.LblEventsColor.Location = new System.Drawing.Point(261, 48);
			this.LblEventsColor.Name = "LblEventsColor";
			this.LblEventsColor.Size = new System.Drawing.Size(16, 16);
			this.LblEventsColor.TabIndex = 4;
			this.LblEventsColor.Tag = "EventsColor";
			this.LblEventsColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LblColor_MouseClick);
			// 
			// LblPropsColor
			// 
			this.LblPropsColor.BackColor = System.Drawing.Color.Silver;
			this.LblPropsColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.LblPropsColor.Location = new System.Drawing.Point(261, 29);
			this.LblPropsColor.Name = "LblPropsColor";
			this.LblPropsColor.Size = new System.Drawing.Size(16, 16);
			this.LblPropsColor.TabIndex = 2;
			this.LblPropsColor.Tag = "PropsColor";
			this.LblPropsColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LblColor_MouseClick);
			// 
			// LblAreaColorLabel
			// 
			this.LblAreaColorLabel.Location = new System.Drawing.Point(16, 70);
			this.LblAreaColorLabel.Name = "LblAreaColorLabel";
			this.LblAreaColorLabel.Size = new System.Drawing.Size(235, 13);
			this.LblAreaColorLabel.TabIndex = 5;
			this.LblAreaColorLabel.Text = "Areas";
			this.ToolTip.SetToolTip(this.LblAreaColorLabel, "Area bounding box color.");
			// 
			// LblEventColorLabel
			// 
			this.LblEventColorLabel.Location = new System.Drawing.Point(16, 51);
			this.LblEventColorLabel.Name = "LblEventColorLabel";
			this.LblEventColorLabel.Size = new System.Drawing.Size(235, 13);
			this.LblEventColorLabel.TabIndex = 3;
			this.LblEventColorLabel.Text = "Events";
			this.ToolTip.SetToolTip(this.LblEventColorLabel, "Event shape color.");
			// 
			// TabsMain
			// 
			this.TabsMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TabsMain.Controls.Add(this.TabGeneral);
			this.TabsMain.Controls.Add(this.TabColors);
			this.TabsMain.Controls.Add(this.TabFolders);
			this.TabsMain.Location = new System.Drawing.Point(12, 12);
			this.TabsMain.Name = "TabsMain";
			this.TabsMain.SelectedIndex = 0;
			this.TabsMain.Size = new System.Drawing.Size(301, 172);
			this.TabsMain.TabIndex = 0;
			// 
			// TabFolders
			// 
			this.TabFolders.Controls.Add(this.LblRestartWarning);
			this.TabFolders.Controls.Add(this.BtnSelectDataFolder);
			this.TabFolders.Controls.Add(this.LblDataFolder);
			this.TabFolders.Controls.Add(this.TxtDataFolder);
			this.TabFolders.Location = new System.Drawing.Point(4, 22);
			this.TabFolders.Name = "TabFolders";
			this.TabFolders.Padding = new System.Windows.Forms.Padding(3);
			this.TabFolders.Size = new System.Drawing.Size(293, 146);
			this.TabFolders.TabIndex = 1;
			this.TabFolders.Text = "Folders";
			this.TabFolders.UseVisualStyleBackColor = true;
			// 
			// LblRestartWarning
			// 
			this.LblRestartWarning.AutoSize = true;
			this.LblRestartWarning.ForeColor = System.Drawing.Color.Gray;
			this.LblRestartWarning.Location = new System.Drawing.Point(16, 120);
			this.LblRestartWarning.Name = "LblRestartWarning";
			this.LblRestartWarning.Size = new System.Drawing.Size(244, 13);
			this.LblRestartWarning.TabIndex = 4;
			this.LblRestartWarning.Text = "Some options might require a restart to take effect.";
			// 
			// BtnSelectDataFolder
			// 
			this.BtnSelectDataFolder.Location = new System.Drawing.Point(248, 10);
			this.BtnSelectDataFolder.Name = "BtnSelectDataFolder";
			this.BtnSelectDataFolder.Size = new System.Drawing.Size(25, 20);
			this.BtnSelectDataFolder.TabIndex = 3;
			this.BtnSelectDataFolder.Text = "...";
			this.BtnSelectDataFolder.UseVisualStyleBackColor = true;
			this.BtnSelectDataFolder.Click += new System.EventHandler(this.BtnSelectDataFolder_Click);
			// 
			// LblDataFolder
			// 
			this.LblDataFolder.Location = new System.Drawing.Point(16, 13);
			this.LblDataFolder.Name = "LblDataFolder";
			this.LblDataFolder.Size = new System.Drawing.Size(44, 13);
			this.LblDataFolder.TabIndex = 2;
			this.LblDataFolder.Text = "Data";
			this.ToolTip.SetToolTip(this.LblDataFolder, "Extracted Mabinogi data folder, used to get additional information, like prop nam" +
        "es.");
			// 
			// TxtDataFolder
			// 
			this.TxtDataFolder.Location = new System.Drawing.Point(66, 10);
			this.TxtDataFolder.Name = "TxtDataFolder";
			this.TxtDataFolder.Size = new System.Drawing.Size(176, 20);
			this.TxtDataFolder.TabIndex = 1;
			// 
			// DlgFolder
			// 
			this.DlgFolder.ShowNewFolderButton = false;
			// 
			// TabGeneral
			// 
			this.TabGeneral.Controls.Add(this.ChkSingleInstance);
			this.TabGeneral.Location = new System.Drawing.Point(4, 22);
			this.TabGeneral.Name = "TabGeneral";
			this.TabGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.TabGeneral.Size = new System.Drawing.Size(293, 146);
			this.TabGeneral.TabIndex = 2;
			this.TabGeneral.Text = "General";
			this.TabGeneral.UseVisualStyleBackColor = true;
			// 
			// ChkSingleInstance
			// 
			this.ChkSingleInstance.AutoSize = true;
			this.ChkSingleInstance.Location = new System.Drawing.Point(16, 13);
			this.ChkSingleInstance.Name = "ChkSingleInstance";
			this.ChkSingleInstance.Size = new System.Drawing.Size(99, 17);
			this.ChkSingleInstance.TabIndex = 0;
			this.ChkSingleInstance.Text = "Single Instance";
			this.ToolTip.SetToolTip(this.ChkSingleInstance, "Allow only one instance of the application to be open at a time?");
			this.ChkSingleInstance.UseVisualStyleBackColor = true;
			// 
			// FrmSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(325, 225);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.BtnOK);
			this.Controls.Add(this.TabsMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmSettings";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			this.Load += new System.EventHandler(this.FrmSettings_Load);
			this.TabColors.ResumeLayout(false);
			this.TabColors.PerformLayout();
			this.TabsMain.ResumeLayout(false);
			this.TabFolders.ResumeLayout(false);
			this.TabFolders.PerformLayout();
			this.TabGeneral.ResumeLayout(false);
			this.TabGeneral.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button BtnOK;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.TabPage TabColors;
		private System.Windows.Forms.TabControl TabsMain;
		private System.Windows.Forms.Label LblPropColorLabel;
		private System.Windows.Forms.Label LblAreasColor;
		private System.Windows.Forms.Label LblEventsColor;
		private System.Windows.Forms.Label LblPropsColor;
		private System.Windows.Forms.Label LblAreaColorLabel;
		private System.Windows.Forms.Label LblEventColorLabel;
		private System.Windows.Forms.Label LblSelectionColorLabel;
		private System.Windows.Forms.Label LblSelectionColor;
		private System.Windows.Forms.Label LblBackgroundColorName;
		private System.Windows.Forms.Label LblBackgroundColor;
		private System.Windows.Forms.Label LblRightClickResetHint;
		private System.Windows.Forms.TabPage TabFolders;
		private System.Windows.Forms.Button BtnSelectDataFolder;
		private System.Windows.Forms.Label LblDataFolder;
		private System.Windows.Forms.TextBox TxtDataFolder;
		private System.Windows.Forms.FolderBrowserDialog DlgFolder;
		private System.Windows.Forms.ToolTip ToolTip;
		private System.Windows.Forms.Label LblRestartWarning;
		private System.Windows.Forms.TabPage TabGeneral;
		private System.Windows.Forms.CheckBox ChkSingleInstance;
	}
}