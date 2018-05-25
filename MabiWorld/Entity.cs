using System.Collections.Generic;

namespace MabiWorld
{
	/// <summary>
	/// A map entity, such as a prop or event.
	/// </summary>
	public interface IEntity
	{
		/// <summary>
		/// Returns the area the entity is in.
		/// </summary>
		Area Area { get; }

		/// <summary>
		/// Returns the entity's unique id.
		/// </summary>
		ulong EntityId { get; }

		/// <summary>
		/// Gets or sets the entity's position.
		/// </summary>
		Vector3F Position { get; set; }

		/// <summary>
		/// Returns a list of the entity's shapes.
		/// </summary>
		List<Shape> Shapes { get; }

		/// <summary>
		/// Returns an object associated with the entity.
		/// </summary>
		object Tag { get; }
	}
}
