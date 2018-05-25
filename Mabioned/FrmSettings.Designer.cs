﻿namespace Mabioned
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettings));
			this.BtnOK = new System.Windows.Forms.Button();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.label5 = new System.Windows.Forms.Label();
			this.LblSelectionColor = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.LblBackgroundColor = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.LblAreasColor = new System.Windows.Forms.Label();
			this.LblEventsColor = new System.Windows.Forms.Label();
			this.LblPropsColor = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.label3 = new System.Windows.Forms.Label();
			this.TxtDataFolder = new System.Windows.Forms.TextBox();
			this.LblDataFolder = new System.Windows.Forms.Label();
			this.BtnSelectDataFolder = new System.Windows.Forms.Button();
			this.DlgFolder = new System.Windows.Forms.FolderBrowserDialog();
			this.tabPage1.SuspendLayout();
			this.tabControl1.SuspendLayout();
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
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.LblSelectionColor);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.LblBackgroundColor);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.LblAreasColor);
			this.tabPage1.Controls.Add(this.LblEventsColor);
			this.tabPage1.Controls.Add(this.LblPropsColor);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(293, 146);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Colors";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(16, 89);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(51, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Selection";
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
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Background";
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
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Props";
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
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(16, 70);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(34, 13);
			this.label6.TabIndex = 5;
			this.label6.Text = "Areas";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(16, 51);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Events";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(301, 172);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage2
			// 
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
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.Gray;
			this.label3.Location = new System.Drawing.Point(16, 120);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(232, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "Right-click a color to reset it to the default color.";
			// 
			// TxtDataFolder
			// 
			this.TxtDataFolder.Location = new System.Drawing.Point(66, 10);
			this.TxtDataFolder.Name = "TxtDataFolder";
			this.TxtDataFolder.Size = new System.Drawing.Size(176, 20);
			this.TxtDataFolder.TabIndex = 1;
			// 
			// LblDataFolder
			// 
			this.LblDataFolder.AutoSize = true;
			this.LblDataFolder.Location = new System.Drawing.Point(16, 13);
			this.LblDataFolder.Name = "LblDataFolder";
			this.LblDataFolder.Size = new System.Drawing.Size(30, 13);
			this.LblDataFolder.TabIndex = 2;
			this.LblDataFolder.Text = "Data";
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
			// DlgFolder
			// 
			this.DlgFolder.ShowNewFolderButton = false;
			// 
			// FrmSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(325, 225);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.BtnOK);
			this.Controls.Add(this.tabControl1);
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
			this.tabControl1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button BtnOK;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label LblAreasColor;
		private System.Windows.Forms.Label LblEventsColor;
		private System.Windows.Forms.Label LblPropsColor;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label LblSelectionColor;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label LblBackgroundColor;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button BtnSelectDataFolder;
		private System.Windows.Forms.Label LblDataFolder;
		private System.Windows.Forms.TextBox TxtDataFolder;
		private System.Windows.Forms.FolderBrowserDialog DlgFolder;
	}
}