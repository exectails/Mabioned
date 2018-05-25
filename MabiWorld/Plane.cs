﻿using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using MabiWorld.Extensions;

namespace MabiWorld
{
	/// <summary>
	/// Represents a plane, a part of an .area file.
	/// </summary>
	public class Plane
	{
		public float Height { get; set; }
		public float Unk1 { get; set; }
		public float Unk2 { get; set; }
		public Color Unk3 { get; set; }

		/// <summary>
		/// Reads plane from reader and returns it.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane ReadFrom(BinaryReader br)
		{
			var plane = new Plane();

			plane.Height = br.ReadSingle();
			plane.Unk1 = br.ReadSingle();
			plane.Unk2 = br.ReadSingle();
			plane.Unk3 = br.ReadColor();

			return plane;
		}

		/// <summary>
		/// Writes plane to given writer.
		/// </summary>
		/// <param name="bw"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteTo(BinaryWriter bw)
		{
			bw.Write(this.Height);
			bw.Write(this.Unk1);
			bw.Write(this.Unk2);
			bw.WriteColor(this.Unk3);
		}
	}
}
