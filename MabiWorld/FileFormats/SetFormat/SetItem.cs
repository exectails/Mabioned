using System.IO;
using MabiWorld.Extensions;

namespace MabiWorld.FileFormats.SetFormat
{
	/// <summary>
	/// Represents an item in a SET file, which might be either animation
	/// or frame data.
	/// </summary>
	public class SetItem
	{
		/// <summary>
		/// ?
		/// </summary>
		public int Unk2 { get; set; }

		/// <summary>
		/// Returns the size of the item data.
		/// </summary>
		public int Size => this.RawData?.Length ?? 0;

		/// <summary>
		/// Gets or sets the file's name.
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// Gets or sets the state's name.
		/// </summary>
		public string StateName { get; set; }

		/// <summary>
		/// Gets or sets the item's raw data.
		/// </summary>
		public byte[] RawData { get; set; }

		/// <summary>
		/// Reads item from binary reader and returns it.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static SetItem ReadFrom(BinaryReader br)
		{
			var result = new SetItem();

			result.Unk2 = br.ReadInt32();
			var dataSize = br.ReadInt32();
			result.FileName = br.ReadWString();
			result.StateName = br.ReadWString();

			var dataSignature = br.ReadString(4);
			if (dataSignature != "pa!" && dataSignature != "pf!")
				throw new InvalidDataException("Expected 'pa!' or 'pv!' signature.");

			br.BaseStream.Seek(-4, SeekOrigin.Current);
			result.RawData = br.ReadBytes(dataSize);

			return result;
		}
	}
}
