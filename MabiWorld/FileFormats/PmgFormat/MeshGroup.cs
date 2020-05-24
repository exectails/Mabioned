using System.Collections.Generic;
using System.IO;
using MabiWorld.Extensions;

namespace MabiWorld.FileFormats.PmgFormat
{
	/// <summary>
	/// Represents a group of meshes in a PMG file.
	/// </summary>
	public class MeshGroup
	{
		/// <summary>
		/// Gets or sets the group's name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public string Unk2 { get; set; }

		/// <summary>
		/// Returns the number of meshes in the group.
		/// </summary>
		public int MeshCount => this.MeshHeaders.Count;

		/// <summary>
		/// Returns a list with the group's meshs' headers.
		/// </summary>
		public List<MeshHeader> MeshHeaders { get; } = new List<MeshHeader>();

		/// <summary>
		/// Reads mesh group from binary reader and returns it.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static MeshGroup ReadFrom(BinaryReader br)
		{
			var result = new MeshGroup();

			result.Name = br.ReadString(32);
			result.Unk2 = br.ReadString(32);

			var meshCount = br.ReadInt32();
			for (var i = 0; i < meshCount; ++i)
			{
				var meshHeader = MeshHeader.ReadFrom(br);
				result.MeshHeaders.Add(meshHeader);
			}

			return result;
		}
	}
}
