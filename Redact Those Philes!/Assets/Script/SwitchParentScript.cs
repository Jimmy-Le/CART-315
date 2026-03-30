using UnityEngine;

public class SwitchParentScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

	void Update()
	{
		GameObject stickerFolder = GameManagerScript.instance.stickerFolder;
		IsInBetweenScript script = stickerFolder.GetComponent<IsInBetweenScript>();
		if(script.IsBetween(this.gameObject.transform))
		{
			transform.SetParent(stickerFolder.transform);
		}
		else 
		{
			transform.SetParent(null);
		}
	
	}


}
