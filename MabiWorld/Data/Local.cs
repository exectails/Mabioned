using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MabiWorld.Data
{
	/// <summary>
	/// Represents the folder "data/db/local/" an all its localization files.
	/// </summary>
	public static class Local
	{
		private static Dictionary<string, string> _entries = new Dictionary<string, string>();
		private static readonly string[] _toLoad = { "minimapinfo", "propdb" };

		/// <summary>
		/// Returns the localization string for the given key. If the key
		/// was not found the key itself is returned.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetString(string key)
		{
			if (key != null)
			{
				if (key.StartsWith("_LT[") && key.EndsWith("]"))
					key = key.Substring(4, key.Length - 4 - 1);

				key = key.ToLowerInvariant();

				if (_entries.TryGetValue(key, out var result))
					return result;
			}

			return key;
		}

		/// <summary>
		/// Loads all text files from given folder.
		/// </summary>
		/// <param name="path"></param>
		public static void Load(string path)
		{
			path = path.Replace("\\", "/");
			if (!path.EndsWith("/"))
				path += "/";

			foreach (var filePath in Directory.EnumerateFiles(path, "*.txt", SearchOption.AllDirectories))
			{
				var fileName = Path.GetFileNameWithoutExtension(filePath);

				// Remove language suffix, e.g. ".english".
				var dotIndex = fileName.IndexOf('.');
				if (dotIndex != -1)
					fileName = fileName.Substring(0, dotIndex);

				// Limit files to load for now, we don't need everything
				// just yet.
				if (!_toLoad.Contains(fileName))
					continue;

				// Get path to the folder containing the file
				var folderPath = Path.GetDirectoryName(filePath).Replace("\\", "/");
				if (!folderPath.EndsWith("/"))
					folderPath += "/";

				// Combine relative folder path and extensionless file name
				// to dotted path.
				var localPath = folderPath.Replace("\\", "/").Replace(path, "");
				localPath = localPath + fileName;
				localPath = localPath.Replace("/", ".");

				// Read lines from file
				using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
				using (var sr = new StreamReader(fs))
				{
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						var index = line.IndexOf('\t');
						if (index == -1)
							continue;

						var key = localPath + "." + line.Substring(0, index);
						var value = line.Substring(index + 1);

						lock (_entries)
							_entries[key.ToLowerInvariant()] = value;
					}
				}
			}
		}
	}
}
