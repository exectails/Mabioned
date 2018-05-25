using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml;
using System.Xml.Linq;
using MabiWorld.PropertyEditing.XML;

namespace MabiWorld.PropertyEditing
{
	/// <summary>
	/// Provides a form with a text editor, in which the formatted
	/// XML can be edited.
	/// </summary>
	public class XmlTextEditor : UITypeEditor
	{
		private const int Padding = 10;

		private IWindowsFormsEditorService _editorService;

		/// <summary>
		/// Returns the edit style for this editor.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		/// <summary>
		/// Provides a way to edit a property and returns the new value.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="provider"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			_editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

			// Get XML code, with fallback to just the string if the XML
			// is invalid.
			var stringValue = (value as string);

			string xml;
			try
			{
				var doc = XDocument.Parse(stringValue);

				var settings = new XmlWriterSettings();
				settings.IndentChars = "\t";
				settings.Indent = true;
				settings.OmitXmlDeclaration = true;

				var sb = new StringBuilder();
				using (var sw = new StringWriter(sb))
				using (var xmlw = XmlWriter.Create(sw, settings))
					doc.Save(xmlw);

				xml = sb.ToString() + Environment.NewLine;
			}
			catch { xml = stringValue; }

			// Create form
			var form = new FrmXmlTextEditor(xml);

			// Show form and return either the original value or the
			// modified one, depending on whether OK was clicked.
			if (_editorService.ShowDialog(form) != DialogResult.OK)
				return value;

			var changedText = form.TxtXml.Text;
			xml = XDocument.Parse(changedText).ToString(SaveOptions.DisableFormatting);

			return xml;
		}
	}
}
