using UnityEngine;

public class VictoryDialogScript : DialogSceneScript
{

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
            GameManagerScript.instance.UpdateEndAudio();
            GameManagerScript.instance.EndGame();
        }
        
    }
}
