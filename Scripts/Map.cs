using Godot;
using System;

namespace MapSystem
{
    public class Map
    {
        public string Name;
        public Vector2 Size;
        public int[,] CaveMap;
        public string Seed;
        public MapGenerator Generator;

        public Map(string _name, string _seed, Vector2 _size, bool generate)
        {
            Name = _name;
            Size = _size;
            Seed = _seed;
            CaveMap = new int[(int)Size.x,(int)Size.y];
            Generator = new MapGenerator((int)Size.x,(int)Size.y);

            if (generate)
            {
                GenerateMap();
            }

        }

        public Map()
        {
        }

        //Reset all map to nothing except settings (seed, size, etc)
        public void ResetMap()
        {
            for (int y = 0; y < Size.y; y++)
            {
                for (int x = 0; x < Size.x; x++)
                {
                    CaveMap[x,y] = 0;

                }
            }
        }

        public void GenerateMap()
        {
            CaveMap = Generator.GenerateMap(Seed);
        }

        public void SaveMap()
        {
            //creates/opens savefile
            var mapsave = new File();
            mapsave.Open("user://PLEASEWORK.json", (int)File.ModeFlags.Write);

            //gets mapdata in proper structure and saves it to file
            var mapdata = Save();

            GD.Print(JSON.Print(mapdata));

            //mapsave.StoreLine();
            mapsave.Close();
            
        }
        
        public Dictionary<object,object> Save(){

            return new Dictionary<object,object>()
            {
                {"Name",Name},
                {"Seed",Seed}
               /* {"SizeX",Size.x},
                {"SizeY",Size.y}
                {"CaveMap",CaveMap}*/
            };

        }
    }

    public static class MapLoader
    {
        
        public static Map LoadMap(string savename){

            Map mapToLoad= new Map();

            var mapSave = new File();
            mapSave.Open("user://" + savename + ".txt", (int)File.ModeFlags.Read);

            mapToLoad.Size = new Vector2();

            mapToLoad.Size.x = mapSave.GetFloat();
            mapToLoad.Size.y = mapSave.GetFloat();
            //mapToLoad.Name = mapSave.GetStr


            for (int y = 0; y < mapToLoad.Size.y; y++)
            {
                for (int x = 0; x < mapToLoad.Size.x; x++)
                {
                    mapToLoad.CaveMap[x,y] = mapSave.Get32();
                }
            }

        
            return mapToLoad;
        }

    }
}