using Enums;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{

    public static UnityAction BoardPieceSpawned;

    [SerializeField] private Material _tileMaterial;
    [SerializeField] private Material _redMaterial;
    [SerializeField] private Material _greenMaterial;

    [SerializeField] private Renderer _renderer;

    private Vector2 _tilePosition;
    public Vector2 TilePosition => _tilePosition;

    [SerializeField]
    private BoardPiece _piece;
    public BoardPiece Piece => _piece;

    
    public bool IsOccupied => _piece != null ? true : false;
    public bool IsSpawningTile = false;

    [SerializeField] private bool _shouldCheckInput = false;
    [SerializeField] private bool _isDraggingCard;

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
        Debug.Log("OnMouseDown");

        if (!_shouldCheckInput) return;

        Debug.Log("OnMouseDown");
        OnTileClicked();
    }


    private void OnMouseUp()
    {
        Debug.Log("OnMouseUp");
        if (_isDraggingCard)
        {
            Debug.Log("OnMouseUp");
            // Card had been spawned
            HandleDragginCard(null, 0);
            BoardPieceSpawned?.Invoke();
        }
    }

    private void OnMouseOver()
    {
        if (!_shouldCheckInput) return;

        if (!_isDraggingCard) return;

        Debug.Log($"Mouse Over Tile with position:{transform.position}");
    }

    public void SetPiece(BoardPiece pieceToSet)
    {
        _piece = pieceToSet;
    }
    public void RemovePiece()
    {
        _piece = null;
    }

    protected virtual void OnTileClicked()
    {
        Debug.Log($"Clicked on Tile with position:{transform.position}");
    }

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

}