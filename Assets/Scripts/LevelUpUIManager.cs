using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Threading.Tasks;
public class LevelUpUIManager : MonoBehaviour
{
    public TMP_Text[] choice_skillNames;
    public TMP_Text[] choice_upgradeNames;
    public TMP_Text[] choice_details;
    private ChoiceData[] cds;
    private void OnEnable()
    {
        Time.timeScale = 0;
        cds = DataManager.Instance.GetUpgradeList();
        for (int i=0; i < 3; ++i)
        {
            choice_skillNames[i].text = cds[i].skill.skillName;
            choice_upgradeNames[i].text = cds[i].upgradeName;
            choice_details[i].text = cds[i].detail;
        }
    }
    public void choice(int a)
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        cds[a].method.Invoke();
    }
}
