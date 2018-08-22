using Godot;
using System;
using MapGenerator;
public class MapManager : Node
{
    //Public Fields
    [Export]
    public int MapWidth, MapHeight;
    [Export]
    public bool DebugMode;
    [Export]
    public string Seed;
    public int[,] Map;

    //Signals
    [Signal]
    delegate void UpdateMapCells(int MapBorderX, int MapBorderY);

    //Private Fields
    TileMap CaveWalls;
    TileMap CaveFloor;
    MapGenerator.MapGenerator Generator;
    
    public override void _Ready()
    {
       Map = new int[MapWidth,MapHeight];

       CaveWalls = (TileMap)GetNode("CaveWalls");
       CaveFloor = (TileMap)GetNode("CaveFloor");
       Generator = new MapGenerator.MapGenerator(MapWidth,MapHeight);
        if (!DebugMode)
        {
            ResetMap();
        }

  
        GD.Print("MapManagerReady");
    }

    //Generates map with the given seed
    public void GenerateMap(){
    
     ResetMap();
     
     Seed = ((MapToolsUI)GetNode("../Camera2D/MapToolsUI")).Seed;
     GD.Print("Generating Map with seed: ", Seed, "...");
     
     
     Map = Generator.GenerateMap(Seed);

        for (int y = 0; y < MapHeight; y++)
        {
            for (int x = 0; x < MapWidth; x++)
            {
                if (Map[x,y] == 1)
                {
                   CaveWalls.SetCell(x,y,6); 
                }else if (Map[x,y] == 0)
                {
                    CaveWalls.SetCell(x,y,-1); 
                }
                
                
            }
        }
     GD.Print("Map generated!");
    }

    public void UpdateMap(){

        EmitSignal(nameof(UpdateMapCells), MapWidth, MapHeight);
    }


    public void ResetMap(){

        

        for (int y = 0; y < MapHeight; y++)
        {
            for (int x = 0; x < MapWidth; x++)
            {
                CaveWalls.SetCell(x,y,-1);
                CaveFloor.SetCell(x,y,0);

            }
        }
    }
        
}
