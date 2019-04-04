using Godot;
using System;

public class MapToolsUI : Node
{

    //CreateMap button
    public string Seed;
    public string MapName;
    public Vector2 Size;

    //Save/Load button
    public string Savename;

    //Init
    public override void _Ready()
    {
        GD.Print("MapTools UI ready");
        Seed = ((LineEdit)GetNode("AllContainer/PanelContainer/MapViewerThings/HBoxContainer2/SeedField")).Text;
        Name = ((LineEdit)GetNode("AllContainer/PanelContainer/MapViewerThings/HBoxContainer3/NameField")).Text;
        MapName = ((LineEdit)GetNode("AllContainer/MenuThings/PanelContainer/VBoxContainer/VBoxContainer2/SavenameField")).Text;
        Savename = ((LineEdit)GetNode("AllContainer/MenuThings/PanelContainer/VBoxContainer/VBoxContainer2/SavenameField")).Text;

        Size = new Vector2();
        Size.x = Int32.Parse(((LineEdit)GetNode("AllContainer/PanelContainer/MapViewerThings/HBoxContainer/SizeXField")).Text);
        Size.y = Int32.Parse(((LineEdit)GetNode("AllContainer/PanelContainer/MapViewerThings/HBoxContainer/SizeYField")).Text);
    }

    //Changes seed field when text in textbox changes
    public void ChangeSeed(string newtext)
    {
        Seed = newtext;

    }

    public void ChangeMapName(string newtext)
    {
        MapName = newtext;

    }

    public void ChangeSaveName(string newtext)
    {
        Savename = newtext;

    }

    public void ChangeSize(string newtext)
    {
        Size.x = Int32.Parse(newtext);
        Size.y = Int32.Parse(newtext);
       
    }

    public void ToMainMenu()
    {
        GD.Print("Exiting to main menu...");
        SceneManager smanager = (SceneManager)GetNode("/root/SceneManager");
        smanager.SwitchScene((PackedScene)GD.Load("res://Scenes/MainMenu.tscn"));
    }

   
    
}
