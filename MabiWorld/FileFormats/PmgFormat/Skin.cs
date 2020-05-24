using System.IO;

namespace MabiWorld.FileFormats.PmgFormat
{
	/// <summary>
	/// Represents a PMG's skinning information.
	/// </summary>
	public class Skin
	{
		/// <summary>
		/// ?
		/// </summary>
		public int Unk1 { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public int Unk2 { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public float Unk3 { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public int Unk4 { get; set; }

		/// <summary>
		/// Reads skinning information from binary reader and returns them.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static Skin ReadFrom(BinaryReader br)
		{
			var result = new Skin();

			result.Unk1 = br.ReadInt32();
			result.Unk2 = br.ReadInt32();
			result.Unk3 = br.ReadSingle();
			result.Unk4 = br.ReadInt32();

			return result;
		}
	}
}
