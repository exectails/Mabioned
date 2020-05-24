using System.IO;
using MabiWorld.Extensions;

namespace MabiWorld.FileFormats.SetFormat
{
	/// <summary>
	/// Represents SET file's header.
	/// </summary>
	public class SetHeader
	{
		/// <summary>
		/// Gets or sets the file's signature.
		/// </summary>
		public string Signature { get; set; }

		/// <summary>
		/// Gets or sets the file's version.
		/// </summary>
		public FormatVersion Version { get; set; }

		/// <summary>
		/// Gets or sets the size of the header size.
		/// </summary>
		public int HeaderSize { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public byte[] Unk1 { get; set; }

		/// <summary>
		/// Reads header from binary reader and returns it.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static SetHeader ReadFrom(BinaryReader br)
		{
			var result = new SetHeader();

			result.Signature = br.ReadString(4);
			if (result.Signature != "set")
				throw new InvalidDataException("Expected 'set' signature.");

			result.Version = FormatVersion.ReadFrom(br);
			result.HeaderSize = br.ReadInt32();
			result.Unk1 = br.ReadBytes(result.HeaderSize - (int)br.BaseStream.Position);

			return result;
		}
	}
}
