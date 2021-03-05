using UnityEngine;
using UnityEngine.Events;

public class InputManager : Singleton<InputManager>
{

    public static UnityAction<bool> OnMouseOverUI;

    public void MouseOverUI(bool isOver)
    {
        OnMouseOverUI?.Invoke(isOver);
    }
}
