using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FollowPlayerCamera))]

public class CameraEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FollowPlayerCamera fpc = (FollowPlayerCamera)target;

        if(GUILayout.Button("Set Min Camera Position"))
        {
            fpc.SetMinCamPosition();
        }

        if (GUILayout.Button("Set Max Camera Position"))
        {
            fpc.SetMaxCamPosition();
        }
    }
}
