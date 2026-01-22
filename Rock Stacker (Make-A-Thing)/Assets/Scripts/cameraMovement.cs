using UnityEngine;
using UnityEngine.InputSystem;

public class cameraMovement : MonoBehaviour
{
    public Transform cameraTransform;
    public InputActionAsset inputActions;
    
    // Camera Data
    private InputAction camera_move;

    
    // Camera Parameters
    public float speed = 10f;

    private float direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        camera_move = inputActions.FindAction("MoveCamera");
    }
    
    // Update is called once per frame
    void Update()
    {
        if (camera_move.IsPressed())
        {
            direction = camera_move.ReadValue<float>();

            if (direction < 0 && cameraTransform.position.y < -10)
            {
                
            }
            else
            {
                cameraTransform.Translate(0, direction * speed * Time.deltaTime, 0, Space.World);
            }

            
        }
    }

	public void IncreaseCameraHeight(){
		cameraTransform.Translate(0, speed * Time.deltaTime, 0, Space.World);
	}

}
