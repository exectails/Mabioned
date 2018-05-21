using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace MabiWorld.PropertyEditing
{
	/// <summary>
	/// A type converter for enums that displays undefined values as
	/// their integer value.
	/// </summary>
	public class SafeEnumConverter : TypeConverter
	{
		/// <summary>
		/// Returns true if the given type can be converted to the enum.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="sourceType"></param>
		/// <returns></returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;

			return base.CanConvertFrom(context, sourceType);
		}

		/// <summary>
		/// Converts string to enum and returns it.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string stringValue)
			{
				return Enum.Parse(context.PropertyDescriptor.PropertyType, stringValue);
			}

			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>
		/// Converts enum to string and returns it.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <param name="destinationType"></param>
		/// <returns></returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
				return value.ToString();

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}

	/// <summary>
	/// Provides an editor for enums, with a drop down list showing all
	/// defined values of the enum, while also accepting undefined values.
	/// </summary>
	public class SafeEnumEditor : UITypeEditor
	{
		private IWindowsFormsEditorService _editorService;

		/// <summary>
		/// Returns the edit style for this editor.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		/// <summary>
		/// Displays drop down list with defined enum values and returns the
		/// selected one.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="provider"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			_editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

			var enumType = context.PropertyDescriptor.PropertyType;
			var map = new Dictionary<int, int>();

			// Create value list
			var listBox = new ListBox();
			listBox.BorderStyle = BorderStyle.None;
			listBox.SelectionMode = SelectionMode.One;
			listBox.SelectedValueChanged += (_, __) => _editorService.CloseDropDown();

			// Add values to list
			var names = Enum.GetNames(enumType);
			for (var i = 0; i < names.Length; ++i)
			{
				var name = names[i];
				var val = (int)Enum.Parse(enumType, name);

				var index = listBox.Items.Add(name + " (" + val + ")");
				if (name == value.ToString())
					listBox.SelectedIndex = index;

				map[index] = val;
			}

			// Let user choose a new value
			_editorService.DropDownControl(listBox);

			// Return either the previous value if nothing was selected,
			// or the new value.
			if (listBox.SelectedItem == null)
				return value;

			return Enum.Parse(context.PropertyDescriptor.PropertyType, map[listBox.SelectedIndex].ToString());
		}
	}
}
