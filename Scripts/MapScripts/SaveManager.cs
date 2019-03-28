using Godot;
using System;
using Newtonsoft.Json;
using MapSystem;

namespace SaveSystem
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
    public struct Gamesave
    {   // absolute directory path
        public string AbsoluteDirPath;
        public string Savename;
    }

    public static class GameSaveManager{
    
        public static Gamesave Save;
        
        //Creating save directory
        public static void CreateSave(string savename){
                string dpath = "user://" + savename;

                Directory dir = new Directory();

                //gamesave struct declaration
                Gamesave gamesave = new Gamesave();
                gamesave.Savename = savename;
                
                
                if(!dir.DirExists(dpath)){
                    //Save directory doest not exist, so we create it

                    //finding absolute path throuth some shitcode
                    //opening file through godot engine, and than working it with System.IO
                    File temp = new File();
                    temp.Open(dpath, (int)File.ModeFlags.Write);
                    dpath = temp.GetPathAbsolute();
                    temp.Close();
                    System.IO.File.Delete(dpath);

                    //saving path into Gamesave struct
                    gamesave.AbsoluteDirPath = dpath;
                    
                    //creating directory
                    GD.Print("Creating save directory at: " + dpath);
                    System.IO.Directory.CreateDirectory(dpath);

                    //creating cfg.txt to store gamesave struct
                    File cfg = new File();
                    cfg.Open(dpath + "/cfg.json", (int)File.ModeFlags.Write);
                    cfg.StoreString(JsonConvert.SerializeObject(gamesave));
                    cfg.Close();


                }else{

                    //Save directory exists so we just log some data to console
                    //Reading data into the console from .cfg
                    File cfg = new File();
                    cfg.Open(dpath + "/cfg.json", (int)File.ModeFlags.ReadWrite);
                    gamesave = JsonConvert.DeserializeObject<Gamesave>(cfg.GetAsText());
                    cfg.Close();
                    GD.Print($"{savename} directory already exists: " + gamesave.AbsoluteDirPath);
                }
        }


        public static void LoadSave(string savename){
            string dpath = "user://" + savename;
            File cfg = new File();
            cfg.Open(dpath + @"/cfg.json", (int)File.ModeFlags.ReadWrite);
            Save = JsonConvert.DeserializeObject<Gamesave>(cfg.GetAsText());
            cfg.Close();
            GD.Print($"Loaded {savename} : " + Save.AbsoluteDirPath);
        }
    }
}