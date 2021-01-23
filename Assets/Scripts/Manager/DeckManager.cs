using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DeckManager : MonoBehaviour
{

    #region Singleton
    /// TODO: Comment this in order to be able to test with editor script.
    //public static DeckManager Instance;

    #endregion Singleton

    #region Events
    public static UnityAction OnCardsInDeckCountChanged;
    #endregion Events

    #region Test Variables for Editor Testing

    [Header("Test Variables")]
    [SerializeField] private DeckScriptable _testChoosenDeck;

    [SerializeField] private List<CardBaseScriptable> _testShuffledCards;

    #endregion Test Variables for Editor Testing

    #region Variables 
    [Header("Variables")]
    [SerializeField] private DeckScriptable _choosenDeck;
    public DeckScriptable ChoosenDeck => _choosenDeck;

    [SerializeField]
    private Queue<CardBaseScriptable> Cards = new Queue<CardBaseScriptable>();

    public int MaxDecks;
    public List<DeckScriptable> Decks;

    #endregion Variables 

    #region Variable References
    [Header("References")]

    [SerializeField] private GameObject _deckCanvas;
    #endregion Variable References

    #region Unity Methods

    public void Awake()
    {
        /// TODO: Comment this in order to be able to test with editor script.
        //Instance = this;

        DeckScriptable[] decks = Resources.FindObjectsOfTypeAll<DeckScriptable>();
        if(decks.Length > MaxDecks)
        {
            Debug.LogError($"You have exceeded the number of max decks you can have!");
        }
        Decks = decks.ToList();
    }

    #endregion Unity Methods

    #region Deck Customization
    public void CreateNewDeck(string deckName)
    {

    }

    public void UpdateDeck(DeckScriptable deckToUpdate)
    {

    }

    public void RemoveDeck(DeckScriptable deckToRemove)
    {

    }
    #endregion Deck Customization

    public List<CardBaseScriptable> DrawFromDeck(int amount = 1)
    {
        List<CardBaseScriptable> drawnCards = new List<CardBaseScriptable>();


        return drawnCards;
    }

    public void SendToGraveyard( List<CardBaseScriptable> cards)
    {
        
    }


    #region Test Methods

    public void SetChoosenDeck()
    {
        _choosenDeck = _testChoosenDeck;
    }

    public void ShuffleDeck()
    {
        _testShuffledCards = _choosenDeck.ShuffleDeck();
    }

    #endregion Test Methods
}
