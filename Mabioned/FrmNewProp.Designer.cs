namespace Mabioned
{
	partial class FrmNewProp
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
			this.TxtId = new System.Windows.Forms.TextBox();
			this.LblPosition = new System.Windows.Forms.Label();
			this.LblId = new System.Windows.Forms.Label();
			this.GrpSearch = new System.Windows.Forms.GroupBox();
			this.LstProps = new System.Windows.Forms.ListView();
			this.ColId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ColName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.TxtSearch = new System.Windows.Forms.TextBox();
			this.BtnOK = new System.Windows.Forms.Button();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.TxtPosX = new System.Windows.Forms.TextBox();
			this.TxtPosY = new System.Windows.Forms.TextBox();
			this.TxtPosZ = new System.Windows.Forms.TextBox();
			this.GrpProperties.SuspendLayout();
			this.GrpSearch.SuspendLayout();
			this.SuspendLayout();
			// 
			// GrpProperties
			// 
			this.GrpProperties.Controls.Add(this.TxtPosX);
			this.GrpProperties.Controls.Add(this.TxtId);
			this.GrpProperties.Controls.Add(this.TxtPosY);
			this.GrpProperties.Controls.Add(this.LblPosition);
			this.GrpProperties.Controls.Add(this.LblId);
			this.GrpProperties.Controls.Add(this.TxtPosZ);
			this.GrpProperties.Location = new System.Drawing.Point(12, 12);
			this.GrpProperties.Name = "GrpProperties";
			this.GrpProperties.Size = new System.Drawing.Size(370, 78);
			this.GrpProperties.TabIndex = 99;
			this.GrpProperties.TabStop = false;
			this.GrpProperties.Text = "Properties";
			// 
			// TxtId
			// 
			this.TxtId.Location = new System.Drawing.Point(92, 19);
			this.TxtId.Name = "TxtId";
			this.TxtId.Size = new System.Drawing.Size(267, 20);
			this.TxtId.TabIndex = 3;
			// 
			// LblPosition
			// 
			this.LblPosition.AutoSize = true;
			this.LblPosition.Location = new System.Drawing.Point(11, 48);
			this.LblPosition.Name = "LblPosition";
			this.LblPosition.Size = new System.Drawing.Size(44, 13);
			this.LblPosition.TabIndex = 2;
			this.LblPosition.Text = "Position";
			// 
			// LblId
			// 
			this.LblId.AutoSize = true;
			this.LblId.Location = new System.Drawing.Point(11, 22);
			this.LblId.Name = "LblId";
			this.LblId.Size = new System.Drawing.Size(16, 13);
			this.LblId.TabIndex = 0;
			this.LblId.Text = "Id";
			// 
			// GrpSearch
			// 
			this.GrpSearch.Controls.Add(this.LstProps);
			this.GrpSearch.Controls.Add(this.TxtSearch);
			this.GrpSearch.Enabled = false;
			this.GrpSearch.Location = new System.Drawing.Point(12, 96);
			this.GrpSearch.Name = "GrpSearch";
			this.GrpSearch.Size = new System.Drawing.Size(370, 222);
			this.GrpSearch.TabIndex = 98;
			this.GrpSearch.TabStop = false;
			this.GrpSearch.Text = "Search";
			// 
			// LstProps
			// 
			this.LstProps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LstProps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColId,
            this.ColName});
			this.LstProps.FullRowSelect = true;
			this.LstProps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.LstProps.Location = new System.Drawing.Point(11, 45);
			this.LstProps.Name = "LstProps";
			this.LstProps.Size = new System.Drawing.Size(348, 166);
			this.LstProps.TabIndex = 2;
			this.LstProps.UseCompatibleStateImageBehavior = false;
			this.LstProps.View = System.Windows.Forms.View.Details;
			this.LstProps.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LstProps_MouseDoubleClick);
			// 
			// ColId
			// 
			this.ColId.Text = "Id";
			this.ColId.Width = 71;
			// 
			// ColName
			// 
			this.ColName.Text = "ClassName";
			this.ColName.Width = 243;
			// 
			// TxtSearch
			// 
			this.TxtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TxtSearch.Location = new System.Drawing.Point(11, 19);
			this.TxtSearch.Name = "TxtSearch";
			this.TxtSearch.Size = new System.Drawing.Size(348, 20);
			this.TxtSearch.TabIndex = 1;
			this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
			// 
			// BtnOK
			// 
			this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnOK.Location = new System.Drawing.Point(225, 329);
			this.BtnOK.Name = "BtnOK";
			this.BtnOK.Size = new System.Drawing.Size(75, 23);
			this.BtnOK.TabIndex = 6;
			this.BtnOK.Text = "Add";
			this.BtnOK.UseVisualStyleBackColor = true;
			this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
			// 
			// BtnCancel
			// 
			this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnCancel.Location = new System.Drawing.Point(306, 329);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(75, 23);
			this.BtnCancel.TabIndex = 7;
			this.BtnCancel.Text = "Cancel";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// TxtPosX
			// 
			this.TxtPosX.Location = new System.Drawing.Point(92, 45);
			this.TxtPosX.Name = "TxtPosX";
			this.TxtPosX.Size = new System.Drawing.Size(85, 20);
			this.TxtPosX.TabIndex = 7;
			// 
			// TxtPosY
			// 
			this.TxtPosY.Location = new System.Drawing.Point(183, 45);
			this.TxtPosY.Name = "TxtPosY";
			this.TxtPosY.Size = new System.Drawing.Size(85, 20);
			this.TxtPosY.TabIndex = 8;
			// 
			// TxtPosZ
			// 
			this.TxtPosZ.Location = new System.Drawing.Point(274, 45);
			this.TxtPosZ.Name = "TxtPosZ";
			this.TxtPosZ.Size = new System.Drawing.Size(85, 20);
			this.TxtPosZ.TabIndex = 9;
			// 
			// FrmNewProp
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(393, 364);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.BtnOK);
			this.Controls.Add(this.GrpSearch);
			this.Controls.Add(this.GrpProperties);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmNewProp";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New Prop";
			this.Load += new System.EventHandler(this.FrmNewProp_Load);
			this.GrpProperties.ResumeLayout(false);
			this.GrpProperties.PerformLayout();
			this.GrpSearch.ResumeLayout(false);
			this.GrpSearch.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox GrpProperties;
		private System.Windows.Forms.GroupBox GrpSearch;
		private System.Windows.Forms.TextBox TxtSearch;
		private System.Windows.Forms.Button BtnOK;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.ListView LstProps;
		private System.Windows.Forms.ColumnHeader ColId;
		private System.Windows.Forms.ColumnHeader ColName;
		private System.Windows.Forms.TextBox TxtId;
		private System.Windows.Forms.Label LblPosition;
		private System.Windows.Forms.Label LblId;
		private System.Windows.Forms.TextBox TxtPosX;
		private System.Windows.Forms.TextBox TxtPosY;
		private System.Windows.Forms.TextBox TxtPosZ;
	}
}