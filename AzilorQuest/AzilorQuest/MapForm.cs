using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AzilorQuest.Properties;
using GameLogicLibrary;

namespace AzilorQuest
{
    public partial class MapForm : Form
    {

        //GLOBALS
        List<MapTile> area;
        Hero hero;

        //CONSTRUCTORS
        public MapForm(List<MapTile> world, Hero player)
        {
            InitializeComponent();
            area = world;
            hero = player;
        }

        //LOAD EVENT
        private void Map_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < area.Count; i++)
            {
                if (area[i].Explored && area[hero.Location].Section == area[i].Section)
                {
                    ((PictureBox)this.Controls["x" + area[i].X.ToString() + "y" + area[i].Y.ToString()]).BackgroundImage = getMapImage(area[i]);
                }
            }
            if(hero.Facing == "North")
            {
                ((PictureBox)this.Controls["x" + area[hero.Location].X.ToString() + "y" + area[hero.Location].Y.ToString()]).Image = Resources.MapArrowNorth;
            }
            else if (hero.Facing == "East")
            {
                ((PictureBox)this.Controls["x" + area[hero.Location].X.ToString() + "y" + area[hero.Location].Y.ToString()]).Image = Resources.MapArrowEast;
            }
            else if (hero.Facing == "South")
            {
                ((PictureBox)this.Controls["x" + area[hero.Location].X.ToString() + "y" + area[hero.Location].Y.ToString()]).Image = Resources.MapArrowSouth;
            }
            else if (hero.Facing == "West")
            {
                ((PictureBox)this.Controls["x" + area[hero.Location].X.ToString() + "y" + area[hero.Location].Y.ToString()]).Image = Resources.MapArrowWest;
            }
        }

        //RETURN MAP IMAGE BASED ON EXITS OF PASSED TILE
        private Image getMapImage(MapTile tile)
        {
            if (!tile.Exits.Contains("North"))
            {
                if (!tile.Exits.Contains("West"))
                {
                    if (!tile.Exits.Contains("South"))
                    {
                        return Resources.northWestSouthWall;
                    }
                    else if (!tile.Exits.Contains("East"))
                    {
                        return Resources.northWestEastWall;
                    }
                    else
                    {
                        return Resources.northWestWall;
                    }
                }
                else if (!tile.Exits.Contains("South"))
                {
                    if (!tile.Exits.Contains("East"))
                    {
                        return Resources.northSouthEastWall;
                    }
                    else
                    {
                        return Resources.northSouthWall;
                    }
                }
                else if (!tile.Exits.Contains("East"))
                {
                    return Resources.northEastWall;
                }
                else
                {
                    return Resources.northWall;
                }
            }
            else if (!tile.Exits.Contains("West"))
            {
                if (!tile.Exits.Contains("South"))
                {
                    if (!tile.Exits.Contains("East"))
                    {
                        return Resources.westSouthEastWall;
                    }
                    else
                    {
                        return Resources.westSouthWall;
                    }
                }
                else if (!tile.Exits.Contains("East"))
                {
                    return Resources.westEastWall;
                }
                else
                {
                    return Resources.westWall;
                }
            }
            else if (!tile.Exits.Contains("South"))
            {
                if (!tile.Exits.Contains("East"))
                {
                    return Resources.southEastWall;
                }
                else
                {
                    return Resources.southWall;
                }
            }
            else if (!tile.Exits.Contains("East"))
            {
                return Resources.eastWall;
            }
            else
            {
                return Resources.noWall;
            }
        }
    }
}
