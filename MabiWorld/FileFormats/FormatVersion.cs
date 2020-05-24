using System.IO;

namespace MabiWorld.FileFormats
{
	/// <summary>
	/// Represents 2 point version of a file or a portion of it.
	/// </summary>
	public class FormatVersion
	{
		/// <summary>
		/// Gets or sets the major version (e.g. 2 == 2.x)
		/// </summary>
		public int Major { get; set; }

		/// <summary>
		/// Gets or sets the minor version (e.g. 1 == x.1).
		/// </summary>
		public int Minor { get; set; }

		/// <summary>
		/// Creates a new instance.
		/// </summary>
		/// <param name="major"></param>
		/// <param name="minor"></param>
		public FormatVersion(int major, int minor)
		{
			this.Major = major;
			this.Minor = minor;
		}

		/// <summary>
		/// Reads version from binary reader and returns it.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static FormatVersion ReadFrom(BinaryReader br)
		{
			var result = new FormatVersion(0, 0);

			result.Major = br.ReadByte();
			result.Minor = br.ReadByte();

			return result;
		}

		/// <summary>
		/// Returns a string representation of the version (e.g. 2.1).
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.Major + "." + this.Minor;
		}

		/// <summary>
		/// Returns true if the two versions represent the same version.
		/// </summary>
		/// <param name="val1"></param>
		/// <param name="val2"></param>
		/// <returns></returns>
		public static bool operator ==(FormatVersion val1, FormatVersion val2)
		{
			return val1.Major == val2.Major && val1.Minor == val2.Minor;
		}

		/// <summary>
		/// Returns true if the two versions don't represent the same
		/// version.
		/// </summary>
		/// <param name="val1"></param>
		/// <param name="val2"></param>
		/// <returns></returns>
		public static bool operator !=(FormatVersion val1, FormatVersion val2)
		{
			return !(val1 == val2);
		}

		/// <summary>
		/// Returns true if version 1 is greater than version 2.
		/// </summary>
		/// <param name="val1"></param>
		/// <param name="val2"></param>
		/// <returns></returns>
		public static bool operator >(FormatVersion val1, FormatVersion val2)
		{
			if (val1.Major == val2.Major)
				return val1.Minor > val2.Minor;

			return val1.Major > val2.Major;
		}

		/// <summary>
		/// Returns true if version 1 is lower than version 2.
		/// </summary>
		/// <param name="val1"></param>
		/// <param name="val2"></param>
		/// <returns></returns>
		public static bool operator <(FormatVersion val1, FormatVersion val2)
		{
			if (val1.Major == val2.Major)
				return val1.Minor < val2.Minor;

			return val1.Major < val2.Major;
		}

		/// <summary>
		/// Returns true if version 1 is equal to or greater than version 2.
		/// </summary>
		/// <param name="val1"></param>
		/// <param name="val2"></param>
		/// <returns></returns>
		public static bool operator >=(FormatVersion val1, FormatVersion val2)
		{
			return val1 == val2 || val1 > val2;
		}

		/// <summary>
		/// Returns true if version 1 is equal to or lower than version 2.
		/// </summary>
		/// <param name="val1"></param>
		/// <param name="val2"></param>
		/// <returns></returns>
		public static bool operator <=(FormatVersion val1, FormatVersion val2)
		{
			return val1 == val2 || val1 < val2;
		}

		/// <summary>
		/// Returns true if the given object is a FormatVersion that
		/// represents the same version as this instance.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			var version = obj as FormatVersion;
			return version != null && this.Major == version.Major && this.Minor == version.Minor;
		}

		/// <summary>
		/// Returns a hash code for this instance's version.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			var hashCode = 317314336;
			hashCode = hashCode * -1521134295 + this.Major.GetHashCode();
			hashCode = hashCode * -1521134295 + this.Minor.GetHashCode();
			return hashCode;
		}
	}
}
