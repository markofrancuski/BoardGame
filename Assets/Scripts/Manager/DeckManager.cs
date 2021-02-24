using System.Collections.Generic;
using System.Linq;
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
    public static UnityAction<List<CardBaseScriptable>> InitializeDrawnCards;

    #endregion Events

    #region Test Variables for Editor Testing

    [Header("Test Variables")]
    [SerializeField] private DeckScriptable _testBattleDeck;

    #endregion Test Variables for Editor Testing

    #region Constants
    public const int InitialDrawCardsAmount = 5;
    #endregion

    #region Variables 
    [Header("Variables")]
    [SerializeField] private DeckScriptable _battleDeck;
    public DeckScriptable BattleDeck => _battleDeck;

    public int MaxDecks;
    public List<DeckScriptable> Decks;

    private Queue<CardBaseScriptable> BattleCards = new Queue<CardBaseScriptable>();
    [Header("Gameplay Variables")]
    [SerializeField] private List<CardBaseScriptable> BattleCardsList = new List<CardBaseScriptable>();

    public int NumberOfCardsInHand => CardsInHand.Count;
    public int NumberOfCardsInGraveyard => CardsInGraveyard.Count;

    public List<CardBaseScriptable> CardsInHand = new List<CardBaseScriptable>();
    public List<CardBaseScriptable> CardsInGraveyard = new List<CardBaseScriptable>();

    #endregion Variables 

    #region Variable References
    [Header("References")]

    [SerializeField] private GameObject _deckCanvas;
    #endregion Variable References

    #region Unity Methods

    public void Awake()
    {
        GameManager.OnInitializeGame += InitializeGame;
        
        /// TODO: Comment this in order to be able to test with editor script.
        Instance = this;

        DeckScriptable[] decks = Resources.FindObjectsOfTypeAll<DeckScriptable>();
        if(decks.Length > MaxDecks)
        {
            Debug.LogError($"You have exceeded the number of max decks you can have!");
        }
        Decks = decks.ToList();
    }

    private void OnDestroy()
    {
        GameManager.OnInitializeGame -= InitializeGame;
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

    #region Public Methods

    public static Queue<T> ListToQueue<T>(List<T> entityList) where T : class
    {
        Queue<T> queue = new Queue<T>();
        foreach (T entity in entityList)
        {
            queue.Enqueue(entity);
        }
        return queue;
    }

    /// <summary>
    /// Sets the Deck that will be used in game.
    /// </summary>
    /// <param name="deck"></param>
    public void SetBattleDeck(DeckScriptable deck)
    {
        if(deck.CurrentCardsInDeck < DeckScriptable.MaxCardsInDeck)
        {
            Debug.LogWarning("Cannot Set Deck need to max out all cards!");
            return;
        }
        _battleDeck = deck;
    }
    /// <summary>
    /// Sets the cards that will be used in game.
    /// </summary>
    /// <param name="cards"></param>
    public void SetBattleCards(List<CardBaseScriptable> cards)
    {
        BattleCardsList = cards;
    }

    public void ShuffleCards()
    {
        BattleCardsList = ShuffleCards(BattleCardsList);
        BattleCards = ListToQueue(BattleCardsList);
    }
    public List<CardBaseScriptable> DrawFromDeck(int amount = 1)
    {
        if(BattleCards.Count <= 0)
        {
            Debug.LogError($"Trying to draw Cards, but none left in deck!");
            return null;
        }

        if(amount > BattleCards.Count)
        {
            Debug.LogWarning($"Trying to Draw:{amount}, but only have:{BattleCards.Count}");
            amount = BattleCards.Count;
        }

        List<CardBaseScriptable> drawnCards = new List<CardBaseScriptable>();

        for (int i = 0; i < amount; i++)
        {
            drawnCards.Add(BattleCards.Dequeue());
            /// TODO: Testing Remove After
            BattleCardsList.RemoveAt(0);
        }

        return drawnCards;
    }

    public void SendToGraveyardFromHand(List<CardBaseScriptable> cards)
    {
        /// TODO:
    }
    #endregion Public Methods

    #region Private Methods

    private void InitializeGame()
    {
        SetBattleDeck(_testBattleDeck);
        SetBattleCards(_testBattleDeck.Cards);

        ShuffleCards();
        // Draw cards from the queue in hand.
        CardsInHand = DrawFromDeck(InitialDrawCardsAmount);
        
        InitializeDrawnCards?.Invoke(CardsInHand);
    }

    private List<CardBaseScriptable> ShuffleCards(List<CardBaseScriptable> cards)
    {
        if (cards.Count <= 2)
        {
            Debug.LogWarning($"Cannot shuffle deck, count is <= 2");
            return null;
        }

        int maxCards = cards.Count;

        CardBaseScriptable[] shuffledCards = new CardBaseScriptable[maxCards];

        for (int i = 0; i < maxCards; i++)
        {
            shuffledCards[i] = cards[i];
        }

        int iteration = Random.Range(50, 101);

        while (iteration > 0)
        {
            int firstIndex = Random.Range(0, maxCards);

            int secondIndex = Random.Range(0, maxCards);
            while (secondIndex == firstIndex)
            {
                secondIndex = Random.Range(0, maxCards);
            }

            CardBaseScriptable firstCard = shuffledCards[firstIndex];
            CardBaseScriptable secondCard = shuffledCards[secondIndex];

            // Swap two cards
            shuffledCards[firstIndex] = secondCard;
            shuffledCards[secondIndex] = firstCard;

            iteration -= 1;
        }

        return shuffledCards.ToList();
    }

    #endregion Private Methods

}
