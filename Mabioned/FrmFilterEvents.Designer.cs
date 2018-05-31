namespace Mabioned
{
	partial class FrmFilterEvents
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
			this.GrpOptions = new System.Windows.Forms.GroupBox();
			this.ChkAllEvents = new System.Windows.Forms.RadioButton();
			this.ChkOnlyEvents = new System.Windows.Forms.RadioButton();
			this.ChkType = new System.Windows.Forms.CheckBox();
			this.CboType = new System.Windows.Forms.ComboBox();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.BtnOK = new System.Windows.Forms.Button();
			this.ChkNotType = new System.Windows.Forms.CheckBox();
			this.CboNotType = new System.Windows.Forms.ComboBox();
			this.GrpOptions.SuspendLayout();
			this.SuspendLayout();
			// 
			// GrpOptions
			// 
			this.GrpOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GrpOptions.Controls.Add(this.ChkNotType);
			this.GrpOptions.Controls.Add(this.CboNotType);
			this.GrpOptions.Controls.Add(this.ChkType);
			this.GrpOptions.Controls.Add(this.CboType);
			this.GrpOptions.Enabled = false;
			this.GrpOptions.Location = new System.Drawing.Point(12, 58);
			this.GrpOptions.Name = "GrpOptions";
			this.GrpOptions.Size = new System.Drawing.Size(341, 84);
			this.GrpOptions.TabIndex = 0;
			this.GrpOptions.TabStop = false;
			// 
			// ChkAllEvents
			// 
			this.ChkAllEvents.AutoSize = true;
			this.ChkAllEvents.Checked = true;
			this.ChkAllEvents.Location = new System.Drawing.Point(12, 12);
			this.ChkAllEvents.Name = "ChkAllEvents";
			this.ChkAllEvents.Size = new System.Drawing.Size(72, 17);
			this.ChkAllEvents.TabIndex = 3;
			this.ChkAllEvents.TabStop = true;
			this.ChkAllEvents.Text = "All Events";
			this.ChkAllEvents.UseVisualStyleBackColor = true;
			// 
			// ChkOnlyEvents
			// 
			this.ChkOnlyEvents.AutoSize = true;
			this.ChkOnlyEvents.Location = new System.Drawing.Point(12, 35);
			this.ChkOnlyEvents.Name = "ChkOnlyEvents";
			this.ChkOnlyEvents.Size = new System.Drawing.Size(112, 17);
			this.ChkOnlyEvents.TabIndex = 4;
			this.ChkOnlyEvents.Text = "Only Events that...";
			this.ChkOnlyEvents.UseVisualStyleBackColor = true;
			this.ChkOnlyEvents.CheckedChanged += new System.EventHandler(this.ChkOnlyEvents_CheckedChanged);
			// 
			// ChkType
			// 
			this.ChkType.AutoSize = true;
			this.ChkType.Location = new System.Drawing.Point(16, 21);
			this.ChkType.Name = "ChkType";
			this.ChkType.Size = new System.Drawing.Size(76, 17);
			this.ChkType.TabIndex = 4;
			this.ChkType.Text = "are of type";
			this.ChkType.UseVisualStyleBackColor = true;
			// 
			// CboType
			// 
			this.CboType.FormattingEnabled = true;
			this.CboType.Location = new System.Drawing.Point(116, 19);
			this.CboType.Name = "CboType";
			this.CboType.Size = new System.Drawing.Size(212, 21);
			this.CboType.TabIndex = 3;
			// 
			// BtnCancel
			// 
			this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnCancel.Location = new System.Drawing.Point(278, 148);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(75, 23);
			this.BtnCancel.TabIndex = 8;
			this.BtnCancel.Text = "Cancel";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// BtnOK
			// 
			this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnOK.Location = new System.Drawing.Point(197, 148);
			this.BtnOK.Name = "BtnOK";
			this.BtnOK.Size = new System.Drawing.Size(75, 23);
			this.BtnOK.TabIndex = 7;
			this.BtnOK.Text = "OK";
			this.BtnOK.UseVisualStyleBackColor = true;
			this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
			// 
			// ChkNotType
			// 
			this.ChkNotType.AutoSize = true;
			this.ChkNotType.Location = new System.Drawing.Point(16, 48);
			this.ChkNotType.Name = "ChkNotType";
			this.ChkNotType.Size = new System.Drawing.Size(94, 17);
			this.ChkNotType.TabIndex = 6;
			this.ChkNotType.Text = "are not of type";
			this.ChkNotType.UseVisualStyleBackColor = true;
			// 
			// CboNotType
			// 
			this.CboNotType.FormattingEnabled = true;
			this.CboNotType.Location = new System.Drawing.Point(116, 46);
			this.CboNotType.Name = "CboNotType";
			this.CboNotType.Size = new System.Drawing.Size(212, 21);
			this.CboNotType.TabIndex = 5;
			// 
			// FrmFilterEvents
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(365, 183);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.BtnOK);
			this.Controls.Add(this.ChkOnlyEvents);
			this.Controls.Add(this.ChkAllEvents);
			this.Controls.Add(this.GrpOptions);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmFilterEvents";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select...";
			this.Load += new System.EventHandler(this.FrmFilterEvents_Load);
			this.GrpOptions.ResumeLayout(false);
			this.GrpOptions.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox GrpOptions;
		private System.Windows.Forms.CheckBox ChkType;
		private System.Windows.Forms.ComboBox CboType;
		private System.Windows.Forms.RadioButton ChkAllEvents;
		private System.Windows.Forms.RadioButton ChkOnlyEvents;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.Button BtnOK;
		private System.Windows.Forms.CheckBox ChkNotType;
		private System.Windows.Forms.ComboBox CboNotType;
	}
}