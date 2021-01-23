using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DeckManager))]
public class DeckManagerEditor : Editor
{

    DeckManager script;

    private void Awake()
    {
        script = target as DeckManager;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Set Choosen Deck"))
        {
            script.SetChoosenDeck();
        }       
        
        if (GUILayout.Button("Shuffle Cards"))
        {
            script.ShuffleDeck();
        }

        base.OnInspectorGUI();
    }

}
