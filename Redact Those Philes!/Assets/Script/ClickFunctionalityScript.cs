using UnityEngine;
using UnityEngine.InputSystem;

public class ClickFunctionalityScript : MonoBehaviour
{
    [SerializeField] public InputActionAsset inputActions;
    [SerializeField] private Camera cam;
    
    private InputAction clickAction;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clickAction = inputActions.FindAction("Click");
    }

    // Update is called once per frame
    void Update()
    {
        if (clickAction.WasPerformedThisFrame())
        {
            OnClickPerformed();
        }
    }
    
    void OnClickPerformed()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null)
        {
            RedactionScript word = hit.collider.GetComponent<RedactionScript>();
            word?.OnClicked();
        }
    }
}
