using UnityEngine;

public class SwitchRedactionListEvent : WorldEventScript
{
   
    public override void ActivateEvent()
    {
        GameManagerScript gm = GameManagerScript.instance;
        gm.ModifyPay(payModifier);
        gm.ModifyPenalty(penaltyModifier);
        gm.ModifyPageDuration(pageDurationModifier);
        gm.ModifyDonations(donationsModifier);
        gm.ModifyBills(billModifier);

        gm.switchRedactionList = true;
        
        Debug.Log(eventName);

    }
}
