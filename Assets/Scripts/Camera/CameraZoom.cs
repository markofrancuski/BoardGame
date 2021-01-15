using Pixelplacement;
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


    private bool CanMove = false;
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = _cameraTransform.position;
    }

    public void MoveToThePosition(Vector3 newPosition)
    {
        if (CanMove) return;

        Debug.Log(newPosition);
        newPosition += transform.position;
        Debug.Log(newPosition);

        newPosition.y = Mathf.Clamp(transform.position.y + newPosition.y, -_minY, _maxY);
        newPosition.z = Mathf.Clamp(transform.position.z + newPosition.z, -_minZ, _maxZ);

        Debug.Log($"Moving from: {transform.position} to: {newPosition}");
        Tween.Position(transform, _cameraTransform.position, newPosition, 5, 0);
    }

    public void ResetCameraPosition()
    {
        Debug.Log($"Moving from: {transform.position} to: {_startPosition}");
        Tween.Position(transform, _cameraTransform.position, _startPosition, 5, 0);
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
