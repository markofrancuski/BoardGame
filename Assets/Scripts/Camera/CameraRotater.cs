using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotater : MonoBehaviour
{
    [SerializeField] private bool _shouldCheck = true;
    [SerializeField] private bool _mouseIsUp = true;

    [SerializeField] private bool _panRight = true;

    [SerializeField] private float _rotateSpeed = 5f;
    [SerializeField] private float _minRotateSpeed;
    [SerializeField] private float _maxRotateSpeed = 15f;
    [SerializeField] private float _rotateSpeedIncreaseStep = 0.05f;

    private void OnEnable()
    {
        CameraPan.OnPanClickedUp += MouseIsUp;
        CameraPan.OnPanClickedDown += MouseIsDown;

        _minRotateSpeed = _rotateSpeed;
    }
    private void OnDisable()
    {
        CameraPan.OnPanClickedUp -= MouseIsUp;
        CameraPan.OnPanClickedDown -= MouseIsDown;
    }
    private void LateUpdate()
    {
        if (!_shouldCheck || _mouseIsUp) return;

        if(_rotateSpeed < _maxRotateSpeed)
            _rotateSpeed += _rotateSpeedIncreaseStep;
        
        if (_panRight)
        {
            PanRight();
        }
        else
        {
            PanLeft();
        }
    }

    public void ResetRotation()
    {
        transform.rotation = Quaternion.identity;
    }
    public void PanLeft()
    {
        Quaternion currentRotation = transform.rotation;
        Vector3 newRotation = new Vector3(0, currentRotation.eulerAngles.y + (_rotateSpeed * Time.deltaTime) , 0);

        currentRotation.eulerAngles = newRotation;
        transform.rotation = currentRotation;
    }
    public void PanRight()
    {
        Quaternion currentRotation = transform.rotation;
        Vector3 newRotation = new Vector3(0, currentRotation.eulerAngles.y - (_rotateSpeed * Time.deltaTime), 0);

        currentRotation.eulerAngles = newRotation;
        transform.rotation = currentRotation;
    }

    public void MouseIsUp()
    {
        _mouseIsUp = true;
        _rotateSpeed = _minRotateSpeed;
    }
    public void MouseIsDown(bool panRight)
    {
        _panRight = panRight;
        _mouseIsUp = false;
    }
    
    public void EnableInput()
    {
        _shouldCheck = true;
    }   
    public void DisableInput()
    {
        _shouldCheck = false;
    }

}
