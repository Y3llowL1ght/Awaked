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


    
    }
}