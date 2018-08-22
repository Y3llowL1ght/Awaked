using Godot;
using System;

public class MainMenu : Node
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    public override void _Ready()
    {
        GD.Print("MainMenu Ready:");
        
    }

    public void StartMapViewer()
    {

        GD.Print("Exiting MainMenu");
        GD.Print("Starting map tools...");
        GetTree().ChangeScene("res://Scenes/MapTools.tscn");
        
    }

    public void ExitGame()
    {
        GD.Print("Exiting MainMenu");
        GD.Print("Exiting...");
        GetTree().Quit();
      
    }

    public void NewGame()
    {
        GD.Print("Exiting MainMenu");
        GD.Print("New Game button pressed...");
        //do something
      
    }

    public void LoadGame()
    {
        GD.Print("Exiting MainMenu");
        GD.Print("Load Game button pressed...");
        //do someting
      
    }
}
