using Godot;
using System;

namespace StructSystem
{



    public class StructManager : Node
    {

        StructMap CurrentSMap;

        //GameManagerReference
        public GameManager GManager;


        //Инициализация
        public void Initialize(){
            
            GManager = (GameManager)GetParent();
            SetupSMap();
            CreateStructure(new GridVector(20,10),STypeLoader.GetStructureType("Test"));
            CreateStructure(new GridVector(25, 10),STypeLoader.GetStructureType("Test"));
            CreateStructure(new GridVector(30, 10), STypeLoader.GetStructureType("Test2"));
            
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

        //Create structure with a type at coordinates
        public void CreateStructure(GridVector position, StructureType type){

            if (CurrentSMap.CanFitStructureHere(type, position))
            {
                //Creating structure
                var packedStructure = (PackedScene)ResourceLoader.Load(type.ScenePath);
                var instancedNode = packedStructure.Instance();
                AddChild(instancedNode, true);
                var structure = (Structure)(instancedNode.GetNode(""));
                structure.FinalizeNodeCreation(type, position);
                CurrentSMap.AddStructure(structure);

            }else
            {
                GD.Print("SManager: i cant fit structure here, thats strange!");
            }



        }

        

    
    }

}