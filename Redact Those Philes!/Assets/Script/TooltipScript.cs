using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[ExecuteInEditMode()]
public class TooltipScript : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI headerText;
    [SerializeField] public TextMeshProUGUI contentText;
    
    [SerializeField] public LayoutElement layoutElement;

    [SerializeField] public int characterWrapLimit;
    
    public RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    
    

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            headerText.gameObject.SetActive(false);
        }
        else
        {
            headerText.gameObject.SetActive(true);
            headerText.text = header;
        }
        
        contentText.text = content;
        
        int headerLength = headerText.text.Length;
        int contentLength = contentText.text.Length;
        
        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true: false;


        
    }
    // Update is called once per frame
    void Update()
    {
        if (Application.isEditor)
        {
            int headerLength = headerText.text.Length;
            int contentLength = contentText.text.Length;
            
            layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true: false;
            
        }

        Vector2 mousePosition = Mouse.current.position.ReadValue();
        

        float pivotX = mousePosition.x / Screen.width;
        float pivotY = mousePosition.y / Screen.height;
        
        rectTransform.pivot = new Vector2(pivotX, pivotY);
        
        transform.position = mousePosition;


    }
}
