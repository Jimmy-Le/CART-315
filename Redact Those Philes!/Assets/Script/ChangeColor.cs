using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{

    [SerializeField] public Image buttonImage;
    [SerializeField] public Color baseColor;
    [SerializeField] public Color goodColor;
    

    // Update is called once per frame
    void Update()
    {
        if (GameManagerScript.instance.stickerFolder.transform.childCount > 0)
        {
           buttonImage.color =  goodColor;
        }
        else
        {
            buttonImage.color = baseColor;
        }
    }
}
