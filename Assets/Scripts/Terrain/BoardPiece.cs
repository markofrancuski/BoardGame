using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPiece : MonoBehaviour
{

    [SerializeField] private GameObject _canvas;

    public void ShowCanvasWithActions()
    {
        ActivateCanvas(true);
    }

    private void SetUpBoardPieceActions(CardPawnScriptable card)
    {

    }

    public void ActivateCanvas(bool activate)
    {
        _canvas.SetActive(activate);
    }
}
