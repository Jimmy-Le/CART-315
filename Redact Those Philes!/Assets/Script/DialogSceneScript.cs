using System;
using UnityEngine;
using TMPro;

public abstract class DialogSceneScript : MonoBehaviour
{
    [SerializeField] public string[] dialog;
	[SerializeField] public AudioSource voiceSound;
    [SerializeField] public AudioSource buttonSound;
	[SerializeField] public bool encounterMafia = false;
   
    protected int dialogIndex = 0;

    void Start()
    {
        //dialogIndex = 0;
		if(encounterMafia)
        {
            GameManagerScript.instance.encounteredMafia = true;
        }
    }
    public virtual void ShowDialog()
    {
        if (dialogIndex != 0)
        {
            voiceSound.Play();
        }
        
        SummaryScript.instance.dialogTextBox.text = dialog[dialogIndex];
        dialogIndex++;
    }

    public virtual void ContinueButtonAction()
    {
        buttonSound.Play();
        if (dialogIndex < dialog.Length)
        {
            Debug.Log("Dialog Index:" + dialogIndex + " Dialog Length :" + dialog.Length);
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
    
    public virtual void SetDialogIndex(int index)
    {
        dialogIndex = index;
    }
    
}
