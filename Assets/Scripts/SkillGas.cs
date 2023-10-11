using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class SkillGas : MonoBehaviour
{
    [SerializeField] private int duration_ms = 20000;
    [SerializeField] private int tick_cnt = 10;
    private async Task OnEnable()
    {
        await Task.Delay(duration_ms);
        ObjManager.Instance.ReturnObj(gameObject, ObjsInPool.Skill_gas);
    }

    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<MobStatusHandler>().Addited(10);
    }
}
