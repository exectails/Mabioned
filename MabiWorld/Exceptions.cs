using System;
using System.Runtime.CompilerServices;

namespace MabiWorld
{
	/// <summary>
	/// Used when an enum has a value that was not defined.
	/// </summary>
	public class EnumValueNotDefinedException : Exception
	{
		/// <summary>
		/// Creates new instance.
		/// </summary>
		/// <param name="enumType"></param>
		/// <param name="value"></param>
		public EnumValueNotDefinedException(Type enumType, object value)
			: base("Enum value not defined: " + enumType.Name + "." + value)
		{
		}

		/// <summary>
		/// Throws this exception if value is not defined in given enum
		/// type.
		/// </summary>
		/// <param name="enumType"></param>
		/// <param name="value"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AssertDefined(Type enumType, object value)
		{
			if (!Enum.IsDefined(enumType, value))
				throw new EnumValueNotDefinedException(enumType, value);
		}
	}

	/// <summary>
	/// Used when a file has a version that is not supported by the library.
	/// </summary>
	public class UnsupportedVersionException : Exception
	{
		/// <summary>
		/// Create new instance.
		/// </summary>
		public UnsupportedVersionException()
		{
		}
	}
}
