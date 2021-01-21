using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static UnityAction<GamePhase> OnGamePhaseChanged;

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
    public enum GamePhase
    {
        INITIAL_SPAWNING,
    }

    public int MaxMana;
    private int _currentMana;
    public int CurrentMana => _currentMana;

    public void AddMana(int amount)
    {
        _currentMana = _currentMana + amount > MaxMana ? MaxMana : _currentMana + amount;
    }

    public bool RemoveMana(int amount)
    {
        if (_currentMana <= 0 || amount > _currentMana) return false;

        _currentMana -= amount;

        return true;
    }

}
