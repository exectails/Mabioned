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
		private void LnkGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(this.LnkGithub.Text);
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
	}
}
