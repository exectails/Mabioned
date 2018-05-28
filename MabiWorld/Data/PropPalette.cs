using System;
using System.Collections.Generic;
using System.IO;

namespace MabiWorld.Data
{
	/// <summary>
	/// Represents "data/db/world/proppalette.plt", which contains default
	/// values for props, such as colors and shapes.
	/// </summary>
	public static class PropPalette
	{
		private static Dictionary<int, Prop> _entries = new Dictionary<int, Prop>();

		/// <summary>
		/// Returns the entry for the given id via out. The method returns
		/// false if no entry was found.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="prop"></param>
		/// <returns></returns>
		public static bool TryGetEntry(int id, out Prop prop)
		{
			return _entries.TryGetValue(id, out prop);
		}

		/// <summary>
		/// Loads entries from file.
		/// </summary>
		/// <param name="filePath"></param>
		public static void Load(string filePath)
		{
			if (Path.GetFileName(filePath) != "proppalette.plt")
				throw new ArgumentException("Expected file named 'proppalette.plt'.");

			using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
				Load(fs);
		}

		/// <summary>
		/// Loads entries from stream.
		/// </summary>
		/// <param name="stream"></param>
		public static void Load(Stream stream)
		{
			using (var br = new BinaryReader(stream))
			{
				var version = br.ReadInt32();
				var count = br.ReadInt32();

				for (var i = 0; i < count; ++i)
				{
					var prop = Prop.ReadFrom(null, br);
					_entries[prop.Id] = prop;
				}
			}
		}
	}
}
