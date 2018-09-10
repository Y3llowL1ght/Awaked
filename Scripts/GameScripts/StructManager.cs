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
            CreateStructure(new GridVector(20,10),GetStructureType(1));

            CreateStructure(new GridVector(22, 11), GetStructureType(1));

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

        public void CreateTestStructure(){
            

            var scene = (PackedScene)ResourceLoader.Load(GetStructureType(1).ScenePath);
            var node = scene.Instance();
            AddChild(node);
            var test = ((TestStructure)node.GetNode(""));
            test.Test();
            //var test = (Sprite)node;
            //test.SetPosition(new Vector2(0,0));
            GD.Print(test.Position);
            

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
                case 1:
                return new StructureType("Test",2,"res://Scenes/Structures/PlaceHolder1.tscn", new GridVector(4,4));
                
                default:
                return new StructureType("default:error",3,"res://Scenes/Structures/PlaceHolder1.tscn",new GridVector(1,1));
            }

        }

    
    }

}