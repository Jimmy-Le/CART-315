using UnityEngine;

public class DeathDialogScript : DialogSceneScript
{
    
    public virtual void ShowDialog()
    {
        if (dialogIndex != 0)
        {
            voiceSound.Play();
        }

        if (dialogIndex == dialog.Length - 1)
        {
            SummaryScript.instance.dialogTextBox.text =
                "You have survived for " + GameManagerScript.instance.level + " days.";
        }
        else
        {
            SummaryScript.instance.dialogTextBox.text = dialog[dialogIndex];
        }
        
        
        dialogIndex++;

    }

    public override void ContinueButtonAction()
    {
        buttonSound.Play();
        if (dialogIndex < dialog.Length)
        {
            ShowDialog();
            // dialogIndex++;
        }
        else
        {
            dialogIndex = 0;
            // SummaryScript.instance.CloseDialogUI();
            // SummaryScript.instance.CloseWholeUI();
            GameManagerScript.instance.EndGame();
        }
        
    }
}
