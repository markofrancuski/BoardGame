using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraRotater))]
public class CameraRotaterEditor : Editor
{

    CameraRotater script;
    private void Awake()
    {
        script = target as CameraRotater;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Reset Rotation"))
        {
            script.ResetRotation();
        }

        base.OnInspectorGUI();
    }
}
