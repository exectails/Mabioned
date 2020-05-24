using System.Drawing;
using System.IO;
using MabiWorld.Extensions;

namespace MabiWorld.FileFormats.PmgFormat
{
	/// <summary>
	/// Represents a vertex of a mesh in a PMG file.
	/// </summary>
	public class Vertex
	{
		/// <summary>
		/// Gets or sets the vertex's position.
		/// </summary>
		public Vector3F Position { get; set; }

		/// <summary>
		/// Gets or sets the vertex's normals.
		/// </summary>
		public Vector3F Normal { get; set; }

		/// <summary>
		/// Gets or sets the vertex color.
		/// </summary>
		public Color Color { get; set; }

		/// <summary>
		/// Gets or sets the vertex's texture coordinates.
		/// </summary>
		public Vector2F UV { get; set; }

		/// <summary>
		/// Reads a vertex from the binary reader and returns it.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static Vertex ReadFrom(BinaryReader br)
		{
			var result = new Vertex();

			result.Position = br.ReadVector3F_XYZ();
			result.Normal = br.ReadVector3F_XYZ();
			result.Color = br.ReadColor();
			result.UV = br.ReadVector2F();

			return result;
		}
	}
}
