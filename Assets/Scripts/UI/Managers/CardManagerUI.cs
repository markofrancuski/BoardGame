using Enums.Card;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CardManagerUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static CardManagerUI Instance;

    public static UnityAction<CardBaseScriptable, CardType>  OnDraggingCardSet;

    [SerializeField] private TextMeshProUGUI _graveyardAmountText;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform _cardSpawnTransform;


    [SerializeField] private CardBaseScriptable _draggingCard;
    public CardBaseScriptable DraggingCard => _draggingCard;

    #region Unity Methods
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        DeckManager.InitializeDrawnCards += InitializeNewCardsInHand;

        CardUI.OnCardInHandClicked += SetDraggingCard;
        //CardUI.OnCardActivation += (null) => { SetDraggingCard(null); }; 

        Tile.BoardPieceSpawned += DragginCardSpawned;

    }
    private void OnDestroy()
    {
        DeckManager.InitializeDrawnCards -= InitializeNewCardsInHand;

        CardUI.OnCardInHandClicked -= SetDraggingCard;
        //CardUI.OnCardActivation -= (null) => { SetDraggingCard(null); };

        Tile.BoardPieceSpawned -= DragginCardSpawned;
    }
    #endregion Unity Methods

    private void DragginCardSpawned()
    {
        GameManager.Instance.HandleSpawnedCard(_draggingCard);
        _draggingCard = null;
    }

    private void InitializeNewCardsInHand(List<CardBaseScriptable> cards)
    {
        foreach (CardBaseScriptable card in cards)
        {
            GameObject go = Instantiate(_cardPrefab, _cardSpawnTransform);
            go.GetComponent<CardUI>().SetCard(card, transform.parent);
        }
    }

    private void UpdateGraveyardAmount()
    {
        _graveyardAmountText.text = DeckManager.Instance.NumberOfCardsInGraveyard.ToString();
    }

    public void SetDraggingCard(CardBaseScriptable draggingCard)
    {
        _draggingCard = draggingCard;
        OnDraggingCardSet?.Invoke(_draggingCard, _draggingCard ? _draggingCard.CardType: 0);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        InputManager.Instance.MouseOverUI(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InputManager.Instance.MouseOverUI(false);
    }

}
