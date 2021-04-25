using Enums.Card;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(MeshRenderer))]
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
    [SerializeField] private BoardPiece _piece;
    #endregion Component References

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

        if (!_shouldCheckInput) return;

        OnTileClicked();
    }
    #endregion Unity Methods

    #region Public Methods

    public void Spawn(CardBaseScriptable card)
    {
        if((int)card.CardType <= 1)
        {
            CardPawnScriptable cardPawn = card as CardPawnScriptable;
            GameObject pawnObject = Instantiate(cardPawn.PawnModel, gameObject.transform.localPosition, Quaternion.identity);
            pawnObject.transform.SetParent(transform);
            SetPiece(pawnObject.GetComponent<BoardPiece>());
        }

        switch (card.CardType)
        {
            case CardType.TERRAIN:
                break;
            case CardType.SPELL:
                break;
            default:
                break;
        }

        GameManager.Instance.RemoveMana(card.ManaCost);
        BoardPieceSpawned?.Invoke();
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

        if (_piece)
        {
            HighlightTile(_greenMaterial);
            UIManager.Instance.ShowCardActionUI(_piece);
            return;
        }


       //Show Action UI

        /// TODO: Perform checks if we are draging card 
        /// if not 
        /// or if its empty 
        /// or if we can to move some pawn onto this tile 
        /// or we want to select board piece
    }
    
    protected virtual void HighlightTile(Material material)
    {
        _renderer.material = material;
    }
    #endregion Protected Methods

    #region Private Methods
    private void HandleDragginCard(CardBaseScriptable draggingCard, CardType cardType)
    {
        if (!draggingCard)
        {
            _isDraggingCard = false;
            HighlightTile(_tileMaterial);
            return;
        }

        _isDraggingCard = true;

        // Its champion or minion card
        if ((int) cardType  <= 1)
        {
            if (IsOccupied)
            {
                HighlightTile(_redMaterial);
            }
            else
            {
                if (IsSpawningTile)
                {
                    HighlightTile(_greenMaterial);
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
    }
    #endregion Private Methods
}