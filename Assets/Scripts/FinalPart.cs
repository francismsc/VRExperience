using System.Collections;
using UnityEngine;

public class FinalPart : MonoBehaviour
{
    [SerializeField]
    private GameObject Soldier;
    [SerializeField]
    private GameObject Soldier2;

    private void Start()
    {
        ClaustrophobiaLvl.OnLevelChange += LastLvl;
    }

    private void LastLvl(int lvl)
    {
        if (lvl == 8)
        {
            StartCoroutine(ActivateSoldierAfterDelay());
        }
    }

    private IEnumerator ActivateSoldierAfterDelay()
    {
        Soldier.SetActive(true);
        yield return new WaitForSeconds(5f);
        Soldier.SetActive(false);
        Soldier2.SetActive(true);
                                          
    }
}
