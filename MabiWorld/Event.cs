using MabiWorld.Extensions;
using MabiWorld.PropertyEditing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Runtime.CompilerServices;

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
		public Area Area { get; }

		/// <summary>
		/// Gets or sets an object associated with this event.
		/// </summary>
		[Browsable(false)]
		public object Tag { get; set; }

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
		/// Returns the position as a point, adjusted for scale and
		/// flip height.
		/// </summary>
		/// <remarks>
		/// Since Mabi's coordinate system starts at the lower left,
		/// but many 2D programs usually start at the top left,
		/// flipHeight allows to directly get the correct Y coordinate.
		/// </remarks>
		/// <param name="scale"></param>
		/// <param name="flipHeight"></param>
		/// <returns></returns>
		public PointF GetPoint(float scale, int? flipHeight)
		{
			var pos = this.Position;

			if (flipHeight != null)
				pos.Y = (float)(flipHeight - pos.Y);

			if (scale != 1)
			{
				pos.X /= scale;
				pos.Y /= scale;
			}

			return new PointF(pos.X, pos.Y);
		}

		/// <summary>
		/// Returns string representation of event.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("Event: 0x{0:X16}", this.EntityId);
		}
	}
}
