using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{

    public static UIScript instance;
    [SerializeField] private TextMeshProUGUI pageIndicator;
    [SerializeField] private TextMeshProUGUI timerIndicator;
    
    [SerializeField] private TextMeshProUGUI levelIndicator;

    [SerializeField] private GameObject eventPanel;
    [SerializeField] private TextMeshProUGUI eventName;
    
    void Awake()
    {
        instance = this;
        // DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        UpdateLevel();
        UpdatePages();
        UpdateEventPanel();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        
    }

    public void UpdateLevel()
    {
        levelIndicator.text = "Day " + GameManagerScript.instance.level;
    }

    public void UpdateTimer()
    {
        timerIndicator.text = "Time Left: " + (int)GameManagerScript.instance.timer + (" Second(s)");
    }

    public void UpdatePages()
    {
        pageIndicator.text = GameManagerScript.instance.pageCounter + "/" + GameManagerScript.instance.pageMax + " Pages";
    }

    public void UpdateEventPanel()
    {
        eventName.text = GameManagerScript.instance.currentWorldEvent.eventName;
        eventPanel.GetComponent<TooltipTrigger>().setHeader("Event");
        eventPanel.GetComponent<TooltipTrigger>().setContent(GameManagerScript.instance.currentWorldEvent.description);
    }
	
    
}
