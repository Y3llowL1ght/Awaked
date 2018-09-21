using Godot;
using System;

namespace StructSystem
{



    public class StructManager : Node
    {

        StructMap CurrentSMap;

        //GameManagerReference
        public GameManager GManager;


        public void Initialize(){
            
            GManager = (GameManager)GetParent();
            SetupSMap();
            CreateStructure(new GridVector(20,10),GetStructureType(2));
            CreateStructure(new GridVector(25, 10), GetStructureType(2));
            CreateStructure(new GridVector(30, 10), GetStructureType(3));
        }

        //Setting SMap class
        public void SetupSMap()
        {

            CurrentSMap = new StructMap(GManager.MapManager.CurrentMap.Size);
            int SizeX = (int)GManager.MapManager.CurrentMap.Size.x;
            int SizeY = (int)GManager.MapManager.CurrentMap.Size.y;

            //Setting walls into IdMap
            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    CurrentSMap.TypeIdMap[x,y] = GManager.MapManager.CurrentMap.CaveMap[x,y];
                }
            }
        }

        public void CreateStructure(GridVector position, StructureType type){

            if (CurrentSMap.CanFitStructureHere(type, position))
            {
                //Creating structure
                var packedStructure = (PackedScene)ResourceLoader.Load(type.ScenePath);
                var instancedNode = packedStructure.Instance();
                AddChild(instancedNode);
                var structure = (Structure)(instancedNode.GetNode(""));
                structure.FinalizeNodeCreation(type, position);
                CurrentSMap.AddStructure(structure);

            }else
            {
                GD.Print("SManager: i cant fit structure here, thats strange!");
            }



        }

        public StructureType GetStructureType(int TypeId){

            switch (TypeId)
            {
                case 2:
                return new StructureType("Test",2,"res://Scenes/Structures/PlaceHolder1.tscn", new GridVector(4,4));
                
                case 3:
                return new StructureType("Test2",3,"res://Scenes/Structures/PlaceHolder2.tscn", new GridVector(1,1));

                default:
                return new StructureType("default:error",9999,"res://Scenes/Structures/PlaceHolder1.tscn",new GridVector(1,1));
            }

        }

    
    }

}