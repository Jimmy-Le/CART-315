using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;
    
    public string[] words;
    
    private string filename = "word_list";

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        ScrapeWords();
    }

    public void ScrapeWords()
    {
        TextAsset textFile = Resources.Load<TextAsset>(filename);
        if (textFile != null)
        {
            string content = textFile.text;

            words = content.Split('\n');
            foreach (string line in words)
                Debug.Log(line.Trim());
        }
        else
        {
             Debug.Log("Error loading words file");
        }
    }

    public string GenerateWord()
    {
        int randomIndex = Random.Range(0, words.Length);
        return words[randomIndex];
    }
    
    
    
    

}
