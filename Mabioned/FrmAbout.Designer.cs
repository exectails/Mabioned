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
			this.LblSubTitle = new System.Windows.Forms.Label();
			this.LblName = new System.Windows.Forms.Label();
			this.ImgIcon = new System.Windows.Forms.PictureBox();
			this.BtnOK = new System.Windows.Forms.Button();
			this.LblCopyright = new System.Windows.Forms.Label();
			this.LnkGithub = new System.Windows.Forms.LinkLabel();
			this.GrpDescription = new System.Windows.Forms.GroupBox();
			this.LblDescription = new System.Windows.Forms.Label();
			this.PnlHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImgIcon)).BeginInit();
			this.GrpDescription.SuspendLayout();
			this.SuspendLayout();
			// 
			// PnlHeader
			// 
			this.PnlHeader.BackColor = System.Drawing.Color.White;
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
			// LblSubTitle
			// 
			this.LblSubTitle.AutoSize = true;
			this.LblSubTitle.Location = new System.Drawing.Point(65, 43);
			this.LblSubTitle.Name = "LblSubTitle";
			this.LblSubTitle.Size = new System.Drawing.Size(111, 13);
			this.LblSubTitle.TabIndex = 2;
			this.LblSubTitle.Text = "Mabinogi Region Tool";
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
			this.BtnOK.Location = new System.Drawing.Point(307, 199);
			this.BtnOK.Name = "BtnOK";
			this.BtnOK.Size = new System.Drawing.Size(75, 23);
			this.BtnOK.TabIndex = 1;
			this.BtnOK.Text = "OK";
			this.BtnOK.UseVisualStyleBackColor = true;
			this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
			// 
			// LblCopyright
			// 
			this.LblCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.LblCopyright.AutoSize = true;
			this.LblCopyright.Location = new System.Drawing.Point(12, 191);
			this.LblCopyright.Name = "LblCopyright";
			this.LblCopyright.Size = new System.Drawing.Size(116, 13);
			this.LblCopyright.TabIndex = 2;
			this.LblCopyright.Text = "Copyright © 2018 exec";
			// 
			// LnkGithub
			// 
			this.LnkGithub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.LnkGithub.AutoSize = true;
			this.LnkGithub.Location = new System.Drawing.Point(12, 204);
			this.LnkGithub.Name = "LnkGithub";
			this.LnkGithub.Size = new System.Drawing.Size(141, 13);
			this.LnkGithub.TabIndex = 3;
			this.LnkGithub.TabStop = true;
			this.LnkGithub.Text = "https://github.com/exectails";
			this.LnkGithub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkGithub_LinkClicked);
			// 
			// GrpDescription
			// 
			this.GrpDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GrpDescription.Controls.Add(this.LblDescription);
			this.GrpDescription.Location = new System.Drawing.Point(12, 87);
			this.GrpDescription.Name = "GrpDescription";
			this.GrpDescription.Size = new System.Drawing.Size(370, 97);
			this.GrpDescription.TabIndex = 4;
			this.GrpDescription.TabStop = false;
			// 
			// LblDescription
			// 
			this.LblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LblDescription.Location = new System.Drawing.Point(10, 16);
			this.LblDescription.Name = "LblDescription";
			this.LblDescription.Size = new System.Drawing.Size(354, 78);
			this.LblDescription.TabIndex = 0;
			this.LblDescription.Text = resources.GetString("LblDescription.Text");
			// 
			// FrmAbout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(394, 234);
			this.Controls.Add(this.GrpDescription);
			this.Controls.Add(this.LnkGithub);
			this.Controls.Add(this.LblCopyright);
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
			this.GrpDescription.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel PnlHeader;
		private System.Windows.Forms.Label LblSubTitle;
		private System.Windows.Forms.Label LblName;
		private System.Windows.Forms.PictureBox ImgIcon;
		private System.Windows.Forms.Button BtnOK;
		private System.Windows.Forms.Label LblCopyright;
		private System.Windows.Forms.LinkLabel LnkGithub;
		private System.Windows.Forms.GroupBox GrpDescription;
		private System.Windows.Forms.Label LblDescription;
	}
}