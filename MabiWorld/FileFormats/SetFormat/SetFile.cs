using System.Collections.Generic;
using System.IO;

namespace MabiWorld.FileFormats.SetFormat
{
	/// <summary>
	/// Represents a SET file.
	/// </summary>
	/// <remarks>
	/// SET files contain a combination of animation and frame data.
	/// </remarks>
	public class SetFile
	{
		/// <summary>
		/// Gets or sets the file's header.
		/// </summary>
		public SetHeader Header { get; set; }

		/// <summary>
		/// Returns a list with the file's items.
		/// </summary>
		public List<SetItem> Items { get; } = new List<SetItem>();

		/// <summary>
		/// Reads SET from file and returns it.
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static SetFile ReadFrom(string filePath)
		{
			using (var fs = new FileStream(filePath, FileMode.Open))
				return ReadFrom(fs);
		}

		/// <summary>
		/// Reads SET from stream and returns it.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public static SetFile ReadFrom(Stream stream)
		{
			var result = new SetFile();
			var br = new BinaryReader(stream);

			result.Header = SetHeader.ReadFrom(br);

			var itemCount = br.ReadInt32();
			for (var i = 0; i < itemCount; ++i)
			{
				var item = SetItem.ReadFrom(br);
				result.Items.Add(item);
			}

			if (stream.Position != stream.Length)
				throw new InvalidDataException("Leftover data in SET.");

			return result;
		}
	}
}
