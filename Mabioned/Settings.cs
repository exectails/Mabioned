using Mabioned.SettingOptions;
using MabiWorld;
using PrimitiveCanvas.Interactions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Mabioned
{
	public static class Settings
	{
		public readonly static MainOptions Default = new MainOptions();
	}

	public class MainOptions : Options
	{
		public List<string> LastOpenedFiles { get; set; } = new List<string>();

		public Point WindowPosition { get; set; } = new Point(-1, -1);
		public Size WindowSize { get; set; } = new Size(-1, -1);
		public bool WindowMaximized { get; set; } = true;
		internal bool WindowPositionSet => (this.WindowPosition.X != -1 || this.WindowPosition.Y != -1);
		internal bool WindowSizeSet => (this.WindowSize.Width != -1 || this.WindowSize.Height != -1);

		public int SplitterMain { get; set; } = -1;
		public int SplitterSidebar { get; set; } = -1;

		public bool SingleInstance { get; set; } = true;

		public XmlColor BackgroundColor { get; set; } = Color.FromArgb(248, 219, 179);
		public XmlColor PropsColor { get; set; } = Color.FromArgb(91, 77, 59);
		public XmlColor EventsColor { get; set; } = Color.FromArgb(158, 133, 103);
		public XmlColor AreasColor { get; set; } = Color.White;
		public XmlColor SelectionColor { get; set; } = Color.Red;

		public string DataFolder { get; set; } = "";

		public bool ShowPropsNormal { get; set; } = true;
		public bool ShowPropsDisabled { get; set; } = true;
		public bool ShowPropsEvent { get; set; } = true;
		public bool ShowPropsTerrain { get; set; } = true;
		public bool ShowAreas { get; set; } = false;
		public bool ShowMiniMap { get; set; } = false;
		public SerializableDictionary<int, bool> ShowEvents { get; set; } = new SerializableDictionary<int, bool>();

		public int Tool { get; set; } = (int)PrimitiveCanvas.Interactions.Tool.Scroll;

		public MainOptions()
		{
			var eventTypeType = typeof(EventType);

			this.ShowEvents[-1] = true;
			foreach (var eventType in Enum.GetValues(eventTypeType).Cast<EventType>())
			{
				var value = (int)eventType;
				this.ShowEvents[value] = true;
			}
		}
	}
}
