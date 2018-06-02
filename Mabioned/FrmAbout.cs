using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Mabioned
{
	/// <summary>
	/// Form with information about the application.
	/// </summary>
	public partial class FrmAbout : Form
	{
		/// <summary>
		/// Creates new instance.
		/// </summary>
		public FrmAbout()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Opens Github page.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Lnk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			var label = (sender as LinkLabel);
			Process.Start(label.Text);
		}

		/// <summary>
		/// Closes form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Opens the link in the control's Tag.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Link_Click(object sender, EventArgs e)
		{
			var tag = ((sender as Control)?.Tag as string);
			if (tag != null)
				Process.Start(tag);
		}
	}
}
