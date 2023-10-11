using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class SkillExplosion : MonoBehaviour
{
    [SerializeField] private int delay_ms=3000;
    [SerializeField] private GameObject particle1;
    [SerializeField] private GameObject particle2;
    [SerializeField] private Collider col;
    private void OnEnable()
    {
        particle1.SetActive(true);
        particle2.SetActive(false);
        col.enabled = false;
        Ignite();
    }
    private async Task Ignite()
    {
        await Task.Delay(delay_ms);
        particle1.SetActive(false);
        particle2.SetActive(true);
        col.enabled = true;
        await Task.Delay(200);
        col.enabled = false;
        await Task.Delay(1000);
        ObjManager.Instance.ReturnObj(gameObject, ObjsInPool.Skill_explosion);
    }
    private void OnTriggerEnter(Collider other)
    {
        int damage = DataManager.Instance.explosion.damage.Get();
        other.GetComponent<MobStatusHandler>().Attacked(damage + Random.Range(-damage/10,damage/10));
        ObjManager.Instance.GetObj(ObjsInPool.Hit_explosion).transform.position = other.transform.position;
    }

}
