using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class SkillManager : MonoBehaviour
{
    public GameObject skillInfoUI;
    private bool skillRunning = false;
    private Transform player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        skillRunning = true;
        DoSkillMagneticTakeItemAsync();
        DoTimeAsync();
        DoSkillHealAsync();
    }
    public void ActivateSlash()
    {
        DataManager.Instance.slash.isOn = true;
        DoSkillSlashAsync();
    }
    public void ActivateExplosion()
    {
        DataManager.Instance.explosion.isOn = true;
        DoSkillExplosionAsync();
    }
    public void ActivateFireBreath()
    {
        DataManager.Instance.fireBreath.isOn = true;
        DoSkillFireBreathAsync();
    }
    public void ActivateGas()
    {
        DataManager.Instance.gas.isOn = true;
        DoSkillGasAsync();

    }
    private async Task DoSkillSlashAsync()
    {
        while (skillRunning)
        {
            if (Time.timeScale != 0)
            {
                GameObject temp = ObjManager.Instance.GetObj(ObjsInPool.Skill_slash);
                temp.transform.position = player.position;
                temp.transform.localScale = new Vector3(DataManager.Instance.slash.scale.Get(), 1, DataManager.Instance.slash.scale.Get());
            }
            await Task.Delay(DataManager.Instance.slash.coolTime.Get());
        }
    }
    private void OnDisable()
    {
        skillRunning = false;

    }
    private async Task DoSkillFireBreathAsync()
    {
        GameObject temp = ObjManager.Instance.GetObj(ObjsInPool.Skill_firebreath);
        temp.transform.SetParent(player);
        temp.transform.localPosition = Vector3.zero;
        while (skillRunning)
        {
            if (Time.timeScale != 0)
            {
                temp.transform.localRotation = Quaternion.identity;
                temp.transform.localScale = new Vector3(DataManager.Instance.fireBreath.scale.Get(), 1, DataManager.Instance.fireBreath.scale.Get());
                temp.SetActive(true);
                await Task.Delay(10000);
                temp.SetActive(false);
            }
            await Task.Delay(DataManager.Instance.fireBreath.coolTime.Get());
        }
    }
    private async Task DoSkillExplosionAsync()
    {
        while (skillRunning)
        {
            if (Time.timeScale != 0)
            {
                GameObject temp = ObjManager.Instance.GetObj(ObjsInPool.Skill_explosion);
                temp.transform.position = player.position;
                temp.transform.localScale = DataManager.Instance.explosion.scale.Get() * Vector3.one;

            }
            await Task.Delay(DataManager.Instance.explosion.coolTime.Get());
        }
    }
    private async Task DoSkillGasAsync()
    {
        while (skillRunning)
        {
            if (Time.timeScale != 0)
            {
                GameObject temp = ObjManager.Instance.GetObj(ObjsInPool.Skill_gas);
                temp.transform.position = player.position + Vector3.forward * Random.Range(-5f, 5f) + Vector3.right * Random.Range(-5f, 5f);
                temp.transform.localScale = DataManager.Instance.gas.scale.Get() * Vector3.one;
            } 
            await Task.Delay(DataManager.Instance.gas.coolTime.Get());
        }
    }
    private async Task DoSkillHealAsync()
    {
        while (skillRunning)
        {
            if (Time.timeScale != 0)
            {
                GameObject temp = ObjManager.Instance.GetObj(ObjsInPool.Item_hp);
                temp.transform.position = player.position + Vector3.forward * Random.Range(-30f, 30f) + Vector3.right * Random.Range(-30f, 30f);
            }
            await Task.Delay(15000);
        }
    }
    private async Task DoSkillMagneticTakeItemAsync()
    {
        while (skillRunning)
        {
            if (Time.timeScale != 0)
            {
                Collider[] items = Physics.OverlapSphere(player.position, 7f, 1 << 3);
                foreach (Collider item in items)
                {
                    MagneticMove(item);
                    item.enabled = false;
                }
            }
            await Task.Delay(500);
        }
    }
    private async Task MagneticMove(Collider col)
    {
        Vector3 itemStartPos = col.transform.position;
        int frame = 0;
        while (frame++<25)
        {
            col.transform.position = Vector3.Lerp(itemStartPos, player.position, 0.05f*frame);
            await Task.Delay(30);
        }
        col.enabled = true;
        GameObject effect;
        if (col.gameObject.name[0] == 'A')
        {
            ObjManager.Instance.ReturnObj(col.gameObject, ObjsInPool.Item_exp);
            effect = ObjManager.Instance.GetObj(ObjsInPool.Effect_exp);
            DataManager.Instance.ExpUp(10);
            effect.transform.position = player.position;
            await Task.Delay(1000);
            ObjManager.Instance.ReturnObj(effect, ObjsInPool.Effect_exp);
        }
        else
        {
            ObjManager.Instance.ReturnObj(col.gameObject, ObjsInPool.Item_hp);
            effect = ObjManager.Instance.GetObj(ObjsInPool.Effect_heal);
            effect.transform.position = player.position;
            effect.transform.SetParent(player);
            DataManager.Instance.playerHp += 500;
            if (DataManager.Instance.playerHp > 10000) DataManager.Instance.playerHp = 10000;
            await Task.Delay(4000);
            ObjManager.Instance.ReturnObj(effect, ObjsInPool.Effect_heal);
        }
        
    }

    private async Task DoTimeAsync()
    {
        while (skillRunning)
        {
            await Task.Delay(1000);
            if (Time.timeScale == 0) continue;
            if (++DataManager.Instance.playTimeS == 60)
            {
                DataManager.Instance.playTimeH += 1;
                DataManager.Instance.playTimeS = 0;
            }
        }
    }
    public void PopUpMenu()
    {
        if (Time.timeScale == 0) return;
        skillInfoUI.SetActive(true);
    }
}