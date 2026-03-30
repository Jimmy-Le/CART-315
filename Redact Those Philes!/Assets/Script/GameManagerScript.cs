using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

	//Prefabs
	[SerializeField] public GameObject filePrefab;
	[SerializeField] public Transform fileSpawnPoint;
	[SerializeField] public GameObject stickerFolder;

	[SerializeField] public GameObject IllegalNote;
	[SerializeField] public Transform IllegalNoteSpawnPoint;
	private GameObject existingIllegalNote;
	public GameObject activeFile;

	[SerializeField] public BannedWordSpawnerScript bannedWordSpawner;

	// 0: Normal Dialog
	// 1: Bribery Dialog
	// 2: Death Dialog
	// 3: Bills Dialog
	// 4: Fired Dialog
	[SerializeField] public List<DialogSceneScript> allDialogs = new List<DialogSceneScript>();
	[SerializeField] public DialogSceneScript currentDialogScene;
    

	// Words 
    public string[] words;
	public List<int> wordsToRedactID = new List<int>(); 
	public List<int> wordsToRedactIllegallyID = new List<int>(); 

    private string filename = "word_list";
	private int baseWords = 2;
	private int maxWords = 5;

	// Money Stats
	public float balance = 200f;
	public float addToBalance = 0f;
	public float penalty = -20f;
	public float pay = 10f;
	public float donations = 0f;
	public float bills = -100f;
	
	// Base Stats
	public float basePenalty = -20f;
	public float basePay = 10f;
	public float baseDonations = 0f;
	public float baseBills = -100f;
	public float basePageDuration = 60f;

	// Game Stats
	public int level = 0;
	public float pageDuration = 60f;
	public float timer = 60f;
	public bool timeStarted = false;
	
	public int pageCounter = 0;
	public int pageMax = 5;
	public int pageLimit = 7;
	public int pageBase = 5;
	
	// Player Stats
	public int correctWords = 0;
	public int mistakes = 0;
	public int corruptionLevel = 0;
	public bool isCorrupted = false;
	public bool corruptionMistakes = false;
	
	// World Events
	[SerializeField] private WorldEventScript[] worldEvents;
	public WorldEventScript currentWorldEvent;

	public bool switchRedactionList = false;
	
	// 0: No Event
	// 1: Dow is over 50000, no penalty is applied
	// 2: Being investigated, Double penalties for mistakes
	// 3: Government Shutdown, No Pay
	// 4: Tariff Money, Double Pay
	// 5: Deadline is approaching, Take as much time as you need, page Timer set to 999f
	// 6: Move the Goalpost, the list to redact changes every new page
	
	

    void Awake()
    {
	    if (instance != null && instance != this)
	    {
		    Destroy(gameObject);  // Destroys the duplicate GameObject
		    return;
	    }
	    
        instance = this;
        // DontDestroyOnLoad(gameObject);
        ScrapeWords();
		// NewDay();
    }

    void Start()
    {
	    OpenIntro();
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
		} while (wordsToRedactID.Contains(redactionWordID) || wordsToRedactIllegallyID.Contains(redactionWordID));

		wordsToRedactID.Add(redactionWordID);
	}
	
	public void GenerateIllegalRedactionWord()
	{
		int redactionWordID;
		
		do {
			redactionWordID = GenerateWordIndex();
		} while (wordsToRedactID.Contains(redactionWordID) || wordsToRedactIllegallyID.Contains(redactionWordID));

		wordsToRedactIllegallyID.Add(redactionWordID);
	}
	

	public void NewDay()
	{
		ClearBannedWords();
		ClearIllegalWords();
		balance += addToBalance;
		pageCounter = 0;
		addToBalance = 0;
		mistakes = 0;
		correctWords = 0;
		donations = 0;
		ResetEvents();
		
		
		timer = pageDuration;

		level++;
		UIScript.instance.UpdateLevel();
		
		int maxBannedWords = System.Math.Min(maxWords, baseWords + (int) System.Math.Ceiling((double) (level / 2)));
		pageMax = System.Math.Min(pageLimit, pageBase + (int)System.Math.Ceiling((double)(level / 3)));
		if (level >= 2)
		{
			SetUpEvents();
			UIScript.instance.UpdateEventPanel();
		}

		for(int i = 0; i < maxBannedWords; i++)
		{
			GenerateRedactionWord();
		}
		bannedWordSpawner.SpawnWords();

		NewFile();
		timeStarted = true;
	}

	public void RegenerateRedactionWords()
	{
		ClearBannedWords();
		int maxBannedWords = System.Math.Min(maxWords, baseWords + (int) System.Math.Ceiling((double) (level / 2)));
		for(int i = 0; i < maxBannedWords; i++)
		{
			GenerateRedactionWord();
		}
		bannedWordSpawner.SpawnWords();
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
			else if (isIllegal && !isRedacted && isCorrupted)					// If a player refuses to cooperate with the bribery or makes a mistake, consequences will happen
			{
				corruptionMistakes = true;
				corruptionLevel--;

			}
		}
		
	}
	

	public void SetUpEvents()
	{
		ResetEvents();
		currentWorldEvent = worldEvents[Random.Range(0, worldEvents.Length)];

		currentWorldEvent.ActivateEvent();
	}

	public void ResetEvents()
	{
		penalty = basePenalty;
		pay = basePay;
		pageDuration = basePageDuration;
		bills = baseBills;
		donations = baseDonations;
		switchRedactionList = false;
	}


	public void SpawnIllegalNote()
	{
		existingIllegalNote = Instantiate(IllegalNote, IllegalNoteSpawnPoint);
	}

	public void NewFile()
	{
		if (activeFile != null)
        {
        	CalculatePoints(); 
        	Destroy(activeFile);
			ClearStickers();
        }

		if (pageCounter < pageMax)
		{
			timer = pageDuration;
			
			
			activeFile = Instantiate(filePrefab, fileSpawnPoint.position, transform.rotation);
			activeFile.transform.SetParent(null);
			pageCounter++;
			UIScript.instance.UpdatePages();

			if (switchRedactionList)
			{
				RegenerateRedactionWords();
			}
			
			
			
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
	public void ClearIllegalWords()
	{
		wordsToRedactIllegallyID.Clear();
		GameObject[] illegalWords = GameObject.FindGameObjectsWithTag("Illegal Words");
		foreach (GameObject word in illegalWords)
		{
			Destroy(word);
		}

		if (existingIllegalNote != null)
		{
			Destroy(existingIllegalNote);
			existingIllegalNote = null;
		}
	}
	
	
	// Setters 

	public void ModifyPenalty(float newPenalty)
	{
		penalty += newPenalty;
	}

	public void ModifyPay(float newPay)
	{
		pay += newPay;
	}

	public void ModifyDonations(float newDonations)
	{
		donations += newDonations;
	}

	public void ModifyBills(float newBills)
	{
		bills += newBills;
	}

	public void ModifyPageDuration(float newPageDuration)
	{
		pageDuration += newPageDuration;
	}

	public void ClearStickers()
	{
		// Copy children first so the loop isn’t affected by Destroy
    	var toDestroy = new System.Collections.Generic.List<GameObject>();
    	foreach (Transform child in stickerFolder.transform)
		{
        	toDestroy.Add(child.gameObject);
		}

    	foreach (var go in toDestroy)
		{
        	Destroy(go); // destroys child and its own children
		}
	}

	// 0: Normal Dialog (Default)
	// 1: Bribery Dialog (Dividable by 4)
	// 2: Death Dialog (Dividable by 3)
	// 3: Bills Dialog (Guaranteed)
	// 4: Fired Dialog (Dividable by 5)
	public void SetupDialogScene()
	{
		int randomChance = Random.Range(0, 100);
		int dialogChoice = 0;
		if (balance + addToBalance < bills)
		{
			currentDialogScene = allDialogs[3];	// Death by Bills
			return;
		}

		if (corruptionLevel > 10 && randomChance % 5 == 0)	
		{
			dialogChoice = 4;	// Fired
		}
		else if (isCorrupted && corruptionMistakes && corruptionLevel < -5 && randomChance % 3 == 0)
		{
			dialogChoice = 2;	// Death by Mafia
		}
		else if (randomChance % 4 == 0)
		{
			dialogChoice = 1;		// Bribery
		}

		currentDialogScene = allDialogs[dialogChoice];
	}


	public void OpenDialog()
	{
		SetupDialogScene();
		currentDialogScene.SetDialogIndex(0);
		currentDialogScene.ShowDialog();
		SummaryScript.instance.ShowDialogUI();
	}

	
	public void OpenIntro()
	{
		currentDialogScene = allDialogs[5];
		currentDialogScene.SetDialogIndex(0);
		currentDialogScene.ShowDialog();
		SummaryScript.instance.OpenSummaryUI();
		SummaryScript.instance.CloseJustSummaryUI();
		SummaryScript.instance.ShowDialogUI();
	}

	public void ContinueButtonClicked()
	{
		currentDialogScene.ContinueButtonAction();
	}

	public void EndGame()
	{
		
	}

}
