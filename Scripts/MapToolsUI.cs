using Godot;
using System;

public class MapToolsUI : Node
{

    
    public string Seed;

    //Init
    public override void _Ready()
    {
        GD.Print("MapTools UI ready");
        ChangeSeed();
    }

    //Changes seed field when text in textbox changes
    public void ChangeSeed()
    {
        Seed = ((TextEdit)GetNode("AllContainer/MapViewerThings/Seed")).Text;

    }

    public void ToMainMenu()
    {
        GD.Print("Exiting to main menu...");
        GetTree().ChangeScene("res://Scenes/MainMenu.tscn");
    }

   
    
}
