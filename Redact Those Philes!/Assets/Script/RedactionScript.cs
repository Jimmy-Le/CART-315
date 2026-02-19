using UnityEngine;

public class RedactionScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private SpriteRenderer blackSquare;
    private bool isClicked = false;
    private bool isRedactable = true;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClicked()
    {
        if (!isClicked && isRedactable)
        {
            blackSquare.enabled = true;
            isClicked = true;
            // Call the points function
        }
        
    }
    public void SetRedactable(bool value)
    {
        isRedactable = value;
    }
}
    