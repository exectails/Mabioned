Background Music
=============================================================================

For the most part the game controls the background music through Events.
These start or stop background music and ambient sound loops based on
the Parameters.

To modify the background music you have to find the Event that triggers
it in a region and modify its Parameters.

Parameters
----------------------------------------------------------------------------

Let's use the Event surrounding Tir square as an example. It has one
Parameter of type `BgmChangeLoop`, with SignalType `OnEnter`. If you enter
this Event's area it uses the data in the Xml property to change the
background music.

```xml
<data file="Town_Tir_Chonaill.mp3" loop="1"/>
```

To modify it, you would simply edit the Xml property to change the name
of the file played from the mp3 folder.

Similarly the background music can be stopped with the Parameter type
`BgmStop`, which is typically used when leaving an Event area and often
times found in dungeon lobby regions.

```xml
<xml file="A_Safety_Zone.mp3"/>
```
