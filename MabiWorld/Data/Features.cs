using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MabiWorld.Data
{
	/// <summary>
	/// Represents "Setting" entry in "features.xml.compiled".
	/// </summary>
	public class FeaturesSetting
	{
		public bool Development { get; internal set; }
		public byte Generation { get; internal set; }
		public string Locale { get; internal set; }
		public string Name { get; internal set; }
		public byte Season { get; internal set; }
		public byte Subseason { get; internal set; }
		public bool Test { get; internal set; }
	}

	/// <summary>
	/// Represents "Feature" entry in "features.xml.compiled".
	/// </summary>
	public class FeaturesFeature
	{
		public string Default { get; internal set; } = "";
		public string Disable { get; internal set; } = "";
		public string Enable { get; internal set; } = "";
		public uint Hash { get; internal set; }

		public float DefaultCode { get; internal set; } = int.MaxValue;
	}

	/// <summary>
	/// Represents "data/features.xml.compiled".
	/// </summary>
	public class Features
	{
		private static readonly Regex _genRegex = new Regex(@"G(?<generation>[0-9]+)S(?<season>[0-9]+)", RegexOptions.Compiled);

		private static List<FeaturesSetting> _settings = new List<FeaturesSetting>();
		private static Dictionary<uint, FeaturesFeature> _features = new Dictionary<uint, FeaturesFeature>();
		private static int _localeGenCode;
		private static Regex _localeGenRegex;
		private static bool _settingSelected;

		public static string Locale { get; private set; }

		/// <summary>
		/// Hashes str.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		private static uint GetStringHash(string str)
		{
			var s = 5381;
			foreach (var ch in str) s = s * 33 + ch;
			return (uint)s;
		}

		/// <summary>
		/// Sets the setting to use for enabled checks, call after loading.
		/// </summary>
		/// <param name="locale"></param>
		/// <param name="generation"></param>
		/// <param name="season"></param>
		/// <param name="subseason"></param>
		public static void SelectSetting(string locale, bool test, bool development)
		{
			var setting = _settings.FirstOrDefault(a => a.Locale == locale && a.Test == test && a.Development == development);
			if (setting == null)
				throw new ArgumentException("Setting not found.");

			SelectSetting(locale, setting.Generation, setting.Season, setting.Subseason);
		}

		/// <summary>
		/// Sets the setting to use for enabled checks, call after loading.
		/// </summary>
		/// <param name="locale"></param>
		/// <param name="generation"></param>
		/// <param name="season"></param>
		/// <param name="subseason"></param>
		public static void SelectSetting(string locale, int generation, int season, int subseason)
		{
			Locale = locale;

			_localeGenRegex = new Regex(@"G(?<generation>[0-9]+)S(?<season>[0-9]+)@" + locale, RegexOptions.Compiled);
			_localeGenCode = ((generation * 100) + season);
			_settingSelected = true;
		}

		/// <summary>
		/// Returns true if the feature is enabled under the selected setting.
		/// Returns false for unknown features or if no setting is selected.
		/// </summary>
		/// <param name="featureName"></param>
		/// <returns></returns>
		public static bool IsEnabled(string featureName)
		{
			if (!_settingSelected)
				return false;

			// Check combinations, such as "-903|-housing".
			if (featureName.Contains("|"))
			{
				var split = featureName.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
				for (var i = 0; i < split.Length; ++i)
				{
					// This is an OR, if we find one enabled feature we can
					// return true.
					if (IsEnabled(split[i]))
						return true;
				}

				return false;
			}

			// If a feature name is prefixed with a minus, the caller wants
			// to know whether the feature has *not* been enabled yet,
			// negate the result.
			var negate = featureName.StartsWith("-");
			if (featureName.StartsWith("-"))
				featureName = featureName.Substring(1);

			var enabled = false;

			// Check legacy features, such as "dungeonrenewal" and "bossrush".
			if (featureName == "bossrush")
			{
				if (string.Equals(Locale, "usa", StringComparison.InvariantCultureIgnoreCase))
					enabled = (_localeGenCode >= GetGenCode(6, 5, 0));
				else
					enabled = (_localeGenCode >= GetGenCode(5, 4, 0));
			}
			else if (featureName == "housing" || featureName == "partyboard")
			{
				enabled = (_localeGenCode >= GetGenCode(3, 5, 0));
			}
			else if (featureName == "dungeonrenewal")
			{
				// TODO: Find gen code.
				enabled = (_localeGenCode >= GetGenCode(0, 0, 0));
			}
			// Check gen codes, such as "1101.1" and "9999".
			else if (float.TryParse(featureName, out var code))
			{
				enabled = (_localeGenCode >= code);
			}
			// Check hashes, such as "gfChristmasDeco".
			else
			{
				var hash = GetStringHash(featureName);
				if (!_features.TryGetValue(hash, out var feature))
					return false;

				// Start with the features default enable value
				enabled = (_localeGenCode >= feature.DefaultCode);

				// If it's not enabled by default, check Enable.
				if (!enabled)
				{
					var match = _localeGenRegex.Match(feature.Enable);
					if (match.Success)
					{
						var genCode = GetGenCode(match);
						enabled = (_localeGenCode >= genCode);
					}
				}

				// If it was enabled check Disable to see if it should be
				// reverted.
				if (enabled)
				{
					var match = _localeGenRegex.Match(feature.Disable);
					if (match.Success)
					{
						var genCode = GetGenCode(match);
						enabled = !(_localeGenCode >= genCode);
					}
				}
			}

			return (!negate ? enabled : !enabled);
		}

		/// <summary>
		/// Extracts generation code from in format "GGSS".
		/// </summary>
		/// <example>
		/// G1S0 -> 100
		/// G20S4 -> 2004
		/// </example>
		/// <param name="match"></param>
		/// <returns></returns>
		private static float GetGenCode(Match match)
		{
			var g = int.Parse(match.Groups["generation"].Value);
			var s = int.Parse(match.Groups["season"].Value);

			return GetGenCode(g, s, 0);
		}

		/// <summary>
		/// Generates generation code from in format "GGSS".
		/// </summary>
		/// <example>
		/// G1S0 -> 100
		/// G20S4 -> 2004
		/// </example>
		/// <param name="match"></param>
		/// <returns></returns>
		private static float GetGenCode(int generation, int season, int subseason)
		{
			return ((generation * 100) + season + (subseason / 10));
		}

		/// <summary>
		/// Loads settings and features from .compiled file.
		/// </summary>
		/// <param name="filePath"></param>
		public static void Load(string filePath)
		{
			if (!File.Exists(filePath))
				throw new FileNotFoundException("File not found: " + filePath);

			if (Path.GetFileName(filePath) != "features.xml.compiled")
				throw new ArgumentException("Expected file named features.xml.compiled.");

			using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
				Load(stream);
		}

		/// <summary>
		/// Loads settings and features from .compiled file.
		/// </summary>
		/// <author>Yiting</author>
		/// <param name="stream"></param>
		public static void Load(Stream stream)
		{
			var buffer = new byte[0x100];
			var num = 0;

			if (stream.Read(buffer, 0, 2) != 2)
				throw new EndOfStreamException();

			var settingCount = BitConverter.ToUInt16(buffer, 0);
			for (var i = 0; i < settingCount; i++)
			{
				var setting = new FeaturesSetting();

				if (stream.Read(buffer, 0, 2) != 2)
					throw new EndOfStreamException();

				num = BitConverter.ToUInt16(buffer, 0);
				if ((num <= 0) || (num > 0x100))
					throw new NotSupportedException();

				if (stream.Read(buffer, 0, num) != num)
					throw new EndOfStreamException();

				for (var k = 0; k < num; k++)
					buffer[k] = (byte)(buffer[k] ^ 0x80);

				setting.Name = Encoding.UTF8.GetString(buffer, 0, num);

				if (stream.Read(buffer, 0, 2) != 2)
					throw new EndOfStreamException();

				num = BitConverter.ToUInt16(buffer, 0);
				if (num <= 0 || num > 0x100)
					throw new NotSupportedException();

				if (stream.Read(buffer, 0, num) != num)
					throw new EndOfStreamException();

				for (var m = 0; m < num; m++)
					buffer[m] = (byte)(buffer[m] ^ 0x80);

				setting.Locale = Encoding.UTF8.GetString(buffer, 0, num);

				if (stream.Read(buffer, 0, 3) != 3)
					throw new EndOfStreamException();

				setting.Generation = buffer[0];
				setting.Season = buffer[1];
				setting.Subseason = (byte)(buffer[2] >> 2);
				setting.Test = (buffer[2] & 1) != 0;
				setting.Development = (buffer[2] & 2) != 0;

				_settings.Add(setting);
			}

			if (stream.Read(buffer, 0, 2) != 2)
				throw new EndOfStreamException();

			var featureCount = BitConverter.ToUInt16(buffer, 0);
			for (var j = 0; j < featureCount; j++)
			{
				var feature = new FeaturesFeature();

				if (stream.Read(buffer, 0, 4) != 4)
					throw new EndOfStreamException();

				feature.Hash = BitConverter.ToUInt32(buffer, 0);

				if (stream.Read(buffer, 0, 2) != 2)
					throw new EndOfStreamException();

				num = BitConverter.ToUInt16(buffer, 0);
				if (num > 0x100)
					throw new NotSupportedException();

				if (num > 0)
				{
					if (stream.Read(buffer, 0, num) != num)
						throw new EndOfStreamException();

					for (var n = 0; n < num; n++)
						buffer[n] = (byte)(buffer[n] ^ 0x80);

					feature.Default = Encoding.UTF8.GetString(buffer, 0, num);

					var match = _genRegex.Match(feature.Default);
					if (match.Success)
						feature.DefaultCode = GetGenCode(match);
				}

				if (stream.Read(buffer, 0, 2) != 2)
					throw new EndOfStreamException();

				num = BitConverter.ToUInt16(buffer, 0);
				if (num > 0x100)
					throw new NotSupportedException();

				if (num > 0)
				{
					if (stream.Read(buffer, 0, num) != num)
						throw new EndOfStreamException();

					for (var num9 = 0; num9 < num; num9++)
						buffer[num9] = (byte)(buffer[num9] ^ 0x80);

					feature.Enable = Encoding.UTF8.GetString(buffer, 0, num);
				}

				if (stream.Read(buffer, 0, 2) != 2)
					throw new EndOfStreamException();

				num = BitConverter.ToUInt16(buffer, 0);
				if (num > 0x100)
					throw new NotSupportedException();

				if (num > 0)
				{
					if (stream.Read(buffer, 0, num) != num)
						throw new EndOfStreamException();

					for (var num10 = 0; num10 < num; num10++)
						buffer[num10] = (byte)(buffer[num10] ^ 0x80);

					feature.Disable = Encoding.UTF8.GetString(buffer, 0, num);
				}

				if (_features.ContainsKey(feature.Hash))
					throw new InvalidOperationException("Encountered duplicate feature hash.");

				_features[feature.Hash] = feature;
			}
		}
	}
}
