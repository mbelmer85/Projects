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
    }
}
