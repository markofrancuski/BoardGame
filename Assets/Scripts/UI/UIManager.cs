using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    [SerializeField] private CardActionUI _cardActionUI;

    public void ShowCardActionUI(BoardPiece boardPiece)
    {
        _cardActionUI.Activate();
    }

}
