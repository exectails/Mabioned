using System;
using System.Windows.Forms;

namespace MabiWorld.PropertyEditing.XML
{
	public partial class FrmXmlTextEditor : Form
	{
		private string _originalXmlText;

		public FrmXmlTextEditor(string xmlText)
		{
			InitializeComponent();

			_originalXmlText = xmlText;
			this.TxtXml.Text = xmlText;
			this.TxtXml.SelectionStart = this.TxtXml.Text.Length;
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void BtnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		private void BtnReset_Click(object sender, EventArgs e)
		{
			this.TxtXml.Text = _originalXmlText;
			this.TxtXml.SelectionStart = this.TxtXml.Text.Length;
			this.TxtXml.Focus();
		}
	}
}
