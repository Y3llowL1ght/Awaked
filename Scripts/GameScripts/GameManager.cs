using Godot;
using System;
using StructSystem;
using StatSystem;

public class GameManager : Node
{

    public string Savename;


    //Manager references
    public MapManager MapManager;
    public StructManager StructManager;
    

    public override void _Ready()
    {

        //Getting Managers
        MapManager = (MapManager)GetNode("MapManager");
        StructManager = (StructManager)GetNode("StructManager");

        //Initializing them
        MapManager.Initialize();
        MapManager.LoadMap("default");
        
        StructManager.Initialize();
        

    }


}
