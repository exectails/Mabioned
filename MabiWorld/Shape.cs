﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using MabiWorld.Extensions;
using MabiWorld.PropertyEditing;

namespace MabiWorld
{
	/// <summary>
	/// Represents a shape, a part of an .area file and its entities
	/// like Prop or Event.
	/// </summary>
	public class Shape
	{
		public float DirX1 { get; set; }
		public float DirX2 { get; set; }
		public float DirY1 { get; set; }
		public float DirY2 { get; set; }
		public float LenX { get; set; }
		public float LenY { get; set; }

		[Browsable(false)]
		public int Type { get; set; }

		[TypeConverter(typeof(PointFConverter))]
		public PointF Position { get; set; }

		// The bounding box is used to get entities for the colission
		// detection, if they're wrong players can walk through entities
		// on the client side.

		[Category("Bounding Box")]
		[TypeConverter(typeof(PointFConverter))]
		public PointF BottomLeft { get; set; }

		[Category("Bounding Box")]
		[TypeConverter(typeof(PointFConverter))]
		public PointF TopRight { get; set; }

		/// <summary>
		/// Reads shape from reader and returns it.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Shape ReadFrom(BinaryReader br)
		{
			var shape = new Shape();

			shape.DirX1 = br.ReadSingle();
			shape.DirX2 = br.ReadSingle();
			shape.DirY1 = br.ReadSingle();
			shape.DirY2 = br.ReadSingle();
			shape.LenX = br.ReadSingle();
			shape.LenY = br.ReadSingle();
			shape.Type = br.ReadInt32();
			shape.Position = br.ReadPointF();
			shape.BottomLeft = br.ReadPointF();
			shape.TopRight = br.ReadPointF();

			return shape;
		}

		/// <summary>
		/// Reads legacy shape from reader and returns it.
		/// </summary>
		/// <param name="br"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Shape ReadLegacyFrom(BinaryReader br)
		{
			var shape = new Shape();

			var pointCount = br.ReadByte(); // 5
			var point1 = br.ReadPointF();
			var point2 = br.ReadPointF();
			var point3 = br.ReadPointF();
			var point4 = br.ReadPointF();
			var point5 = br.ReadPointF();
			shape.Type = br.ReadInt32();
			shape.Position = br.ReadPointF();
			shape.BottomLeft = br.ReadPointF();
			shape.TopRight = br.ReadPointF();

			if (point1 == shape.TopRight)
			{
				var p1 = point1;
				var p2 = point2;
				var p3 = point3;
				var p4 = point4;
				point1 = p2;
				point2 = p3;
				point3 = p4;
				point4 = p1;
			}

			// Calculate newer format values
			// TODO: Make sure this is correct, there were minor issues
			//   in Mabioned at one point.
			var points = new PointF[] { point1, point2, point3, point4 };
			var x = (points[0].X + points[2].X) * 0.5;
			var y = (points[0].Y + points[2].Y) * 0.5;

			var angle = Math.Atan2(points[1].Y - points[0].Y, points[1].X - points[0].X);

			shape.DirX1 = (float)Math.Cos(angle);
			shape.DirX2 = (float)Math.Sin(angle);
			shape.DirY1 = (float)Math.Sin(angle);
			shape.DirY2 = (float)-Math.Cos(angle);
			shape.LenX = (float)Math.Abs((points[1].X - points[0].X) * 0.5 / Math.Cos(angle));
			shape.LenY = (float)Math.Abs((points[2].Y - points[1].Y) * 0.5 / Math.Cos(angle));

			return shape;
		}

		/// <summary>
		/// Writes shape to given writer.
		/// </summary>
		/// <param name="bw"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteTo(BinaryWriter bw)
		{
			bw.Write(this.DirX1);
			bw.Write(this.DirX2);
			bw.Write(this.DirY1);
			bw.Write(this.DirY2);
			bw.Write(this.LenX);
			bw.Write(this.LenY);
			bw.Write(this.Type);
			bw.WritePointF(this.Position);
			bw.WritePointF(this.BottomLeft);
			bw.WritePointF(this.TopRight);
		}

		/// <summary>
		/// Creates and returns copy of shape.
		/// </summary>
		/// <returns></returns>
		public Shape Copy()
		{
			var shape = new Shape();

			shape.DirX1 = this.DirX1;
			shape.DirX2 = this.DirX2;
			shape.DirY1 = this.DirY1;
			shape.DirY2 = this.DirY2;
			shape.LenX = this.LenX;
			shape.LenY = this.LenY;
			shape.Type = this.Type;
			shape.Position = this.Position;
			shape.BottomLeft = this.BottomLeft;
			shape.TopRight = this.TopRight;

			return shape;
		}

		/// <summary>
		/// Returns the shape's 4 edge points, making up a polygon.
		/// </summary>
		/// <returns></returns>
		public PointF[] GetPoints()
		{
			var points = new PointF[4];

			double pX = this.Position.X;
			double pY = this.Position.Y;

			double a00 = this.DirX1 * this.LenX;
			double a01 = this.DirX2 * this.LenX;
			double a02 = this.DirY1 * this.LenY;
			double a03 = this.DirY2 * this.LenY;

			var sx1 = pX - a00 - a02; if (sx1 < pX) sx1 = Math.Ceiling(sx1);
			var sy1 = pY - a01 - a03; if (sy1 < pY) sy1 = Math.Ceiling(sy1);
			var sx2 = pX + a00 - a02; if (sx2 < pX) sx2 = Math.Ceiling(sx2);
			var sy2 = pY + a01 - a03; if (sy2 < pY) sy2 = Math.Ceiling(sy2);
			var sx3 = pX + a00 + a02; if (sx3 < pX) sx3 = Math.Ceiling(sx3);
			var sy3 = pY + a01 + a03; if (sy3 < pY) sy3 = Math.Ceiling(sy3);
			var sx4 = pX - a00 + a02; if (sx4 < pX) sx4 = Math.Ceiling(sx4);
			var sy4 = pY - a01 + a03; if (sy4 < pY) sy4 = Math.Ceiling(sy4);

			if (a02 * a01 > a03 * a00)
			{
				points[0] = new PointF((float)sx1, (float)sy1);
				points[1] = new PointF((float)sx2, (float)sy2);
				points[2] = new PointF((float)sx3, (float)sy3);
				points[3] = new PointF((float)sx4, (float)sy4);
			}
			else
			{
				points[0] = new PointF((float)sx1, (float)sy1);
				points[3] = new PointF((float)sx2, (float)sy2);
				points[2] = new PointF((float)sx3, (float)sy3);
				points[1] = new PointF((float)sx4, (float)sy4);
			}

			return points;
		}

		/// <summary>
		/// Sets the shape's rect values based on the four given points.
		/// </summary>
		/// <param name="points"></param>
		public void SetRotationFromPoints(PointF[] points)
		{
			if (points.Length != 4)
				throw new ArgumentException("Expected 4 points.");

			var x = (points[0].X + points[2].X) * 0.5;
			var y = (points[0].Y + points[2].Y) * 0.5;
			this.Position = new PointF((float)x, (float)y);

			var angle = Math.Atan2(points[1].Y - points[0].Y, points[1].X - points[0].X);

			this.DirX1 = (float)Math.Cos(angle);
			this.DirX2 = (float)Math.Sin(angle);
			this.DirY1 = (float)Math.Sin(angle);
			this.DirY2 = (float)-Math.Cos(angle);

			// For some reason the shape shrinks when the length is
			// calculated repeatedly. There's also a problem with the initial
			// rotation of some shapes though, for which we need to set the
			// length. We'll use the original length, because we only care
			// about rotation, but swap them as necessary to get the correct
			// rotation.

			var oldLenX = this.LenX;
			var oldLenY = this.LenY;
			var newLenX = (float)Math.Abs((points[1].X - points[0].X) * 0.5 / Math.Cos(angle));
			var newLenY = (float)Math.Abs((points[2].Y - points[1].Y) * 0.5 / Math.Cos(angle));
			var swap = ((oldLenX < oldLenY && newLenX > newLenY) || (oldLenX > oldLenY && newLenX < newLenY));

			this.LenX = (swap ? oldLenY : oldLenX);
			this.LenY = (swap ? oldLenX : oldLenY);

			var minX = points.Min(a => a.X);
			var maxX = points.Max(a => a.X);
			var minY = points.Min(a => a.Y);
			var maxY = points.Max(a => a.Y);

			this.BottomLeft = new PointF(minX, minY);
			this.TopRight = new PointF(maxX, maxY);
		}

		/// <summary>
		/// Returns true if the given position is within this shape.
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public bool IsInside(PointF position)
		{
			var points = this.GetPoints();

			var result = false;

			for (int i = 0, j = points.Length - 1; i < points.Length; j = i++)
			{
				if (((points[i].Y > position.Y) != (points[j].Y > position.Y)) && (position.X < (points[j].X - points[i].X) * (position.Y - points[i].Y) / (points[j].Y - points[i].Y) + points[i].X))
					result = !result;
			}

			return result;
		}

		/// <summary>
		/// Returns shape's outer bounding box.
		/// </summary>
		/// <returns></returns>
		public RectangleF GetBoundingBox()
		{
			var blX = this.BottomLeft.X;
			var blY = this.BottomLeft.Y;
			var trX = this.TopRight.X;
			var trY = this.TopRight.Y;

			var x = blX;
			var y = blY;
			var w = (trX - blX);
			var h = (trY - blY);

			return new RectangleF(x, y, w, h);
		}
	}
}
