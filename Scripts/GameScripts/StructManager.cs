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


            //Testing
            CreateStructure(new GridVector(20,10),STypeLoader.GetStructureType("Test"));
            CreateStructure(new GridVector(25, 10),STypeLoader.GetStructureType("Test"));
            CreateStructure(new GridVector(30, 10), STypeLoader.GetStructureType("Test2"));
            //DestroyStructure(CurrentSMap.SList[0]);
            
        }

        //Setting SMap class
        public void SetupSMap()
        {
            //Redo this
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
                //Packing structure scene
                var packedStructure = (PackedScene)ResourceLoader.Load(type.ScenePath);
                //Instansing
                var instancedNode = packedStructure.Instance();
                //Adding structure node to the StructManager
                AddChild(instancedNode, true);

                var structure = (Structure)(instancedNode.GetNode(""));
                structure.FinalizeNodeCreation(type, position);
                CurrentSMap.AddStructure(structure);

            }else
            {
                GD.Print("SManager: i cant fit structure here, thats strange!");
            }
        }

        public void DestroyStructure(Structure structure)
        {
            //Deleting structure from the SMap
            CurrentSMap.DeleteStructure(structure);
            //Calling structure script to prepare it to disconnect from everything
            structure.Delete();
            //Deleting node itself
            structure.Free();

        }


    
    }

}