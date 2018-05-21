using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace Mabioned.SettingOptions
{
	public class Options
	{
		private const string DefaultSaveFileName = "settings.xml";

		private XmlSerializer _serializer;

		public Options()
		{
			_serializer = new XmlSerializer(this.GetType());
		}

		public void Save()
		{
			var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DefaultSaveFileName);
			this.Save(filePath);
		}

		public void Save(string filePath)
		{
			var settings = this;

			using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
				_serializer.Serialize(fs, settings);
		}

		public void Load()
		{
			var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DefaultSaveFileName);
			this.Load(filePath);
		}

		public void Load(string filePath)
		{
			if (!File.Exists(filePath))
				return;

			var settings = this;

			object deserialized;
			using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
				deserialized = _serializer.Deserialize(fs);

			var properties = settings.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty);
			foreach (var property in properties)
			{
				var value = property.GetValue(deserialized);
				property.SetValue(this, value);
			}
		}
	}
}
