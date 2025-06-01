using System;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField]
    private string[] audioClipNamesArray;
    private string TriggerTag = "Player";

    private bool wasTriggered = false;
    public static event Action OnAudioTrigger;

    private void OnTriggerEnter(Collider other)
    {

        if(wasTriggered)
        {
            return;
        }

        if (other.CompareTag(TriggerTag))
        {
            wasTriggered = true;
            OnAudioTrigger?.Invoke();
            foreach (string name in audioClipNamesArray)
            {
                AudioManager.Instance.PlayNarration(name);
            }
        }
    }
}
