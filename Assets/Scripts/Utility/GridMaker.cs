using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour
{

    [SerializeField]
    private int _xSize;
    [SerializeField]
    private int _zSize;

    [SerializeField]
    private Color _gizmoColor;

    [SerializeField] private Transform _tileTransform;
    [SerializeField] private GameObject _tilePrefab;
    private Tile[,] _board;


    public int firstSpawningRows = 2;

    public List<int> spawningRows = new List<int>();
    private void Start()
    {
        CreateMap();
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < _xSize; i++)
        {
            for (int j = 0; j < _zSize; j++)
            {
                Vector3 vec = gameObject.transform.position;
                vec.x += i;
                vec.z += j;
                Gizmos.color = _gizmoColor;
                Gizmos.DrawSphere(vec, .1f);
            }
        }
       
    }
    public void CreateMap()
    {
        _board = new Tile[_xSize, _zSize];

        for (int i = 0; i < _tileTransform.childCount; i++)
        {
            Destroy(_tileTransform.GetChild(i).gameObject);
        }

        List<int> spawningRows = new List<int>();
        // Add first X rows into the list
        for (int i = 0; i < firstSpawningRows; i++)
        {
            spawningRows.Add(i);
        }
        // Add last-x to last into the list
        for (int i = _zSize-firstSpawningRows; i < _zSize; i++)
        {
            spawningRows.Add(i);
        }


        for (int z = 0; z < _zSize; z++)
        {
            GameObject row = new GameObject();
            row.name = $"Row {z}";
            row.transform.SetParent(_tileTransform);

            bool isSpawningRow = false;
            if (spawningRows[0] == z)
            {
                Debug.Log($"{row.name} is Spawning Row ");
                isSpawningRow = true;
            }

            for (int x = 0; x < _xSize; x++)
            {
                GameObject tilePrefab = Instantiate(_tilePrefab);
                _board[z, x] = tilePrefab.GetComponent<Tile>();

                tilePrefab.GetComponent<Tile>().IsSpawningTile = isSpawningRow;

                Vector3 vec = gameObject.transform.position;
                vec.x += x;
                vec.z += z;
                tilePrefab.transform.position = vec;
                tilePrefab.transform.SetParent(row.transform);
            }

            if(isSpawningRow)
            {
                spawningRows.RemoveAt(0);
            }
        }
    }
}
