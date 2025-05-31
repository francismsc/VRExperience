using System;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField]
    private string audioClipName;
    private string TriggerTag = "Player";

    private bool wasTriggered = false;

    public static event Action OnTrigger;

    private void OnTriggerEnter(Collider other)
    {

        if(wasTriggered)
        {
            return;
        }

        if (other.CompareTag(TriggerTag))
        {
            OnTrigger?.Invoke();
            wasTriggered = true;
            AudioManager.Instance.PlaySfx(audioClipName);
        }
    }
}
