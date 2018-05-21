using System.Collections.Generic;
using System.Drawing;

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
		/// Returns the entity's position.
		/// </summary>
		Vector3F Position { get; }

		/// <summary>
		/// Returns a list of the entity's shapes.
		/// </summary>
		List<Shape> Shapes { get; }

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
		PointF GetPoint(float scale, int? flipHeight);
	}
}
