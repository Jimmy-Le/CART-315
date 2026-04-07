using UnityEngine;

public abstract class WorldEventScript : MonoBehaviour
{
    [SerializeField] public string eventName;
    [SerializeField] public int eventId;
    [SerializeField] public string description;
    
    // Base Modifier stats
    [SerializeField] public float payModifier = 0;
    [SerializeField] public float penaltyModifier = 0;
    [SerializeField] public float pageDurationModifier = 0;
    [SerializeField] public float donationsModifier = 0;
    [SerializeField] public float billModifier = 0;


    // A function that every world event will have, it can be overriden if needed
    public virtual void ActivateEvent()
    {
        GameManagerScript gm = GameManagerScript.instance;
        gm.ModifyPay(payModifier);
        gm.ModifyPenalty(penaltyModifier);
        gm.ModifyPageDuration(pageDurationModifier);
        gm.ModifyDonations(donationsModifier);
        gm.ModifyBills(billModifier);
     
        
        Debug.Log(eventName);

    }
    
    
}
