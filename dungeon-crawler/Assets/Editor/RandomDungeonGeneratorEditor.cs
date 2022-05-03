using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractDungeonGenerator), true)] //this ensures that our random walk generator will also have a cusstom button

public class RandomDungeonGeneratorEditor : Editor
{
    AbstractDungeonGenerator generator;
    
    private void Awake()
    {
        generator = (AbstractDungeonGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Create Dungeon")) //if the buggon create dungeon is pressed we're gonna call generator
        {
            generator.GenerateDungeon();
        }
    }
}
