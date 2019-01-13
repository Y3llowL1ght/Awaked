using Godot;
using System;
using StructSystem;
using StatSystem;

public class TestStructure : Structure
{


    StatHolder testholder;
    ProgressBar bar;
    
    public override void _Ready()
    {
        SetupStructure();
        testholder = new StatHolder();
        testholder.AddStat(StatLoader.GetStatType("Health"));
        testholder.AddStat(StatLoader.GetStatType("Energy"));
        Test();
    }

    public void Test(){
        
        GD.Print("Its me! teststructure! here goes my teststat: ");
        GD.Print(testholder.GetStat("Health").Name);
        bar = (ProgressBar)GetNode("pg1");
        if (bar != null)
        {
            bar.Value = 10;
            bar.MaxValue = (float)testholder.Stats["Energy"].Capacity;
        }
        

    }

    public override void _PhysicsProcess(float delta){
        testholder.Stats["Energy"].Increase(100/60);
        if (bar != null)
        {
            bar.Value = (float)testholder.Stats["Energy"].Value;
        }
        
    }

    public override void SetupStructure(){

        SManager = (StructManager)GetParent();
        //SNode = (Sprite)GetNode("");

    }

}
