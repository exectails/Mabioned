using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace MabiWorld
{
	/// <summary>
	/// Represents a square, a part of an .area file and its area planes.
	/// </summary>
	public class Square
	{
		public List<byte[]> Values { get; set; } = new List<byte[]>();

		/// <summary>
		/// Reads square from reader and returns it.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Square ReadFrom(BinaryReader br)
		{
			var square = new Square();

			for (var i = 0; i < 16; ++i)
				square.Values.Add(br.ReadBytes(2));

			return square;
		}

		/// <summary>
		/// Writes square to given writer.
		/// </summary>
		/// <param name="bw"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteTo(BinaryWriter bw)
		{
			for (var i = 0; i < 16; ++i)
				bw.Write(this.Values[i]);
		}
	}
}
