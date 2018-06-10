using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MabiWorld.Extensions;
using MabiWorld.PropertyEditing;
using Color = System.Drawing.Color;

namespace MabiWorld
{
	/// <summary>
	/// Represents a .rgn file.
	/// </summary>
	public class Region
	{
		public int Version { get; set; }

		[Browsable(false)]
		public int Length { get; set; }

		public int Id { get; set; }
		public int GroupId { get; set; }
		public string Name { get; set; }
		public int CellSize { get; set; }
		public byte Sight { get; set; }

		[Browsable(false)]
		public int AreaCount => this.AreaFileNames.Count;

		public int Unk1 { get; set; }
		public Vector3F BottomLeft { get; set; }
		public Vector3F BottomRight { get; set; }
		public Vector3F TopRight { get; set; }
		public Vector3F TopLeft { get; set; }
		public RegionType Type { get; set; }

		[Browsable(false)]
		public LegacyRegionType LegacyType { get; set; }

		public IndoorType IndoorType { get; set; }
		public float Unk2 { get; set; }
		public string Scene { get; set; }
		public Color Unk3 { get; set; }
		public Color Unk4 { get; set; }
		public Vector3F LightDirection { get; set; }
		public Color GlobalOverrideColor { get; set; }
		public Color Unk5 { get; set; }
		public byte ApplyGlowOverlay { get; set; }
		public Color Unk6 { get; set; }
		public Vector3F GlowOverlayPosition { get; set; }
		public string CameraName { get; set; }
		public string LightName { get; set; }
		public float CameraRadius { get; set; }
		public float Unk7 { get; set; }
		public float ReferenceRadius { get; set; }

		[Browsable(false)]
		public List<string> AreaFileNames { get; set; } = new List<string>();

		public string Unk28 { get; set; }
		public byte Unk8 { get; set; }
		public float Unk9 { get; set; }
		public float Unk10 { get; set; }
		public int Unk11 { get; set; }
		public byte Unk12 { get; set; }
		public byte Unk13 { get; set; }
		public float Unk14 { get; set; }
		public float Unk31 { get; set; }

		[Browsable(false)]
		public int XmlLength => this.Xml.Length;

		[Editor(typeof(XmlTextEditor), typeof(UITypeEditor))]
		public string Xml { get; set; }

		public int Unk30 { get; set; }
		public short Unk15 { get; set; }
		public int Unk29 { get; set; }
		public string Path { get; set; }
		public short Unk16 { get; set; }
		public float Unk17 { get; set; }
		public float Unk18 { get; set; }
		public float Unk19 { get; set; }
		public float Unk20 { get; set; }
		public float Unk21 { get; set; }
		public float Unk22 { get; set; }
		public float Unk23 { get; set; }
		public float Unk24 { get; set; }
		public float Unk25 { get; set; }
		public float Unk26 { get; set; }
		public byte[] Unk27 { get; set; }

		/// <summary>
		/// List of areas, only filled automatically if region is
		/// ReadFromFile.
		/// </summary>
		[Browsable(false)]
		public List<Area> Areas { get; private set; } = new List<Area>();

		/// <summary>
		/// Reads region from given file and its areas from the
		/// respective files in the same directory.
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static Region ReadFromFile(string filePath)
		{
			var dirPath = System.IO.Path.GetDirectoryName(filePath);

			Region region = null;
			using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
				region = ReadFrom(fs);

			UnsupportedVersionException versionException = null;

			Parallel.For(0, region.AreaFileNames.Count, i =>
			{
				try
				{
					var areaFileName = region.AreaFileNames[i];
					var areaFilePath = System.IO.Path.Combine(dirPath, areaFileName + ".area");

					using (var fs2 = new FileStream(areaFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
					{
						var area = Area.ReadFrom(fs2);
						lock (region.Areas)
							region.Areas.Add(area);
					}
				}
				catch (UnsupportedVersionException ex)
				{
					versionException = ex;
				}
			});

			if (versionException != null)
				throw versionException;

			region.Areas = region.Areas.OrderBy(a => a.Name).ToList();

			return region;
		}

		/// <summary>
		/// Reads region from given stream and returns it.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public static Region ReadFrom(Stream stream)
		{
			using (var br = new BinaryReader(stream))
			{
				var region = new Region();

				region.Version = br.ReadInt32();
				region.Length = br.ReadInt32();
				region.Id = br.ReadInt32();
				region.GroupId = br.ReadInt32();
				region.Name = br.ReadWString();

				var supportedVersion = (/*region.Version == 100 ||*/ region.Version == 102 || region.Version == 103);
				if (!supportedVersion || region.Name == "DungeonProp_Temp")
					throw new UnsupportedVersionException();

				region.CellSize = br.ReadInt32();
				region.Sight = br.ReadByte();
				var areaCount = br.ReadInt32();
				region.Unk1 = br.ReadInt32();
				region.BottomLeft = br.ReadVector3F_XZY();
				region.BottomRight = br.ReadVector3F_XZY();
				region.TopRight = br.ReadVector3F_XZY();
				region.TopLeft = br.ReadVector3F_XZY();

				if (region.Version > 100)
				{
					region.Type = (RegionType)br.ReadInt32();
					region.IndoorType = (IndoorType)br.ReadInt32();
					region.Unk2 = br.ReadSingle();
				}
				else
				{
					region.LegacyType = (LegacyRegionType)br.ReadInt32();
				}

				region.Scene = br.ReadWString();
				region.Unk3 = br.ReadColor();
				region.Unk4 = br.ReadColor();
				region.LightDirection = br.ReadVector3F_XZY();
				region.GlobalOverrideColor = br.ReadColor();
				region.Unk5 = br.ReadColor();
				region.ApplyGlowOverlay = br.ReadByte();
				region.Unk6 = br.ReadColor();
				region.GlowOverlayPosition = br.ReadVector3F_XZY();
				region.CameraName = br.ReadWString();
				region.LightName = br.ReadWString();
				region.CameraRadius = br.ReadSingle();
				region.Unk7 = br.ReadSingle();
				region.ReferenceRadius = br.ReadSingle();

				region.AreaFileNames = new List<string>(areaCount);
				for (var i = 0; i < areaCount; ++i)
					region.AreaFileNames.Add(br.ReadWString());

				if (region.Version >= 103)
					region.Unk28 = br.ReadWString();

				if (region.Version > 100)
				{
					region.Unk8 = br.ReadByte();
					region.Unk9 = br.ReadSingle();
					region.Unk10 = br.ReadSingle();
					region.Unk11 = br.ReadInt32();
					region.Unk12 = br.ReadByte();
					region.Unk13 = br.ReadByte();
					region.Unk14 = br.ReadSingle();
					region.Unk31 = br.ReadSingle();
					var xmlLength = br.ReadInt32();
					region.Xml = br.ReadWString(xmlLength);
				}

				region.Unk29 = br.ReadInt32();
				region.Path = br.ReadWString();
				region.Unk16 = br.ReadInt16();
				region.Unk17 = br.ReadSingle();
				region.Unk18 = br.ReadSingle();
				region.Unk19 = br.ReadSingle();
				region.Unk20 = br.ReadSingle();
				region.Unk21 = br.ReadSingle();
				region.Unk22 = br.ReadSingle();
				region.Unk23 = br.ReadSingle();
				region.Unk24 = br.ReadSingle();
				region.Unk25 = br.ReadSingle();
				region.Unk26 = br.ReadSingle();

				// Sometimes a few bytes follow Unk26, but it seems random.
				// The data in those bytes also appears to be random,
				// usually it's 4 zero bytes, but there's also a case
				// where a full copy of the previous unknown floats can
				// be found here, "ula_emainmacha_oidtobar_hall".

				if (region.Version > 100 && br.BaseStream.Position < br.BaseStream.Length)
				{
					var length = (int)(br.BaseStream.Length - br.BaseStream.Position);
					region.Unk27 = br.ReadBytes(length);

					switch (region.Name)
					{
						case "Ula_Emainmacha_OidTobar_Hall": AssertTailLength(48, length); break;
						case "JP_Nekojima_islet": AssertTailLength(12, length); break;
						default: AssertTailLength(4, length); break;
					}
				}

				if (region.Version == 100)
				{
					EnumValueNotDefinedException.AssertDefined(typeof(LegacyRegionType), region.LegacyType);

					switch (region.LegacyType)
					{
						case LegacyRegionType.Outdoor:
							region.Type = RegionType.Normal;
							region.IndoorType = IndoorType.Outdoor;
							break;

						case LegacyRegionType.Indoor:
							region.Type = RegionType.Normal;
							region.IndoorType = IndoorType.Indoor;
							break;

						case LegacyRegionType.DungeonLobby:
							region.Type = RegionType.DungeonLobby;
							region.IndoorType = IndoorType.Indoor;
							break;

						case LegacyRegionType.DungeonField:
							region.Type = RegionType.DungeonField1;
							region.IndoorType = IndoorType.Indoor;
							break;
					}
				}
				else
				{
					if (region.IndoorType == IndoorType.Indoor)
						region.LegacyType = LegacyRegionType.Indoor;
					else if (region.IndoorType == IndoorType.Outdoor)
						region.LegacyType = LegacyRegionType.Outdoor;

					if (region.Type == RegionType.DungeonLobby)
						region.LegacyType = LegacyRegionType.DungeonLobby;
					else if (region.Type == RegionType.DungeonField1 || region.Type == RegionType.DungeonField2)
						region.LegacyType = LegacyRegionType.DungeonField;

					EnumValueNotDefinedException.AssertDefined(typeof(RegionType), region.Type);
					EnumValueNotDefinedException.AssertDefined(typeof(IndoorType), region.IndoorType);
				}

				return region;
			}
		}

		/// <summary>
		/// Throws FormatException if expected number of bytes is not
		/// equal the amount found.
		/// </summary>
		/// <param name="expected"></param>
		/// <param name="got"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void AssertTailLength(int expected, int got)
		{
			if (expected != got)
				throw new FormatException($"Expected {expected} byte tail, got {got}.");
		}

		/// <summary>
		/// Writes region to stream.
		/// </summary>
		/// <param name="stream"></param>
		public void WriteTo(Stream stream)
		{
			using (var bw = new BinaryWriter(stream))
			{
				bw.Write(this.Version);

				var lengthPos = (int)bw.BaseStream.Position;
				bw.Write(this.Length);

				bw.Write(this.Id);
				bw.Write(this.GroupId);
				bw.WriteWString(this.Name);
				bw.Write(this.CellSize);
				bw.Write(this.Sight);
				bw.Write(this.AreaCount);
				bw.Write(this.Unk1);
				bw.WriteVector3F_XZY(this.BottomLeft);
				bw.WriteVector3F_XZY(this.BottomRight);
				bw.WriteVector3F_XZY(this.TopRight);
				bw.WriteVector3F_XZY(this.TopLeft);

				if (this.Version > 100)
				{
					bw.Write((int)this.Type);
					bw.Write((int)this.IndoorType);
					bw.Write(this.Unk2);
				}
				else
				{
					bw.Write((int)this.LegacyType);
				}

				bw.WriteWString(this.Scene);
				bw.WriteColor(this.Unk3);
				bw.WriteColor(this.Unk4);
				bw.WriteVector3F_XZY(this.LightDirection);
				bw.WriteColor(this.GlobalOverrideColor);
				bw.WriteColor(this.Unk5);
				bw.Write(this.ApplyGlowOverlay);
				bw.WriteColor(this.Unk6);
				bw.WriteVector3F_XZY(this.GlowOverlayPosition);
				bw.WriteWString(this.CameraName);
				bw.WriteWString(this.LightName);
				bw.Write(this.CameraRadius);
				bw.Write(this.Unk7);
				bw.Write(this.ReferenceRadius);

				for (var i = 0; i < this.AreaFileNames.Count; ++i)
					bw.WriteWString(this.AreaFileNames[i]);

				if (this.Version >= 103)
					bw.WriteWString(this.Unk28);

				if (this.Version > 100)
				{
					bw.Write(this.Unk8);
					bw.Write(this.Unk9);
					bw.Write(this.Unk10);
					bw.Write(this.Unk11);
					bw.Write(this.Unk12);
					bw.Write(this.Unk13);
					bw.Write(this.Unk14);
					bw.Write(this.Unk31);
					bw.Write(this.Xml.Length);
					bw.WriteWString(this.Xml, this.Xml.Length);
				}

				bw.Write(this.Unk29);
				bw.WriteWString(this.Path);
				bw.Write(this.Unk16);
				bw.Write(this.Unk17);
				bw.Write(this.Unk18);
				bw.Write(this.Unk19);
				bw.Write(this.Unk20);
				bw.Write(this.Unk21);
				bw.Write(this.Unk22);
				bw.Write(this.Unk23);
				bw.Write(this.Unk24);
				bw.Write(this.Unk25);
				bw.Write(this.Unk26);

				if (this.Unk27 != null)
					bw.Write(this.Unk27);

				var length = (int)bw.BaseStream.Position;
				bw.Seek(lengthPos, SeekOrigin.Begin);
				bw.Write(length);
			}
		}

		/// <summary>
		/// Returns string representation of region.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Region [Id: {0}, Name: {1}]", this.Id, this.Name);
		}
	}
}
