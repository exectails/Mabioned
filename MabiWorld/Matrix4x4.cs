namespace MabiWorld
{
	/// <summary>
	/// Represents a 4x4 floating point matrix.
	/// </summary>
	public struct Matrix4x4
	{
		/// <summary>
		/// A first row and first column value.
		/// </summary>
		public float M11;

		/// <summary>
		/// A first row and second column value.
		/// </summary>
		public float M12;

		/// <summary>
		/// A first row and third column value.
		/// </summary>
		public float M13;

		/// <summary>
		/// A first row and fourth column value.
		/// </summary>
		public float M14;

		/// <summary>
		/// A second row and first column value.
		/// </summary>
		public float M21;

		/// <summary>
		/// A second row and second column value.
		/// </summary>
		public float M22;

		/// <summary>
		/// A second row and third column value.
		/// </summary>
		public float M23;

		/// <summary>
		/// A second row and fourth column value.
		/// </summary>
		public float M24;

		/// <summary>
		/// A third row and first column value.
		/// </summary>
		public float M31;

		/// <summary>
		/// A third row and second column value.
		/// </summary>
		public float M32;

		/// <summary>
		/// A third row and third column value.
		/// </summary>
		public float M33;

		/// <summary>
		/// A third row and fourth column value.
		/// </summary>
		public float M34;

		/// <summary>
		/// A fourth row and first column value.
		/// </summary>
		public float M41;

		/// <summary>
		/// A fourth row and second column value.
		/// </summary>
		public float M42;

		/// <summary>
		/// A fourth row and third column value.
		/// </summary>
		public float M43;

		/// <summary>
		/// A fourth row and fourth column value.
		/// </summary>
		public float M44;

		/// <summary>
		/// Creates a new matrix.
		/// </summary>
		/// <param name="m11"></param>
		/// <param name="m12"></param>
		/// <param name="m13"></param>
		/// <param name="m14"></param>
		/// <param name="m21"></param>
		/// <param name="m22"></param>
		/// <param name="m23"></param>
		/// <param name="m24"></param>
		/// <param name="m31"></param>
		/// <param name="m32"></param>
		/// <param name="m33"></param>
		/// <param name="m34"></param>
		/// <param name="m41"></param>
		/// <param name="m42"></param>
		/// <param name="m43"></param>
		/// <param name="m44"></param>
		public Matrix4x4(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
		{
			this.M11 = m11;
			this.M12 = m12;
			this.M13 = m13;
			this.M14 = m14;
			this.M21 = m21;
			this.M22 = m22;
			this.M23 = m23;
			this.M24 = m24;
			this.M31 = m31;
			this.M32 = m32;
			this.M33 = m33;
			this.M34 = m34;
			this.M41 = m41;
			this.M42 = m42;
			this.M43 = m43;
			this.M44 = m44;
		}
	}
}
