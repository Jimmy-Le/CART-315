using System;
using UnityEngine;
using TMPro;

public abstract class DialogSceneScript : MonoBehaviour
{
    [SerializeField] public string[] dialog;
   
    protected int dialogIndex = 0;

    void Start()
    {
        dialogIndex = 0;
    }
    public virtual void ShowDialog()
    {
        Debug.Log("Dialog Index: " + dialogIndex);
        SummaryScript.instance.dialogTextBox.text = dialog[dialogIndex];
        Debug.Log(dialog[dialogIndex]);
        dialogIndex++;
    }

    public virtual void ContinueButtonAction()
    {
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
