using Godot;
using System;
using Newtonsoft.Json;

namespace MapSystem
{
    public static class MapSaveManager
    {
        
        public static Map LoadMap(string savename)
        {
            //Getting absolute path instead of user://
            string path = "user://" + savename + ".json";
            File mapsave = new File();
            mapsave.Open(path,(int)File.ModeFlags.Read);
            path = mapsave.GetPathAbsolute();
            mapsave.Close();

            GD.Print(System.IO.File.Exists(path));

            if (System.IO.File.Exists(path))
            {
                Map loadedmap = JsonConvert.DeserializeObject<Map>(System.IO.File.ReadAllText(path));
                return loadedmap;

            }else
            {
                return null;
            }
            

            
        }

        public static void SaveMap(Map map, string savename)
        {

            //Getting absolute path instead of user://
            string path = "user://" + savename + ".json";
            File mapsave = new File();
            mapsave.Open(path,(int)File.ModeFlags.Write);
            path = mapsave.GetPathAbsolute();
            mapsave.Close();

            System.IO.File.WriteAllText(path,JsonConvert.SerializeObject(map));
            

        }

    }
}