namespace Mabioned
{
	partial class FrmFilterProps
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
			this.ChkAllPropsThat = new System.Windows.Forms.RadioButton();
			this.GrpFilters = new System.Windows.Forms.GroupBox();
			this.TxtMatchID = new System.Windows.Forms.TextBox();
			this.ChkMatchID = new System.Windows.Forms.CheckBox();
			this.PnlTerrain = new System.Windows.Forms.Panel();
			this.ChkTerrainNo = new System.Windows.Forms.RadioButton();
			this.ChkTerrainYes = new System.Windows.Forms.RadioButton();
			this.ChkTerrain = new System.Windows.Forms.CheckBox();
			this.TxtNotMatchTag = new System.Windows.Forms.TextBox();
			this.ChkNotMatchTag = new System.Windows.Forms.CheckBox();
			this.TxtMatchTag = new System.Windows.Forms.TextBox();
			this.ChkMatchTag = new System.Windows.Forms.CheckBox();
			this.ChkAllProps = new System.Windows.Forms.RadioButton();
			this.BtnOK = new System.Windows.Forms.Button();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.LblDataFolderInfo = new System.Windows.Forms.Label();
			this.TxtMatchClassName = new System.Windows.Forms.TextBox();
			this.ChkMatchClassName = new System.Windows.Forms.CheckBox();
			this.GrpFilters.SuspendLayout();
			this.PnlTerrain.SuspendLayout();
			this.SuspendLayout();
			// 
			// ChkAllPropsThat
			// 
			this.ChkAllPropsThat.AutoSize = true;
			this.ChkAllPropsThat.Location = new System.Drawing.Point(12, 35);
			this.ChkAllPropsThat.Name = "ChkAllPropsThat";
			this.ChkAllPropsThat.Size = new System.Drawing.Size(106, 17);
			this.ChkAllPropsThat.TabIndex = 2;
			this.ChkAllPropsThat.TabStop = true;
			this.ChkAllPropsThat.Text = "Only Props that...";
			this.ChkAllPropsThat.UseVisualStyleBackColor = true;
			this.ChkAllPropsThat.CheckedChanged += new System.EventHandler(this.ChkAllPropsThat_CheckedChanged);
			// 
			// GrpFilters
			// 
			this.GrpFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GrpFilters.Controls.Add(this.TxtMatchClassName);
			this.GrpFilters.Controls.Add(this.ChkMatchClassName);
			this.GrpFilters.Controls.Add(this.TxtMatchID);
			this.GrpFilters.Controls.Add(this.ChkMatchID);
			this.GrpFilters.Controls.Add(this.PnlTerrain);
			this.GrpFilters.Controls.Add(this.ChkTerrain);
			this.GrpFilters.Controls.Add(this.TxtNotMatchTag);
			this.GrpFilters.Controls.Add(this.ChkNotMatchTag);
			this.GrpFilters.Controls.Add(this.TxtMatchTag);
			this.GrpFilters.Controls.Add(this.ChkMatchTag);
			this.GrpFilters.Enabled = false;
			this.GrpFilters.Location = new System.Drawing.Point(12, 58);
			this.GrpFilters.Name = "GrpFilters";
			this.GrpFilters.Size = new System.Drawing.Size(437, 160);
			this.GrpFilters.TabIndex = 4;
			this.GrpFilters.TabStop = false;
			// 
			// TxtMatchID
			// 
			this.TxtMatchID.Location = new System.Drawing.Point(133, 97);
			this.TxtMatchID.Name = "TxtMatchID";
			this.TxtMatchID.Size = new System.Drawing.Size(289, 20);
			this.TxtMatchID.TabIndex = 18;
			// 
			// ChkMatchID
			// 
			this.ChkMatchID.AutoSize = true;
			this.ChkMatchID.Location = new System.Drawing.Point(16, 99);
			this.ChkMatchID.Name = "ChkMatchID";
			this.ChkMatchID.Size = new System.Drawing.Size(69, 17);
			this.ChkMatchID.TabIndex = 17;
			this.ChkMatchID.Text = "match ID";
			this.ChkMatchID.UseVisualStyleBackColor = true;
			// 
			// PnlTerrain
			// 
			this.PnlTerrain.Controls.Add(this.ChkTerrainNo);
			this.PnlTerrain.Controls.Add(this.ChkTerrainYes);
			this.PnlTerrain.Location = new System.Drawing.Point(133, 71);
			this.PnlTerrain.Name = "PnlTerrain";
			this.PnlTerrain.Size = new System.Drawing.Size(200, 18);
			this.PnlTerrain.TabIndex = 16;
			// 
			// ChkTerrainNo
			// 
			this.ChkTerrainNo.AutoSize = true;
			this.ChkTerrainNo.Location = new System.Drawing.Point(49, 2);
			this.ChkTerrainNo.Name = "ChkTerrainNo";
			this.ChkTerrainNo.Size = new System.Drawing.Size(39, 17);
			this.ChkTerrainNo.TabIndex = 11;
			this.ChkTerrainNo.TabStop = true;
			this.ChkTerrainNo.Text = "No";
			this.ChkTerrainNo.UseVisualStyleBackColor = true;
			this.ChkTerrainNo.CheckedChanged += new System.EventHandler(this.ChkTerrainYesNo_CheckedChanged);
			// 
			// ChkTerrainYes
			// 
			this.ChkTerrainYes.AutoSize = true;
			this.ChkTerrainYes.Location = new System.Drawing.Point(0, 1);
			this.ChkTerrainYes.Name = "ChkTerrainYes";
			this.ChkTerrainYes.Size = new System.Drawing.Size(43, 17);
			this.ChkTerrainYes.TabIndex = 10;
			this.ChkTerrainYes.TabStop = true;
			this.ChkTerrainYes.Text = "Yes";
			this.ChkTerrainYes.UseVisualStyleBackColor = true;
			this.ChkTerrainYes.CheckedChanged += new System.EventHandler(this.ChkTerrainYesNo_CheckedChanged);
			// 
			// ChkTerrain
			// 
			this.ChkTerrain.AutoSize = true;
			this.ChkTerrain.Location = new System.Drawing.Point(16, 73);
			this.ChkTerrain.Name = "ChkTerrain";
			this.ChkTerrain.Size = new System.Drawing.Size(73, 17);
			this.ChkTerrain.TabIndex = 7;
			this.ChkTerrain.Text = "are terrain";
			this.ChkTerrain.UseVisualStyleBackColor = true;
			this.ChkTerrain.CheckedChanged += new System.EventHandler(this.ChkTerrain_CheckedChanged);
			// 
			// TxtNotMatchTag
			// 
			this.TxtNotMatchTag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TxtNotMatchTag.Location = new System.Drawing.Point(133, 45);
			this.TxtNotMatchTag.Name = "TxtNotMatchTag";
			this.TxtNotMatchTag.Size = new System.Drawing.Size(289, 20);
			this.TxtNotMatchTag.TabIndex = 6;
			// 
			// ChkNotMatchTag
			// 
			this.ChkNotMatchTag.AutoSize = true;
			this.ChkNotMatchTag.Location = new System.Drawing.Point(16, 47);
			this.ChkNotMatchTag.Name = "ChkNotMatchTag";
			this.ChkNotMatchTag.Size = new System.Drawing.Size(103, 17);
			this.ChkNotMatchTag.TabIndex = 5;
			this.ChkNotMatchTag.Text = "don\'t match Tag";
			this.ChkNotMatchTag.UseVisualStyleBackColor = true;
			// 
			// TxtMatchTag
			// 
			this.TxtMatchTag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TxtMatchTag.Location = new System.Drawing.Point(133, 19);
			this.TxtMatchTag.Name = "TxtMatchTag";
			this.TxtMatchTag.Size = new System.Drawing.Size(289, 20);
			this.TxtMatchTag.TabIndex = 4;
			// 
			// ChkMatchTag
			// 
			this.ChkMatchTag.AutoSize = true;
			this.ChkMatchTag.Location = new System.Drawing.Point(16, 21);
			this.ChkMatchTag.Name = "ChkMatchTag";
			this.ChkMatchTag.Size = new System.Drawing.Size(77, 17);
			this.ChkMatchTag.TabIndex = 3;
			this.ChkMatchTag.Text = "match Tag";
			this.ChkMatchTag.UseVisualStyleBackColor = true;
			// 
			// ChkAllProps
			// 
			this.ChkAllProps.AutoSize = true;
			this.ChkAllProps.Location = new System.Drawing.Point(12, 12);
			this.ChkAllProps.Name = "ChkAllProps";
			this.ChkAllProps.Size = new System.Drawing.Size(66, 17);
			this.ChkAllProps.TabIndex = 1;
			this.ChkAllProps.TabStop = true;
			this.ChkAllProps.Text = "All Props";
			this.ChkAllProps.UseVisualStyleBackColor = true;
			// 
			// BtnOK
			// 
			this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnOK.Location = new System.Drawing.Point(293, 224);
			this.BtnOK.Name = "BtnOK";
			this.BtnOK.Size = new System.Drawing.Size(75, 23);
			this.BtnOK.TabIndex = 10;
			this.BtnOK.Text = "OK";
			this.BtnOK.UseVisualStyleBackColor = true;
			this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
			// 
			// BtnCancel
			// 
			this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnCancel.Location = new System.Drawing.Point(374, 224);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(75, 23);
			this.BtnCancel.TabIndex = 11;
			this.BtnCancel.Text = "Cancel";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// LblDataFolderInfo
			// 
			this.LblDataFolderInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.LblDataFolderInfo.AutoSize = true;
			this.LblDataFolderInfo.ForeColor = System.Drawing.Color.Gray;
			this.LblDataFolderInfo.Location = new System.Drawing.Point(9, 229);
			this.LblDataFolderInfo.Name = "LblDataFolderInfo";
			this.LblDataFolderInfo.Size = new System.Drawing.Size(221, 13);
			this.LblDataFolderInfo.TabIndex = 12;
			this.LblDataFolderInfo.Text = "Enter data folder in settings to enable filtering.";
			// 
			// TxtMatchClassName
			// 
			this.TxtMatchClassName.Location = new System.Drawing.Point(133, 123);
			this.TxtMatchClassName.Name = "TxtMatchClassName";
			this.TxtMatchClassName.Size = new System.Drawing.Size(289, 20);
			this.TxtMatchClassName.TabIndex = 20;
			// 
			// ChkMatchClassName
			// 
			this.ChkMatchClassName.AutoSize = true;
			this.ChkMatchClassName.Location = new System.Drawing.Point(16, 125);
			this.ChkMatchClassName.Name = "ChkMatchClassName";
			this.ChkMatchClassName.Size = new System.Drawing.Size(111, 17);
			this.ChkMatchClassName.TabIndex = 19;
			this.ChkMatchClassName.Text = "match ClassName";
			this.ChkMatchClassName.UseVisualStyleBackColor = true;
			// 
			// FrmFilterProps
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(461, 259);
			this.Controls.Add(this.LblDataFolderInfo);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.BtnOK);
			this.Controls.Add(this.ChkAllProps);
			this.Controls.Add(this.GrpFilters);
			this.Controls.Add(this.ChkAllPropsThat);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmFilterProps";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select...";
			this.Load += new System.EventHandler(this.FrmFilterProps_Load);
			this.GrpFilters.ResumeLayout(false);
			this.GrpFilters.PerformLayout();
			this.PnlTerrain.ResumeLayout(false);
			this.PnlTerrain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.RadioButton ChkAllPropsThat;
		private System.Windows.Forms.GroupBox GrpFilters;
		private System.Windows.Forms.CheckBox ChkTerrain;
		private System.Windows.Forms.TextBox TxtNotMatchTag;
		private System.Windows.Forms.CheckBox ChkNotMatchTag;
		private System.Windows.Forms.TextBox TxtMatchTag;
		private System.Windows.Forms.CheckBox ChkMatchTag;
		private System.Windows.Forms.RadioButton ChkAllProps;
		private System.Windows.Forms.Button BtnOK;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.Label LblDataFolderInfo;
		private System.Windows.Forms.Panel PnlTerrain;
		private System.Windows.Forms.RadioButton ChkTerrainNo;
		private System.Windows.Forms.RadioButton ChkTerrainYes;
		private System.Windows.Forms.CheckBox ChkMatchID;
		private System.Windows.Forms.TextBox TxtMatchID;
		private System.Windows.Forms.TextBox TxtMatchClassName;
		private System.Windows.Forms.CheckBox ChkMatchClassName;
	}
}
