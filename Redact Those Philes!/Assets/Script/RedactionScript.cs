using UnityEngine;

public class RedactionScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private SpriteRenderer blackSquare;
    [SerializeField] public AudioSource audio;
    [SerializeField] public AudioSource badAudio;
    [SerializeField] public bool isBad = true;
    [SerializeField] public Color badColor;
    private bool isClicked = false;
    private bool isRedactable = true;
    

    public void OnClicked()
    {
        if (!isClicked && isRedactable)
        {
            

            if (isBad)
            {
                badAudio.Play();
                blackSquare.color = badColor;
            }

            else
            {
                audio.Play();
            }
            
            blackSquare.enabled = true;
            isClicked = true;
            // Call the points function
        }
        
    }
        
    public void SetRedactable(bool value)
    {
        isRedactable = value;
    }

    public void SetBanned(bool value)
    {
        isBad = value;
    }


	public bool IsRedacted()
	{
		return (isRedactable && isClicked);
	}

}
    