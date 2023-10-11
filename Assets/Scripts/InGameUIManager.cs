using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InGameUIManager : MonoBehaviour
{
    public TMP_Text time;
    public TMP_Text exp;
    public TMP_Text hp;
    public Slider times;
    public Slider exps;
    public Slider hps;

    private void Update()
    {
        times.value = ((float)DataManager.Instance.playTimeH * 60 + (float)DataManager.Instance.playTimeS) / 300;
        exps.value = (float)DataManager.Instance.playerExp/DataManager.Instance.levelExp.Get();
        hps.value = (float)DataManager.Instance.playerHp / 10000;

        string h = DataManager.Instance.playTimeH.ToString();
        if (h.Length == 1) h = "0" + h;
        string s = DataManager.Instance.playTimeS.ToString();
        if (s.Length == 1) s = "0" + s;
        time.text = $"{h}:{s} / 05:00";

        exp.text = $"Lv{DataManager.Instance.levelExp.level+1}   {DataManager.Instance.playerExp} / {DataManager.Instance.levelExp.Get()}";
        hp.text = $"{DataManager.Instance.playerHp} / 10000";
    }

}
