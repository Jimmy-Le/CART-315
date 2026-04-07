using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SwitchScenes : MonoBehaviour
{
	[SerializeField] public AudioSource audioSource;
	// [SerializeField] public AudioClip audioClip;
	[SerializeField] public string sceneName;
	[SerializeField] public float additionalDelay = 0f;
   
	public void ChangeScene()
	{
		StartCoroutine(PlayThenLoad());
	}
	
	private IEnumerator PlayThenLoad()
	{
		audioSource.Play();
		yield return new WaitForSeconds(audioSource.clip.length + additionalDelay);
		SceneManager.LoadScene(sceneName);
	}
	
	
	public void ChangeAudio(AudioSource newAudio)
	{
		audioSource = newAudio;
	}


}
