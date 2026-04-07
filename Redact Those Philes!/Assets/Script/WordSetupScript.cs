using UnityEngine;
using TMPro;

public class WordSetupScript : MonoBehaviour
{
    [SerializeField] public TMP_Text wordText;

    public string wordUsed;
	public int wordID;
    
    
    void Awake()
    {
        wordID = GameManagerScript.instance.GenerateWordIndex();
		wordUsed = GameManagerScript.instance.GetWord(wordID);
        wordText.text =  wordUsed;
        bool isRedactionWord = GameManagerScript.instance.wordsToRedactID.Contains(wordID);

        RedactionScript redactionScript = this.gameObject.GetComponent<RedactionScript>();
        redactionScript?.SetBanned(!isRedactionWord);
        
    }

	public void SetWord(int newWordID)
	{
		wordID = newWordID;
		wordUsed = GameManagerScript.instance.GetWord(wordID);
		wordText.text = wordUsed;
		bool isRedactionWord = GameManagerScript.instance.wordsToRedactID.Contains(wordID);

		RedactionScript redactionScript = this.gameObject.GetComponent<RedactionScript>();
		redactionScript?.SetBanned(!isRedactionWord);
	}


	
}
