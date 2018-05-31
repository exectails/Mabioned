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
		/// <summary>
		/// General purpose, usually used for music and notices.
		/// </summary>
		General = 0,

		/// <summary>
		/// Portal destinations, such as for moon gates and wings.
		/// </summary>
		Portal = 1,

		/// <summary>
		/// Used to change music and show notices.
		/// </summary>
		AreaChange = 10,

		//Unk11 = 11,

		/// <summary>
		/// Enclose dungeon lobbies, save dungeon return locations.
		/// </summary>
		DungeonLobby = 12,

		/// <summary>
		/// Warp destination.
		/// </summary>
		SpawnPoint = 13,

		/// <summary>
		/// Unpassable block.
		/// </summary>
		Collision = 14,

		//Unk100 = 100,

		/// <summary>
		/// NPC spawn area.
		/// </summary>
		SpawnArea = 2000,

		/// <summary>
		/// Defines streets, presumably used to determine where props like
		/// shops and campfires can be placed.
		/// </summary>
		Street = 3000,

		/// <summary>
		/// Specify fishing grounds.
		/// </summary>
		FishingGrounds = 3100,

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
		/// Displays dungeon information when stopping on altar and used
		/// to determine who is warped into a dungeon.
		/// </summary>
		Altar = 2110,

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
