using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
		public bool IsTerrainBlock { get; internal set; }
		public string ExtraXML { get; internal set; }
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
		public Tags StringID { get; internal set; }

		/// <summary>
		/// Custom field, part of some prop's ExtraXML.
		/// </summary>
		public string Feature { get; internal set; }
	}

	/// <summary>
	/// Represents data's propdb.xml.
	/// </summary>
	public static class PropDb
	{
		private readonly static Regex ExtraXmlFeatureRegex = new Regex(@"feature\s*=""(?<feature>[^""]+)""", RegexOptions.Compiled);

		private static Dictionary<int, PropDbEntry> _entries = new Dictionary<int, PropDbEntry>();

		/// <summary>
		/// Returns true if the db has any data loaded.
		/// </summary>
		public static bool HasEntries => (_entries.Count > 0);

		/// <summary>
		/// Removes all entries.
		/// </summary>
		public static void Clear()
		{
			_entries.Clear();
		}

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
		/// Returns all props which's names include the given string.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static IEnumerable<PropDbEntry> FindEntriesByName(string str)
		{
			return _entries.Values.Where(a => a.ClassName.Contains(str));
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
					entry.IsTerrainBlock = (xmlReader.GetAttribute("IsTerrainBlock") == "true");
					entry.ExtraXML = xmlReader.GetAttribute("ExtraXML");
					entry.Events = xmlReader.GetAttribute("Events");
					entry.Actions = xmlReader.GetAttribute("Actions");
					entry.HasState = (xmlReader.GetAttribute("HasState") == "true");
					entry.Name = xmlReader.GetAttribute("Name");
					entry.ShowName = (xmlReader.GetAttribute("ShowName") == "true");
					entry.IsHugeProp = (xmlReader.GetAttribute("IsHugeProp") == "true");
					entry.UsedServer = (xmlReader.GetAttribute("UsedServer") == "true");
					entry.NonTrivialAnimation = (xmlReader.GetAttribute("NonTrivialAnimation") == "true");
					entry.PickRestrict = (xmlReader.GetAttribute("PickRestrict") == "true");
					entry.StringID = xmlReader.GetAttribute("StringID") ?? "";

					var soundDescIDs = xmlReader.GetAttribute("SoundDescIDs");
					if (soundDescIDs != null)
					{
						var split = soundDescIDs.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
						var ids = new int[split.Length];

						for (var i = 0; i < split.Length; ++i)
							ids[i] = int.Parse(split[i]);

						entry.SoundDescIDs = ids;
					}

					if (entry.ExtraXML != null)
					{
						entry.ExtraXML = FixXml(entry.ExtraXML);
						entry.Feature = GetFeatureFromExtraXml(entry.ExtraXML);

						//System.Xml.Linq.XDocument.Parse(entry.ExtraXML);
					}

					_entries[entry.ClassID] = entry;
				}
			}
		}

		/// <summary>
		/// Reads feature from ExtraXML and returns it, or null if no
		/// feature was found.
		/// </summary>
		/// <param name="extraXml"></param>
		/// <returns></returns>
		private static string GetFeatureFromExtraXml(string extraXml)
		{
			var match = ExtraXmlFeatureRegex.Match(extraXml);
			if (!match.Success)
				return null;

			return match.Groups["feature"].Value;
		}

		/// <summary>
		/// Fixes all known instances of invalid XML code found in the
		/// prop's ExtraXML attribute.
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		private static string FixXml(string xml)
		{
			// ExtraXML containing itself...

			if (xml == "ExtraXML=\"&lt;xml DIST=&quot;800&quot; TIME=&quot;6000&quot; /&gt;\"")
			{
				xml = "<xml DIST=\"800\" TIME=\"6000\" />";
			}

			// Missing closings

			// <xml broken_duration=\"30000\">
			// <xml attraction=\"catchingtail\"
			else if (xml.EndsWith("\""))
			{
				xml += " />";
			}
			// <xml broken_duration=\"30000\">
			else if (xml.EndsWith("\">"))
			{
				xml = xml.Substring(0, xml.Length - 2) + "\" />";
			}

			// Missing spaces

			// <xml sit_motion_category=\"2\" sit_motion=\"89\"sit_motion2=\"90\" seat_number = \"2\" hideequip=\"none\" hideidle=\"false\" />
			else if (xml.Contains("sit_motion=\"89\"sit_motion2"))
			{
				xml = xml.Replace("sit_motion=\"89\"sit_motion2", "sit_motion=\"89\" sit_motion2");
			}
			// <xml sit_motion_category=\"2\" sit_motion=\"98\"hideidle=\"false\"/>
			else if (xml.Contains("sit_motion=\"98\"hideidle"))
			{
				xml = xml.Replace("sit_motion=\"98\"hideidle", "sit_motion=\"98\" hideidle");
			}
			// <xml sit_motion_category=\"2\" sit_motion=\"101\"hideidle=\"false\"/>
			else if (xml.Contains("sit_motion=\"101\"hideidle"))
			{
				xml = xml.Replace("sit_motion=\"101\"hideidle", "sit_motion=\"101\" hideidle");
			}

			// Duplicate attributes

			// <xml sit_motion = \"27\" sit_motion_category=\"2\" sit_motion=\"102\" hideidle=\"false\"/>
			else if (xml.Contains("sit_motion = \"27\" sit_motion_category=\"2\" sit_motion=\"102\""))
			{
				xml = xml.Replace("sit_motion = \"27\" sit_motion_category=\"2\" sit_motion=\"102\"", "sit_motion_category=\"2\" sit_motion=\"102\"");
			}

			return xml;
		}
	}
}
