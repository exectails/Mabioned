using System;
using System.Globalization;
using System.Windows.Forms;

namespace Mabioned
{
	/// <summary>
	/// Provides a form to enter a height.
	/// </summary>
	public partial class FrmFlattenHeight : Form
	{
		public float Value { get; private set; }

		/// <summary>
		/// Creates new instance.
		/// </summary>
		/// <param name="height"></param>
		/// <param name="averageHeight"></param>
		public FrmFlattenHeight(float height, float averageHeight)
		{
			InitializeComponent();

			this.TxtNewHeight.Text = height.ToString(CultureInfo.InvariantCulture);
			this.TxtAverage.Text = averageHeight.ToString(CultureInfo.InvariantCulture);

			this.Value = height;
		}

		/// <summary>
		/// Closes form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnOK_Click(object sender, EventArgs e)
		{
			if (!float.TryParse(this.TxtNewHeight.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
			{
				MessageBox.Show("Invalid format.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.TxtNewHeight.Select();
				return;
			}

			this.Value = value;

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
	}
}
