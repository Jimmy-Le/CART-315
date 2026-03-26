using UnityEngine;
using UnityEngine.InputSystem;

public class DraggingHandlerScript : MonoBehaviour
{

    [SerializeField] public bool isChild = false;
    [SerializeField] public InputActionAsset inputActions;
	[SerializeField] public string objectToDrag;    

    private Camera cam;
    private InputAction dragAction;
    private bool isDragging = false;
    
    private Transform originalParent;

    public void Awake()
    {
        cam = Camera.main;
        originalParent = transform.parent;
        dragAction = inputActions.FindAction("Drag");
        
    }
    

    void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag(objectToDrag) && hit.collider.gameObject == gameObject && !isDragging)
        {
            if (dragAction.WasPressedThisFrame() )
            {
                isDragging = true;
				if(isChild)
				{
					transform.SetParent(GameManagerScript.instance.stickerFolder.transform);
				}
            }

            if (dragAction.WasReleasedThisFrame())
            {
                transform.position = new Vector3(worldPos.x, worldPos.y, 0);
                
                
            
            }
        }

        if (isDragging)
        {
            // transform.SetParent(GameManagerScript.instance.stickerFolder.transform);
            transform.position = new Vector3(worldPos.x, worldPos.y, 0);
        }
        if (dragAction.WasReleasedThisFrame())
        {
            isDragging = false;
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
				
                // transform.SetParent(hit.collider.transform);
            }
        }
       
    }
    
    
    
    public void SetDraggingStatus(bool isDragging)
    {
        isChild = isDragging;
    }
}
