using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;

    public void PlayAudioClip()
    {
        audioSource.PlayOneShot(audioClip);
    }
}
