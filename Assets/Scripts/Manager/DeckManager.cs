using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
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
    /// TODO: Remove After this is only for testing purpouse to see the order of cards inside the queue.
    [SerializeField] private List<CardBaseScriptable> BattleCardsList = new List<CardBaseScriptable>();

    public int NumberOfCardsInHand => CardsInHand.Count;
    public int NumberOfCardsInGraveyard => CardsInGraveyard.Count;
    public int NumberOfCardsRemainingInDeck => BattleCards.Count;

    public List<CardBaseScriptable> CardsInHand = new List<CardBaseScriptable>();
    public List<CardBaseScriptable> CardsInGraveyard = new List<CardBaseScriptable>();

    #endregion Variables 

    #region UI References
    [Header("UI References")]
    [SerializeField] private GameObject _deckCanvas;
    [SerializeField] private TextMeshProUGUI _cardsInGraveyardAmountText;
    [SerializeField] private TextMeshProUGUI _cardsInDeckRemainingText;
    #endregion

    #region Unity Methods

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log($"Instance of a {this.GetType()} Already exists. Destroying");
            Destroy(this);
        }

        GameManager.OnInitializeGame += InitializeGame;
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

        UpdateCardsInDeckText(NumberOfCardsRemainingInDeck);

        return drawnCards;
    }

    public void SendToGraveyardFromHand(List<CardBaseScriptable> cards)
    {
        foreach (CardBaseScriptable card in cards)
        {
            CardsInHand.Remove(card);
            CardsInGraveyard.Add(card);
        }

        UpdateGraveyardText(NumberOfCardsInGraveyard);
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
        UpdateGraveyardText(NumberOfCardsInGraveyard);
        UpdateCardsInDeckText(NumberOfCardsRemainingInDeck);
    }

    private void UpdateGraveyardText(int amount)
    {
        if(!_cardsInGraveyardAmountText)
        {
            Debug.Log("Cannot Update Cards in Graveyard Text Value, Text Component is NULL");
            return;
        }
        _cardsInGraveyardAmountText.text = amount.ToString();
    }    
    private void UpdateCardsInDeckText(int amount)
    {
        if(!_cardsInDeckRemainingText)
        {
            Debug.Log("Cannot Update Cards Remaining in Deck Text Value, Text Component is NULL");
            return;
        }
        _cardsInDeckRemainingText.text = amount.ToString();
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
