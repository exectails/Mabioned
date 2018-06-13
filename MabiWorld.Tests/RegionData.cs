using MabiWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace WorldTest
{
	public class Tests
	{
		private const string WorldPath = @"E:\Mabinogi\MabiPacker-1.2.1\data na278\world";

		[Fact]
		private static void FileNames()
		{
			foreach (var filePath in Directory.EnumerateFiles(WorldPath, "*.rgn", SearchOption.AllDirectories))
			{
				var fileName = Path.GetFileName(filePath);
				Assert.Equal(fileName.ToLowerInvariant(), fileName);
			}
			foreach (var filePath in Directory.EnumerateFiles(WorldPath, "*.area", SearchOption.AllDirectories))
			{
				var fileName = Path.GetFileName(filePath);
				Assert.Equal(fileName.ToLowerInvariant(), fileName);
			}
		}

		[Fact]
		private static void ReadWrite()
		{
			foreach (var filePath in Directory.EnumerateFiles(WorldPath, "*.rgn", SearchOption.AllDirectories))
			{
				if (Path.GetFileName(filePath) == "dungeonprop_temp.rgn")
					continue;

				using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					var dirPath = Path.GetDirectoryName(filePath);

					// Test region
					var region = Region.ReadFrom(fs);

					using (var ms = new MemoryStream())
					{
						region.WriteTo(ms);

						var originalBuffer = File.ReadAllBytes(filePath);
						var copyBuffer = ms.ToArray();

						Assert.Equal(originalBuffer.Length, copyBuffer.Length);

						for (var i = 0; i < copyBuffer.Length; ++i)
						{
							// Ignore Length, as that's 0 for some area files.
							if (i <= 3 || i > 7)
								Assert.Equal(originalBuffer[i], copyBuffer[i]);
						}
					}

					// Test areas
					foreach (var areaFileName in region.AreaFileNames)
					{
						var areaFilePath = Path.Combine(dirPath, areaFileName + ".area");

						using (var fs2 = new FileStream(areaFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
						{
							var area = Area.ReadFrom(fs2);

							Assert.Equal(areaFileName, area.Name, true);

							using (var ms = new MemoryStream())
							{
								area.WriteTo(ms);

								var originalBuffer = File.ReadAllBytes(areaFilePath);
								var copyBuffer = ms.ToArray();

								Assert.Equal(originalBuffer.Length, copyBuffer.Length);

								for (var i = 0; i < copyBuffer.Length; ++i)
								{
									// Ignore Length, as that's 0 for some area files.
									if (i <= 3 || i > 7)
										Assert.Equal(originalBuffer[i], copyBuffer[i]);
								}
							}
						}
					}
				}
			}
		}
	}
}
