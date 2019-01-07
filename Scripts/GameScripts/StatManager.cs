using Godot;
using System;
namespace StatSystem{

public class StatManager : Node
{

    
    //Managers
    public GameManager GManager;

    public void Initialize()
    {
        
    }

    public override void _Ready(){
        //GD.Print(StatLoader.GetStatType("default").Capacity);
    }



}
}
