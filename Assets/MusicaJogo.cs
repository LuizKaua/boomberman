using UnityEngine;

public class MusicaJogo : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    // Update is called once per frame
    void Update()
    {
        audioSource.PlayOneShot(audioClip);
    }
}
