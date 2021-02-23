using Enums.Card;
using Enums.Tile;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IEndDragHandler
{
    #region Event Declaration

    public static UnityAction BoardPieceSpawned;

    #endregion Event Declaration

    #region UI References
    
    [SerializeField] private Material _tileMaterial;
    [SerializeField] private Material _redMaterial;
    [SerializeField] private Material _greenMaterial;

    [SerializeField] private Renderer _renderer;

    #endregion UI References

    #region Component References

    [SerializeField]
    private BoardPiece _piece;
    public BoardPiece Piece => _piece;

    #endregion Component References

    #region Enums
    [SerializeField] private TileState _tileState;
    public TileState TileState
    {
        get { return _tileState; }
        protected set { _tileState = value; }
    }
    #endregion Enums

    #region Private Fields
    private Vector2 _tilePosition;
    #endregion Private Fields

    #region Public Fields
    public Vector2 TilePosition => _tilePosition;
    public bool IsOccupied => _piece != null ? true : false;
    #endregion Public Fields

    #region Private variables
    [SerializeField] private bool _shouldCheckInput = false;
    [SerializeField] private bool _isDraggingCard;
    #endregion Private variables

    #region Public variables
    public bool IsSpawningTile = false;
    #endregion Public variables

    #region Unity Methods
    void Start()
    {
        if (!_renderer)
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        _tilePosition = new Vector2(transform.position.x, transform.position.z );
        InputManager.OnMouseOverUI += OnInputStateChanged;
        CardManagerUI.OnDraggingCardSet += HandleDragginCard;

    }
    private void OnDestroy()
    {
        InputManager.OnMouseOverUI -= OnInputStateChanged;
        CardManagerUI.OnDraggingCardSet -= HandleDragginCard;
    }
    private void OnMouseDown()
    {
        //Debug.Log("OnMouseDown");

        if (!_shouldCheckInput) return;

        OnTileClicked();
    }
    private void OnMouseUp()
    {
        /*Debug.Log("OnMouseUp");
        if (_isDraggingCard)
        {
            Debug.Log("OnMouseUp");
            // Card had been spawned
            HandleDragginCard(null, 0);
            BoardPieceSpawned?.Invoke();
        }*/
    }
    private void OnMouseOver()
    {
        /*if (!_shouldCheckInput) return;

        if (!_isDraggingCard) return;
        {

        }
        */
        
        
       // Debug.Log($"Mouse Over Tile with position:{transform.position}");
    }
    #endregion Unity Methods

    #region Public Methods

    public void Spawn(CardBaseScriptable card)
    {

        GameManager.Instance.RemoveMana(card.ManaCost);

        switch (card.CardType)
        {
            case CardType.NORMAL:
                CardPawnScriptable cardPawn = card as CardPawnScriptable;
                GameObject pawnObject = Instantiate(cardPawn.PawnModel);
                _piece = pawnObject.GetComponent<BoardPiece>();

                break;
            case CardType.CHAMPION:
                break;
            case CardType.TERRAIN:
                break;
            case CardType.SPELL:
                break;
            default:
                break;
        }
    }

    public void SetPiece(BoardPiece pieceToSet)
    {
        _piece = pieceToSet;
    }
    public void RemovePiece()
    {
        _piece = null;
    }

    #endregion Public Methods

    #region Protected Methods
    protected virtual void OnTileClicked()
    {
        Debug.Log($"Clicked on Tile with position:{transform.position}");
    }
    #endregion Protected Methods

    #region Private Methods
    private void HandleDragginCard(CardBaseScriptable draggingCard, CardType cardType)
    {
        if (!draggingCard)
        {
            _isDraggingCard = false;
            _renderer.material = _tileMaterial;
            return;
        }

        _isDraggingCard = true;

        // Its champion or minion card
        if ((int) cardType  <= 1)
        {
            if (IsOccupied)
            {
                _renderer.material = _redMaterial;
            }
            else
            {
                if (IsSpawningTile)
                {
                    _renderer.material = _greenMaterial;
                }
            }

        }
    }
    private void OnInputStateChanged(bool shouldCheck)
    {
        // Reverse. We want to check input when value (shouldCheck) is false (Pointer is not over the UI).
        _shouldCheckInput = !shouldCheck;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("MRS BRE");
    }
    #endregion Private Methods
}