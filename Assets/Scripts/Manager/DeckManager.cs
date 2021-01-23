using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckManager : MonoBehaviour
{

    #region Singleton
    public static DeckManager Instance;
    #endregion

    public int MaxDecks;
    [SerializeField]
    private DeckScriptable _choosenDeck;
    public DeckScriptable ChoosenDeck => _choosenDeck;

    public List<DeckScriptable> Decks;

    #region Unity Methods
    public void Awake()
    {
        Instance = this;

        DeckScriptable[] decks = Resources.FindObjectsOfTypeAll<DeckScriptable>();
        if(decks.Length > MaxDecks)
        {
            Debug.LogError($"You have exceeded the number of max decks you can have!");
        }
        Decks = decks.ToList();
    }
    #endregion

    public void CreateNewDeck(string deckName)
    {

    }

    public void UpdateDeck(DeckScriptable deckToUpdate)
    {

    }

    public void RemoveDeck(DeckScriptable deckToRemove)
    {

    }

    public void SetChoosenDeck(DeckScriptable choosenDeck)
    {
        _choosenDeck = choosenDeck;
    }
}
