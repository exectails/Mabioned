using System.IO;
using MabiWorld.Extensions;

namespace MabiWorld.FileFormats.PmgFormat
{
	/// <summary>
	/// Represents a mesh header in a PMG file.
	/// </summary>
	public class MeshHeader
	{
		/// <summary>
		/// Gets or sets the size of the mesh data.
		/// </summary>
		public int MeshSize { get; set; }

		/// <summary>
		/// Gets or sets the mesh's bone name.
		/// </summary>
		public string BoneName { get; set; }

		/// <summary>
		/// Gets or sets the mesh's name.
		/// </summary>
		public string MeshName { get; set; }

		/// <summary>
		/// Gets or sets the mesh's joint name.
		/// </summary>
		public string JointName { get; set; }

		/// <summary>
		/// Gets or sets the mesh's index.
		/// </summary>
		public int Index { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public int Unk9 { get; set; }

		/// <summary>
		/// Reads mesh header from binary reader and returns it.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static MeshHeader ReadFrom(BinaryReader br)
		{
			var result = new MeshHeader();

			result.MeshSize = br.ReadInt32();
			result.BoneName = br.ReadString(32);
			result.MeshName = br.ReadString(128);
			result.JointName = br.ReadString(32);
			result.Index = br.ReadInt32();
			result.Unk9 = br.ReadInt32();

			return result;
		}
	}
}
