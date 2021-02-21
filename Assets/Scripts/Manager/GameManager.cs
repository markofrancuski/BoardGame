using Enums.Game;
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
    public static UnityAction<int> OnManaChanged;
    public static UnityAction<CardBaseScriptable> OnCardsSentToGraveyard;
    public static UnityAction OnInitializeGame;
    #endregion Event declaration

    public static int MaxMana = 10;
    private int _currentMana = 10;
    public int CurrentMana => _currentMana;
    

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
        /// TODO: Make proper singleton pattern.
        Instance = this;
    }

    private void Start()
    {
        Invoke("TestDrawCards", 1f);
    }

    #endregion Unity Methods

    #region Public Methods
    public void InitializeStartGame()
    {
        /// TODO: Rework this initializing game state.
        /// inCase we disconnected during the gameplay and return to the game, we would need to sync up the data.
        AddMana(MaxMana);
        
        OnInitializeGame?.Invoke();
    }
    public void HandleSpawnedCard(CardBaseScriptable spawnedCard)
    {
        //CardsInGraveyard.Add(spawnedCard);
    }

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

    public void CardsSentToGraveyard(List<CardBaseScriptable> cards)
    {
        //CardsInGraveyard.AddRange(cards);
    }

    #endregion Public Methods

    #region Test Methods

    void TestDrawCards()
    {
        InitializeStartGame();
    }

    #endregion Test Methods

}
