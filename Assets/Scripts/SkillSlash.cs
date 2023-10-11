using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class SkillSlash : MonoBehaviour
{
    private Collider col;
    private void Awake()
    {
        col = GetComponent<Collider>();
    }
    private async void OnEnable()
    {
        col.enabled = true;
        await Task.Delay(200);
        col.enabled = false;
        await Task.Delay(1000);
        ObjManager.Instance.ReturnObj(gameObject, ObjsInPool.Skill_slash);
    }

    private void OnTriggerEnter(Collider other)
    {
        int damage = DataManager.Instance.slash.damage.Get();
        other.GetComponent<MobStatusHandler>().Attacked(damage + Random.Range(-damage / 10, damage / 10));
        ObjManager.Instance.GetObj(ObjsInPool.Hit_slash).transform.position = other.transform.position;
    }
}
