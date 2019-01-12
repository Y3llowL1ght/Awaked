using Godot;
using System;
using StructSystem;
using StatSystem;

public class TestStructure : Structure
{


    StatHolder testholder;
    
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
        ProgressBar bar = (ProgressBar)GetNode("PlaceHolder1/pg1");
        PrintTree();
        //bar.Value = 90;

    }

    public override void SetupStructure(){

        SManager = (StructManager)GetParent();
        //SNode = (Sprite)GetNode("");

    }

}
