using Godot;
using System;
using StructSystem;

public class TestStructure : Structure
{

    public override void _Ready()
    {
        SetupStructure();
    }

    public void Test(){

        GD.Print("Its me! teststructure!");

    }

    public override void SetupStructure(){

        SManager = (StructManager)GetParent();
        //SNode = (Sprite)GetNode("");

    }

}
