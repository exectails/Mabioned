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
			if (!TryGetString(key, out var result))
				return key;

			return result;
		}

		/// <summary>
		/// Returns the localization string for the given key via out.
		/// Returns false if the key doesn't exist.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="result"></param>
		/// <returns></returns>
		public static bool TryGetString(string key, out string result)
		{
			result = null;

			if (key == null)
				return false;

			if (!key.StartsWith("_LT[") || !key.EndsWith("]"))
				return false;

			key = key.Substring(4, key.Length - 4 - 1);
			key = key.ToLowerInvariant();

			if (!_entries.TryGetValue(key, out result))
				return false;

			return true;
		}

		/// <summary>
		/// Loads all text files from given folder.
		/// </summary>
		/// <param name="localFolderPath"></param>
		public static void Load(string localFolderPath)
		{
			localFolderPath = localFolderPath.Replace("\\", "/");
			if (!localFolderPath.EndsWith("/"))
				localFolderPath += "/";

			foreach (var filePath in Directory.EnumerateFiles(localFolderPath, "*.txt", SearchOption.AllDirectories))
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
				var localPath = folderPath.Replace("\\", "/").Replace(localFolderPath, "");
				localPath = localPath + fileName;
				localPath = localPath.Replace("/", ".");

				// Read lines from file
				using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
					Load(localPath, fs);
			}
		}

		/// <summary>
		/// Loads translation strings from given stream.
		/// </summary>
		/// <param name="localPath"></param>
		/// <param name="stream"></param>
		public static void Load(string localPath, Stream stream)
		{
			using (var sr = new StreamReader(stream))
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
