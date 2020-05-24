﻿using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace MabiWorld.Extensions
{
	public static class BinaryReaderExtensions
	{
		/// <summary>
		/// Reads string with the given length from binary reader.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string ReadString(this BinaryReader br, int length)
		{
			var buffer = br.ReadBytes(length);

			var terminatorIndex = -1;
			for (var i = 0; i < length; ++i)
			{
				if (buffer[i] == 0)
				{
					terminatorIndex = i;
					break;
				}
			}

			var actualLength = length;
			if (terminatorIndex != -1)
				actualLength = terminatorIndex;

			return Encoding.UTF8.GetString(buffer, 0, actualLength);
		}

		/// <summary>
		/// Reads length prefixed string from binary reader.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string ReadLpString(this BinaryReader br)
		{
			var length = br.ReadInt32();
			return ReadString(br, length);
		}

		/// <summary>
		/// Reads null-terminated Unicode string from binary reader.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string ReadWString(this BinaryReader br)
		{
			//var buffer = new byte[64];
			var length = 0;
			var start = br.BaseStream.Position;

			for (var i = 0; br.BaseStream.Position < br.BaseStream.Length - 1; i += 2)
			{
				if (br.ReadInt16() == 0)
					break;

				length += 2;
			}

			var pos = br.BaseStream.Position;
			br.BaseStream.Seek(start, SeekOrigin.Begin);
			var buffer = br.ReadBytes(length);
			br.BaseStream.Seek(pos, SeekOrigin.Begin);

			return Encoding.Unicode.GetString(buffer);
		}

		/// <summary>
		/// Reads given number of Unicode characters from binary reader.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string ReadWString(this BinaryReader br, int length)
		{
			var buffer = br.ReadBytes(length * 2);
			return Encoding.Unicode.GetString(buffer);
		}

		/// <summary>
		/// Writes null-terminated Unicode string to binary writer as.
		/// </summary>
		/// <param name="bw"></param>
		/// <param name="str"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteWString(this BinaryWriter bw, string str)
		{
			var bytes = Encoding.Unicode.GetBytes(str + '\0');
			bw.Write(bytes);
		}

		/// <summary>
		/// Writes Unicode string to binary writer.
		/// </summary>
		/// <param name="bw"></param>
		/// <param name="str"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteWString(this BinaryWriter bw, string str, int length)
		{
			var bytes = Encoding.Unicode.GetBytes(str.Substring(0, length));
			bw.Write(bytes);
		}

		/// <summary>
		/// Reads three floats from binary reader and returns them as
		/// a vector.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3F ReadVector3F_XZY(this BinaryReader br)
		{
			var result = new Vector3F();

			result.X = br.ReadSingle();
			result.Z = br.ReadSingle();
			result.Y = br.ReadSingle();

			return result;
		}

		/// <summary>
		/// Reads three floats from binary reader and returns them as
		/// a vector.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3F ReadVector3F_XYZ(this BinaryReader br)
		{
			var result = new Vector3F();

			result.X = br.ReadSingle();
			result.Y = br.ReadSingle();
			result.Z = br.ReadSingle();

			return result;
		}

		/// <summary>
		/// Writes vector to binary writer using three floats.
		/// </summary>
		/// <param name="bw"></param>
		/// <param name="vector"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteVector3F_XZY(this BinaryWriter bw, Vector3F vector)
		{
			bw.Write(vector.X);
			bw.Write(vector.Z);
			bw.Write(vector.Y);
		}

		/// <summary>
		/// Reads two floats from binary reader and returns them as
		/// a point.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PointF ReadPointF(this BinaryReader br)
		{
			var result = new PointF();

			result.X = br.ReadSingle();
			result.Y = br.ReadSingle();

			return result;
		}

		/// <summary>
		/// Writes point to binary writer using two floats.
		/// </summary>
		/// <param name="bw"></param>
		/// <param name="vector"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WritePointF(this BinaryWriter bw, PointF vector)
		{
			bw.Write(vector.X);
			bw.Write(vector.Y);
		}

		/// <summary>
		/// Reads two floats from binary reader and returns them as
		/// a vector.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2F ReadVector2F(this BinaryReader br)
		{
			var result = new Vector2F();

			result.X = br.ReadSingle();
			result.Y = br.ReadSingle();

			return result;
		}

		/// <summary>
		/// Writes vector to binary writer using two floats.
		/// </summary>
		/// <param name="bw"></param>
		/// <param name="vector"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteVector2F(this BinaryWriter bw, Vector2F vector)
		{
			bw.Write(vector.X);
			bw.Write(vector.Y);
		}

		/// <summary>
		/// Writes vector to binary writer using three floats.
		/// </summary>
		/// <param name="bw"></param>
		/// <param name="vector"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteVector3F_XYZ(this BinaryWriter bw, Vector3F vector)
		{
			bw.Write(vector.X);
			bw.Write(vector.Y);
			bw.Write(vector.Z);
		}

		/// <summary>
		/// Writes color to binary writer using four bytes.
		/// </summary>
		/// <param name="bw"></param>
		/// <param name="color"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteColor(this BinaryWriter bw, Color color)
		{
			bw.Write(color.B);
			bw.Write(color.G);
			bw.Write(color.R);
			bw.Write(color.A);
		}

		/// <summary>
		/// Reads four bytes from binary reader and returns them as color.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color ReadColor(this BinaryReader br)
		{
			var b = br.ReadByte();
			var g = br.ReadByte();
			var r = br.ReadByte();
			var a = br.ReadByte();

			return Color.FromArgb(a, r, g, b);
		}

		/// <summary>
		/// Reads 16 floats from binary reader and returns them as a matrix.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Matrix4x4 ReadMatrix4x4(this BinaryReader br)
		{
			var matrix = new float[16];
			for (var i = 0; i < matrix.Length; ++i)
				matrix[i] = br.ReadSingle();

			return new Matrix4x4(
				matrix[00], matrix[04], matrix[08], matrix[12],
				matrix[01], matrix[05], matrix[09], matrix[13],
				matrix[02], matrix[06], matrix[10], matrix[14],
				matrix[03], matrix[07], matrix[11], matrix[15]
			);
		}
	}
}
