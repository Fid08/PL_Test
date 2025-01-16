using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private LayerMask hitmask;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private CameraSettings _cameraSettings;


    private GameObject dragObject;
    private float CameraPosX;
    private Vector2 touchPos;
    private void Awake()
    {
        _gameInput = new GameInput();
    }
    private void Start()

    {
        _gameInput.Touchscreen.TapOnScreen.started += ctx => TapStart();
        _gameInput.Touchscreen.TapOnScreen.canceled += ctx => TapEnd();
    }

    void OnEnable()
    {
        _gameInput.Enable();
    }
    void OnDisable()
    {
        _gameInput.Disable();
    }
    private void Update()
    {
        
        TapObserver();
        
    }


    private void TapObserver()
    {
        CameraPosX = Camera.main.transform.position.x;
        touchPos = Camera.main.ScreenToWorldPoint(_gameInput.Touchscreen.FingerPos.ReadValue<Vector2>());

        if (dragObject)
        {
            BoardingScroll(touchPos);
            dragObject.transform.position = touchPos;
            return;
        }

        ScrollCamera();
    }

    private void ObjectHandler()
    {
        touchPos = Camera.main.ScreenToWorldPoint(_gameInput.Touchscreen.FingerPos.ReadValue<Vector2>());
        RaycastHit2D hit;

        if (hit = Physics2D.Raycast(touchPos, Vector2.zero, 1, hitmask))
        {
            dragObject = hit.transform.gameObject;
            EventBus.DragObjectStart.Invoke();
        }
    }
    private void DropActions()
    {
        if (dragObject)
        {
            EventBus.DragObjectStop.Invoke();
        }
        dragObject = null;
    }

    private void ScrollCamera()
    {
      
        CameraPosX = Mathf.Clamp(CameraPosX, _cameraSettings.CameraBoundsL, _cameraSettings.CameraBoundsR);
        CameraPosX -= _gameInput.Touchscreen.ScreenLocator.ReadValue<Vector2>().x * _cameraSettings.CameraScrollDelta;
        Camera.main.transform.position = new Vector3(CameraPosX, Camera.main.transform.position.y, Camera.main.transform.position.z);
    }

    private void BoardingScroll(Vector2 touchPos)
    {
        CameraPosX = Mathf.Clamp(CameraPosX, _cameraSettings.CameraBoundsL, _cameraSettings.CameraBoundsR);
        touchPos = _gameInput.Touchscreen.TapPosition.ReadValue<Vector2>();
        if (touchPos.x > Screen.width - _cameraSettings.ScreenWeighOffset)
        {
            Camera.main.transform.position = new Vector3(CameraPosX + _cameraSettings.CameraBoardingScrollSpeed * Time.deltaTime, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
        if (touchPos.x < _cameraSettings.ScreenWeighOffset)
        {
            Camera.main.transform.position = new Vector3(CameraPosX - _cameraSettings.CameraBoardingScrollSpeed * Time.deltaTime, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
    }



    private void TapStart()
    {
        
        ObjectHandler();
    }

    private void TapEnd()
    {

        DropActions();
    
    }

}






