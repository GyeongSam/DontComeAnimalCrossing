using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class MobStatusHandler : MonoBehaviour
{
    public ObjsInPool type = ObjsInPool.Mob_monkey;
    private Animator _ani;
    public bool isAlive=true;
    public bool isMotioning = false;
    public int hp;
    private int additedCnt = 0;
    private void Awake()
    {
        _ani = gameObject.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        isAlive = true;
        hp = DataManager.Instance.Get(type).maxHp;
        additedCnt = 0;
    }
    public void Attacked(int damage)
    {
        if (!isAlive) return;
        hp -= damage;
        Transform t = ObjManager.Instance.GetObj(ObjsInPool.Text_damage).transform;
        t.GetComponent<FloatDamageText>().t.text = damage.ToString();
        t.position = transform.position;
        t.LookAt(DataManager.Instance.playerCameraTransform);
        

        if (hp <= 0)
        {
            _ani.CrossFade("Death", 0.1f);
            isAlive = false;
            isMotioning = true;
            ObjManager.Instance.GetObj(ObjsInPool.Item_exp).transform.position = transform.position;
            return;
        }
        _ani.CrossFade("Hit", 0.1f);
        isMotioning = true;
    }
    public void Addited(int cnt)
    {

        if (additedCnt == 0)
        {
            additedCnt = cnt;
            AdditedAsync();
        }
        else
        {
            additedCnt = cnt;
        }
    }
    private async Task AdditedAsync()
    {
        while (additedCnt > 0 && isAlive)
        {
            if (Time.timeScale != 0)
            {
                int damage = DataManager.Instance.gas.damage.Get();
                additedCnt -= 1;
                Attacked(damage + Random.Range(-damage / 10, damage / 10));
                ObjManager.Instance.GetObj(ObjsInPool.Hit_poison).transform.position = transform.position;
            }
            await Task.Delay(750);
        }
    }
    public void AttackPlayer()
    {
        isMotioning = true;
        _ani.CrossFade("Attack", 0.1f);
        DataManager.Instance.playerHp -= DataManager.Instance.Get(type).power;
        GameObject go = ObjManager.Instance.GetObj(ObjsInPool.Text_damage2);
        go.transform.position = DataManager.Instance.playerTrs.position;
        go.GetComponent<FloatDamageText>().t.text = DataManager.Instance.Get(type).power.ToString();
        go.transform.LookAt(DataManager.Instance.playerCameraTransform);
    }

    public void EndMotion()
    {
        isMotioning = false;
    }
}
