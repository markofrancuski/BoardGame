using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    private Vector2 _tilePosition;
    public Vector2 TilePosition => _tilePosition;

    [SerializeField]
    private BoardPiece _piece;
    public BoardPiece Piece => _piece;

    public bool IsOccupied => _piece != null ? true : false;
    public bool IsSpawningTile = false;

    [SerializeField] private bool _shouldCheckInput = false;

    void Start()
    {
        _tilePosition = new Vector2(transform.position.x, transform.position.z );
        InputManager.OnMouseOverUI += OnInputStateChanged;
    }

    private void OnDestroy()
    {
        InputManager.OnMouseOverUI -= OnInputStateChanged;
    }

    private void OnMouseDown()
    {
        if (!_shouldCheckInput) return;

        OnTileClicked();
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

    private void OnInputStateChanged(bool shouldCheck)
    {
        _shouldCheckInput = shouldCheck;
    }

}