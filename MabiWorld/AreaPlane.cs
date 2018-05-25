using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Runtime.CompilerServices;
using MabiWorld.PropertyEditing;

namespace MabiWorld
{
	/// <summary>
	/// Represents an area plane, a part of an .area file.
	/// </summary>
	public class AreaPlane
	{
		public const int MaterialSlotCount = 4;
		public const int SquareCount = 4;
		public const int MaterialSlotIndexCount = 16;

		public byte Version { get; set; }
		public byte Unk1 { get; set; }
		public byte Unk2 { get; set; }
		public byte Unk3 { get; set; }
		public byte Unk4 { get; set; }
		public byte Size { get; set; }
		public int[] MaterialSlots { get; } = new int[MaterialSlotCount];
		public byte ShowPlane { get; set; }
		public byte UseTiles { get; set; }
		public byte[] MaterialSlotIndexes { get; } = new byte[MaterialSlotIndexCount];
		public float MinHeight { get; set; }
		public float MaxHeight { get; set; }
		public float Unk5 { get; set; }
		public float Unk6 { get; set; }

		[Editor(typeof(NotifyingCollectionEditor), typeof(UITypeEditor))]
		public List<Plane> Planes { get; internal set; } = new List<Plane>();

		[Editor(typeof(NotifyingCollectionEditor), typeof(UITypeEditor))]
		public List<Square> Squares { get; internal set; } = new List<Square>();

		/// <summary>
		/// Reads area plane from given reader and returns it.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AreaPlane ReadFrom(BinaryReader br)
		{
			var areaPlane = new AreaPlane();

			areaPlane.Version = br.ReadByte();

			if (areaPlane.Version >= 240)
			{
				areaPlane.Unk1 = br.ReadByte();
				areaPlane.Unk2 = br.ReadByte();
				areaPlane.Unk3 = br.ReadByte();
				areaPlane.Unk4 = br.ReadByte();
			}

			areaPlane.Size = br.ReadByte();

			for (var i = 0; i < MaterialSlotCount; ++i)
			{
				if (areaPlane.Version >= 240)
					areaPlane.MaterialSlots[i] = br.ReadInt32();
				else
					areaPlane.MaterialSlots[i] = br.ReadByte();
			}

			areaPlane.ShowPlane = br.ReadByte();
			areaPlane.UseTiles = br.ReadByte();

			br.Read(areaPlane.MaterialSlotIndexes, 0, MaterialSlotIndexCount);

			areaPlane.MinHeight = br.ReadSingle();
			areaPlane.MaxHeight = br.ReadSingle();

			if (areaPlane.Version == 1 || areaPlane.Version == 241)
			{
				areaPlane.Unk5 = br.ReadSingle();
				areaPlane.Unk6 = br.ReadSingle();
			}

			var planeCount = areaPlane.Size * areaPlane.Size;
			areaPlane.Planes = new List<Plane>(planeCount);
			for (var i = 0; i < planeCount; ++i)
			{
				var plane = Plane.ReadFrom(br);
				areaPlane.Planes.Add(plane);
			}

			areaPlane.Squares = new List<Square>(SquareCount);
			for (var i = 0; i < SquareCount; ++i)
			{
				var square = Square.ReadFrom(br);
				areaPlane.Squares.Add(square);
			}

			return areaPlane;
		}

		/// <summary>
		/// Writes area plane to given writer.
		/// </summary>
		/// <param name="bw"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteTo(BinaryWriter bw)
		{
			bw.Write(this.Version);

			if (this.Version >= 240)
			{
				bw.Write(this.Unk1);
				bw.Write(this.Unk2);
				bw.Write(this.Unk3);
				bw.Write(this.Unk4);
			}

			bw.Write(this.Size);

			for (var i = 0; i < MaterialSlotCount; ++i)
			{
				if (this.Version >= 240)
					bw.Write(this.MaterialSlots[i]);
				else
					bw.Write((byte)this.MaterialSlots[i]);
			}

			bw.Write(this.ShowPlane);
			bw.Write(this.UseTiles);

			for (var i = 0; i < this.MaterialSlotIndexes.Length; ++i)
				bw.Write(this.MaterialSlotIndexes[i]);

			bw.Write(this.MinHeight);
			bw.Write(this.MaxHeight);

			if (this.Version == 1 || this.Version == 241)
			{
				bw.Write(this.Unk5);
				bw.Write(this.Unk6);
			}

			for (var i = 0; i < this.Planes.Count; ++i)
				this.Planes[i].WriteTo(bw);

			for (var i = 0; i < this.Squares.Count; ++i)
				this.Squares[i].WriteTo(bw);
		}
	}
}
