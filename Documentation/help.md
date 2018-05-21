Help
=============================================================================

Mabioned provides an easy to use UI to modify region and area files,
but has only few features that go beyond editing raw values at this
point in time. This file gives you some basic information necessary
to utilize this application.

Terminology
-----------------------------------------------------------------------------

### Region

Mabinogi's world is made up of regions, which are in turn made up of
areas. The region files in themselves contain only general information
about a region, such as lighting and the areas to use.

### Area

Areas contain the more substantial information about a region,
such as props, events, and the terrain. They can be edited on their
own, but need to be placed in a region.

### Entity

Entities are things that are placed in a region. They include props,
such as trees, events that make something happen when a player
enters them for example, and even players themselves.

### Prop

Every object in the world is a prop. Examples are trees, houses, statues,
chairs, and so on. Many props can be interacted with, allowing players
to punch or touch them. Officially their behavior is controlled by
parameters in the area files, but server emulators such as Aura
might not actually use this information.

### Event

An event is a section in a region where something happens under
certain circumstances. Examples of event signals are entering or
leaving them.

Events typically consist of only one rectangular section and are used
to control the background music, show notices telling player where they
are, provide warp locations to the official servers, and more.

### Shape

Shapes give props and events their form, controlling their collision
or in which section of a region an event should be triggered. Any prop
and event can be made up of several shapes, which are always rectangular.

Some props don't have shapes, which means you can walk through them.
Due to the application currently basing all rendering on those
shapes such props are represented by circles.

### Parameter

Props and events often times contain information in form of parameters
that tell the official clients and servers what to do with them.
These paramaters usually have a type descriping what type of parameter
it is, a signal type that controls when something should happen, and an
XML object for additional information, such as which track to play
when entering a town.

### Area Plane/Plane/Sqare

Area planes, planes, and squares are the part of the area that contains
the terrain information. To put it another way, the height map and
texture information are saved here. This part of the region files
isn't too well researched yet.

Unknown properties
-----------------------------------------------------------------------------

When opening a region or area file you will likely notice some properties
with names like "Unk3", "Unk4", etc. These are values in the files which's
purpose is currently unknown. They are displayed only to give full access
to the file's information, and modifying them might have unexpected
consequences.

Control/Features
-----------------------------------------------------------------------------

### Moving entities

Going by raw values, moving an entity would usually mean to edit both
an entity's actual position, where it's placed in the world, and its
shape's positions, because they are placed independently. For some reason.

Because of this, when you modify an entity's position, its shape's
positions are automatically updated as well.

### Deleting entities

To delete an entity, select it in the tree on the left and press
the delete key. You can also click its shape on the map to select it.

### Using the map

To zoom the map in and out, use the mouse wheel over the map. To scroll,
use either the scroll bars or drag the map with the left mouse button.

To select a prop or event click into one of its shapes. If there are
multiple events layered on top of each other clicking repeatedly
cycles through them.

Right-click anywhere to get options to copy the coordinates you
clicked on or a warp command that can be used in Aura to get to
that exact location.

Use the View menu at the top to control what's displayed on the map.
You can toggle the display of props, various known and unknown types
of events, and areas.
