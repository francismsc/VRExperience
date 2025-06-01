using System;
using UnityEngine;

public class ClaustrophobiaLvlTriggers : MonoBehaviour
{
    [SerializeField]
    private int lvlUpAmount = 1;

    private string TriggerTag = "Player";
    private bool wasTriggered = false;

    public static event Action<int> OnLvlTrigger;

    private void OnTriggerEnter(Collider other)
    {

        if (wasTriggered)
        {
            return;
        }

        if (other.CompareTag(TriggerTag))
        {
            OnLvlTrigger?.Invoke(lvlUpAmount);
            wasTriggered = true;
        }
    }
}
