using UnityEngine;

public class StickerSlotScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject stickerPrefab;
    private GameObject activeSticker = null;

    void Start()
    {
        GenerateNewSticker();
    }

    public void GenerateNewSticker()
    {
        if (stickerPrefab != null)
        {
            GameObject sticker = Instantiate(stickerPrefab, this.transform);
            activeSticker = sticker;
        }
    }

    void Update()
    {
        if(activeSticker == null)
        {
            GenerateNewSticker();
        }
    }
    

 
}
