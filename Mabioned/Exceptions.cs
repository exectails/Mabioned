using System;

namespace Mabioned
{
	/// <summary>
	/// Thrown when no new entity id could be found for an entity.
	/// </summary>
	public class NoEntityIdException : Exception
	{
		/// <summary>
		/// Creates new instance.
		/// </summary>
		/// <param name="message"></param>
		public NoEntityIdException(string message) : base(message)
		{
		}
	}
}
