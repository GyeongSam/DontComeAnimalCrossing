using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ObjsInPool
{
    Mob_monkey, Mob_lizard, Mob_mouse, Mob_dear, Mob_sparrow, Mob_squid, Mob_snake,
    Skill_slash, Skill_firebreath, Skill_explosion, Skill_gas,
    Item_exp, Item_hp,
    Hit_slash, Hit_fire, Hit_explosion, Hit_poison,
    Effect_exp, Effect_heal,
    Text_damage, Text_damage2
}
public class ObjManager : MonoBehaviour
{
    public static ObjManager Instance;
    [SerializeField] private GameObject[] prefabs;
    private Queue<GameObject>[] pools;

    private void Awake()
    {
        Instance = this;
        Instance.pools = new Queue<GameObject>[prefabs.Length];
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new Queue<GameObject>();
        }
    }
    public GameObject GetObj(ObjsInPool type)
    {
        GameObject temp=null;
        int objIdx = (int)type;
        if (pools[objIdx].Count == 0)
        {
            temp = Instantiate(prefabs[objIdx]);
        }
        else temp = pools[objIdx].Dequeue();
        temp.SetActive(true);
        return temp;
    }
    public void ReturnObj(GameObject obj, ObjsInPool type)
    {
        pools[(int)type].Enqueue(obj);
        obj.SetActive(false);
    }

}
