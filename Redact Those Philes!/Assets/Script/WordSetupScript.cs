using UnityEngine;
using TMPro;

public class WordSetupScript : MonoBehaviour
{
    [SerializeField] public TMP_Text wordText;

    public string wordUsed;
	public int wordID;
    
    
    void Start()
    {
        wordID = GameManagerScript.instance.GenerateWordIndex();
		wordUsed = GameManagerScript.instance.GetWord(wordID);
        wordText.text =  wordUsed;
    }

	public void SetWord(int newWordID)
	{
		wordID = newWordID;
		wordUsed = GameManagerScript.instance.GetWord(wordID);
		wordText.text = wordUsed;
	}

	
}
