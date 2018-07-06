using System.Collections.Generic;
using System.Resources;

namespace GameLogicLibrary
{
    public class MapTile
    {

        //Constructors

        public MapTile(int x, int y, int eventId, int section, string exits, string description)
        {
            X = x;
            Y = y;
            EventId = eventId;
            Section = section;
            Exits = exits;
            Description = description;
            Explored = false;
        }

        public MapTile()
        {
            X = -1;
            Y = -1;
            EventId = -2;
            Section = -1;
            Exits = "";
            Description = "";
            Explored = false;
        }

        //Protected Values

        public int X { get; set; }
        public int Y { get; set; }
        public int Section { get; set; }
        public int EventId { get; set; }
        public string Exits { get; set; }
        public bool Explored { get; set; }
        public string Description { get; set; }

        //MapTile Methods

        public static void CreateWorld(List<MapTile> world)
        {
            int i = 0;
            string[] objects = Resources.MapData.Split(',');
            while (i < objects.Length)
            {
                MapTile tile = new MapTile
                {
                    X = int.Parse(objects[i])
                };
                i++;
                tile.Y = int.Parse(objects[i]);
                i++;
                tile.EventId = int.Parse(objects[i]);
                i++;
                tile.Section = int.Parse(objects[i]);
                i++;
                tile.Exits = objects[i];
                i++;
                tile.Description = objects[i];
                i++;
                world.Add(tile);
            }
        }
    }
}
