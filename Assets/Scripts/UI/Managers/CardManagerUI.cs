using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardManagerUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _cardPrefab;

    [SerializeField] private TextMeshProUGUI _graveyardAmountText;

    [SerializeField] private Transform _cardSpawnTransform;

    #region Unity Methods
    private void Start()
    {
        GameManager.InitializeDrawnCards += InitializeNewCardsInHand;
        GameManager.OnCardsSentToGraveyard += UpdateGraveyardAmount;
    }

    private void OnDestroy()
    {
        GameManager.InitializeDrawnCards -= InitializeNewCardsInHand;
        GameManager.OnCardsSentToGraveyard -= UpdateGraveyardAmount;
    }
    #endregion Unity Methods

    private void InitializeNewCardsInHand(List<CardBaseScriptable> cards)
    {
        foreach (CardBaseScriptable card in cards)
        {
            GameObject go = Instantiate(_cardPrefab, _cardSpawnTransform);
            go.GetComponent<CardUI>().SetCard(card);
        }
    }

    private void UpdateGraveyardAmount()
    {
        _graveyardAmountText.text = GameManager.Instance.NumberOfCardsInGraveyard.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InputManager.Instance.MouseOverUI(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InputManager.Instance.MouseOverUI(true);
    }

}
