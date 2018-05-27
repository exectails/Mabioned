using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace MabiWorld.Data
{
	/// <summary>
	/// Represents an entry in "data/db/propdb.xml::PropDB/PropClassList".
	/// </summary>
	public class PropDbEntry
	{
		public int ClassID { get; internal set; }
		public string ClassName { get; internal set; }
		public string ClassPath { get; internal set; }
		public int ColorType { get; internal set; }
		public string Events { get; internal set; }
		public string Actions { get; internal set; }
		public bool HasState { get; internal set; }
		public string Name { get; internal set; }
		public bool ShowName { get; internal set; }
		public bool IsHugeProp { get; internal set; }
		public bool UsedServer { get; internal set; }
		public bool NonTrivialAnimation { get; internal set; }
		public bool PickRestrict { get; internal set; }
		public int[] SoundDescIDs { get; internal set; }
		public string StringID { get; internal set; }
	}

	/// <summary>
	/// Represents data's propdb.xml.
	/// </summary>
	public static class PropDb
	{
		private static Dictionary<int, PropDbEntry> _entries = new Dictionary<int, PropDbEntry>();

		/// <summary>
		/// Returns entry with given class id via out if it exists, or null
		/// if it doesn't.
		/// </summary>
		/// <param name="classId"></param>
		/// <param name="entry"></param>
		public static bool TryGetEntry(int classId, out PropDbEntry entry)
		{
			return _entries.TryGetValue(classId, out entry);
		}

		/// <summary>
		/// Loads prop data from given XML file.
		/// </summary>
		/// <param name="filePath"></param>
		public static void Load(string filePath)
		{
			if (Path.GetFileName(filePath) != "propdb.xml")
				throw new ArgumentException("Expected file called propdb.xml.");

			using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
				Load(fs);
		}

		/// <summary>
		/// Loads prop data from given XML file.
		/// </summary>
		/// <param name="filePath"></param>
		public static void Load(Stream stream)
		{
			using (var xmlReader = new XmlTextReader(stream))
			{
				while (xmlReader.ReadToFollowing("PropClass"))
				{
					var entry = new PropDbEntry();

					entry.ClassID = int.Parse(xmlReader.GetAttribute("ClassID"));
					entry.ClassName = xmlReader.GetAttribute("ClassName");
					entry.ClassPath = xmlReader.GetAttribute("ClassPath");
					entry.ColorType = int.Parse(xmlReader.GetAttribute("ColorType"));
					entry.Events = xmlReader.GetAttribute("Events");
					entry.Actions = xmlReader.GetAttribute("Actions");
					entry.HasState = (xmlReader.GetAttribute("HasState") == "true");
					entry.Name = xmlReader.GetAttribute("Name");
					entry.ShowName = (xmlReader.GetAttribute("ShowName") == "true");
					entry.IsHugeProp = (xmlReader.GetAttribute("IsHugeProp") == "true");
					entry.UsedServer = (xmlReader.GetAttribute("UsedServer") == "true");
					entry.NonTrivialAnimation = (xmlReader.GetAttribute("NonTrivialAnimation") == "true");
					entry.PickRestrict = (xmlReader.GetAttribute("PickRestrict") == "true");
					entry.StringID = xmlReader.GetAttribute("StringID");

					var soundDescIDs = xmlReader.GetAttribute("SoundDescIDs");
					if (soundDescIDs != null)
					{
						var split = soundDescIDs.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
						var ids = new int[split.Length];

						for (var i = 0; i < split.Length; ++i)
							ids[i] = int.Parse(split[i]);

						entry.SoundDescIDs = ids;
					}

					_entries[entry.ClassID] = entry;
				}
			}
		}
	}
}
