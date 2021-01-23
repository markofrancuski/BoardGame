using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DeckManager))]
public class DeckManagerEditor : Editor
{

    DeckManager script;

    [SerializeField]
    private DeckScriptable asd;
    private void Awake()
    {
        script = DeckManager.Instance;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Set Choosen Deck"))
        {
            script.SetChoosenDeck(asd);
        }

        base.OnInspectorGUI();
    }

}
