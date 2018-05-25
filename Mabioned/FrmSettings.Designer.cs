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
			this.tabPage1 = new System.Windows.Forms.TabPage();
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
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.BtnSelectDataFolder = new System.Windows.Forms.Button();
			this.LblDataFolder = new System.Windows.Forms.Label();
			this.TxtDataFolder = new System.Windows.Forms.TextBox();
			this.DlgFolder = new System.Windows.Forms.FolderBrowserDialog();
			this.LblRestartWarning = new System.Windows.Forms.Label();
			this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.tabPage1.SuspendLayout();
			this.TabsMain.SuspendLayout();
			this.tabPage2.SuspendLayout();
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
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.LblRightClickResetHint);
			this.tabPage1.Controls.Add(this.LblSelectionColorLabel);
			this.tabPage1.Controls.Add(this.LblSelectionColor);
			this.tabPage1.Controls.Add(this.LblBackgroundColorName);
			this.tabPage1.Controls.Add(this.LblBackgroundColor);
			this.tabPage1.Controls.Add(this.LblPropColorLabel);
			this.tabPage1.Controls.Add(this.LblAreasColor);
			this.tabPage1.Controls.Add(this.LblEventsColor);
			this.tabPage1.Controls.Add(this.LblPropsColor);
			this.tabPage1.Controls.Add(this.LblAreaColorLabel);
			this.tabPage1.Controls.Add(this.LblEventColorLabel);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(293, 146);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Colors";
			this.tabPage1.UseVisualStyleBackColor = true;
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
			this.TabsMain.Controls.Add(this.tabPage1);
			this.TabsMain.Controls.Add(this.tabPage2);
			this.TabsMain.Location = new System.Drawing.Point(12, 12);
			this.TabsMain.Name = "TabsMain";
			this.TabsMain.SelectedIndex = 0;
			this.TabsMain.Size = new System.Drawing.Size(301, 172);
			this.TabsMain.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.LblRestartWarning);
			this.tabPage2.Controls.Add(this.BtnSelectDataFolder);
			this.tabPage2.Controls.Add(this.LblDataFolder);
			this.tabPage2.Controls.Add(this.TxtDataFolder);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(293, 146);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Folders";
			this.tabPage2.UseVisualStyleBackColor = true;
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
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.TabsMain.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button BtnOK;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.TabPage tabPage1;
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
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button BtnSelectDataFolder;
		private System.Windows.Forms.Label LblDataFolder;
		private System.Windows.Forms.TextBox TxtDataFolder;
		private System.Windows.Forms.FolderBrowserDialog DlgFolder;
		private System.Windows.Forms.ToolTip ToolTip;
		private System.Windows.Forms.Label LblRestartWarning;
	}
}