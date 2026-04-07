using UnityEngine;

public class DonationsScript : DialogSceneScript
{
    [SerializeField] public int maxIllegalWords = 2;
    [SerializeField] public float donationMoney = 300f;

    public override void ContinueButtonAction()
    {
        if (dialogIndex < dialog.Length)
        {
            ShowDialog();
            // dialogIndex++;
        }
        else
        {
            dialogIndex = 0;
            SummaryScript.instance.CloseDialogUI();
            SummaryScript.instance.CloseWholeUI();
            GameManagerScript.instance.NewDay();
            GameManagerScript.instance.ModifyDonations(donationMoney);
            
            for(int i = 0; i < maxIllegalWords; i++)
            {
                GameManagerScript.instance.GenerateIllegalRedactionWord();
            }
           GameManagerScript.instance.SpawnIllegalNote();
        }
        
    }
}