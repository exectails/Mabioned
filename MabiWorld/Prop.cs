using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using MabiWorld.Data;
using MabiWorld.Extensions;
using MabiWorld.PropertyEditing;

namespace MabiWorld
{
	/// <summary>
	/// Represents a prop, a part of an .area file.
	/// </summary>
	public class Prop : IEntity
	{
		public const int ColorCount = 9;
		public static readonly Color DefaultColor = Color.FromArgb(0x00808080);

		public int Id { get; set; }

		[TypeConverter(typeof(UInt64HexConverter))]
		public ulong EntityId { get; set; }

		public string Name { get; set; }

		/// <summary>
		/// Filled from loaded prop db, not part of the actual prop struct.
		/// </summary>
		[ReadOnly(true)]
		public string ClassName { get; private set; }

		/// <summary>
		/// Filled from loaded prop db, not part of the actual prop struct.
		/// </summary>
		[ReadOnly(true)]
		public string StringId { get; private set; }

		public Vector3F Position { get; set; }
		public byte ShapeCount => (byte)this.Shapes.Count;

		[DefaultValue(0)]
		public int ShapeType { get; set; }

		[Editor(typeof(NotifyingCollectionEditor), typeof(UITypeEditor))]
		public List<Shape> Shapes { get; internal set; } = new List<Shape>();

		[Description("If true and prop has shapes, they are treated as walls.")]
		[DefaultValue(true)]
		public bool IsCollision { get; set; } = true;

		[DefaultValue(false)]
		public bool FixedAltitude { get; set; }

		[DefaultValue(1f)]
		public float Scale { get; set; } = 1;

		[DefaultValue(0f)]
		public float Rotation { get; set; } = 0;

		public Vector3F BottomLeft { get; set; }
		public Vector3F TopRight { get; set; }

		[DefaultValue(typeof(Color), "FFFFFFFF")]
		public Color ColorOverride { get; set; } = Color.FromArgb(-1);

		public Color[] Colors { get; set; } = new Color[ColorCount] { DefaultColor, DefaultColor, DefaultColor, DefaultColor, DefaultColor, DefaultColor, DefaultColor, DefaultColor, DefaultColor };

		public string Title { get; set; }
		public string State { get; set; }
		public byte ParameterCount => (byte)this.Parameters.Count;

		[Editor(typeof(NotifyingCollectionEditor), typeof(UITypeEditor))]
		public List<EntityParameter> Parameters { get; internal set; } = new List<EntityParameter>();

		/// <summary>
		/// Returns reference to the area the prop is in.
		/// </summary>
		[Browsable(false)]
		public Area Area { get; set; }

		/// <summary>
		/// Gets or sets an object associated with this prop.
		/// </summary>
		[Browsable(false)]
		public object Tag { get; set; }

		/// <summary>
		/// Creates new instance.
		/// </summary>
		public Prop()
		{
		}

		/// <summary>
		/// Creates new instance.
		/// </summary>
		public Prop(Area area)
		{
			this.Area = area;
		}

		/// <summary>
		/// Reads prop from reader and returns it.
		/// </summary>
		/// <param name="area"></param>
		/// <param name="br"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Prop ReadFrom(Area area, BinaryReader br)
		{
			var prop = new Prop(area);

			prop.Id = br.ReadInt32();
			prop.EntityId = br.ReadUInt64();
			prop.Name = br.ReadWString();
			prop.Position = br.ReadVector3F_XYZ();
			var shapeCount = br.ReadByte();
			prop.ShapeType = br.ReadInt32();

			prop.Shapes = new List<Shape>(shapeCount);
			for (var i = 0; i < shapeCount; ++i)
			{
				var shape = Shape.ReadFrom(br);
				prop.Shapes.Add(shape);
			}

			prop.IsCollision = br.ReadBoolean();
			prop.FixedAltitude = br.ReadBoolean();
			prop.Scale = br.ReadSingle();
			prop.Rotation = br.ReadSingle();
			prop.BottomLeft = br.ReadVector3F_XZY();
			prop.TopRight = br.ReadVector3F_XZY();
			prop.ColorOverride = br.ReadColor();

			for (var i = 0; i < ColorCount; ++i)
				prop.Colors[i] = br.ReadColor();

			prop.Title = br.ReadWString();
			prop.State = br.ReadWString();

			var parameterCount = br.ReadByte();
			prop.Parameters = new List<EntityParameter>(parameterCount);
			for (var i = 0; i < parameterCount; ++i)
			{
				var param = EntityParameter.ReadFrom(br);
				prop.Parameters.Add(param);
			}

			prop.LoadData();

			return prop;
		}

		/// <summary>
		/// Loads data from prop db, filling additional info properties.
		/// </summary>
		public void LoadData()
		{
			if (PropDb.TryGetEntry(this.Id, out var entry))
			{
				this.ClassName = entry.ClassName;
				this.StringId = entry.StringID;
			}
		}

		/// <summary>
		/// Writes prop to given reader.
		/// </summary>
		/// <param name="bw"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteTo(BinaryWriter bw)
		{
			bw.Write(this.Id);
			bw.Write(this.EntityId);
			bw.WriteWString(this.Name);
			bw.WriteVector3F_XYZ(this.Position);
			bw.Write(this.ShapeCount);
			bw.Write(this.ShapeType);

			for (var i = 0; i < this.Shapes.Count; ++i)
				this.Shapes[i].WriteTo(bw);

			bw.Write(this.IsCollision);
			bw.Write(this.FixedAltitude);
			bw.Write(this.Scale);
			bw.Write(this.Rotation);
			bw.WriteVector3F_XZY(this.BottomLeft);
			bw.WriteVector3F_XZY(this.TopRight);
			bw.WriteColor(this.ColorOverride);

			for (var i = 0; i < ColorCount; ++i)
				bw.WriteColor(this.Colors[i]);

			bw.WriteWString(this.Title);
			bw.WriteWString(this.State);

			bw.Write(this.ParameterCount);
			for (var i = 0; i < this.Parameters.Count; ++i)
				this.Parameters[i].WriteTo(bw);
		}

		/// <summary>
		/// Creates and returns copy of prop.
		/// </summary>
		/// <returns></returns>
		public Prop Copy()
		{
			var prop = new Prop();

			prop.Id = this.Id;
			prop.EntityId = this.EntityId;
			prop.Name = this.Name;
			prop.Position = this.Position;
			prop.ShapeType = this.ShapeType;

			prop.Shapes = new List<Shape>(this.Shapes.Count);
			for (var i = 0; i < this.Shapes.Count; ++i)
				prop.Shapes.Add(this.Shapes[i].Copy());

			prop.IsCollision = this.IsCollision;
			prop.FixedAltitude = this.FixedAltitude;
			prop.Scale = this.Scale;
			prop.Rotation = this.Rotation;
			prop.BottomLeft = this.BottomLeft;
			prop.TopRight = this.TopRight;
			prop.ColorOverride = this.ColorOverride;

			for (var i = 0; i < ColorCount; ++i)
			{
				if (this.Colors[i].ToArgb() != 0)
					prop.Colors[i] = this.Colors[i];
			}

			prop.Title = this.Title;
			prop.State = this.State;

			prop.Parameters = new List<EntityParameter>(this.Parameters.Count);
			for (var i = 0; i < this.Parameters.Count; ++i)
				prop.Parameters.Add(this.Parameters[i].Copy());

			return prop;
		}

		/// <summary>
		/// Sets prop's position and updates its shapes.
		/// </summary>
		/// <param name="pos"></param>
		public void MoveTo(PointF pos)
		{
			this.MoveTo(pos.X, pos.Y, this.Position.Z);
		}

		/// <summary>
		/// Sets prop's position and updates its shapes.
		/// </summary>
		/// <param name="pos"></param>
		public void MoveTo(float x, float y, float z)
		{
			var delta = new SizeF(x - this.Position.X, y - this.Position.Y);

			this.Position = new Vector3F(x, y, z);
			this.BottomLeft += delta;
			this.TopRight += delta;

			foreach (var shape in this.Shapes)
			{
				shape.Position += delta;
				shape.BottomLeft += delta;
				shape.TopRight += delta;
			}
		}

		/// <summary>
		/// Returns string representation of prop.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Prop [Id: {1}, EntityId: 0x{0:X16}]", this.EntityId, this.Id);
		}
	}
}
