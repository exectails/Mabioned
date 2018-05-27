using System;
using System.Windows.Forms;
using MabiWorld;
using MabiWorld.Data;

namespace Mabioned
{
	/// <summary>
	/// Provides a form to select filtering for props.
	/// </summary>
	public partial class FrmFilterProps : Form
	{
		/// <summary>
		/// Creates new instance.
		/// </summary>
		public FrmFilterProps()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Initializes controls.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmFilterProps_Load(object sender, EventArgs e)
		{
			if (PropDb.HasEntries)
			{
				this.ChkAllPropsThat.Enabled = true;
				this.LblDataFolderInfo.Visible = false;
			}
		}

		/// <summary>
		/// Toggles filtering options.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ChkAllPropsThat_CheckedChanged(object sender, EventArgs e)
		{
			this.GrpFilters.Enabled = this.ChkAllPropsThat.Checked;
		}

		/// <summary>
		/// Closes form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		/// <summary>
		/// Closes form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		/// <summary>
		/// Returns true if the given prop matches the options selected
		/// on the form.
		/// </summary>
		/// <param name="prop">The prop to check.</param>
		/// <param name="notFoundInDb">
		/// Default value for props that couldn't be found in the loaded
		/// data while filters are applied.
		/// </param>
		/// <returns></returns>
		public bool Matches(Prop prop, bool notFoundInDb)
		{
			if (this.ChkAllProps.Checked)
				return true;

			if (!PropDb.TryGetEntry(prop.Id, out var data))
				return notFoundInDb;

			if (this.ChkMatchTag.Checked)
			{
				if (!data.StringID.Matches(this.TxtMatchTag.Text))
					return false;
			}

			if (this.ChkNotMatchTag.Checked)
			{
				if (data.StringID.Matches(this.TxtNotMatchTag.Text))
					return false;
			}

			if (this.ChkTerrain.Checked)
			{
				if (this.ChkTerrainYes.Checked)
				{
					if (!data.IsTerrainBlock)
						return false;
				}
				else if (data.IsTerrainBlock)
					return false;
			}

			return true;
		}

		/// <summary>
		/// Sets Terrain check to Yes by default.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ChkTerrain_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.ChkTerrainYes.Checked && !this.ChkTerrainNo.Checked)
				this.ChkTerrainYes.Checked = true;
		}
	}
}
