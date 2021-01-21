using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraZoom))]
public class CameraZoomEditor : Editor
{
    public Vector3 positionToMove;

    CameraZoom script;
    private void Awake()
    {
        script = target as CameraZoom;
    }

    public override void OnInspectorGUI()
    {
        positionToMove = EditorGUILayout.Vector3Field("Move Position", positionToMove);

        if (GUILayout.Button("Test Moving"))
        {
            script.MoveToThePosition(positionToMove);
        }    
        
        if (GUILayout.Button("Test Reset Moving"))
        {
            script.ResetCameraPosition();
        }


        base.OnInspectorGUI();
    }
}
