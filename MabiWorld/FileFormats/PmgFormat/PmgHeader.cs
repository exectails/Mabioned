using System.Collections.Generic;
using System.IO;
using MabiWorld.Extensions;

namespace MabiWorld.FileFormats.PmgFormat
{
	/// <summary>
	/// Represents a PMG file's header.
	/// </summary>
	public class PmgHeader
	{
		/// <summary>
		/// Gets or sets the file's signature.
		/// </summary>
		public string Signature { get; set; }

		/// <summary>
		/// Gets or sets the PMG file's version.
		/// </summary>
		public FormatVersion Version { get; set; }

		/// <summary>
		/// Gets or sets the size of the header.
		/// </summary>
		public int HeaderSize { get; set; }

		/// <summary>
		/// Gets or sets the file's name (limited to 32 characters).
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		private byte[] Unk1 { get; set; }

		/// <summary>
		/// Returns a list of the file's mesh groups.
		/// </summary>
		public List<MeshGroup> MeshGroups { get; } = new List<MeshGroup>();

		/// <summary>
		/// Reads header from binary reader and returns it.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static PmgHeader ReadFrom(BinaryReader br)
		{
			var result = new PmgHeader();

			result.Signature = br.ReadString(4);
			if (result.Signature != "pmg")
				throw new InvalidDataException("Expected 'pmg' signature.");

			result.Version = FormatVersion.ReadFrom(br);
			result.HeaderSize = br.ReadInt32();
			result.Name = br.ReadString(32);
			result.Unk1 = br.ReadBytes(96);

			var meshGroupCount = br.ReadInt32();
			for (var i = 0; i < meshGroupCount; ++i)
			{
				var meshGroup = MeshGroup.ReadFrom(br);
				result.MeshGroups.Add(meshGroup);
			}

			return result;
		}
	}
}
