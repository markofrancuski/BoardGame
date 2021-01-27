using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.Events;

public class DeckManager : MonoBehaviour
{

    #region Singleton
    /// TODO: Comment this in order to be able to test with editor script.
    public static DeckManager Instance;

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

    public int MaxDecks;
    public List<DeckScriptable> Decks;
    
    // Gameplay Variables
    [SerializeField]
    private Queue<CardBaseScriptable> Cards = new Queue<CardBaseScriptable>();


    #endregion Variables 

    #region Variable References
    [Header("References")]

    [SerializeField] private GameObject _deckCanvas;
    #endregion Variable References

    #region Unity Methods

    public void Awake()
    {
        /// TODO: Comment this in order to be able to test with editor script.
        Instance = this;

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

    public void SetChoosenDeck(DeckScriptable choosenDeck)
    {
        if(choosenDeck.CurrentCardsInDeck < DeckScriptable.MaxCardsInDeck)
        {
            Debug.LogWarning("Cannot Set Deck need to max out all cards!");
            return;
        }
        _choosenDeck = choosenDeck;
    }
    public void ShuffleDeck()
    {
        Cards = ConvertToQueue();
    }
    public List<CardBaseScriptable> DrawFromDeck(int amount = 1)
    {
        if(Cards.Count <= 0)
        {
            Debug.LogError($"Trying to draw Cards, but none left in deck!");
            return null;
        }

        if(amount > Cards.Count)
        {
            Debug.LogWarning($"Trying to Draw:{amount}, but only have:{Cards.Count}");
            amount = Cards.Count;
        }

        List<CardBaseScriptable> drawnCards = new List<CardBaseScriptable>();

        for (int i = 0; i < amount; i++)
        {
            drawnCards.Add(Cards.Dequeue());
        }

        return drawnCards;
    }

    #region Private Methods
    private Queue<CardBaseScriptable> ConvertToQueue()
    {
        Queue<CardBaseScriptable> queue = new Queue<CardBaseScriptable>();
        _testShuffledCards = _choosenDeck.ShuffleDeck();
        foreach (CardBaseScriptable card in _testShuffledCards)
        {
            queue.Enqueue(card);
        }
        return queue;
    }

    #endregion Private Methods

    #region Test Methods

    #endregion Test Methods
}
