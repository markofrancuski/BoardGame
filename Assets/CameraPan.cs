using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraPan : MonoBehaviour
{
    public bool panRight = true;

    public void OnClickDown()
    {
        OnPanClickedDown?.Invoke(panRight);
    }

    public void OnClickUp()
    {
        OnPanClickedUp?.Invoke();
    }

    public static UnityAction<bool> OnPanClickedDown; 
    public static UnityAction OnPanClickedUp; 

}
