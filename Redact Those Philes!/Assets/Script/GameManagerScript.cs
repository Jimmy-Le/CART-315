using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;
    
    public string[] words;
	public List<string> wordsToRedact = new List<string>();
    
    private string filename = "word_list";

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
		string redactionWord;
		
		do {
			redactionWord = GetWord(GenerateWordIndex());
		} while (wordsToRedact.Contains(redactionWord));

		wordsToRedact.Add(redactionWord);
	}

	public void newDay()
	{
		wordsToRedact.Clear();
		GenerateRedactionWord();
		GenerateRedactionWord();
		GenerateRedactionWord();
	}
    
    
    
    

}
