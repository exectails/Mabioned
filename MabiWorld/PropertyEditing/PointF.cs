using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace MabiWorld.PropertyEditing
{
	/// <summary>
	/// A type converter for PointF.
	/// </summary>
	public class PointFConverter : ExpandableObjectConverter
	{
		/// <summary>
		/// Returns true if given type can be converted to PointF.
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
		/// Converts string to PointF and returns it.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string stringValue)
			{
				var split = stringValue.Split(new char[] { ';' });
				return new PointF(float.Parse(split[0], culture), float.Parse(split[1], culture));
			}

			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>
		/// Converts PointF to string and returns it.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <param name="destinationType"></param>
		/// <returns></returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
				return ((PointF)value).X + "; " + ((PointF)value).Y;

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
