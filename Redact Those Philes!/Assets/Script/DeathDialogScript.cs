using UnityEngine;

public class DeathDialogScript : DialogSceneScript
{

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
        }
        
    }
}
