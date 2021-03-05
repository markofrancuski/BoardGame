using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IDraggable
{
    #region Event Declaration
    public static UnityAction<CardBaseScriptable> OnCardInHandClicked;
    public static UnityAction<CardBaseScriptable> OnCardActivation;
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
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private GameManager _gameManager;
    #endregion Component References

    #region Private Variables

    [SerializeField] private bool _isHoldingMouseDown;
    private Color _transparentColor = new Color(0, 0, 0, 125);

    #endregion Private Variables

    #region Unity Methods
    private void Start()
    {
        _images = GetComponentsInChildren<Image>();
        _gameManager = GameManager.Instance;

        if (!_rectTransform)
            _rectTransform = GetComponent<RectTransform>();

        if (!_handsParent)
            _handsParent = transform.parent.transform;

        _cameraController = CameraController.Instance;

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

        transform.localScale = new Vector3(1f,1f,1f);
        transform.SetParent(_handsParent);

        OnCardInHandClicked?.Invoke(null);
        HandleDraggingCardImage(false);

        Tile.BoardPieceSpawned -= SendToGraveyard;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        Debug.DrawRay(ray.origin, ray.direction * 20, Color.red, 10);
        if(Physics.Raycast(ray.origin, ray.direction, out hit, 50, 1<< 8))
        {
            if (_gameManager.CurrentMana < _card.ManaCost)
            {
                Debug.Log($"Cannot Activate Card:({_card.Name}) not enough mana ({_card.ManaCost}), have ({_gameManager.CurrentMana })!");
                OnCancel();
                return;
            }
            
            Debug.Log(hit.collider.name);
            Tile tile = hit.collider.gameObject.GetComponent<Tile>();
            if(!tile.IsOccupied && tile.IsSpawningTile)
            {
                /// TODO: Subscibe all of there methods to the OnCardActivation event.
                tile.Spawn(_card);
                OnCardActivation?.Invoke(_card);
                CardManagerUI.Instance.SetDraggingCard(null);
                Destroy(gameObject);
            }
        }
        else
        {
            OnCancel();
            // Cancel => Return card to the hand
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPos = new Vector3(eventData.delta.x, eventData.delta.y, 0);
        _rectTransform.position += newPos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isHoldingMouseDown = true;
        transform.localScale = new Vector3(0.5f, 0.5f);
        OnCardInHandClicked?.Invoke(_card);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Create a copy of this card (UI)
        _rectTransform.SetParent(_canvasParent);
        HandleDraggingCardImage(true);
    }

    #endregion Interface Implementation

}
