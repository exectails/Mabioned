using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;

namespace MabiWorld.Data
{
	/// <summary>
	/// Represents a "MinimapInfo" entry in "data/db/minimapinfo.xml".
	/// </summary>
	public class MiniMapInfoEntry
	{
		public int RegionID { get; internal set; }
		public string feature { get; internal set; }
		public string MapName { get; internal set; }
		public string MapLocalName { get; internal set; }
		public int MapWidth { get; internal set; }
		public int MapHeight { get; internal set; }
		public string MapFile { get; internal set; }
		public int MapImageWidth { get; internal set; }
		public int MapImageHeight { get; internal set; }
		public int MapOffsetX { get; internal set; }
		public int MapOffsetY { get; internal set; }

		// ...
	}

	/// <summary>
	/// Represents db/minimapinfo.xml.
	/// </summary>
	public static class MiniMapInfo
	{
		private static Dictionary<int, MiniMapInfoEntry> _entries = new Dictionary<int, MiniMapInfoEntry>();

		/// <summary>
		/// Returns entry with given region id via out if it exists.
		/// Returns true if entry was found.
		/// </summary>
		/// <param name="regionId"></param>
		/// <param name="entry"></param>
		/// <returns></returns>
		public static bool TryGetEntry(int regionId, out MiniMapInfoEntry entry)
		{
			return _entries.TryGetValue(regionId, out entry);
		}

		/// <summary>
		/// Loads entries from given XML file.
		/// </summary>
		/// <param name="filePath"></param>
		public static void Load(string filePath)
		{
			if (Path.GetFileName(filePath) != "minimapinfo.xml")
				throw new ArgumentException("Expected file called minimapinfo.xml.");

			using (var xmlReader = new XmlTextReader(filePath))
			{
				xmlReader.ReadToFollowing("FieldMapInfoList");

				while (xmlReader.ReadToFollowing("MinimapInfo"))
				{
					var regionId = int.Parse(xmlReader.GetAttribute("RegionID"));
					var featureName = xmlReader.GetAttribute("feature");

					// Ignore featureless entries when we already have one
					var isFeatureEmpty = string.IsNullOrWhiteSpace(featureName);
					if (_entries.ContainsKey(regionId) && isFeatureEmpty)
						continue;

					// Ignore entry if feature is not enabled
					if (!isFeatureEmpty && !Features.IsEnabled(featureName))
						continue;

					var entry = new MiniMapInfoEntry();

					entry.RegionID = regionId;
					entry.feature = featureName;
					entry.MapName = xmlReader.GetAttribute("MapName");
					entry.MapLocalName = xmlReader.GetAttribute("MapLocalName");
					entry.MapWidth = int.Parse(xmlReader.GetAttribute("MapWidth"));
					entry.MapHeight = int.Parse(xmlReader.GetAttribute("MapHeight"));
					entry.MapFile = xmlReader.GetAttribute("MapFile");
					entry.MapImageWidth = int.Parse(xmlReader.GetAttribute("MapImageWidth"));
					entry.MapImageHeight = int.Parse(xmlReader.GetAttribute("MapImageHeight"));
					entry.MapOffsetX = int.Parse(xmlReader.GetAttribute("MapOffsetX"));
					entry.MapOffsetY = int.Parse(xmlReader.GetAttribute("MapOffsetY"));

					entry.MapName = Local.GetString(entry.MapName);
					entry.MapLocalName = Local.GetString(entry.MapLocalName);
					entry.MapFile = Local.GetString(entry.MapFile);

					_entries[entry.RegionID] = entry;
				}
			}
		}
	}
}
