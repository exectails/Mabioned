using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using MabiWorld.Extensions;
using MabiWorld.PropertyEditing;

namespace MabiWorld
{
	/// <summary>
	/// Represents an event, part of an .area file.
	/// </summary>
	public class Event : IEntity
	{
		[TypeConverter(typeof(UInt64HexConverter))]
		public ulong EntityId { get; set; }

		public string Name { get; set; }

		public Vector3F Position { get; set; }

		public byte ShapeCount => (byte)this.Shapes.Count;

		public int ShapeType { get; set; }

		[Editor(typeof(NotifyingCollectionEditor), typeof(UITypeEditor))]
		public List<Shape> Shapes { get; internal set; } = new List<Shape>();

		[TypeConverter(typeof(SafeEnumConverter))]
		[Editor(typeof(SafeEnumEditor), typeof(UITypeEditor))]
		public EventType Type { get; set; }

		public byte ParameterCount => (byte)this.Parameters.Count;

		[Editor(typeof(NotifyingCollectionEditor), typeof(UITypeEditor))]
		public List<EntityParameter> Parameters { get; internal set; } = new List<EntityParameter>();

		/// <summary>
		/// Returns reference to the area the event is in.
		/// </summary>
		[Browsable(false)]
		public Area Area { get; set; }

		/// <summary>
		/// Gets or sets an object associated with this event.
		/// </summary>
		[Browsable(false)]
		public object Tag { get; set; }

		/// <summary>
		/// Creates new instance.
		/// </summary>
		public Event()
		{
		}

		/// <summary>
		/// Creates new instance.
		/// </summary>
		/// <param name="area"></param>
		public Event(Area area)
		{
			this.Area = area;
		}

		/// <summary>
		/// Reads event from reader and returns it.
		/// </summary>
		/// <param name="area"></param>
		/// <param name="br"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Event ReadFrom(Area area, BinaryReader br)
		{
			var evnt = new Event(area);

			evnt.EntityId = br.ReadUInt64();
			evnt.Name = br.ReadWString();
			evnt.Position = br.ReadVector3F_XYZ();
			var shapeCount = br.ReadByte();
			evnt.ShapeType = br.ReadInt32();

			evnt.Shapes = new List<Shape>(shapeCount);
			for (var i = 0; i < shapeCount; ++i)
			{
				var shape = Shape.ReadFrom(br);
				evnt.Shapes.Add(shape);
			}

			evnt.Type = (EventType)br.ReadInt32();

			var parameterCount = br.ReadByte();
			evnt.Parameters = new List<EntityParameter>(parameterCount);
			for (var i = 0; i < parameterCount; ++i)
			{
				var param = EntityParameter.ReadFrom(br);
				evnt.Parameters.Add(param);
			}

			return evnt;
		}

		/// <summary>
		/// Writes event to given writer.
		/// </summary>
		/// <param name="bw"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteTo(BinaryWriter bw)
		{
			bw.Write(this.EntityId);
			bw.WriteWString(this.Name);
			bw.WriteVector3F_XYZ(this.Position);
			bw.Write(this.ShapeCount);
			bw.Write(this.ShapeType);

			for (var i = 0; i < this.Shapes.Count; ++i)
				this.Shapes[i].WriteTo(bw);

			bw.Write((int)this.Type);

			bw.Write(this.ParameterCount);
			for (var i = 0; i < this.Parameters.Count; ++i)
				this.Parameters[i].WriteTo(bw);
		}

		/// <summary>
		/// Creates and returns copy of this event.
		/// </summary>
		public Event Copy()
		{
			var evnt = new Event();

			evnt.EntityId = this.EntityId;
			evnt.Name = this.Name;
			evnt.Position = this.Position;
			evnt.ShapeType = this.ShapeType;

			evnt.Shapes = new List<Shape>(this.Shapes.Count);
			for (var i = 0; i < this.Shapes.Count; ++i)
				evnt.Shapes.Add(this.Shapes[i].Copy());

			evnt.Type = this.Type;

			evnt.Parameters = new List<EntityParameter>(this.Parameters.Count);
			for (var i = 0; i < this.Parameters.Count; ++i)
				evnt.Parameters.Add(this.Parameters[i].Copy());

			return evnt;
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
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public void MoveTo(float x, float y, float z)
		{
			var delta = new SizeF(x - this.Position.X, y - this.Position.Y);

			this.Position = new Vector3F(x, y, z);

			foreach (var shape in this.Shapes)
			{
				shape.Position += delta;
				shape.BottomLeft += delta;
				shape.TopRight += delta;
			}
		}

		/// <summary>
		/// Returns string representation of event.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Event [Type: {1}, EntityId: 0x{0:X16}]", this.EntityId, this.Type);
		}
	}
}
