namespace Mabioned
{
	partial class FrmAbout
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbout));
			this.PnlHeader = new System.Windows.Forms.Panel();
			this.LblVersion = new System.Windows.Forms.Label();
			this.LblSubTitle = new System.Windows.Forms.Label();
			this.LblName = new System.Windows.Forms.Label();
			this.ImgIcon = new System.Windows.Forms.PictureBox();
			this.BtnOK = new System.Windows.Forms.Button();
			this.ImgPatreon = new System.Windows.Forms.PictureBox();
			this.ImgGitHub = new System.Windows.Forms.PictureBox();
			this.GrpLicense = new System.Windows.Forms.GroupBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.PnlHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ImgPatreon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ImgGitHub)).BeginInit();
			this.GrpLicense.SuspendLayout();
			this.SuspendLayout();
			// 
			// PnlHeader
			// 
			this.PnlHeader.BackColor = System.Drawing.Color.White;
			this.PnlHeader.Controls.Add(this.LblVersion);
			this.PnlHeader.Controls.Add(this.LblSubTitle);
			this.PnlHeader.Controls.Add(this.LblName);
			this.PnlHeader.Controls.Add(this.ImgIcon);
			this.PnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.PnlHeader.ForeColor = System.Drawing.SystemColors.ControlText;
			this.PnlHeader.Location = new System.Drawing.Point(0, 0);
			this.PnlHeader.Name = "PnlHeader";
			this.PnlHeader.Size = new System.Drawing.Size(394, 81);
			this.PnlHeader.TabIndex = 0;
			// 
			// LblVersion
			// 
			this.LblVersion.AutoSize = true;
			this.LblVersion.ForeColor = System.Drawing.Color.Gray;
			this.LblVersion.Location = new System.Drawing.Point(153, 28);
			this.LblVersion.Name = "LblVersion";
			this.LblVersion.Size = new System.Drawing.Size(43, 13);
			this.LblVersion.TabIndex = 3;
			this.LblVersion.Text = "v0.8.0a";
			// 
			// LblSubTitle
			// 
			this.LblSubTitle.AutoSize = true;
			this.LblSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.LblSubTitle.Location = new System.Drawing.Point(65, 43);
			this.LblSubTitle.Name = "LblSubTitle";
			this.LblSubTitle.Size = new System.Drawing.Size(117, 13);
			this.LblSubTitle.TabIndex = 2;
			this.LblSubTitle.Text = "Mabinogi Region Editor";
			// 
			// LblName
			// 
			this.LblName.AutoSize = true;
			this.LblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.LblName.Location = new System.Drawing.Point(63, 20);
			this.LblName.Name = "LblName";
			this.LblName.Size = new System.Drawing.Size(95, 24);
			this.LblName.TabIndex = 2;
			this.LblName.Text = "Mabioned";
			// 
			// ImgIcon
			// 
			this.ImgIcon.Image = ((System.Drawing.Image)(resources.GetObject("ImgIcon.Image")));
			this.ImgIcon.Location = new System.Drawing.Point(25, 24);
			this.ImgIcon.Name = "ImgIcon";
			this.ImgIcon.Size = new System.Drawing.Size(32, 32);
			this.ImgIcon.TabIndex = 2;
			this.ImgIcon.TabStop = false;
			// 
			// BtnOK
			// 
			this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnOK.Location = new System.Drawing.Point(307, 294);
			this.BtnOK.Name = "BtnOK";
			this.BtnOK.Size = new System.Drawing.Size(75, 23);
			this.BtnOK.TabIndex = 1;
			this.BtnOK.Text = "OK";
			this.BtnOK.UseVisualStyleBackColor = true;
			this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
			// 
			// ImgPatreon
			// 
			this.ImgPatreon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ImgPatreon.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ImgPatreon.Image = ((System.Drawing.Image)(resources.GetObject("ImgPatreon.Image")));
			this.ImgPatreon.Location = new System.Drawing.Point(12, 285);
			this.ImgPatreon.Name = "ImgPatreon";
			this.ImgPatreon.Size = new System.Drawing.Size(189, 32);
			this.ImgPatreon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.ImgPatreon.TabIndex = 22;
			this.ImgPatreon.TabStop = false;
			this.ImgPatreon.Tag = "https://www.patreon.com/exectails";
			this.ImgPatreon.Click += new System.EventHandler(this.Link_Click);
			// 
			// ImgGitHub
			// 
			this.ImgGitHub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ImgGitHub.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ImgGitHub.Image = ((System.Drawing.Image)(resources.GetObject("ImgGitHub.Image")));
			this.ImgGitHub.Location = new System.Drawing.Point(207, 285);
			this.ImgGitHub.Name = "ImgGitHub";
			this.ImgGitHub.Size = new System.Drawing.Size(32, 32);
			this.ImgGitHub.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.ImgGitHub.TabIndex = 21;
			this.ImgGitHub.TabStop = false;
			this.ImgGitHub.Tag = "https://github.com/exectails";
			this.ImgGitHub.Click += new System.EventHandler(this.Link_Click);
			// 
			// GrpLicense
			// 
			this.GrpLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GrpLicense.Controls.Add(this.textBox1);
			this.GrpLicense.Location = new System.Drawing.Point(12, 87);
			this.GrpLicense.Name = "GrpLicense";
			this.GrpLicense.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
			this.GrpLicense.Size = new System.Drawing.Size(370, 192);
			this.GrpLicense.TabIndex = 23;
			this.GrpLicense.TabStop = false;
			this.GrpLicense.Text = "License";
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox1.Location = new System.Drawing.Point(12, 21);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(346, 163);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = resources.GetString("textBox1.Text");
			// 
			// FrmAbout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(394, 329);
			this.Controls.Add(this.GrpLicense);
			this.Controls.Add(this.ImgPatreon);
			this.Controls.Add(this.ImgGitHub);
			this.Controls.Add(this.BtnOK);
			this.Controls.Add(this.PnlHeader);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmAbout";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.PnlHeader.ResumeLayout(false);
			this.PnlHeader.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ImgPatreon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ImgGitHub)).EndInit();
			this.GrpLicense.ResumeLayout(false);
			this.GrpLicense.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel PnlHeader;
		private System.Windows.Forms.Label LblSubTitle;
		private System.Windows.Forms.Label LblName;
		private System.Windows.Forms.PictureBox ImgIcon;
		private System.Windows.Forms.Button BtnOK;
		private System.Windows.Forms.PictureBox ImgPatreon;
		private System.Windows.Forms.PictureBox ImgGitHub;
		private System.Windows.Forms.Label LblVersion;
		private System.Windows.Forms.GroupBox GrpLicense;
		private System.Windows.Forms.TextBox textBox1;
	}
}
