using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using MabiWorld.PropertyEditing;

namespace MabiWorld
{
	/// <summary>
	/// Represents a position in 3D space.
	/// </summary>
	[TypeConverter(typeof(Vector3FConverter))]
	public struct Vector3F
	{
		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }

		/// <summary>
		/// Creates new instance.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public Vector3F(float x, float y, float z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		/// <summary>
		/// Creates new instance.
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="z"></param>
		public Vector3F(PointF pos, float z)
		{
			this.X = pos.X;
			this.Y = pos.Y;
			this.Z = z;
		}

		/// <summary>
		/// Returns string representation of this instance.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "VectorF3 [X={0:0.00}, Y={1:0.00}, Z={2:0.00}]", this.X, this.Y, this.Z);
		}

		/// <summary>
		/// Implicitly converts vector to PointF, using its X and Y
		/// coordinates.
		/// </summary>
		/// <param name="val"></param>
		public static implicit operator PointF(Vector3F val)
		{
			return new PointF(val.X, val.Y);
		}

		/// <summary>
		/// Implicitly converts vector to PointF, using its X and Y
		/// coordinates.
		/// </summary>
		/// <param name="val"></param>
		public static implicit operator Point(Vector3F val)
		{
			return new Point((int)val.X, (int)val.Y);
		}

		/// <summary>
		/// Implicitly converts vector to SizeF, using its X and Y
		/// coordinates.
		/// </summary>
		/// <param name="val"></param>
		public static implicit operator SizeF(Vector3F val)
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
		public static Vector3F operator -(Vector3F val1, Vector3F val2)
		{
			return new Vector3F(val1.X - val2.X, val1.Y - val2.Y, val1.Z - val2.Z);
		}

		/// <summary>
		/// Returns new vector with the values of val1 added to the
		/// ones in val2.
		/// </summary>
		/// <param name="val1"></param>
		/// <param name="val2"></param>
		/// <returns></returns>
		public static Vector3F operator +(Vector3F val1, Vector3F val2)
		{
			return new Vector3F(val1.X + val2.X, val1.Y + val2.Y, val1.Z + val2.Z);
		}

		/// <summary>
		/// Returns new vector with the values of val1 subtracted by the
		/// ones in val2.
		/// </summary>
		/// <param name="val1"></param>
		/// <param name="val2"></param>
		/// <returns></returns>
		public static Vector3F operator -(Vector3F val1, SizeF val2)
		{
			return new Vector3F(val1.X - val2.Width, val1.Y - val2.Height, val1.Z);
		}

		/// <summary>
		/// Returns new vector with the values of val1 added to the
		/// ones in val2.
		/// </summary>
		/// <param name="val1"></param>
		/// <param name="val2"></param>
		/// <returns></returns>
		public static Vector3F operator +(Vector3F val1, SizeF val2)
		{
			return new Vector3F(val1.X + val2.Width, val1.Y + val2.Height, val1.Z);
		}
	}
}
