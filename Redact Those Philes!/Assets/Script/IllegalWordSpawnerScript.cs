using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class IllegalWordSpawnerScript : MonoBehaviour
{
 
    [SerializeField] public GameObject wordPrefab;
    [SerializeField] public Transform spawnLocation;

    [SerializeField] private int rows;
    [SerializeField] private int columns = 1;

    private float widthBetween = 1f;
    private float heightBetween = 0.5f;

    private float margin = 0.2f;
    private float alternateMargin = 0.4f;

    private List<int> IllegalWordIDs;
    
    void Start()
    {
        
        SpawnWords();
    }

	public void UpdateInformation()
	{
        IllegalWordIDs = GameManagerScript.instance.wordsToRedactIllegallyID;
        rows = IllegalWordIDs.Count;
	}


    public void SpawnWords()
    {
		UpdateInformation();
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                float chosenMargin = margin;
                if (rows % 2 == 0)
                {
                    chosenMargin = alternateMargin;
                }
                
                float xPos = (this.transform.position.x + chosenMargin) + (i *  widthBetween);
                float yPos = this.transform.position.y - (j * heightBetween);
                
                GameObject newWord = Instantiate(wordPrefab, new Vector3(xPos,yPos,0), transform.rotation);
                
                newWord.GetComponent<WordSetupScript>().SetWord(IllegalWordIDs[j]);
                newWord.GetComponent<RedactionScript>().SetRedactable(false);
                
                newWord.transform.SetParent(null);
                newWord.transform.localScale = Vector3.one;
                newWord.transform.SetParent(spawnLocation);
				newWord.tag = "Illegal Words";
            }
            
        }
    }
}
