Menus
=============================================================================

Main Menu
-----------------------------------------------------------------------------

Found in the upper left of the application's window at all times.
Note that some options are also found on the toolbar below the Main Menu
for quicker access.

### File

- Open: Opens a .rgn or .area file.
- Save: Saves the currently open file.
- Save As: Saves currently open file to a new location.
- Recent Files: A list of recently opened files.
- Exit: Exits the application.

### Edit

- Remove Props: Removes Props from all areas that match select criteria.
- Remove All Events: Removes all Events from all areas.
- Flatten Entire Terrain: Sets the height of the entire terrain to one value.
- Settings: Opens settings dialog to configure application.
  - Colors: Modifies the colors the map is drawn with.
  - Folders: Specifies folder(s) where the application can find additional
    information optionally used in its operation.

### View

- Show Props: Toggles which kind of Props are displayed on the map.
  - Normal: Props that aren't part of any other category.
  - Disabled: Props that are currently disabled based on the feature settings
    in the features.xml.compiled found in the data folder, such as the
    MabiLand props in Tir.
  - Event: Props that are activated during certain times or events,
    such as Christmas or Halloween decoration.
- Show Events: Sub-menu items toggle which kinds of Events are displayed
  on the map. All known types of events appear in this list,
  with yet unknown types of events being grouped under "Undefined".
- Show Areas: Toggles whether Areas are displyed on the map.
- Show Mini Map: Toggles whether the game's minimap image for the loaded
  region is displayed as background on the map. This requires the data
  folder to have been set in the settings.
- Show Heightmap: Not available yet.
- Scale to fit: Scales and moves map so it's displayed in its entirety
  in the map's part of the application's window.
- Expand Tree: Expands the entire Region, Area, Prop, and Event tree on
  the left of the application.
- Collapse Tree: Collapses the entire Region, Area, Prop, and Event tree on
  the left of the application.

### Help

- About: Opens window with information about the program.

Area Context Menu
-----------------------------------------------------------------------------

Right-click an Area in the tree on the left of the application for area-
specific options.

- Remove Props: Removes Props from the selected area that match
  select criteria.
- Remove All Events: Removes all Events from the selected area.
- Flatten Terrain: Sets the height of the terrain in the selected area
  to one value.

Map Context Menu
-----------------------------------------------------------------------------

Right-click anywhere on the map to open this menu.

- Add Prop: Creates new Prop on the clicked position, based on entered
  information.
- Add Event: Creates new Event on the clicked position, based on entered
  information.
- Copy Coordinates: Copies the coordinates to the clicked position to the
  clipboard in the format "X; Y", which can be directly paste into
  Position properties.
- Copy Aura Warp Command: Copies a warp command to the clipboard that
  will warp to the clicked location on an Aura server.
