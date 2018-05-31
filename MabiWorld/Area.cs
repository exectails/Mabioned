using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using MabiWorld.Extensions;
using MabiWorld.PropertyEditing;

namespace MabiWorld
{
	/// <summary>
	/// Represents an .area file.
	/// </summary>
	public class Area
	{
		public short Version { get; set; }
		public short Unk8 { get; set; }
		[Browsable(false)]
		public int Length { get; set; }
		public ushort Id { get; set; }
		public ushort RegionId { get; set; }
		public string ServerName { get; set; }
		public string Name { get; set; }
		public int PlaneX { get; set; }
		public int PlaneY { get; set; }
		public int Unk1 { get; set; }
		public int Unk2 { get; set; }
		public int EventCount => this.Events.Count;
		public int PropCount => this.Props.Count;
		public float Unk3 { get; set; }
		public float Unk4 { get; set; }
		public int Unk5 { get; set; }
		public int Unk6 { get; set; }
		public int Unk7 { get; set; }
		public Vector3F BottomLeft { get; set; }
		public Vector3F BottomRight { get; set; }
		public Vector3F TopRight { get; set; }
		public Vector3F TopLeft { get; set; }
		public int Unk9 { get; set; }

		[Browsable(false)]
		public int Version2 { get; set; }

		[Browsable(false)]
		public int PropCount2 => this.Props.Count;

		[Browsable(false)]
		public List<Prop> Props { get; internal set; } = new List<Prop>();

		[Browsable(false)]
		public List<Event> Events { get; internal set; } = new List<Event>();

		[Editor(typeof(NotifyingCollectionEditor), typeof(UITypeEditor))]
		public List<AreaPlane> AreaPlanes { get; internal set; } = new List<AreaPlane>();

		public byte[] Unk10 { get; set; }

		/// <summary>
		/// Gets or sets an object associated with this area.
		/// </summary>
		[Browsable(false)]
		public object Tag { get; set; }

		/// <summary>
		/// Reads area from given file.
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static Area ReadFromFile(string filePath)
		{
			using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
				return ReadFrom(fs);
		}

		/// <summary>
		/// Reads area from stream and returns it.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public static Area ReadFrom(Stream stream)
		{
			using (var br = new BinaryReader(stream))
			{
				var area = new Area();

				area.Version = br.ReadInt16();
				area.Unk8 = br.ReadInt16();
				area.Length = br.ReadInt32();
				area.Id = br.ReadUInt16();
				area.RegionId = br.ReadUInt16();
				area.ServerName = br.ReadWString();
				area.Name = br.ReadWString();
				area.PlaneX = br.ReadInt32();
				area.PlaneY = br.ReadInt32();
				area.Unk1 = br.ReadInt32();
				area.Unk2 = br.ReadInt32();
				var eventCount = br.ReadInt32();
				var propCount = br.ReadInt32();
				area.Unk3 = br.ReadSingle();
				area.Unk4 = br.ReadSingle();
				area.Unk5 = br.ReadInt32();
				area.Unk6 = br.ReadInt32();
				area.Unk7 = br.ReadInt32();
				area.BottomLeft = br.ReadVector3F_XZY();
				area.BottomRight = br.ReadVector3F_XZY();
				area.TopRight = br.ReadVector3F_XZY();
				area.TopLeft = br.ReadVector3F_XZY();

				if (area.Version == 203)
					area.Unk9 = br.ReadInt32();

				area.Version2 = br.ReadInt32();
				var propCount2 = br.ReadInt32();

				area.Props = new List<Prop>(propCount2);
				for (var i = 0; i < propCount2; ++i)
				{
					var prop = Prop.ReadFrom(area, br);
					area.Props.Add(prop);
				}

				area.Events = new List<Event>(eventCount);
				for (var i = 0; i < eventCount; ++i)
				{
					var evnt = Event.ReadFrom(area, br);
					area.Events.Add(evnt);
				}

				var areaPlanesCount = (area.PlaneX * area.PlaneY);
				area.AreaPlanes = new List<AreaPlane>(areaPlanesCount);
				for (var i = 0; i < areaPlanesCount; ++i)
				{
					var plane = AreaPlane.ReadFrom(br);
					area.AreaPlanes.Add(plane);
				}

				// Usually 65|68 bytes follow the planes, purpose unknown
				// and why they appear hasn't been researched yet, neither
				// why the lengths differ..

				var length = (int)(br.BaseStream.Length - br.BaseStream.Position);
				area.Unk10 = br.ReadBytes(length);

				if (length != 65 && length != 68)
					throw new FormatException($"Expected 65|68 byte tail, got {length}.");

				if (br.BaseStream.Position != br.BaseStream.Length)
					throw new FormatException($"Area file '{area.Name}' longer or shorter than expected.");

				return area;
			}
		}

		/// <summary>
		/// Writes area to given stream.
		/// </summary>
		/// <param name="stream"></param>
		public void WriteTo(Stream stream)
		{
			using (var bw = new BinaryWriter(stream))
			{
				bw.Write(this.Version);
				bw.Write(this.Unk8);

				var lengthPos = (int)bw.BaseStream.Position;
				bw.Write(this.Length);

				bw.Write(this.Id);
				bw.Write(this.RegionId);
				bw.WriteWString(this.ServerName);
				bw.WriteWString(this.Name);
				bw.Write(this.PlaneX);
				bw.Write(this.PlaneY);
				bw.Write(this.Unk1);
				bw.Write(this.Unk2);
				bw.Write(this.EventCount);
				bw.Write(this.PropCount);
				bw.Write(this.Unk3);
				bw.Write(this.Unk4);
				bw.Write(this.Unk5);
				bw.Write(this.Unk6);
				bw.Write(this.Unk7);
				bw.WriteVector3F_XZY(this.BottomLeft);
				bw.WriteVector3F_XZY(this.BottomRight);
				bw.WriteVector3F_XZY(this.TopRight);
				bw.WriteVector3F_XZY(this.TopLeft);

				if (this.Version == 203)
					bw.Write(this.Unk9);

				bw.Write(this.Version2);
				bw.Write(this.PropCount2);

				for (var i = 0; i < this.Props.Count; ++i)
					this.Props[i].WriteTo(bw);

				for (var i = 0; i < this.Events.Count; ++i)
					this.Events[i].WriteTo(bw);

				for (var i = 0; i < this.AreaPlanes.Count; ++i)
					this.AreaPlanes[i].WriteTo(bw);

				bw.Write(this.Unk10);

				var length = (int)bw.BaseStream.Position;
				bw.Seek(lengthPos, SeekOrigin.Begin);
				bw.Write(length);
			}
		}

		/// <summary>
		/// Compares result of WriteTo with the given file, throws
		/// exception if they're no equal.
		/// </summary>
		/// <param name="filePath"></param>
		public void ExportTest(string filePath)
		{
			//using (var fs2 = new FileStream(filePath + ".test", FileMode.OpenOrCreate, FileAccess.Write))
			//	this.WriteTo(fs2);

			using (var ms = new MemoryStream())
			{
				this.WriteTo(ms);

				var buffer = File.ReadAllBytes(filePath);
				var myBuffer = ms.ToArray();

				if (buffer.Length != myBuffer.Length)
					throw new Exception();

				for (var i = 0; i < myBuffer.Length; ++i)
				{
					// Ignore Length, as that's 0 for some area files.
					if (buffer[i] != myBuffer[i] && (i <= 3 || i > 7))
						throw new Exception();
				}
			}
		}

		/// <summary>
		/// Returns an unused prop id for this area. If no available ids
		/// were found, 0 is returned.
		/// </summary>
		/// <returns></returns>
		public ulong GetNewPropId()
		{
			var baseId = 0x00A0_0000_0000_0000UL;

			baseId |= ((ulong)this.RegionId) << 32;
			baseId |= ((ulong)this.Id) << 16;

			for (ulong i = 1; i <= ushort.MaxValue; ++i)
			{
				var id = (baseId | i);
				var prop = this.Props.FirstOrDefault(a => a.EntityId == id);
				if (prop == null)
					return id;
			}

			return 0;
		}

		/// <summary>
		/// Returns an unused event id for this area. If no available ids
		/// were found, 0 is returned.
		/// </summary>
		/// <returns></returns>
		public ulong GetNewEventId()
		{
			var baseId = 0x00B0_0000_0000_0000UL;

			baseId |= ((ulong)this.RegionId) << 32;
			baseId |= ((ulong)this.Id) << 16;

			for (ulong i = 1; i <= ushort.MaxValue; ++i)
			{
				var id = (baseId | i);
				var prop = this.Events.FirstOrDefault(a => a.EntityId == id);
				if (prop == null)
					return id;
			}

			return 0;
		}

		/// <summary>
		/// Returns true if the given position is inside this area, based
		/// on its bounds.
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public bool IsInside(PointF pos)
		{
			return !(pos.X < this.BottomLeft.X || pos.X > this.BottomRight.X || pos.Y < this.BottomLeft.Y || pos.Y > this.TopLeft.Y);
		}

		/// <summary>
		/// Returns string representation of area.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Area [Name: {0}]", this.Name);
		}
	}
}
