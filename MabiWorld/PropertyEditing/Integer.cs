using System;
using System.ComponentModel;
using System.Globalization;

namespace MabiWorld.PropertyEditing
{
	/// <summary>
	/// A type converter for ulongs, displaying them
	/// in hex format.
	/// </summary>
	public class UInt64HexConverter : TypeConverter
	{
		/// <summary>
		/// Returns true if the given type can be converted to a ulong.
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
		/// Converts string in (hexa)decimal format to ulong and returns it.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string stringValue)
			{
				if (stringValue.StartsWith("0x"))
					return ulong.Parse(stringValue.Substring(2), NumberStyles.HexNumber);
				else
					return ulong.Parse(stringValue);
			}

			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>
		/// Converts ulong to string in hexadecimal format.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <param name="destinationType"></param>
		/// <returns></returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
				return "0x" + ((ulong)value).ToString("X16");

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
