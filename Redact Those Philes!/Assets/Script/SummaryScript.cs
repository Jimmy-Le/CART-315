using UnityEngine;
using TMPro;

public class SummaryScript : MonoBehaviour
{
	public static SummaryScript instance;
    [SerializeField] public GameObject summaryUI;
    [SerializeField] public TextMeshProUGUI balanceText;
    [SerializeField] public TextMeshProUGUI totalText;
    [SerializeField] public TextMeshProUGUI correctText;
    [SerializeField] public TextMeshProUGUI mistakesText;
    
    [SerializeField] public TextMeshProUGUI donationText;
    [SerializeField] public TextMeshProUGUI billsText;


    void Start()
    {
        summaryUI.SetActive(false);
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
        balanceText.text = "$ " + GameManagerScript.instance.balance;
		totalText.text = "$ " + newBalance;
		correctText.text = "$ " + GameManagerScript.instance.pay + " X " + GameManagerScript.instance.correctWords;
		mistakesText.text = "$ " + GameManagerScript.instance.penalty + " X " + GameManagerScript.instance.mistakes;
		donationText.text = "$ " + GameManagerScript.instance.donations;
		billsText.text = "$ " + GameManagerScript.instance.bills;
		
    }

    public void OpenSummaryUI()
    {
		UpdateAllText();
        summaryUI.SetActive(true);
    }
    public void CloseSummaryUI()
    {
        summaryUI.SetActive(false);
		GameManagerScript.instance.NewDay();
    }
}
