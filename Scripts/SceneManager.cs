using Godot;
using System;
using SaveSystem;

public class SceneManager : Node
{
    public Node CurrentScene { get; set; }

    
    public override void _Ready()
    {
        Viewport root = GetTree().GetRoot();
        CurrentScene = root.GetChild(root.GetChildCount() - 1);
        GD.Print("SceneManagerReady");
    }


    //Custom SceneSwitcher singleton implementation from godot docs
    public void SwitchScene(PackedScene scene){
        CallDeferred(nameof(SwitchSceneD), scene);
    }

    public void SwitchSceneD(PackedScene scene){
        CurrentScene.Free();
        // Instance the new scene.
        CurrentScene = scene.Instance();

        // Add it to the active scene, as child of root.
        GetTree().GetRoot().AddChild(CurrentScene);

        // Optionally, to make it compatible with the SceneTree.change_scene() API.
        GetTree().SetCurrentScene(CurrentScene);
    }


    //Function that assembles gamescene from the scratch
    public PackedScene CreateNewGameScene(string name, string seed){

        return new PackedScene();
    }
}
