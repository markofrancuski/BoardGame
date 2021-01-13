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


    public void SetPiece(BoardPiece pieceToSet)
    {
        _piece = pieceToSet;
    }

    public void RemovePiece()
    {
        _piece = null;
    }

}