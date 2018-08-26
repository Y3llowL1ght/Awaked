using Godot;
using System;
using MapSystem;

public class MapManager : Node
{
    //Public Fields
    
    public Map CurrentMap;
    

    //Signals
    [Signal]
    delegate void UpdateMapCells(int MapBorderX, int MapBorderY);

    //Private Fields
    TileMap CaveWalls;
    TileMap CaveFloor;

    //References
    MapToolsUI MapToolsUI;
    
    public override void _Ready()
    {
       
        CurrentMap = new Map();
        CaveWalls = (TileMap)GetNode("CaveWalls");
        CaveFloor = (TileMap)GetNode("CaveFloor");
	    MapToolsUI = (MapToolsUI)GetNode("../Camera2D/MapToolsUI");


        GD.Print("MapManagerReady");
    }

    //Generates map with the given seed
    public void CreateMap(){
    
     
     Vector2 size = new Vector2(64,32);
     
     GD.Print("Creating new Map with seed: ", MapToolsUI.Seed, " and size of:", MapToolsUI.Size, "...");
     
     CurrentMap = new Map(MapToolsUI.Name, MapToolsUI.Seed, MapToolsUI.Size, true);

        
     GD.Print("Map generated!");
     ShowMap();
    }

    public void ShowMap(){
        //Resetting tilemaps
        ResetVisualMap();
        //Setting walls
        for (int y = 0; y < CurrentMap.Size.y; y++)
        {
            for (int x = 0; x < CurrentMap.Size.x; x++)
            {
                if (CurrentMap.CaveMap[x,y] == 1)
                {
                   CaveWalls.SetCell(x,y,6); 
                }
                else if (CurrentMap.CaveMap[x,y] == 0)
                {
                    CaveWalls.SetCell(x,y,-1); 
                }
                
                
            }
        }

        //Updating tilemap tiles
        EmitSignal(nameof(UpdateMapCells), CurrentMap.Size.x, CurrentMap.Size.y);
    }


    public void ResetVisualMap(){

        for (int y = 0; y < CurrentMap.Size.y; y++)
        {
            for (int x = 0; x < CurrentMap.Size.x; x++)
            {
                CaveWalls.SetCell(x,y,-1);
                CaveFloor.SetCell(x,y,0);

            }
        }
    }

     public void SaveMap()
    {
        MapSaveManager.SaveMap(CurrentMap, MapToolsUI.Savename);
        GD.Print("SaveMap pressed");
        
    }

    public void LoadMap()
    {
        ResetVisualMap();
        CurrentMap = MapSaveManager.LoadMap(MapToolsUI.Savename);

        if (CurrentMap != null)
        {
            ShowMap();
        }else
        {
            GD.Print("Map loading failed");
        }
        
        
        GD.Print("LoadMap Pressed");

    }

        
}
