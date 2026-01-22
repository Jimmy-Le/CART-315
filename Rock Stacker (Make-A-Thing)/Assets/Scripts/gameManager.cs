using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class gameManager : MonoBehaviour
{

    public TextMeshProUGUI rockCounterText;
    public spawnRocks spawnRocksScript;

    public GameObject gameOverPanel;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverPanel.SetActive(false);
    }


    public void EndGame()
    {
        int rockCounter = spawnRocksScript.GetRockCounter();
        rockCounterText.text = $"{rockCounter} Rocks!";
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}
