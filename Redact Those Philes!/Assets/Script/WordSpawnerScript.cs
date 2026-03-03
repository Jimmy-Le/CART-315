using UnityEngine;
using System;


public class WordSpawnerScript : MonoBehaviour
{
    
    [SerializeField] public GameObject wordPrefab;
    [SerializeField] public Transform spawnLocation;

    [SerializeField] private int rows;
    private int baseRows = 4;
    private int maxRows = 10;
    
    [SerializeField] private int columns = 4;

    private float widthBetween = 1.3f;
    private float heightBetween = 0.5f;

    private float margin = 0.1f;
    private float alternateMargin = 0.2f;
    
    void Start()
    {
        rows = Math.Min(baseRows * GameManagerScript.instance.level, maxRows);
        SpawnWords();
    }

    public void SpawnWords()
    {
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
                newWord.transform.SetParent(null);
                newWord.transform.localScale = Vector3.one;
                newWord.transform.SetParent(spawnLocation);
            }
            
        }
    }
}
