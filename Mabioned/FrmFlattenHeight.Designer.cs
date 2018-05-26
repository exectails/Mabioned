namespace Mabioned
{
	partial class FrmFlattenHeight
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
			this.LblNewHeight = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.BtnOK = new System.Windows.Forms.Button();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.TxtAverage = new System.Windows.Forms.TextBox();
			this.TxtNewHeight = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// LblNewHeight
			// 
			this.LblNewHeight.AutoSize = true;
			this.LblNewHeight.Location = new System.Drawing.Point(12, 38);
			this.LblNewHeight.Name = "LblNewHeight";
			this.LblNewHeight.Size = new System.Drawing.Size(63, 13);
			this.LblNewHeight.TabIndex = 0;
			this.LblNewHeight.Text = "New Height";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 69);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Average";
			// 
			// BtnOK
			// 
			this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnOK.Location = new System.Drawing.Point(99, 99);
			this.BtnOK.Name = "BtnOK";
			this.BtnOK.Size = new System.Drawing.Size(75, 23);
			this.BtnOK.TabIndex = 4;
			this.BtnOK.Text = "OK";
			this.BtnOK.UseVisualStyleBackColor = true;
			this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
			// 
			// BtnCancel
			// 
			this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnCancel.Location = new System.Drawing.Point(180, 99);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(75, 23);
			this.BtnCancel.TabIndex = 5;
			this.BtnCancel.Text = "Cancel";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// TxtAverage
			// 
			this.TxtAverage.Location = new System.Drawing.Point(81, 66);
			this.TxtAverage.Name = "TxtAverage";
			this.TxtAverage.ReadOnly = true;
			this.TxtAverage.Size = new System.Drawing.Size(174, 20);
			this.TxtAverage.TabIndex = 6;
			// 
			// TxtNewHeight
			// 
			this.TxtNewHeight.Location = new System.Drawing.Point(81, 35);
			this.TxtNewHeight.Name = "TxtNewHeight";
			this.TxtNewHeight.Size = new System.Drawing.Size(174, 20);
			this.TxtNewHeight.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(164, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Enter the new height to flatten to.";
			// 
			// FrmFlattenHeight
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(267, 134);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.TxtNewHeight);
			this.Controls.Add(this.TxtAverage);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.BtnOK);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.LblNewHeight);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmFlattenHeight";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Flatten Terrain";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label LblNewHeight;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button BtnOK;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.TextBox TxtAverage;
		private System.Windows.Forms.TextBox TxtNewHeight;
		private System.Windows.Forms.Label label1;
	}
}
