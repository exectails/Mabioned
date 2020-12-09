using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

namespace MabiWorld.Data
{
	/// <summary>
	/// Represents "data/db/world/proppalette.plt", which contains default
	/// values for props, such as colors and shapes.
	/// </summary>
	public static class PropPalette
	{
		private static List<Prop> _entries = new List<Prop>();
		private static int _version;

		/// <summary>
		/// Returns true if an entry with the given prop id exists.
		/// </summary>
		/// <param name="propId"></param>
		/// <returns></returns>
		public static bool Exists(int propId)
		{
			for (var i = 0; i < _entries.Count; i++)
			{
				var entry = _entries[i];
				if (entry.Id == propId)
					return true;
			}

			return false;
		}

		/// <summary>
		/// Returns the first entry for the given id via out. The method
		/// returns false if no entry was found.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="prop"></param>
		/// <returns></returns>
		public static bool TryGetEntry(int propId, out Prop prop)
		{
			for (var i = 0; i < _entries.Count; i++)
			{
				var entry = _entries[i];
				if (entry.Id == propId)
				{
					prop = entry;
					return true;
				}
			}

			prop = null;
			return false;
		}

		/// <summary>
		/// Returns a list fo all entries.
		/// </summary>
		/// <returns></returns>
		public static List<Prop> GetEntries()
		{
			return _entries.ToList();
		}

		/// <summary>
		/// Adds prop to entries, replaces existing entries with the same
		/// prop id.
		/// </summary>
		/// <param name="prop"></param>
		public static void Add(Prop prop)
		{
			_entries.Add(prop);
		}

		/// <summary>
		/// Removes prop from entries, returns false if the prop didn't
		/// exist.
		/// </summary>
		/// <param name="prop"></param>
		/// <returns></returns>
		public static bool Remove(int propId)
		{
			if (!TryGetEntry(propId, out var prop))
				return false;

			Remove(prop);
			return true;
		}

		/// <summary>
		/// Removes prop from entries, returns false if the prop didn't
		/// exist.
		/// </summary>
		/// <param name="prop"></param>
		/// <returns></returns>
		public static bool Remove(Prop prop)
		{
			return _entries.Remove(prop);
		}

		/// <summary>
		/// Removes all entries.
		/// </summary>
		public static void Clear()
		{
			_entries.Clear();
		}

		/// <summary>
		/// Loads entries from file.
		/// </summary>
		/// <param name="filePath"></param>
		public static void Load(string filePath)
		{
			if (Path.GetExtension(filePath) != ".plt")
				throw new ArgumentException("Expected '.plt' extension.");

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
				_version = br.ReadInt32();
				var count = br.ReadInt32();

				for (var i = 0; i < count; ++i)
				{
					var prop = Prop.ReadFrom(null, br);
					_entries.Add(prop);
				}
			}
		}

		/// <summary>
		/// Writes entries to given file.
		/// </summary>
		/// <param name="filePath"></param>
		public static void Save(string filePath)
		{
			using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
				Save(fs);
		}

		/// <summary>
		/// Writes entries to given stream.
		/// </summary>
		/// <param name="stream"></param>
		public static void Save(Stream stream)
		{
			using (var bw = new BinaryWriter(stream))
			{
				bw.Write(_version);
				bw.Write(_entries.Count);

				foreach (var entry in _entries)
					entry.WriteTo(bw);
			}
		}
	}
}
