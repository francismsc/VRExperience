using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;

public class ClaustrophobiaLvl : MonoBehaviour
{
    [SerializeField]
    private int currentLevel = 0;
    public static event Action<int> OnLevelChange;

    [SerializeField]
    private GameObject OriginalBuilding;
    [SerializeField]
    private GameObject ClaustrophobicBuilding;

    public void Awake()
    {
        ClaustrophobiaLvlTriggers.OnLvlTrigger += ChangeClaustrophobiaLvl;
    }

    public void OnDisable()
    {
        ClaustrophobiaLvlTriggers.OnLvlTrigger -= ChangeClaustrophobiaLvl;
    }

    public void ChangeClaustrophobiaLvl(int amount)
    {
        currentLevel = amount;
        LevelChange();
    }



    public void LevelChange()
    {
        OnLevelChange?.Invoke(currentLevel);

        switch(currentLevel)
        {
            case 5:
                OriginalBuilding.SetActive(false);
                ClaustrophobicBuilding.SetActive(true);
                break;
            case 8:
                break;
        }
    }

    



    


}
