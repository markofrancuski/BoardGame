using Pixelplacement;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : Singleton<CameraController>
{
    public static UnityAction OnInitializeGame;

    [SerializeField] private Transform _cameraTransform;

    public Transform CameraTransform => _cameraTransform;
    [SerializeField] private Vector3 InitialPosition;

    private void Start()
    {
        if (!_cameraTransform)
        {
            Transform childTransform = transform.GetChild(0);
            if (!childTransform.gameObject.name.Equals("Main Camera"))
            {
                Debug.LogError("Raycasting for tiles will not work, Camera child not found!");
            }
            else
            {
                _cameraTransform = childTransform;
            }
        }
        Tween.LocalPosition(_cameraTransform, InitialPosition, 1, 0, completeCallback: InitializeGame);
    }

    private void InitializeGame()
    {
        OnInitializeGame?.Invoke();
    }

    public Vector3 GetDirectionFromCameraToMouseInput()
    {
        Vector3 directionFromCamera = new Vector3();

        return directionFromCamera;
    }
}
