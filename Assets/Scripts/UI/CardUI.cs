using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IDraggable
{

    public static UnityAction<CardBaseScriptable> OnCardInHandClicked;

    #region UI References

    [Header("UI References - Text")]
    [SerializeField] private TextMeshProUGUI _manaCostText;
    [SerializeField] private TextMeshProUGUI _cardTypeText;
    [SerializeField] private TextMeshProUGUI _cardNameText;
    [Header("UI References - Image")]
    [SerializeField] private Image _cardIconImage;

    [SerializeField]
    private Image[] _images;

    #endregion UI References

    [Header("References")]
    [SerializeField] private CardBaseScriptable _card;

    #region Variables
    [SerializeField] private bool _isHoldingMouseDown;
    private Color _transparentColor = new Color(0, 0, 0, 125);

    #endregion Variables

    private void Start()
    {
        _images = GetComponentsInChildren<Image>();
    }


    private void Update()
    {
        if (_isHoldingMouseDown)
        {
            if (Input.GetMouseButtonDown(1))
            {
                OnCancel();
            }
        }
    }

    private void OnDestroy()
    {
        
    }

    public void SetCard(CardBaseScriptable card)
    {
        _card = card;
        UpdateUI();
        gameObject.SetActive(true);
    }
    private void UpdateUI()
    {
        _manaCostText.text = _card.ManaCost.ToString();
        _cardTypeText.text = StringHelper.CardTypeAsString(_card.CardType);
        _cardNameText.text = _card.Name;

        _cardIconImage.sprite = _card.Icon;
    }

    private void HandleDraggingCardImage(bool transparent)
    {
        foreach (Image image in _images)
        {
            image.color = transparent ? _transparentColor: Color.white;
        }
    }

    /// <summary>
    /// Gets called when spawning card (Monster, Spell, Terrain).
    /// </summary>
    public void Action()
    {

    }

    public void SendToGraveyard()
    {
        Debug.Log($"Destroying card:{_card.Name}");
        Destroy(gameObject);
    }

    public void OnDrag()
    {
        throw new System.NotImplementedException();
    }

    public void OnDrop()
    {
        throw new System.NotImplementedException();
    }

    public void OnCancel()
    {
        Debug.Log("OnCancel");
        _isHoldingMouseDown = false;

        OnCardInHandClicked?.Invoke(null);
        HandleDraggingCardImage(false);

        GameManager.OnCardsSentToGraveyard -= SendToGraveyard;
        Tile.BoardPieceSpawned -= SendToGraveyard;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isHoldingMouseDown = true;
        OnCardInHandClicked?.Invoke(_card);
        HandleDraggingCardImage(true);
        GameManager.OnCardsSentToGraveyard += SendToGraveyard;
        Tile.BoardPieceSpawned += SendToGraveyard;
    }
}
