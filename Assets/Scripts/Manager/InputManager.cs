using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public static UnityAction<bool> OnMouseOverUI;

    private void Awake()
    {
        Instance = this;
    }

    public void MouseOverUI(bool isOver)
    {
        OnMouseOverUI?.Invoke(isOver);
    }
}
