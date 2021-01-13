using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static UnityAction<GamePhase> OnGamePhaseChanged;

    private GamePhase _phase;
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
        SPAWNING,
    }

}
