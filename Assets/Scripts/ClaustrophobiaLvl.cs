using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;

public class ClaustrophobiaLvl : MonoBehaviour
{
    [SerializeField]
    private int currentLevel = 0;
    public static event Action<int> OnLevelChange;

    public void Awake()
    {
        ClaustrophobiaLvlTriggers.OnLvlTrigger += IncreaseClaustrophobiaLvl;
    }

    public void OnDisable()
    {
        ClaustrophobiaLvlTriggers.OnLvlTrigger -= IncreaseClaustrophobiaLvl;
    }

    public void IncreaseClaustrophobiaLvl(int amount)
    {
        currentLevel+= amount;
        LevelChange();
    }

    public void DecreasePostProcessingLevel(int amount)
    {
        currentLevel-= amount;
        LevelChange();
    }

    public void LevelChange()
    {
        OnLevelChange?.Invoke(currentLevel);
    }



    


}
