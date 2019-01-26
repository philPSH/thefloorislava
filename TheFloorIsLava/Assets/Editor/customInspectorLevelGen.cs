using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelGenerator))]
public class customInspectorLevelGen : Editor
{

    public override void OnInspectorGUI()
    {

        LevelGenerator myTarget = (LevelGenerator)target;
        DrawDefaultInspector();
        if(GUILayout.Button("Generate Test Level"))
        {
           myTarget.GenerateLevel(myTarget.testLength, myTarget.testLength);
        }

    }

}
