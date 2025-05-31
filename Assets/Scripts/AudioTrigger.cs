using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    private string TriggerTag = "Player";

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TriggerTag))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
