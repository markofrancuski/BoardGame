using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraZoom : MonoBehaviour
{

    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;

    [SerializeField] private float _minZ;
    [SerializeField] private float _maxZ;

    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Transform _cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = _cameraTransform.position;
    }

    private void OnDrawGizmos()
    {
     
        Gizmos.color = Color.yellow;
        _startPosition = _cameraTransform.position;

        Gizmos.DrawSphere(_startPosition - new Vector3(0, _minY, 0), .25f);
        Gizmos.DrawSphere(_startPosition + new Vector3(0, _maxY, 0), .25f);   

        Gizmos.DrawSphere(_startPosition - new Vector3(0, 0, _minZ), .25f);
        Gizmos.DrawSphere(_startPosition + new Vector3(0, 0, _maxZ), .25f);   
        
        Gizmos.DrawSphere(_startPosition - new Vector3(0, 0, _minZ), .25f);
        Gizmos.DrawSphere(_startPosition + new Vector3(0, 0, _maxZ), .25f);  

        Gizmos.DrawSphere(_startPosition + new Vector3(0, _maxY, _minZ), .25f);
        Gizmos.DrawSphere(_startPosition + new Vector3(0, _maxY, -_maxZ), .25f); 
        
        Gizmos.DrawSphere(_startPosition + new Vector3(0, -_minY, _minZ), .25f);
        Gizmos.DrawSphere(_startPosition + new Vector3(0, -_minY, -_maxZ), .25f);

    }
}
