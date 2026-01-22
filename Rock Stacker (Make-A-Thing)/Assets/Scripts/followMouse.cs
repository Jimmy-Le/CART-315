using UnityEngine;
using UnityEngine.InputSystem;

public class followMouse : MonoBehaviour
{
    public Transform objectToAttach;
    public Camera mainCamera;
    private Vector2  mousePosition;
    private Vector3 targetPosition;

	private bool gameOver = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
	{
		if(!gameOver)
		{
        	mousePosition = Mouse.current.position.ReadValue();
        	targetPosition =
            	mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane));

        	targetPosition.z = transform.position.z;
        
        	objectToAttach.position = targetPosition;
		}
    }   

	public void EndGame()
	{
		gameOver = true;
	}
}
