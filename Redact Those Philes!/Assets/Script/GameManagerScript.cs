using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

	//Prefabs
	[SerializeField] public GameObject filePrefab;
	[SerializeField] public Transform fileSpawnPoint;
	public GameObject activeFile;

	[SerializeField] public BannedWordSpawnerScript bannedWordSpawner;
    

	// Words 
    public string[] words;
	public List<int> wordsToRedactID = new List<int>(); 
	public List<int> wordsToRedactIllegallyID = new List<int>(); 

    private string filename = "word_list";
	private int baseWords = 2;
	private int maxWords = 6;

	// Money Stats
	public float balance = 200f;
	public float addToBalance = 0f;
	public float penalty = -20f;
	public float pay = 10f;
	public float donations = 0f;
	public float bills = -100f;

	// Game Stats
	public int level = 0;
	public float pageDuration = 60f;
	public float timer = 60f;
	public bool timeStarted = false;
	
	public int pageCounter = 0;
	public int pageMax = 10;
	
	// Player Stats
	public int correctWords = 0;
	public int mistakes = 0;
	public int corruptionLevel = 0;
	public bool isCorrupted = false;
	public bool corruptionMistakes = false;
	
	

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        ScrapeWords();
		NewDay();
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

    void Update()
    {
	    if (timeStarted)
	    {
		    if (timer <= 0)
		    {
			    NewFile();
			    timer = pageDuration;
		    }
		    else
		    {
				timer -= Time.deltaTime;
		    }
		    
		    
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

	public void NewDay()
	{
		ClearBannedWords();
		balance += addToBalance;
		pageCounter = 0;
		addToBalance = 0;
		mistakes = 0;
		correctWords = 0;
		donations = 0;
		
		
		timer = pageDuration;

		level++;
		UIScript.instance.UpdateLevel();
		
		int maxBannedWords = System.Math.Min(maxWords, baseWords + (int) System.Math.Ceiling((double) (level / 2)));

		for(int i = 0; i < maxBannedWords; i++)
		{
			GenerateRedactionWord();
		}
		bannedWordSpawner.SpawnWords();

		NewFile();
		timeStarted = true;
	}
    
	public void CalculatePoints()
	{
		GameObject[] page = GameObject.FindGameObjectsWithTag("Word");
		foreach (GameObject word in page)
		{
			int wordID = word.GetComponent<WordSetupScript>().wordID;
			bool isBanned = wordsToRedactID.Contains(wordID);
			bool isIllegal = wordsToRedactIllegallyID.Contains(wordID);
			bool isRedacted = word.GetComponent<RedactionScript>().IsRedacted();

			if(isBanned && isRedacted)											// If the player redacted a banned word, they get points
			{
				correctWords++;
			}
			else if (isBanned && !isRedacted || !isBanned && isRedacted)		// If a player redacted a normal word or they failed to redact a banned word, they lose points
			{ 		
				mistakes++;
			} 

			if(isIllegal && isRedacted)											// If a player redacts an Illegal bribery word, it sets the player as corrupted and keeps track
			{
				isCorrupted = true;
				corruptionLevel++;

			} 
			else if (isIllegal && !isRedacted)									// If a player refuses to cooperate with the bribery or makes a mistake, consequences will happen
			{
				corruptionMistakes = true;
				corruptionLevel--;

			}
		}
		
	}



	public void NewFile()
	{
		if (activeFile != null)
        {
        	CalculatePoints(); 
        	Destroy(activeFile);
        }

		if (pageCounter < pageMax)
		{
			timer = pageDuration;
			
			
			activeFile = Instantiate(filePrefab, fileSpawnPoint.position, transform.rotation);
			activeFile.transform.SetParent(null);
			pageCounter++;
			UIScript.instance.UpdatePages();
		}
		// 10 rounds have passed
		else
		{
			activeFile = null;
			timeStarted = false;
			Debug.Log("The Day Has Ended!");
			CalculateProfit();
			SummaryScript.instance.OpenSummaryUI();
			// NewDay();
		}

	}

	public void CalculateProfit()
	{
		addToBalance = (pay * correctWords) + (penalty * mistakes) + donations + bills; 
	}

	public void ClearBannedWords()
	{
		wordsToRedactID.Clear();
		GameObject[] bannedWords = GameObject.FindGameObjectsWithTag("Banned Words");
		foreach (GameObject word in bannedWords)
		{
			Destroy(word);
		}
	}
    
   
    

}
