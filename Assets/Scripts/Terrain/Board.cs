using UnityEngine;

public class Board : MonoBehaviour
{
    public int Size;

    [SerializeField]
    private GameObject _cameraParent;

    [SerializeField]
    private Vector3 _boardCenter;
    public Vector3 BoardCenter => _boardCenter;

    void Start()
    {
        float pos = (Size/2) -0.5f;
        _boardCenter = new Vector3(pos, 0, pos);
        PositionParentToTheCenter();
    }

    public void PositionParentToTheCenter()
    {
        _cameraParent.transform.position = _boardCenter;
    }

}
