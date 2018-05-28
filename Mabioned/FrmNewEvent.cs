using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using MabiWorld;

namespace Mabioned
{
	/// <summary>
	/// Provides form for entering information about new event.
	/// </summary>
	public partial class FrmNewEvent : Form
	{
		/// <summary>
		/// Returns the event created by this form.
		/// </summary>
		public Event Event { get; private set; }

		/// <summary>
		/// Creates new instance, initialized with the given position.
		/// </summary>
		/// <param name="pos"></param>
		public FrmNewEvent(Vector3F pos)
		{
			InitializeComponent();

			this.TxtPosX.Text = pos.X.ToString(CultureInfo.InvariantCulture);
			this.TxtPosY.Text = pos.Y.ToString(CultureInfo.InvariantCulture);
			this.TxtPosZ.Text = pos.Z.ToString(CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Generates event and closes form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnOK_Click(object sender, System.EventArgs e)
		{
			if (!float.TryParse(this.TxtPosX.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var x) || !float.TryParse(this.TxtPosY.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var y) || !float.TryParse(this.TxtPosZ.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var z))
			{
				MessageBox.Show("The position you entered is invalid.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!float.TryParse(this.TxtWidth.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var width) || !float.TryParse(this.TxtHeight.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var height))
			{
				MessageBox.Show("The size you entered is invalid.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			this.Event = this.GenerateEvent(x, y, z, width, height);

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		/// <summary>
		/// Generates event for this instance based on entered info.
		/// </summary>
		private Event GenerateEvent(float x, float y, float z, float width, float height)
		{
			var halfLen = new SizeF((width / 2), (height / 2));

			var shape = new Shape();
			shape.DirX1 = -1;
			shape.DirX2 = 0;
			shape.DirY1 = 0;
			shape.DirY2 = 1;
			shape.LenX = width;
			shape.LenY = height;
			shape.Position = new PointF(x, y);
			shape.BottomLeft = shape.Position - halfLen;
			shape.TopRight = shape.Position + halfLen;

			var evnt = new Event();
			evnt.Type = EventType.General;
			evnt.Position = new Vector3F(x, y, z);
			evnt.Shapes.Add(shape);

			return evnt;
		}

		/// <summary>
		/// Closes form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
