using OpenPainter.ColorPicker;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;

namespace MabiWorld.PropertyEditing
{
	public class MyColorConverter : TypeConverter
	{
		/// <summary>
		/// Returning false is necessary here for the property grid to
		/// recognize the custom color editor.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>
		/// Returns true if given type can be converted to Color.
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
		/// Converts string to Color and returns it.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string stringValue)
			{
				if (stringValue.StartsWith("#"))
					stringValue = stringValue.Substring(1);
				else if (stringValue.StartsWith("0x"))
					stringValue = stringValue.Substring(2);

				if (stringValue.Length == 3)
				{
					var r = int.Parse(stringValue.Substring(0, 1), NumberStyles.HexNumber);
					var g = int.Parse(stringValue.Substring(1, 1), NumberStyles.HexNumber);
					var b = int.Parse(stringValue.Substring(2, 1), NumberStyles.HexNumber);

					r = (r << 4) | r;
					g = (g << 4) | g;
					b = (b << 4) | b;

					return Color.FromArgb(r, g, b);
				}
				else if (stringValue.Length == 6)
				{
					var r = int.Parse(stringValue.Substring(0, 2), NumberStyles.HexNumber);
					var g = int.Parse(stringValue.Substring(2, 2), NumberStyles.HexNumber);
					var b = int.Parse(stringValue.Substring(4, 2), NumberStyles.HexNumber);

					return Color.FromArgb(r, g, b);
				}
				else if (stringValue.Length == 8)
				{
					var a = int.Parse(stringValue.Substring(0, 2), NumberStyles.HexNumber);
					var r = int.Parse(stringValue.Substring(2, 2), NumberStyles.HexNumber);
					var g = int.Parse(stringValue.Substring(4, 2), NumberStyles.HexNumber);
					var b = int.Parse(stringValue.Substring(6, 2), NumberStyles.HexNumber);

					return Color.FromArgb(a, r, g, b);
				}
			}

			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>
		/// Converts Color to string and returns it.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <param name="destinationType"></param>
		/// <returns></returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				var color = (Color)value;
				return color.ToArgb().ToString("X8");
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}

	/// <summary>
	/// Provides a form with a text editor, in which the formatted
	/// XML can be edited.
	/// </summary>
	public class ColorEditor : UITypeEditor
	{
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
			var color = ((Color)value);

			var form = new PsColorPicker(color);
			if (form.ShowDialog() != DialogResult.OK)
				return color;

			var newColor = form.Color;
			var result = Color.FromArgb(color.A, newColor.R, newColor.G, newColor.B);

			return result;
		}

		/// <summary>
		/// Returns true if editor supports painting something in a small
		/// area left of the value.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>
		/// Paints value to a small area left of the value.
		/// </summary>
		/// <param name="e"></param>
		public override void PaintValue(PaintValueEventArgs e)
		{
			var brush = new SolidBrush((Color)e.Value);
			e.Graphics.FillRectangle(brush, e.Bounds);
		}
	}
}
