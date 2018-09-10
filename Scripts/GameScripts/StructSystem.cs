using Godot;
using System;
using System.Collections.Generic;

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
        List<Structure> SList;

        public StructMap(Vector2 _SMapSize)
        {
            SMapSize = _SMapSize;
            TypeIdMap = new int[(int)SMapSize.x, (int)SMapSize.y];
            SList = new List<Structure>();
        }

        public void AddStructure(Structure structure)
        {
            
            SList.Add(structure);
            int testS = structure.GPosition.y;
            int testE = structure.GPosition.y + structure.GSize.y;

            for (int y = structure.GPosition.y; y < structure.GPosition.y + structure.GSize.y; y++)
            {
                for (int x = structure.GPosition.x; x < structure.GPosition.x + structure.GSize.x; x++)
                {

                    TypeIdMap[x,y] = structure.TypeID;
                
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
        public GridVector GPosition;
        public GridVector GSize;
        public string SName;
        public int TypeID;
        public int ID;

        public virtual void SetupStructure(){}

        public void FinalizeNodeCreation(StructureType type, GridVector position)
        {
            float posmultiplier = 16;
            SName = type.SName;
            TypeID = type.TypeID;
            GSize = type.Size;
            GPosition = position;
            Vector2 newPosition = new Vector2(GPosition.x * posmultiplier, GPosition.y * posmultiplier);
            SetPosition(newPosition);

        }
    }



    public struct StructureType
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
