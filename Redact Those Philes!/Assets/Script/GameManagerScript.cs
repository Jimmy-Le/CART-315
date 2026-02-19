using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;
    

	// Words 
    public string[] words;
	public List<int> wordsToRedactID = new List<int>(); 
    private string filename = "word_list";
	private int baseWords = 2;
	private int maxWords = 6;

	// Money Stats
	public float money = 100f;
	public float penalty = -10f;

	// Game Stats
	public int level = 1;
	public float pageDuration = 60f;
	
	

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        ScrapeWords();
		newDay();
    }

    public void ScrapeWords()
    {
        TextAsset textFile = Resources.Load<TextAsset>(filename);
        if (textFile != null)
        {
            string content = textFile.text;
            words = content.Split('\n');
        }
        else
        {
             Debug.Log("Error loading words file");
        }
    }

    public int GenerateWordIndex()
    {
        int randomIndex = Random.Range(0, words.Length);
        return randomIndex;
    }

	public string GetWord(int wordID)
	{
		return words[wordID];
	}

	public void GenerateRedactionWord()
	{
		int redactionWordID;
		
		do {
			redactionWordID = GenerateWordIndex();
		} while (wordsToRedactID.Contains(redactionWordID));

		wordsToRedactID.Add(redactionWordID);
	}

	public void newDay()
	{
		wordsToRedactID.Clear();
		int maxBannedWords = System.Math.Min(maxWords, baseWords + (int) System.Math.Ceiling((double) (level / 2)));

		for(int i = 0; i < maxBannedWords; i++)
		{
			GenerateRedactionWord();
		}
	}
    
    
    
    

}
