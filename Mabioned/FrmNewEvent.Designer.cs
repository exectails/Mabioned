namespace Mabioned
{
	partial class FrmNewEvent
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
			this.GrpProperties = new System.Windows.Forms.GroupBox();
			this.TxtPosX = new System.Windows.Forms.TextBox();
			this.TxtPosY = new System.Windows.Forms.TextBox();
			this.LblPosition = new System.Windows.Forms.Label();
			this.TxtPosZ = new System.Windows.Forms.TextBox();
			this.TxtWidth = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.TxtHeight = new System.Windows.Forms.TextBox();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.BtnOK = new System.Windows.Forms.Button();
			this.GrpProperties.SuspendLayout();
			this.SuspendLayout();
			// 
			// GrpProperties
			// 
			this.GrpProperties.Controls.Add(this.TxtWidth);
			this.GrpProperties.Controls.Add(this.label1);
			this.GrpProperties.Controls.Add(this.TxtHeight);
			this.GrpProperties.Controls.Add(this.TxtPosX);
			this.GrpProperties.Controls.Add(this.TxtPosY);
			this.GrpProperties.Controls.Add(this.LblPosition);
			this.GrpProperties.Controls.Add(this.TxtPosZ);
			this.GrpProperties.Location = new System.Drawing.Point(12, 12);
			this.GrpProperties.Name = "GrpProperties";
			this.GrpProperties.Size = new System.Drawing.Size(370, 78);
			this.GrpProperties.TabIndex = 100;
			this.GrpProperties.TabStop = false;
			this.GrpProperties.Text = "Properties";
			// 
			// TxtPosX
			// 
			this.TxtPosX.Location = new System.Drawing.Point(92, 19);
			this.TxtPosX.Name = "TxtPosX";
			this.TxtPosX.Size = new System.Drawing.Size(85, 20);
			this.TxtPosX.TabIndex = 7;
			// 
			// TxtPosY
			// 
			this.TxtPosY.Location = new System.Drawing.Point(183, 19);
			this.TxtPosY.Name = "TxtPosY";
			this.TxtPosY.Size = new System.Drawing.Size(85, 20);
			this.TxtPosY.TabIndex = 8;
			// 
			// LblPosition
			// 
			this.LblPosition.AutoSize = true;
			this.LblPosition.Location = new System.Drawing.Point(11, 22);
			this.LblPosition.Name = "LblPosition";
			this.LblPosition.Size = new System.Drawing.Size(44, 13);
			this.LblPosition.TabIndex = 2;
			this.LblPosition.Text = "Position";
			// 
			// TxtPosZ
			// 
			this.TxtPosZ.Location = new System.Drawing.Point(274, 19);
			this.TxtPosZ.Name = "TxtPosZ";
			this.TxtPosZ.Size = new System.Drawing.Size(85, 20);
			this.TxtPosZ.TabIndex = 9;
			// 
			// TxtWidth
			// 
			this.TxtWidth.Location = new System.Drawing.Point(92, 45);
			this.TxtWidth.Name = "TxtWidth";
			this.TxtWidth.Size = new System.Drawing.Size(131, 20);
			this.TxtWidth.TabIndex = 11;
			this.TxtWidth.Text = "500";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(27, 13);
			this.label1.TabIndex = 10;
			this.label1.Text = "Size";
			// 
			// TxtHeight
			// 
			this.TxtHeight.Location = new System.Drawing.Point(228, 45);
			this.TxtHeight.Name = "TxtHeight";
			this.TxtHeight.Size = new System.Drawing.Size(131, 20);
			this.TxtHeight.TabIndex = 13;
			this.TxtHeight.Text = "500";
			// 
			// BtnCancel
			// 
			this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnCancel.Location = new System.Drawing.Point(307, 99);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(75, 23);
			this.BtnCancel.TabIndex = 102;
			this.BtnCancel.Text = "Cancel";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// BtnOK
			// 
			this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnOK.Location = new System.Drawing.Point(226, 99);
			this.BtnOK.Name = "BtnOK";
			this.BtnOK.Size = new System.Drawing.Size(75, 23);
			this.BtnOK.TabIndex = 101;
			this.BtnOK.Text = "Add";
			this.BtnOK.UseVisualStyleBackColor = true;
			this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
			// 
			// FrmNewEvent
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(394, 134);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.BtnOK);
			this.Controls.Add(this.GrpProperties);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmNewEvent";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "New Event";
			this.GrpProperties.ResumeLayout(false);
			this.GrpProperties.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox GrpProperties;
		private System.Windows.Forms.TextBox TxtWidth;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox TxtHeight;
		private System.Windows.Forms.TextBox TxtPosX;
		private System.Windows.Forms.TextBox TxtPosY;
		private System.Windows.Forms.Label LblPosition;
		private System.Windows.Forms.TextBox TxtPosZ;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.Button BtnOK;
	}
}