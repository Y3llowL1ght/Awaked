using Godot;
using System;
using MapSystem;
using SaveSystem;

public class MapManager : Node
{
    //Public Fields
    
    public Map CurrentMap;
    public bool MapToolsMode;

    //Signals
    [Signal]
    delegate void UpdateMapCells(int MapBorderX, int MapBorderY);

    //Private Fields
    TileMap CaveWalls;
    TileMap CaveFloor;

    //References
    MapToolsUI MapToolsUI;
    GameManager GManager;
    
    public override void _Ready()
    {
       
        CaveWalls = (TileMap)GetNode("CaveWalls");
        CaveFloor = (TileMap)GetNode("CaveFloor");
        GD.Print(GetParent().Name);

        if (GetParent().Name == "MapToolsScene")
        {
            GD.Print("MapManager: MapToolsMode");
            MapToolsMode = true;
            MapToolsUI = (MapToolsUI)GetNode("../Camera2D/MapToolsUI");
        }else
        {
            GD.Print("MapManager says: implement gamescene mode u fucking retard");
            GManager = (GameManager)GetParent();
        }
	    


        GD.Print("MapManagerReady");
    }

    public void Initialize(){




        GD.Print("MapManagerInitialized");
    }

    //Generates map with the given seed
    public void CreateMap(){
     
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

    public void SaveMap(string savename)
    {
        //MapTools mode
        if (MapToolsMode)
        {
            MapSaveManager.SaveMap(CurrentMap, MapToolsUI.Savename);
        }else
        {
            MapSaveManager.SaveMap(CurrentMap, savename);
        }
       
        GD.Print("SaveMap pressed");
        
    }

    //MapTools mode
    public void SaveMapMT()
    {
        MapSaveManager.SaveMap(CurrentMap, MapToolsUI.Savename);
        GD.Print("SaveMap pressed");
    }

    //MapLoading not in maptools
    public void LoadMap(string savename)
    {
        //Was there any map before?
        if (CurrentMap != null)
        {
            ResetVisualMap();
        }

        //loading in the map
        CurrentMap = MapSaveManager.LoadMap(savename);

        //Showing it in
        if (CurrentMap != null)
        {
            ShowMap();

        }else
        {
            GD.Print("Map loading failed");
        }
    }

    //MapTools mode
    public void LoadMapMT()
    {
        //Was there any map before?
        if (CurrentMap != null)
        {
            ResetVisualMap();
        }

        //loading in the map
        CurrentMap = MapSaveManager.LoadMap(MapToolsUI.Savename);

        //Showing it in
        if (CurrentMap != null)
        {
            ShowMap();

        }else
        {
            GD.Print("Map loading failed");
        }
    }
      
}
