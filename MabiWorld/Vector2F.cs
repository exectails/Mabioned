using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using MabiWorld.PropertyEditing;

namespace MabiWorld
{
	/// <summary>
	/// Represents a position in 2D space.
	/// </summary>
	[TypeConverter(typeof(Vector3FConverter))]
	public struct Vector2F
	{
		public float X { get; set; }
		public float Y { get; set; }

		/// <summary>
		/// Creates new instance.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public Vector2F(float x, float y)
		{
			this.X = x;
			this.Y = y;
		}

		/// <summary>
		/// Creates new instance.
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="z"></param>
		public Vector2F(PointF pos)
		{
			this.X = pos.X;
			this.Y = pos.Y;
		}

		/// <summary>
		/// Returns string representation of this instance.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Vector2F [X={0:0.00}, Y={1:0.00}]", this.X, this.Y);
		}

		/// <summary>
		/// Implicitly converts vector to PointF, using its X and Y
		/// coordinates.
		/// </summary>
		/// <param name="val"></param>
		public static implicit operator PointF(Vector2F val)
		{
			return new PointF(val.X, val.Y);
		}

		/// <summary>
		/// Implicitly converts vector to PointF, using its X and Y
		/// coordinates.
		/// </summary>
		/// <param name="val"></param>
		public static implicit operator Point(Vector2F val)
		{
			return new Point((int)val.X, (int)val.Y);
		}

		/// <summary>
		/// Implicitly converts vector to SizeF, using its X and Y
		/// coordinates.
		/// </summary>
		/// <param name="val"></param>
		public static implicit operator SizeF(Vector2F val)
		{
			return new SizeF(val.X, val.Y);
		}

		/// <summary>
		/// Returns new vector with the values of val1 subtracted by the
		/// ones in val2.
		/// </summary>
		/// <param name="val1"></param>
		/// <param name="val2"></param>
		/// <returns></returns>
		public static Vector2F operator -(Vector2F val1, Vector2F val2)
		{
			return new Vector2F(val1.X - val2.X, val1.Y - val2.Y);
		}

		/// <summary>
		/// Returns new vector with the values of val1 added to the
		/// ones in val2.
		/// </summary>
		/// <param name="val1"></param>
		/// <param name="val2"></param>
		/// <returns></returns>
		public static Vector2F operator +(Vector2F val1, Vector2F val2)
		{
			return new Vector2F(val1.X + val2.X, val1.Y + val2.Y);
		}

		/// <summary>
		/// Returns new vector with the values of val1 subtracted by the
		/// ones in val2.
		/// </summary>
		/// <param name="val1"></param>
		/// <param name="val2"></param>
		/// <returns></returns>
		public static Vector2F operator -(Vector2F val1, SizeF val2)
		{
			return new Vector2F(val1.X - val2.Width, val1.Y - val2.Height);
		}

		/// <summary>
		/// Returns new vector with the values of val1 added to the
		/// ones in val2.
		/// </summary>
		/// <param name="val1"></param>
		/// <param name="val2"></param>
		/// <returns></returns>
		public static Vector2F operator +(Vector2F val1, SizeF val2)
		{
			return new Vector2F(val1.X + val2.Width, val1.Y + val2.Height);
		}
	}
}
