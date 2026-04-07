using UnityEngine;
using TMPro;

public class SummaryScript : MonoBehaviour
{
	public static SummaryScript instance;
    [SerializeField] public GameObject summaryUI;
    [SerializeField] public GameObject summaryUIBlock;
	[SerializeField] public TextMeshProUGUI dayEndText;
    [SerializeField] public TextMeshProUGUI balanceText;
    [SerializeField] public TextMeshProUGUI totalText;
    [SerializeField] public TextMeshProUGUI correctText;
    [SerializeField] public TextMeshProUGUI mistakesText;
    
    [SerializeField] public TextMeshProUGUI donationText;
    [SerializeField] public TextMeshProUGUI billsText;
    
    // Dialog Stuff
    [SerializeField] public GameObject dialogUI;
    [SerializeField] public TextMeshProUGUI dialogTextBox;
    


    void Start()
    {
        summaryUI.SetActive(false);
        summaryUIBlock.SetActive(false);
        dialogUI.SetActive(false);
    }
	
	void Awake()
	{
		instance = this;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAllText()
    {
		float newBalance = GameManagerScript.instance.balance + GameManagerScript.instance.addToBalance;
		dayEndText.text = "End of Day " + GameManagerScript.instance.level;
        balanceText.text = GameManagerScript.instance.balance + " $";
		totalText.text = newBalance + " $";
		correctText.text = GameManagerScript.instance.correctWords + " X " + GameManagerScript.instance.pay + " $";
		mistakesText.text = "- (" + GameManagerScript.instance.mistakes + " X " + Mathf.Abs(GameManagerScript.instance.penalty) + ") $";
		donationText.text = GameManagerScript.instance.donations + " $";
		billsText.text = GameManagerScript.instance.bills + " $";
		
    }

    public void OpenSummaryUI()
    {
		UpdateAllText();
        summaryUI.SetActive(true);
        summaryUIBlock.SetActive(true);
        dialogUI.SetActive(false);
    }
    public void CloseSummaryUI()
    {
	    summaryUIBlock.SetActive(false);
		GameManagerScript.instance.OpenDialog();
    }
    public void CloseJustSummaryUI()
    {
	    summaryUIBlock.SetActive(false);
    }
    
    

    public void CloseWholeUI()
    {
	    summaryUI.SetActive(false);
    }
    
    public void ShowDialogUI()
    {
	    dialogUI.SetActive(true);
    }
    public void CloseDialogUI()
    {
	    dialogUI.SetActive(true);
    }
}
