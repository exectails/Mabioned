namespace MabiWorld
{
	public enum RegionType : int
	{
		Normal = 100,
		ArenaLobby = 200,
		ArenaField = 300,
		DungeonLobby = 400,
		DungeonField1 = 500,
		DungeonField2 = 600,
		CampRegion = 700,
		DynamicRegion = 800,
		RuinLobby = 900,
	}

	public enum LegacyRegionType : int
	{
		Indoor = 0,
		Outdoor = 1,
		DungeonLobby = 2,
		DungeonField = 3,
	}

	public enum IndoorType : int
	{
		Indoor = 100,
		Outdoor = 200,
	}

	public enum EventType : int
	{
		General = 0,
		Portal = 1,
		AreaChange = 10,
		//Unk11 = 11,
		DungeonLobby = 12,
		SpawnPoint = 13,
		Collision = 14,
		//Unk100 = 100,
		//Unk202 = 202,
		//Unk212 = 212,
		//Unk1200 = 1200,
		//Unk1300 = 1300,
		//Unk1400 = 1400,
		//Unk1401 = 1401,
		SpawnArea = 2000,
		//Unk2001 = 2001,
		Altar = 2110,
		//Unk2120 = 2120,
		//Unk2150 = 2150,
		//Unk2160 = 2160,
		//Unk2200 = 2200,
		//Unk2220 = 2220,
		//Unk2230 = 2230,
		//Unk2240 = 2240,
		//Unk2300 = 2300,
		//Unk2400 = 2400,
		//Unk2401 = 2401,
		//Unk2620 = 2620,
		//Unk2820 = 2820,
		//Unk2901 = 2901,
		Street = 3000,
		FishingSpot = 3100,
		//Unk3101 = 3101,
		//Unk3102 = 3102,
		//Unk3103 = 3103,
		//Unk3200 = 3200,
		//Unk49152 = 49152,
		//Unk49166 = 49166,
		//Unk57358 = 57358,
		//Unk60444 = 60444,
	}

	public enum ParameterType : int
	{
		/// <summary>
		/// Type in portals that warp to a SpawnPoint.
		/// </summary>
		Warp = 100,

		/// <summary>
		/// Type of BGM change parameters where loop is 1.
		/// </summary>
		BgmChangeLoop = 201,

		/// <summary>
		/// Type of BGM stop parameters.
		/// </summary>
		BgmStop = 202,

		/// <summary>
		/// Type of BGM change parameters where loop is 0.
		/// </summary>
		BgmChangeNoLoop = 211,

		/// <summary>
		/// Shows notice on client.
		/// </summary>
		Notice = 300,

		/// <summary>
		/// Makes client confirm an action before executing something.
		/// </summary>
		Confirmation = 1100,

		/// <summary>
		/// Found on events named "commerce_safezone", no XML.
		/// </summary>
		CommerceSafeZone = 1401,

		/// <summary>
		/// Runs server side script.
		/// </summary>
		ServerScript = 2000,

		/// <summary>
		/// Drops a random item from a group.
		/// </summary>
		DropGroupItem = 2200,

		/// <summary>
		/// A spawner for creatures.
		/// </summary>
		CreatureSpawner = 2500,

		/// <summary>
		/// Displays dungeon information when stopping on altar.
		/// </summary>
		AltarNotice = 2110,

		/// <summary>
		/// Saves town location for return warps.
		/// </summary>
		SaveTown = 2610,

		/// <summary>
		/// Presumably saves dungeon return location, encloses lobby regions.
		/// </summary>
		SaveDungeon = 2620,

		/// <summary>
		/// ?
		/// </summary>
		RestrictedArea = 2901,
	}

	public enum SignalType : int
	{
		/// <summary>
		/// On entering the shape.
		/// </summary>
		OnEnter = 101,

		/// <summary>
		/// On leaving the shape.
		/// </summary>
		OnLeave = 102,

		/// <summary>
		/// Presumably on stepping on prop, used in altars.
		/// </summary>
		OnStepOn = 103,

		/// <summary>
		/// Type of normal creature spawners.
		/// </summary>
		Respawn = 150,

		/// <summary>
		/// On prop hit. (probably)
		/// </summary>
		OnHit = 201,

		/// <summary>
		/// On prop touch.
		/// </summary>
		OnTouch = 202,
	}
}
