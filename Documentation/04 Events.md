Events
=============================================================================

Events are areas in a region that are triggered by different actions,
such as entering or leaving them, but they are also used as walls to
stop players from going somewhere.

Properties
----------------------------------------------------------------------------

| Name | Description |
|------|-------------|
| EntityId | The event's globally unique id. |
| Name | The event's name (usually empty, used in warps). |
| Position | The central position of the event. |
| ShapeCount | Amount of shapes (usually only 1). |
| ShapeType | ? |
| Shapes | Collection of rectangular shapes that make up the event's area. |
| Type | The event's type, important for certain functions. |
| ParameterCount | Amount of parameters the event has. |
| Parameters | The event's parameters, such as BGM changes and notices. |

Known Types
----------------------------------------------------------------------------

### General

The General type is used for events that don't serve a specific purpose
and only exist for their parameters. They are often times used to change
the background or ambient music or display notices.

### Portal

Portals are warp destinations that servers use by name. Two uses of this
type are the Moon Gates/Tunnels, which have portal events on their
platforms which players are warped to, and wing items. A list of portals
can be found in the client's XML file "data/db/portal.xml".

### AreaChange

This type is used similarly to the General type, changing music and
showing notices, but it's specifically used to separate the region into
areas. Often times there is just one AreaChange event inside a Region's
Area.

### DungeonLobby

These events enclose dungeon lobbies and save the dungeon return location.

### SpawnPoint

Not unlike portals, SpawnPoints are used as warp destinations, but they
are used for normal warps.

### Collision

Collision events always have the type "Collision" and usually have one
shape and no parameters. The event's shape is used to create a wall that
players can't walk through. This type of event is used to close off parts
of a region where no props and their shapes stop players from passing
through.

### SpawnArea

SpawnArea type events define monsters and NPC spawn areas via their
shape and parameters.

### Street

These are placed on paths and in towns and are presumably used to
determine where certain props can be placed, such as personal shops
or campfires.

### FishingGrounds

This type of event defines where fishing spots are, it doesn't
define what can be caught though, that's determined by the server.
