Controls
=============================================================================

Selecting entities
-----------------------------------------------------------------------------

To select an entity, either click on it on the map or in the tree.
Often times there are several entities in one location, in which case
the selector cycles through all available entities on repeated clicks.
For example, if you click on a spot where four events overlap each other,
the first click will select the first event, the second click the second,
and so on.

In this cycle selection props have a higher priority than events, which
means clicking a prop will always select the prop, no matter the events
that are found in the same location. If you do want to cycle through
*all* available entities you can hold the Alt key while clicking.

To unselect the currently selected entity click on a free spot on the map,
or press the Esc (Escape) key.

Scrolling the map
-----------------------------------------------------------------------------

Depending on the selected tool you can scroll over the map with the left
mouse button, or you can use the middle mouse button, which is always
available.

Moving entities
-----------------------------------------------------------------------------

Entities can be moved by selecting them and either changing their position
in the property window on the lower left or by moving them on the map with
the Move or Free tool.

Rotating entities
-----------------------------------------------------------------------------

Either change their rotation property in the window on the lower left or
by moving them on the map with the Rotate or Free tool.

Deleting entities
-----------------------------------------------------------------------------

An entity can be deleted by selecting it and then pressing the
Del (Delete) key. Alternatively you can also delete a number of props or
events in an area at once by right-clicking the area in the tree and
selecting the appropriate option, or using the same options in the Edit
menu for all areas at once.

When mass-deleting props you have several filtering options to remove
only certain props.

- Match Tag: Searches for props that have the given Tag, or StringID.
  For example: /firewood/, to find trees.
- Don't Match Tag: Opposite of "Match Tag", finds props that don't match
  the given tag.
- Are Terrain: Searches for props that are either marked as being part
  of the terrain or not.

Adding props
-----------------------------------------------------------------------------

Right-click on the map and select "Add Prop..." to open a dialog to
add a new prop to the region. In the new dialog you can enter an id
and a position, with the position being pre-filled with the position
you clicked.

You can use the search feature on the bottom to search the prop
database by name if set a data folder in the settings. Double-click
a prop in the list to use its id.

Zooming
-----------------------------------------------------------------------------

To zoom in and out of the map, use the mouse's wheel. Scrolling up will
zoom in, while scrolling down zooms out. The zoom will also focus
on the location the cursor is above, to quickly zoom where you want to go.
To reset the zoom, select "View > Scale to fit", or double-click the
map with the middle mouse button.

To zoom more quickly, hold the Ctrl (Control) key while scrolling.

Hint: The quickest way to navigate the map is to reset the zoom, hover the
location you want to focus on, and scroll up to zoom in.

Getting Coordinates
-----------------------------------------------------------------------------

While you move the mouse across the map you can always see the current
location in the status bar on the lower left. Click anywhere on the map
to open a context menu for additional options.

For one, you can copy the X and Y coordinates of the spot you clicked on
in the format "X; Y", which you can directly paste into the property window
to change an entity's position. You can also copy an Aura warp command to
the clipboard that will bring you to that location.

Modifying the terrain
-----------------------------------------------------------------------------

The only comfort functions to modify the terrain at the current time
are flattening options in the area context menu (right-click at area
in the tree) and the Edit menu. With these options you can set the
height of the terrain in one area or the entire region to one value.

Tools
-----------------------------------------------------------------------------

### Hand

The hand tool allows you only to scroll over the map and select entities
with the left mouse button, without the risk of accidentally moving,
rotating, or otherwise modifying an entity.

To quickly select it, press 1.

### Move

With the Move tool you can select and move entities with the left mouse
button. You can't scroll the map or rotate entities.

To quickly select it, press 2.

### Rotate

Similar to the Move tool, the Rotate tool only allows you to rotate
entities with the left mouse button. You can't scroll or move.

To quickly select it, press 3.

### Free

The Free tool combines all others, in that it allows scrolling over the
map, selecting entities, and moving them with the left mouse button,
and rotating them with the right mouse button.

When an entity was selected with a left click, it can be moved by
dragging it with the left mouse button, while it can be rotated with
the right mouse button. If no entity is selected, the left mouse
button scrolls over the map.

To quickly select it, press 0.

Hotkeys
-----------------------------------------------------------------------------

- 1, Num1: Hand Tool
- 2, Num2: Move Tool
- 3, Num3: Rotate Tool
- 0, Num0: Free Tool
- Esc: Unselect entity
- Del: Delete selected entity
- Ctrl+O: Open file
- Ctrl+S: Save
- Ctrl+Shift+S: Save as
