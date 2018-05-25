using System;
using System.IO;
using System.Windows.Forms;
using OpenPainter.ColorPicker;

namespace Mabioned
{
	public partial class FrmSettings : Form
	{
		private MainOptions _defaultOptions = new MainOptions();

		public FrmSettings()
		{
			InitializeComponent();
		}

		private void FrmSettings_Load(object sender, EventArgs e)
		{
			this.LblBackgroundColor.BackColor = Settings.Default.BackgroundColor;
			this.LblPropsColor.BackColor = Settings.Default.PropsColor;
			this.LblEventsColor.BackColor = Settings.Default.EventsColor;
			this.LblAreasColor.BackColor = Settings.Default.AreasColor;
			this.LblSelectionColor.BackColor = Settings.Default.SelectionColor;

			this.TxtDataFolder.Text = Settings.Default.DataFolder;
		}

		private void BtnOK_Click(object sender, EventArgs e)
		{
			Settings.Default.BackgroundColor = this.LblBackgroundColor.BackColor;
			Settings.Default.PropsColor = this.LblPropsColor.BackColor;
			Settings.Default.EventsColor = this.LblEventsColor.BackColor;
			Settings.Default.AreasColor = this.LblAreasColor.BackColor;
			Settings.Default.SelectionColor = this.LblSelectionColor.BackColor;

			if (this.IsDataFolder(this.TxtDataFolder.Text))
				Settings.Default.DataFolder = this.TxtDataFolder.Text;

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void LblColor_MouseClick(object sender, MouseEventArgs e)
		{
			var control = (sender as Control);
			if (e.Button == MouseButtons.Left)
			{
				var form = new PsColorPicker(control.BackColor);
				form.ShowDialog();

				if (form.DialogResult == DialogResult.OK)
					control.BackColor = form.Color;
			}
			else if (e.Button == MouseButtons.Right)
			{
				this.ResetOption(control.Tag as string);
			}
		}

		private void ResetOption(string optionName)
		{
			switch (optionName)
			{
				case "BackgroundColor":
					Settings.Default.BackgroundColor = _defaultOptions.BackgroundColor;
					this.LblBackgroundColor.BackColor = Settings.Default.BackgroundColor;
					break;

				case "PropsColor":
					Settings.Default.PropsColor = _defaultOptions.PropsColor;
					this.LblPropsColor.BackColor = Settings.Default.PropsColor;
					break;

				case "EventsColor":
					Settings.Default.EventsColor = _defaultOptions.EventsColor;
					this.LblEventsColor.BackColor = Settings.Default.EventsColor;
					break;

				case "AreasColor":
					Settings.Default.AreasColor = _defaultOptions.AreasColor;
					this.LblAreasColor.BackColor = Settings.Default.AreasColor;
					break;

				case "SelectionColor":
					Settings.Default.SelectionColor = _defaultOptions.SelectionColor;
					this.LblSelectionColor.BackColor = Settings.Default.SelectionColor;
					break;
			}
		}

		private void BtnSelectDataFolder_Click(object sender, EventArgs e)
		{
			var prevPath = this.TxtDataFolder.Text;
			if (Directory.Exists(prevPath))
				this.DlgFolder.SelectedPath = prevPath;

			if (this.DlgFolder.ShowDialog() != DialogResult.OK)
				return;

			var selectedPath = this.DlgFolder.SelectedPath;
			if (!this.IsDataFolder(selectedPath))
			{
				MessageBox.Show("No prop data found in the selected folder, make sure to select a complete data folder that contains the db and world folders.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			this.TxtDataFolder.Text = selectedPath;
		}

		private bool IsDataFolder(string path)
		{
			var propDbPath = Path.Combine(path, "db", "propdb.xml");
			var propPalettePath = Path.Combine(path, "world", "proppalette.plt");

			return (File.Exists(propDbPath) && File.Exists(propPalettePath));
		}
	}
}
