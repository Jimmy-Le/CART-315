using UnityEngine;
using TMPro;

public class FadeToBlackScript : MonoBehaviour
{
    [SerializeField] public GameObject dialogUI;

    void Start()
    {
        dialogUI.SetActive(false);
    }

    public void ShowDialogUI()
    {
        dialogUI.SetActive(true);
    }
}
