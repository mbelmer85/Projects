using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzilorQuest
{
    public class MapTile
    {

        //Constructors

        public MapTile(int x, int y, int eventID, int section, string exits, string description)
        {
            X = x;
            Y = y;
            EventID = eventID;
            Section = section;
            Exits = exits;
            Description = description;
            Explored = false;
        }

        public MapTile()
        {
            X = -1;
            Y = -1;
            EventID = -2;
            Section = -1;
            Exits = "";
            Description = "";
            Explored = false;
        }

        //Protected Values

        public int X { get; set; }
        public int Y { get; set; }
        public int Section { get; set; }
        public int EventID { get; set; }
        public string Exits { get; set; }
        public bool Explored { get; set; }
        public string Description { get; set; }
    }
}
