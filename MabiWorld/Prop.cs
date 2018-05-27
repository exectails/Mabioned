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
		public int ShapeType { get; set; }

		[Editor(typeof(NotifyingCollectionEditor), typeof(UITypeEditor))]
		public List<Shape> Shapes { get; internal set; } = new List<Shape>();

		public bool IsCollision { get; set; }
		public bool FixedAltitude { get; set; }
		public float Scale { get; set; }
		public float Rotation { get; set; }
		public Vector3F BottomLeft { get; set; }
		public Vector3F TopRight { get; set; }
		public Color ColorOverride { get; set; }
		public Color[] Colors { get; set; } = new Color[ColorCount];

		public string Title { get; set; }
		public string State { get; set; }
		public byte ParameterCount => (byte)this.Parameters.Count;

		[Editor(typeof(NotifyingCollectionEditor), typeof(UITypeEditor))]
		public List<EntityParameter> Parameters { get; internal set; } = new List<EntityParameter>();

		/// <summary>
		/// Returns reference to the area the event is in.
		/// </summary>
		[Browsable(false)]
		public Area Area { get; }

		/// <summary>
		/// Gets or sets an object associated with this prop.
		/// </summary>
		[Browsable(false)]
		public object Tag { get; set; }

		/// <summary>
		/// Creates new instance.
		/// </summary>
		/// <param name="area"></param>
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

			if (PropDb.TryGetEntry(prop.Id, out var entry))
			{
				prop.ClassName = entry.ClassName;
				prop.StringId = entry.StringID;
			}

			return prop;
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
		/// Returns string representation of prop.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Prop [Id: {1}, EntityId: 0x{0:X16}]", this.EntityId, this.Id);
		}
	}
}
