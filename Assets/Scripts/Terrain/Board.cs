using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int Size;

    [SerializeField]
    private GameObject _cameraParent;

    [SerializeField]
    private Vector2 _boardCenter;
    public Vector2 BoardCenter => _boardCenter;

    void Start()
    {
        float pos = (Size/2) -0.5f;
        _boardCenter = new Vector2(pos, pos);
    }

}
