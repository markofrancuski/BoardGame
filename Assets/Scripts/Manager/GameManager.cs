using Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;
    #endregion

    #region Event declaration
    public static UnityAction<GamePhase> OnGamePhaseChanged;
    public static UnityAction<List<CardBaseScriptable>> InitializeDrawnCards;
    public static UnityAction<int> OnManaChanged;
    public static UnityAction OnCardsSentToGraveyard;
    #endregion Event declaration


    public static int MaxMana = 10;
    private int _currentMana = 10;
    public int CurrentMana => _currentMana;
    
    public const int InitialDrawCardsAmount = 5;
    public int CurrentCardsInHand = 0;
    public int NumberOfCardsInGraveyard => CardsInGraveyard.Count;

    public List<CardBaseScriptable> CardsInHand = new List<CardBaseScriptable>();
    public List<CardBaseScriptable> CardsInGraveyard = new List<CardBaseScriptable>();

    [SerializeField] private GamePhase _phase;
    public GamePhase CurrentPhase
    {
        get { return _phase; }
        set
        {
            _phase = value;
            OnGamePhaseChanged?.Invoke(value);
        }
    }

    #region Unity Methods
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Invoke("TestDrawCards", 5f);
    }

    #endregion Unity Methods

    public void AddMana(int amount)
    {
        _currentMana = _currentMana + amount > MaxMana ? MaxMana : _currentMana + amount;
        OnManaChanged?.Invoke(_currentMana);
    }

    public bool RemoveMana(int amount)
    {
        if (_currentMana <= 0 || amount > _currentMana) return false;

        _currentMana -= amount;
        OnManaChanged?.Invoke(_currentMana);

        return true;
    }
    
    public void InitializeStartGame()
    {
        AddMana(MaxMana);
        // Shuffle Deck and prepare it, in queue.
        DeckManager.Instance.ShuffleDeck();
        // Draw cards from the queue in hand.
        CardsInHand = DeckManager.Instance.DrawFromDeck(InitialDrawCardsAmount);
        CurrentCardsInHand = 5;
        
        InitializeDrawnCards?.Invoke(CardsInHand);
        OnCardsSentToGraveyard?.Invoke();
    }

    public void HandleSpawnedCard(CardBaseScriptable spawnedCard)
    {
        CardsInGraveyard.Add(spawnedCard);
        OnCardsSentToGraveyard?.Invoke();
    }

    #region Test Methods

    void TestDrawCards()
    {
        InitializeStartGame();
    }

    #endregion Test Methods

}
