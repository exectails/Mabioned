using MabiWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WorldTest
{
	class Program
	{
		static void Main(string[] args)
		{
			//TestLoading();
			//TestPerformance();
			TestSomething();

			Console.WriteLine("end.");
			Console.ReadLine();
		}

		private static void TestSomething()
		{
			var worldPath = @"E:\Mabinogi\MabiPacker-1.2.1\data na278\world";

			foreach (var filePath in Directory.EnumerateFiles(worldPath, "*.rgn", SearchOption.AllDirectories))
			{
				var fileName = Path.GetFileName(filePath);
				if (fileName != fileName.ToLowerInvariant())
					throw new Exception();

				Console.WriteLine(fileName);
			}
			foreach (var filePath in Directory.EnumerateFiles(worldPath, "*.area", SearchOption.AllDirectories))
			{
				var fileName = Path.GetFileName(filePath);
				if (fileName != fileName.ToLowerInvariant())
					throw new Exception();

				Console.WriteLine(fileName);
			}
		}

		private static void TestLoading()
		{
			var worldPath =
				//@"E:\Mabinogi\MabiPacker-1.2.1\data na278\world";
				@"E:\Mabinogi\MabiPacker-1.2.1\data kr72\world";
			var totalTimer = Stopwatch.StartNew();

			var eventTypes = new List<EventType>();

			Parallel.ForEach(Directory.EnumerateFiles(worldPath, "*.rgn", SearchOption.AllDirectories), filePath =>
			{
				if (Path.GetFileName(filePath) == "dungeonprop_temp.rgn")
					return;

				var timer = Stopwatch.StartNew();

				using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					Region region = null;

					try
					{
						region = Region.ReadFrom(fs);
						region.ExportTest(filePath);
					}
					catch (Exception ex)
					{
						Console.WriteLine("Error: {0} :: {1}", filePath, ex.Message);
					}

					timer.Stop();
					//Console.Write(region.Name + " :: " + timer.Elapsed);
					timer.Restart();

					try
					{
						var areas = new List<Area>();
						var dirPath = Path.GetDirectoryName(filePath);

						//for (var i = 0; i < region.AreaFileNames.Count; ++i)
						Parallel.For(0, region.AreaFileNames.Count, i =>
						{
							var areaFileName = region.AreaFileNames[i];
							var areaFilePath = Path.Combine(dirPath, areaFileName + ".area");

							using (var fs2 = new FileStream(areaFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
							{
								var area = Area.ReadFrom(fs2);
								area.ExportTest(areaFilePath);
								areas.Add(area);
							}
						});

						foreach (var area in areas)
						{
							foreach (var entity in area.Events.Cast<IEntity>().Concat(area.Props))
							{
								foreach (var shape in entity.Shapes)
								{
									if (shape.Type != 0)
										Console.WriteLine(area + " :: " + entity + " :: " + shape);
								}
							}
						}

						eventTypes.AddRange(areas.SelectMany(a => a.Events).Select(a => a.Type));
						//foreach (var area in areas)
						//{
						//	foreach (var evnt in area.Events)
						//	{
						//		foreach (var parameter in evnt.Parameters)
						//		{
						//			//if (parameter.EventType == EventType.CreatureSpawn)
						//			//	Console.WriteLine(area + " :: " + evnt);
						//			eventTypes.Add(parameter.EventType);
						//		}
						//	}
						//}
						//eventTypes.AddRange(areas.SelectMany(a => a.Props).SelectMany(a => a.Parameters).Select(a => a.EventType));
					}
					catch (Exception ex)
					{
						Console.WriteLine("Error: {0} :: {1}", filePath, ex.Message);
					}

					timer.Stop();
					//Console.WriteLine(" :: Areas " + timer.Elapsed);
				}
			});

			//foreach (var type in eventTypes.Distinct().OrderBy(a => (int)a))
			//{
			//	var name = (!Enum.IsDefined(typeof(EventType), type) ? "Unk" : "") + type.ToString();
			//	Console.WriteLine(name + " = " + (int)type + ",");
			//}

			totalTimer.Stop();
			Console.WriteLine("Total: " + totalTimer.Elapsed);
		}

		private static void TestPerformance()
		{
			var regionPath = @"E:\Mabinogi\MabiPacker-1.2.1\data na278\world\iria_c_main_field\iria_c_main_field.rgn";
			regionPath = @"C:\Users\exec\Desktop\iria_c_main_field\iria_c_main_field.rgn";

			var n = 100;
			var timer = Stopwatch.StartNew();

			for (var k = 0; k < n; ++k)
			{
				using (var fs = new FileStream(regionPath, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					Region region = null;

					//try
					{
						region = Region.ReadFrom(fs);
					}
					//catch (Exception ex)
					//{
					//	Console.WriteLine("Error: {0} :: {1}", regionPath, ex.Message);
					//}

					//try
					{
						var areas = new List<Area>(region.AreaFileNames.Count);
						var dirPath = Path.GetDirectoryName(regionPath);

						//for (var i = 0; i < region.AreaFileNames.Count; ++i)
						Parallel.For(0, region.AreaFileNames.Count, i =>
						{
							var areaFileName = region.AreaFileNames[i];
							var areaFilePath = Path.Combine(dirPath, areaFileName + ".area");

							using (var fs2 = new FileStream(areaFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
							{
								var area = Area.ReadFrom(fs2);
								areas.Add(area);
							}
						}
						);
					}
					//catch (Exception ex)
					//{
					//	Console.WriteLine("Error: {0} :: {1}", regionPath, ex.Message);
					//}
				}
			}

			timer.Stop();
			Console.WriteLine(TimeSpan.FromTicks(timer.Elapsed.Ticks / n));
		}
	}
}
