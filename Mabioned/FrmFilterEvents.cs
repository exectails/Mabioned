using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MabiWorld;

namespace Mabioned
{
	/// <summary>
	/// Provides a form to select even filtering options.
	/// </summary>
	public partial class FrmFilterEvents : Form
	{
		private EventType _eventType, _notEventType;

		/// <summary>
		/// Creates new instance.
		/// </summary>
		public FrmFilterEvents()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Initializes combo boxes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmFilterEvents_Load(object sender, EventArgs e)
		{
			foreach (var enumType in Enum.GetValues(typeof(EventType)).Cast<EventType>())
			{
				//var text = string.Format("{0} ({1})", enumType, (int)enumType);

				this.CboType.Items.Add(enumType);
				this.CboNotType.Items.Add(enumType);
			}
		}

		/// <summary>
		/// Toggles option availability.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ChkOnlyEvents_CheckedChanged(object sender, EventArgs e)
		{
			this.GrpOptions.Enabled = this.ChkOnlyEvents.Checked;
		}

		/// <summary>
		///  Closes form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnOK_Click(object sender, EventArgs e)
		{
			if (this.ChkType.Checked)
			{
				if (!Enum.TryParse<EventType>(this.CboType.Text, out var eventType))
				{
					MessageBox.Show("The event type you entered is not valid, please enter a type name or id.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.CboType.Select();
					return;
				}

				_eventType = eventType;
			}

			if (this.ChkNotType.Checked)
			{
				if (!Enum.TryParse<EventType>(this.CboNotType.Text, out var eventType))
				{
					MessageBox.Show("The event type you entered is not valid, please enter a type name or id.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.CboNotType.Select();
					return;
				}

				_notEventType = eventType;
			}

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
		/// Returns true if the event matches the options selected in
		/// this form.
		/// </summary>
		/// <param name="evnt"></param>
		/// <returns></returns>
		public bool Matches(Event evnt)
		{
			if (this.ChkAllEvents.Checked)
				return true;

			if (this.ChkType.Checked)
			{
				if (evnt.Type != _eventType)
					return false;
			}

			if (this.ChkNotType.Checked)
			{
				if (evnt.Type == _notEventType)
					return false;
			}

			return true;
		}
	}
}
