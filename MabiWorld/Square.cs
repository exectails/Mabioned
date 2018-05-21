using System.IO;
using System.Runtime.CompilerServices;

namespace MabiWorld
{
	/// <summary>
	/// Represents a square, a part of an .area file and its area planes.
	/// </summary>
	public class Square
	{
		public byte[] Square1_0 { get; set; }
		public byte[] Square2_0 { get; set; }
		public byte[] Square3_0 { get; set; }
		public byte[] Square4_0 { get; set; }
		public byte[] Square1_1 { get; set; }
		public byte[] Square2_1 { get; set; }
		public byte[] Square3_1 { get; set; }
		public byte[] Square4_1 { get; set; }

		/// <summary>
		/// Reads square from reader and returns it.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Square ReadFrom(BinaryReader br)
		{
			var square = new Square();

			square.Square1_0 = br.ReadBytes(4);
			square.Square2_0 = br.ReadBytes(4);
			square.Square3_0 = br.ReadBytes(4);
			square.Square4_0 = br.ReadBytes(4);
			square.Square1_1 = br.ReadBytes(4);
			square.Square2_1 = br.ReadBytes(4);
			square.Square3_1 = br.ReadBytes(4);
			square.Square4_1 = br.ReadBytes(4);

			return square;
		}

		/// <summary>
		/// Writes square to given writer.
		/// </summary>
		/// <param name="bw"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteTo(BinaryWriter bw)
		{
			bw.Write(this.Square1_0);
			bw.Write(this.Square2_0);
			bw.Write(this.Square3_0);
			bw.Write(this.Square4_0);
			bw.Write(this.Square1_1);
			bw.Write(this.Square2_1);
			bw.Write(this.Square3_1);
			bw.Write(this.Square4_1);
		}
	}
}
