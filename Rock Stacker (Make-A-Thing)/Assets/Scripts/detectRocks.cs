using UnityEngine;

public class detectRocks : MonoBehaviour
{

	public spawnRocks spawnRocksScript;
	public gameManager gameManagerScript;
	public followMouse followMouseScript;
	public AudioSource audioSource;

	private bool gameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rock") && !gameOver)
        {
			gameOver = true;			
			audioSource.Play();
            spawnRocksScript.SetGameOver();
			gameManagerScript.EndGame();
			followMouseScript.EndGame();
        }
    }
}
