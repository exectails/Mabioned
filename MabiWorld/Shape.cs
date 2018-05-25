using MabiWorld.Extensions;
using MabiWorld.PropertyEditing;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;

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
		/// Returns the shape's 4 edge points, making up a polygon.
		/// </summary>
		/// <remarks>
		/// Since Mabi's coordinate system starts at the lower left,
		/// but many 2D programs usually start at the top left,
		/// flipHeight allows to directly get the correct Y coordinate.
		/// </remarks>
		/// <param name="scale"></param>
		/// <param name="flipHeight"></param>
		/// <returns></returns>
		public PointF[] GetPoints(float scale, int? flipHeight)
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

			for (var i = 0; i < points.Length; ++i)
			{
				if (flipHeight != null)
				{
					points[i].Y = (float)(flipHeight - points[i].Y);
				}

				if (scale != 1)
				{
					points[i].X /= scale;
					points[i].Y /= scale;
				}
			}

			return points;
		}

		/// <summary>
		/// Sets the shape's values based on the four given points.
		/// </summary>
		/// <param name="points"></param>
		public void SetFromPoints(PointF[] points)
		{
			if (points.Length != 4)
				throw new ArgumentException("Expected 4 points.");

			var x = (points[0].X + points[2].X) * 0.5;
			var y = (points[0].Y + points[2].Y) * 0.5;

			var angle = Math.Atan2(points[1].Y - points[0].Y, points[1].X - points[0].X);

			this.Position = new PointF((float)x, (float)y);
			this.DirX1 = (float)Math.Cos(angle);
			this.DirX2 = (float)Math.Sin(angle);
			this.DirY1 = (float)Math.Sin(angle);
			this.DirY2 = (float)-Math.Cos(angle);
			this.LenX = (float)Math.Abs((points[1].X - points[0].X) * 0.5 / Math.Cos(angle));
			this.LenY = (float)Math.Abs((points[2].Y - points[1].Y) * 0.5 / Math.Cos(angle));

		}

		/// <summary>
		/// Returns true if the given point is within this shape.
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public bool IsInside(PointF point)
		{
			var points = this.GetPoints(1, null);

			var result = false;

			for (int i = 0, j = points.Length - 1; i < points.Length; j = i++)
			{
				if (((points[i].Y > point.Y) != (points[j].Y > point.Y)) && (point.X < (points[j].X - points[i].X) * (point.Y - points[i].Y) / (points[j].Y - points[i].Y) + points[i].X))
					result = !result;
			}

			return result;
		}

		/// <summary>
		/// Returns shape's outer bounding box.
		/// </summary>
		/// <param name="scale"></param>
		/// <param name="flipHeight"></param>
		/// <returns></returns>
		public Rectangle GetBoundingBox(float scale, int? flipHeight)
		{
			var blX = this.BottomLeft.X;
			var blY = this.BottomLeft.Y;
			var trX = this.TopRight.X;
			var trY = this.TopRight.Y;

			var x = blX;
			var y = blY;
			var w = (trX - blX);
			var h = (trY - blY);

			if (flipHeight != null)
			{
				y = ((float)flipHeight - y - h);
			}

			if (scale != 1)
			{
				x /= scale;
				y /= scale;
				w /= scale;
				h /= scale;
			}

			return new Rectangle((int)x, (int)y, (int)w, (int)h);
		}
	}
}
