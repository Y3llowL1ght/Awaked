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
        Test();
    }

    public void Test(){
        
        GD.Print("Its me! teststructure! here goes my teststat: ");
        GD.Print(testholder.GetStat("Health").Name);
        

    }

    public override void SetupStructure(){

        SManager = (StructManager)GetParent();
        //SNode = (Sprite)GetNode("");

    }

}
