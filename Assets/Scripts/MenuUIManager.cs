using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MenuUIManager : MonoBehaviour
{

    public GameObject onSlash;
    public TMP_Text textSlash;

    public GameObject onFireBreath;
    public TMP_Text textFireBreath;

    public GameObject onExplosion;
    public TMP_Text textExplosion;

    public GameObject onGas;
    public TMP_Text textGas;


    private void OnEnable()
    {
        Time.timeScale = 0;

        if (DataManager.Instance.slash.isOn)
        {
            onSlash.SetActive(true);
            textSlash.text = $"Damage : Lv{DataManager.Instance.slash.damage.level + 1} ({DataManager.Instance.slash.damage.Get()})\nCool Time : Lv{DataManager.Instance.slash.coolTime.level + 1} ({DataManager.Instance.slash.coolTime.Get()}ms)\nRange : Lv{DataManager.Instance.slash.scale.level + 1} ({DataManager.Instance.slash.scale.Get()})";

        }
        if (DataManager.Instance.fireBreath.isOn)
        {
            onFireBreath.SetActive(true);
            textFireBreath.text = $"Damage : Lv{DataManager.Instance.fireBreath.damage.level + 1} ({DataManager.Instance.fireBreath.damage.Get()})\nCool Time : Lv{DataManager.Instance.fireBreath.coolTime.level + 1} ({DataManager.Instance.fireBreath.coolTime.Get()}ms)\nRange : Lv{DataManager.Instance.fireBreath.scale.level + 1} ({DataManager.Instance.fireBreath.scale.Get()})";

        }
        if (DataManager.Instance.explosion.isOn)
        {
            onExplosion.SetActive(true);
            textExplosion.text = $"Damage : Lv{DataManager.Instance.explosion.damage.level + 1} ({DataManager.Instance.explosion.damage.Get()})\nCool Time : Lv{DataManager.Instance.explosion.coolTime.level + 1} ({DataManager.Instance.explosion.coolTime.Get()}ms)\nRange : Lv{DataManager.Instance.explosion.scale.level + 1} ({DataManager.Instance.explosion.scale.Get()})";

        }
        if (DataManager.Instance.gas.isOn)
        {
            onGas.SetActive(true);
            textGas.text = $"Damage : Lv{DataManager.Instance.gas.damage.level + 1} ({DataManager.Instance.gas.damage.Get()})\nCool Time : Lv{DataManager.Instance.gas.coolTime.level + 1} ({DataManager.Instance.gas.coolTime.Get()}ms)\nRange : Lv{DataManager.Instance.gas.scale.level + 1} ({DataManager.Instance.gas.scale.Get()})";

        }
    }


    public void Close()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}