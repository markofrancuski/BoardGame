using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraPan : MonoBehaviour
{
    public static UnityAction<bool> OnPanClickedDown;
    public static UnityAction OnPanClickedUp;

    [SerializeField] private bool _panRight = true;

    public void OnClickDown()
    {
        OnPanClickedDown?.Invoke(_panRight);
    }

    public void OnClickUp()
    {
        OnPanClickedUp?.Invoke();
    }

}
