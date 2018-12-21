using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace Mabioned
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			CultureInfo.DefaultThreadCurrentCulture =
			CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("en-US");
			Thread.CurrentThread.CurrentCulture = CultureInfo.DefaultThreadCurrentCulture;
			Thread.CurrentThread.CurrentUICulture = CultureInfo.DefaultThreadCurrentUICulture;

			Settings.Default.Load();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//Application.Run(new FrmMain());

			var controller = new SingleInstanceController();
			controller.Run(Environment.GetCommandLineArgs());
		}
	}

	/// <summary>
	/// Provides an easy way to start the application only once.
	/// </summary>
	public class SingleInstanceController : WindowsFormsApplicationBase
	{
		/// <summary>
		/// Creates new instance.
		/// </summary>
		public SingleInstanceController()
		{
			this.IsSingleInstance = Settings.Default.SingleInstance;
			this.StartupNextInstance += this.OnStartupNextInstance;
		}

		/// <summary>
		/// Called when application is started for the first time.
		/// </summary>
		protected override void OnCreateMainForm()
		{
			this.MainForm = new FrmMain();
		}

		/// <summary>
		/// Called when application is started again.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnStartupNextInstance(object sender, StartupNextInstanceEventArgs e)
		{
			// Get the current form and open the new file in it if one
			// was passed as argument
			var form = this.MainForm as FrmMain;
			if (e.CommandLine.Count > 1)
				form.OpenFile(e.CommandLine[1]);

			// Minimize and show form again to get it to the foreground.
			//var state = form.WindowState;
			//form.WindowState = FormWindowState.Minimized;
			//form.Show();
			//form.WindowState = state;
		}
	}
}
