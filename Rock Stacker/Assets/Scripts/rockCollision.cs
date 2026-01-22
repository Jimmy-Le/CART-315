using UnityEngine;

public class rockCollision : MonoBehaviour
{

    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool landed = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!landed && (collision.gameObject.CompareTag("Rock") || collision.gameObject.CompareTag("BaseRock")))
        {
            audioSource.Play();
            landed = true;
            
        }
    }
}
