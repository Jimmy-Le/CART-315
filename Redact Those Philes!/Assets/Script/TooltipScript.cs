using UnityEngine;
using TMPro;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class TooltipScript : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI headerText;
    [SerializeField] public TextMeshProUGUI contentText;
    
    [SerializeField] public LayoutElement layoutElement;

    [SerializeField] public int characterWrapLimit;

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
       

    }
}
