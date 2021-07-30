using System;
using System.Collections.Generic;
using System.IO;
using DataDogLib;

namespace MabiWorld.Data
{
	/// <summary>
	/// Represents an entry in the tile index db.
	/// </summary>
	public class TileIndexEntry
	{
		/// <summary>
		/// Gets or sets the tile's id.
		/// </summary>
		public int TileID { get; set; }

		/// <summary>
		/// Gets or sets the tile's name.
		/// </summary>
		public string TileName { get; set; }

		/// <summary>
		/// Gets or sets the billboard objects that are to be placed on
		/// this tile, such as grass.
		/// </summary>
		public string BillBoardName { get; set; }

		/// <summary>
		/// Gets or sets the property (?).
		/// </summary>
		public byte Property { get; set; }
	}

	/// <summary>
	/// Represents "data/db/tileindex.data", which contains material
	/// information for terrains.
	/// </summary>
	public static class TileIndex
	{
		private static readonly Dictionary<int, TileIndexEntry> _entries = new Dictionary<int, TileIndexEntry>();

		/// <summary>
		/// Returns the tile with the given id via out. Returns false if
		/// the tile id doesn't exist.
		/// </summary>
		/// <param name="tileId"></param>
		/// <param name="tileIndexEntry"></param>
		/// <returns></returns>
		public static bool TryGet(int tileId, out TileIndexEntry tileIndexEntry)
		{
			return _entries.TryGetValue(tileId, out tileIndexEntry);
		}

		/// <summary>
		/// Loads tiles from given stream of a data dog file.
		/// </summary>
		/// <param name="stream"></param>
		public static void Load(Stream stream)
		{
			var dataDogFile = DataDogFile.Read(stream);

			if (!dataDogFile.Lists.TryGetValue("TileList", out var objList))
				throw new ArgumentException($"DataDog object list 'TileList' not found in file.");

			foreach (var obj in objList.Objects)
			{
				var entry = new TileIndexEntry();

				entry.TileID = (int)obj.Fields["TileID"].Value;
				entry.TileName = (string)obj.Fields["TileName"].Value;
				entry.BillBoardName = (string)obj.Fields["BillBoardName"].Value;

				// Conditional reading of Property, as it doesn't exist in
				// KR281.
				if (obj.Fields.TryGetValue("Property", out var property))
					entry.Property = (byte)property.Value;

				_entries[entry.TileID] = entry;
			}
		}
	}
}
