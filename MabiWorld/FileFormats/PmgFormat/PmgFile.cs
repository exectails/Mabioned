using System.Collections.Generic;
using System.IO;

namespace MabiWorld.FileFormats.PmgFormat
{
	/// <summary>
	/// Represents a PMG file.
	/// </summary>
	/// <remarks>
	/// PMG files contain models.
	/// </remarks>
	public class PmgFile
	{
		/// <summary>
		/// Gets or sets the PMG's header.
		/// </summary>
		public PmgHeader Header { get; set; }

		/// <summary>
		/// Returns a list with the PMG's meshes.
		/// </summary>
		public List<Mesh> Meshes { get; } = new List<Mesh>();

		/// <summary>
		/// Reads a PMG from the given file and returns it.
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static PmgFile ReadFrom(string filePath)
		{
			using (var fs = new FileStream(filePath, FileMode.Open))
				return ReadFrom(fs);
		}

		/// <summary>
		/// Reads a PMG from the given stream and returns it.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public static PmgFile ReadFrom(Stream stream)
		{
			var result = new PmgFile();
			var br = new BinaryReader(stream);

			result.Header = PmgHeader.ReadFrom(br);

			foreach (var group in result.Header.MeshGroups)
			{
				for (var i = 0; i < group.MeshCount; ++i)
				{
					var mesh = Mesh.ReadFrom(br);
					result.Meshes.Add(mesh);
				}
			}

			if (stream.Position != stream.Length)
				throw new InvalidDataException("Leftover data in PMG.");

			return result;
		}
	}
}
