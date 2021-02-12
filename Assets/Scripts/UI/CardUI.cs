using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IDraggable
{
    #region Event Declaration
    public static UnityAction<CardBaseScriptable> OnCardInHandClicked;
    #endregion Event Declaration

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

    #region Component References
    [Header("Component References")]
    [SerializeField] private CardBaseScriptable _card;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Transform _handsParent;
    [SerializeField] private Transform _canvasParent;
    #endregion Component References

    #region Private Variables

    [SerializeField] private bool _isHoldingMouseDown;
    private Color _transparentColor = new Color(0, 0, 0, 125);

    #endregion Private Variables

    #region Unity Methods
    private void Start()
    {
        _images = GetComponentsInChildren<Image>();

        if (!_rectTransform)
            _rectTransform = GetComponent<RectTransform>();

        if (!_handsParent)
            _handsParent = transform.parent.transform;

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

    #endregion Unity Methods

    #region Public Methods

    public void SetCard(CardBaseScriptable card, Transform canvasParent)
    {
        _card = card;
        UpdateUI();
        gameObject.SetActive(true);
        _canvasParent = canvasParent;
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

    #endregion Public Methods

    #region Private Methods
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
            image.color = transparent ? _transparentColor : Color.white;
        }
    }
    #endregion Private Methods

    #region Interface Implementation
    public void OnCancel()
    {
        Debug.Log("OnCancel");
        _isHoldingMouseDown = false;

        OnCardInHandClicked?.Invoke(null);
        HandleDraggingCardImage(false);

        GameManager.OnCardsSentToGraveyard -= SendToGraveyard;
        Tile.BoardPieceSpawned -= SendToGraveyard;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        RaycastHit hit;

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        _rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Create a copy of this card (UI)
        _rectTransform.SetParent(_canvasParent);
        HandleDraggingCardImage(true);
    }

    #endregion Interface Implementation

    /*
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
*/
}
