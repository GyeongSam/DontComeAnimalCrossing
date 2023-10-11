using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class SkillFireBreath : MonoBehaviour
{
    Collider col;
    private int HitCycleTime = 250;
    private void Awake()
    {
        col = GetComponent<Collider>();
    }
    private void OnEnable()
    {
        col.enabled = true;
        HitCycleAsync();
        
    }
    private async Task HitCycleAsync()
    {
        while (gameObject.activeSelf==true)
        {
            col.enabled = !col.enabled;
            await Task.Delay(HitCycleTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int damage = DataManager.Instance.fireBreath.damage.Get();
        other.GetComponent<MobStatusHandler>().Attacked(damage + Random.Range(-damage / 10, damage / 10));
        ObjManager.Instance.GetObj(ObjsInPool.Hit_fire).transform.position = other.transform.position;

    }


    
}
