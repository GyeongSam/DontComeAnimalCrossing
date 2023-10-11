using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    [SerializeField] private float spawnRad = 25f;
    private Transform target;
    private LinkedList<MobStatusHandler> activeMobList = new LinkedList<MobStatusHandler>(); 
    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(CreateMobCRT());
    }
    private void SpawnMob()
    {
        GameObject temp = ObjManager.Instance.GetObj((ObjsInPool)Random.Range(0, 7));
        temp.transform.position = new Vector3(target.position.x+Random.Range(-spawnRad, spawnRad), 0.5f, target.position.z + Random.Range(-spawnRad, spawnRad));
        temp.GetComponent<Rigidbody>().velocity = Vector3.zero;
        activeMobList.AddLast(temp.GetComponent<MobStatusHandler>());
    }
    private void MobAction()
    {
        List<MobStatusHandler> dl = new List<MobStatusHandler>();
        foreach (MobStatusHandler mob in activeMobList)
        {
            if (mob.isMotioning) continue;
            if (!mob.isAlive)
            {
                dl.Add(mob);
                continue;
            }
            mob.transform.LookAt(target);
            float d = Vector3.Distance(mob.transform.position, target.position);
            if (d < DataManager.Instance.Get(mob.type).range)
            {
                mob.AttackPlayer();
            }
            else
            {
                mob.GetComponent<Rigidbody>().MovePosition(mob.transform.position + mob.transform.forward * DataManager.Instance.Get(mob.type).moveSpeed * Time.deltaTime);
            }
        }
        foreach(MobStatusHandler mob in dl)
        {
            ObjManager.Instance.ReturnObj(mob.gameObject,mob.type);
            activeMobList.Remove(mob);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MobAction();
    }
    IEnumerator CreateMobCRT()
    {
        WaitForSeconds wt= new WaitForSeconds(3f);
        while (true)
        {
            yield return wt;
            SpawnMob();
            SpawnMob();
            SpawnMob();

        }

    }
}
