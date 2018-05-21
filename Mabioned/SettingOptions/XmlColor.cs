using System.Drawing;
using System.Globalization;

namespace Mabioned.SettingOptions
{
	public class XmlColor
	{
		private Color _color;

		public string HexColor
		{
			get { return _color.ToArgb().ToString("X8", CultureInfo.InvariantCulture); }
			set
			{
				if (int.TryParse(value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var n))
					_color = Color.FromArgb(n);
			}
		}

		public XmlColor() { }
		public XmlColor(Color c) { _color = c; }

		public static implicit operator Color(XmlColor x)
		{
			return x._color;
		}

		public static implicit operator XmlColor(Color c)
		{
			return new XmlColor(c);
		}
	}
}
