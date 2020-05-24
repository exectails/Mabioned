using System.Collections.Generic;
using System.IO;
using MabiWorld.Extensions;

namespace MabiWorld.FileFormats.PmgFormat
{
	/// <summary>
	/// Represents a mesh in a PMG file.
	/// </summary>
	public class Mesh
	{
		public readonly static FormatVersion Version1_7 = new FormatVersion(1, 7);
		public readonly static FormatVersion Version2 = new FormatVersion(2, 0);
		public readonly static FormatVersion Version3 = new FormatVersion(3, 0);

		/// <summary>
		/// Gets or sets the mesh's signature.
		/// </summary>
		public string Signature { get; set; }

		/// <summary>
		/// Gets or sets the mesh's version.
		/// </summary>
		public FormatVersion Version { get; set; }

		/// <summary>
		/// Gets or sets the size of the mesh's data.
		/// </summary>
		public int Size { get; set; }

		/// <summary>
		/// Gets or sets the bone name.
		/// </summary>
		public string BoneName { get; set; }

		/// <summary>
		/// Gets or sets the mesh's name.
		/// </summary>
		public string MeshName { get; set; }

		/// <summary>
		/// Gets or sets the joint name.
		/// </summary>
		public string JointName { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public string StateName { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public string NormalName { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public string Unk25 { get; set; }

		/// <summary>
		/// Gets or sets the name of the color to use (e.g. "b1").
		/// </summary>
		public string ColorName { get; set; }

		/// <summary>
		/// Gets or sets the texture the mesh uses.
		/// </summary>
		public string TextureName { get; set; }

		/// <summary>
		/// Gets or sets one of the matrices the mesh (potentially) uses.
		/// </summary>
		public Matrix4x4 Matrix1 { get; set; }

		/// <summary>
		/// Gets or sets one of the matrices the mesh (potentially) uses.
		/// </summary>
		public Matrix4x4 Matrix2 { get; set; }

		/// <summary>
		/// Gets or sets the mesh's index.
		/// </summary>
		public int Index { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public byte[] Unk8 { get; set; }

		/// <summary>
		/// Gets or sets whether the mesh uses a texture (0 or 1).
		/// </summary>
		public int IsTextureMapped { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public byte[] Unk10 { get; set; }

		/// <summary>
		/// Returns the number of indices the mesh has.
		/// </summary>
		public int IndexCount => this.Indices.Count;

		/// <summary>
		/// Gets or sets the number of faces the mesh has.
		/// </summary>
		public int FaceCount { get; set; }

		/// <summary>
		/// Returns the number of strip vertices the mesh has.
		/// </summary>
		public int StripFaceVertexCount => this.StripVertices.Count;

		/// <summary>
		/// Gets or sets the number of strip faces the mesh has.
		/// </summary>
		public int StripFaceCount { get; set; }

		/// <summary>
		/// Returns the number of vertices the mesh has.
		/// </summary>
		public int VertexCount => this.Vertices.Count;

		/// <summary>
		/// Returns the number of skins the mesh has.
		/// </summary>
		public int SkinCount => this.Skins.Count;

		/// <summary>
		/// Returns the number of physics elements the mesh has.
		/// </summary>
		public int PhysicsCount { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public int IsAnimated { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public int MorphFrameSize { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public int MorphFrameCount { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public int Unk18 { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public int Unk19 { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public int Unk20 { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public int Unk21 { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public int Unk22 { get; set; }

		/// <summary>
		/// Gets or sets the size of the index data.
		/// </summary>
		public int IndicesSize { get; set; }

		/// <summary>
		/// Gets or sets the size of the triangle strip index data.
		/// </summary>
		public int TriangleStripIndicesSize { get; set; }

		/// <summary>
		/// Gets or sets the size of the vertex data.
		/// </summary>
		public int VerticesSize { get; set; }

		/// <summary>
		/// Gets or sets the size of the skin data.
		/// </summary>
		public int SkinsSize { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public int Unk23 { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public int Unk3XSize { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public Vector3F Unk30 { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public Vector3F Unk31 { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public Vector3F Unk32 { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public Vector3F Unk33 { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public Vector3F Unk34 { get; set; }

		/// <summary>
		/// Returns a list with the mesh's indices.
		/// </summary>
		public List<int> Indices { get; } = new List<int>();

		/// <summary>
		/// Returns a list with the mesh's strip vertices.
		/// </summary>
		public List<int> StripVertices { get; } = new List<int>();

		/// <summary>
		/// Returns a list with the mesh's vertices.
		/// </summary>
		public List<Vertex> Vertices { get; } = new List<Vertex>();

		/// <summary>
		/// Returns a list with the mesh's skinning information.
		/// </summary>
		public List<Skin> Skins { get; } = new List<Skin>();

		/// <summary>
		/// ?
		/// </summary>
		public byte[] Unk26 { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public byte[] Unk27 { get; set; }

		/// <summary>
		/// Reads mesh from binary reader and returns it.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		public static Mesh ReadFrom(BinaryReader br)
		{
			var result = new Mesh();

			result.Signature = br.ReadString(4);
			if (result.Signature != "pm!")
				throw new InvalidDataException("Expected 'pm!' signature.");

			result.Version = FormatVersion.ReadFrom(br);
			result.Size = br.ReadInt32();

			if (result.Version < Version2)
			{
				result.BoneName = br.ReadString(32);
				result.MeshName = br.ReadString(128);
				result.JointName = br.ReadString(32);
				result.StateName = br.ReadString(32);
				result.NormalName = br.ReadString(32);
				result.ColorName = br.ReadString(32);
			}

			result.Matrix1 = br.ReadMatrix4x4();
			result.Matrix2 = br.ReadMatrix4x4();

			result.Index = br.ReadInt32();
			result.Unk8 = br.ReadBytes(8);

			if (result.Version < Version2)
				result.TextureName = br.ReadString(32);

			result.IsTextureMapped = br.ReadInt32();
			result.Unk10 = br.ReadBytes(36);

			var indexCount = br.ReadInt32();
			result.FaceCount = br.ReadInt32();
			var stripFaceVertexCount = br.ReadInt32();
			result.StripFaceCount = br.ReadInt32();
			var vertexCount = br.ReadInt32();
			var skinCount = br.ReadInt32();

			result.PhysicsCount = br.ReadInt32();
			result.IsAnimated = br.ReadInt32();
			result.MorphFrameSize = br.ReadInt32();
			result.MorphFrameCount = br.ReadInt32();
			result.Unk18 = br.ReadInt32();
			result.Unk19 = br.ReadInt32();
			result.Unk20 = br.ReadInt32();
			result.Unk21 = br.ReadInt32();

			result.Unk22 = br.ReadInt32();
			result.IndicesSize = br.ReadInt32();
			result.TriangleStripIndicesSize = br.ReadInt32();
			result.VerticesSize = br.ReadInt32();
			result.SkinsSize = br.ReadInt32();
			result.Unk23 = br.ReadInt32();

			if (result.Version >= Version2)
			{
				result.BoneName = br.ReadLpString();
				result.MeshName = br.ReadLpString();
				result.JointName = br.ReadLpString();
				result.StateName = br.ReadLpString();
				result.NormalName = br.ReadLpString();

				if (result.Version >= Version3)
					result.Unk25 = br.ReadLpString();

				result.ColorName = br.ReadLpString();
				result.TextureName = br.ReadLpString();
			}

			result.Unk3XSize = br.ReadInt32();
			result.Unk30 = br.ReadVector3F_XYZ();
			result.Unk31 = br.ReadVector3F_XYZ();
			result.Unk32 = br.ReadVector3F_XYZ();
			result.Unk33 = br.ReadVector3F_XYZ();
			result.Unk34 = br.ReadVector3F_XYZ();

			for (var i = 0; i < indexCount; ++i)
			{
				var faceVertex = br.ReadInt16();
				result.Indices.Add(faceVertex);
			}

			for (var i = 0; i < stripFaceVertexCount; ++i)
			{
				var stripVertex = br.ReadInt16();
				result.StripVertices.Add(stripFaceVertexCount);
			}

			for (var i = 0; i < vertexCount; ++i)
			{
				var vertex = Vertex.ReadFrom(br);
				result.Vertices.Add(vertex);
			}

			for (var i = 0; i < skinCount; ++i)
			{
				var skin = Skin.ReadFrom(br);
				result.Skins.Add(skin);
			}

			result.Unk26 = br.ReadBytes(result.PhysicsCount * 32);
			result.Unk27 = br.ReadBytes(result.MorphFrameCount * (result.MorphFrameSize + 4));

			return result;
		}
	}
}
