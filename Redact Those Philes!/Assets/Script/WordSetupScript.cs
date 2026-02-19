using UnityEngine;
using TMPro;

public class WordSetupScript : MonoBehaviour
{
    [SerializeField] public TMP_Text wordText;

    public string wordUsed;
    
    
    void Start()
    {
        wordUsed = GameManagerScript.instance.GenerateWord();
        wordText.text =  wordUsed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
