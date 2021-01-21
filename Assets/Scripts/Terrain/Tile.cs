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

    void Start()
    {
        _tilePosition = new Vector2(transform.position.x, transform.position.z );
    }

    private void OnMouseDown()
    {
        OnTileClicked();
    }

    protected virtual void OnTileClicked()
    {
        Debug.Log($"Clicked on Tile iwht position:{transform.position}");
    }

    public void SetPiece(BoardPiece pieceToSet)
    {
        _piece = pieceToSet;
    }

    public void RemovePiece()
    {
        _piece = null;
    }
}