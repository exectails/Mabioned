using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using MabiWorld;
using MabiWorld.Data;

namespace Mabioned
{
	/// <summary>
	/// Provides a form to enter information for the creation of a new prop.
	/// </summary>
	public partial class FrmNewProp : Form
	{
		/// <summary>
		/// A basic prop created based on the entered data.
		/// </summary>
		public Prop Prop { get; private set; }

		/// <summary>
		/// Creates new instance.
		/// </summary>
		public FrmNewProp(Vector3F pos)
		{
			InitializeComponent();

			this.TxtPosX.Text = pos.X.ToString(CultureInfo.InvariantCulture);
			this.TxtPosY.Text = pos.Y.ToString(CultureInfo.InvariantCulture);
			this.TxtPosZ.Text = pos.Z.ToString(CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Initializes controls.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmNewProp_Load(object sender, EventArgs e)
		{
			this.Prop = new Prop();

			if (PropDb.HasEntries)
			{
				this.GrpSearch.Enabled = true;
				this.LblDataFolderInfo.Visible = false;
				this.TxtSearch.Select();
			}
		}

		/// <summary>
		/// Shows prop search results.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtSearch_TextChanged(object sender, EventArgs e)
		{
			var searchString = this.TxtSearch.Text;

			if (searchString.Length < 3)
			{
				if (this.LstProps.Items.Count != 0)
					this.LstProps.Items.Clear();
				return;
			}

			this.LstProps.BeginUpdate();
			this.LstProps.Items.Clear();

			foreach (var entry in PropDb.FindEntriesByName(searchString))
			{
				var item = new ListViewItem(entry.ClassID.ToString());
				item.SubItems.Add(entry.ClassName);
				item.Tag = entry;

				this.LstProps.Items.Add(item);
			}

			this.LstProps.EndUpdate();
		}

		/// <summary>
		/// Sets properties based on selected prop data.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LstProps_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			var selectedItems = this.LstProps.SelectedItems;

			if (selectedItems.Count == 0 || !(selectedItems[0].Tag is PropDbEntry entry))
				return;

			this.TxtId.Text = entry.ClassID.ToString();
		}

		/// <summary>
		/// Checks input and closes form if everything is in order.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnOK_Click(object sender, EventArgs e)
		{
			// Check properties
			if (!int.TryParse(this.TxtId.Text, out var id))
			{
				MessageBox.Show("The id you entered contains invalid characters.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(this.TxtPosX.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var x) || !float.TryParse(this.TxtPosY.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var y) || !float.TryParse(this.TxtPosZ.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var z))
			{
				MessageBox.Show("The position you entered contains invalid characters.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Check prop existence
			if (PropDb.HasEntries && !PropDb.TryGetEntry(id, out var data))
			{
				var result = MessageBox.Show("No prop with this id was found in the prop database, do you still want to add it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (result != DialogResult.Yes)
					return;
			}

			this.Prop = this.GenerateBaseProp(id, x, y, z);

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		/// <summary>
		/// Creates prop based on data.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		private Prop GenerateBaseProp(int id, float x, float y, float z)
		{
			var prop = new Prop();

			prop.Id = id;
			prop.Position = new Vector3F(x, y, z);
			prop.BottomLeft = prop.Position;
			prop.TopRight = prop.Position;

			prop.LoadData();

			return prop;
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
	}
}
