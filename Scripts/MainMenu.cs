using Godot;
using System;

public class MainMenu : Node
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        
    }

    public void StartMapViewer()
    {

        GD.Print("Starting map tools");
        GetTree().ChangeScene("res://Scenes/MapViewScene.tscn");

    }

    public void ExitGame()
    {
        GD.Print("Exiting...");
        GetTree().Quit();

    }
}
