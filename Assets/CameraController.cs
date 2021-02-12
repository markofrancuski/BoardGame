using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController Instance;

    [SerializeField] private Transform _cameraTransform;
    public Transform CameraTransform => _cameraTransform;
    public Vector3 DirectionFromCameraToBoard;

    private void Awake()
    {

        if(!Instance)
        {
            Instance = this;
            /// TODO: Check if i need this?
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if(!_cameraTransform)
        {
            Transform childTransform = transform.GetChild(0);
            if(!childTransform.gameObject.name.Equals("Main Camera"))
            {
                Debug.LogError("Raycasting for tiles will not work, Camera child not found!");
            }
            else
            {
                _cameraTransform = childTransform;
                DirectionFromCameraToBoard = transform.position - _cameraTransform.position;
            }
        }
    }
}
