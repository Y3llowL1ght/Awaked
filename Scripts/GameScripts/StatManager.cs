using Godot;
using System;
namespace StatSystem{

public class StatManager : Node
{

    //Stats
    private int _Energy;
    public int EnergyCap;



    //Getter/Setter for stats
    public int Energy { get => _Energy; }

    //Managers
    public GameManager GManager;

    public void Initialize()
    {
        //GManager = (GameManager)GetParent();
    }



}
}
