using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StructSystem
{

    public class StructMap
    {
        //id -1 for teststruct
        //id 0 for empty
        //id 1 for walls
        //id above 1 is free for use with structs

        public int[,] TypeIdMap;
        public Vector2 SMapSize;
        public List<Structure> SList;

        public StructMap(Vector2 _SMapSize)
        {
            SMapSize = _SMapSize;
            TypeIdMap = new int[(int)SMapSize.x, (int)SMapSize.y];
            SList = new List<Structure>();
        }

        public void AddStructure(Structure structure)
        {
            //adding structur to the list
            SList.Add(structure);

            //Setting IdMap cells
            for (int y = structure.GPosition.y; y < structure.GPosition.y + structure.GSize.y; y++)
            {
                for (int x = structure.GPosition.x; x < structure.GPosition.x + structure.GSize.x; x++)
                {

                    TypeIdMap[x,y] = structure.TypeID;
                
                }
            }

        }

        public void DeleteStructure(Structure structure){
            //Removing from the list
            SList.Remove(structure);

            for (int y = structure.GPosition.y; y < structure.GPosition.y + structure.GSize.y; y++)
            {
                for (int x = structure.GPosition.x; x < structure.GPosition.x + structure.GSize.x; x++)
                {
                    //Clearing tiles
                    TypeIdMap[x, y] = 0;

                }
            }
        }


        public bool CanFitStructureHere(StructureType type, GridVector position)
        {
            
            bool Result = true;
            
            for (int y = position.y; y < position.y + type.Size.y; y++)
            {
                for (int x = position.x; x < position.x + type.Size.x; x++)
                {

                    if (TypeIdMap[x,y] != 0)
                    {
                        Result = false;
                    }

                }
            }
            return Result;
        }

    }



    public class Structure : Node2D
    {

        public StructManager SManager;
        public Sprite SNode;
        public string SName;

        public GridVector GPosition;
        public GridVector GSize;
        
        public int TypeID;
        public int ID;

        public virtual void SetupStructure(){}
        public void FinalizeNodeCreation(StructureType type, GridVector position)
        {
            //Node size
            float posmultiplier = 16;
            //Position multiplier, 1 node = 16 units;
            SName = type.SName;
            TypeID = type.TypeID;
            GSize = type.Size;
            GPosition = position;
            Vector2 newPosition = new Vector2(GPosition.x * posmultiplier, GPosition.y * posmultiplier);
            SetPosition(newPosition);
        }

        public virtual void Delete(){

        }
    }



    public class StructureType
    {
        public string SName;
        public int TypeID;
        public string ScenePath;
        public GridVector Size;

        public StructureType(string name, int typeid,string path, GridVector size)
        {
                SName = name;
                TypeID = typeid;
                ScenePath = path;
                Size = size;
        }
    }

    public static class STypeLoader
    {
        public static StructureType GetStructureType(string Name)
        {

            //Getting absolute path instead of user://
            string path = "res://" + "config/struct_list" + ".json";
            File structfile = new File();
            structfile.Open(path, (int)File.ModeFlags.Read);
            string structpath = structfile.GetPathAbsolute();
            structfile.Close();

            System.Collections.Generic.Dictionary<string, StructureType> structs = JsonConvert.DeserializeObject<System.Collections.Generic.Dictionary<string, StructureType>>(System.IO.File.ReadAllText(structpath));
            if (structs[Name] == null)
            {

                return structs["Test"];
            }

            return structs[Name];
        }
    }

    public struct GridVector
    {
        public int x;
        public int y;

        public GridVector(int _x, int _y)
        {

            x = _x;
            y = _y;

        }
    }


}

