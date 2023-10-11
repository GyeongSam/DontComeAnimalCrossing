using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class StepData
{
    public string upgradeName;
    public int level;
    public int[] values;
    public void LevelUp()
    {
        if (CanLevelUp()) level++;
    }
    public int Get()
    {
        return values[level];
    }
    public bool CanLevelUp()
    {
        return level < values.Length - 1;
    }
}
[System.Serializable]
public class SkillData
{
    public bool isOn;
    public string skillName;
    public StepData damage;
    public StepData coolTime;
    public StepData scale;
}
[System.Serializable]
public class MobData
{
    public int maxHp;
    public float moveSpeed;
    public int power;
    public float range;
}
[System.Serializable]
public class ChoiceData
{
    public SkillData skill;
    public string upgradeName;
    public string detail;
    public Action method;
}

public class DataManager : MonoBehaviour
{
    public int playerHp=10000;
    public StepData levelExp;
    public int playerExp = 0;

    public int playTimeH = 0;
    public int playTimeS = 0;

    public SkillData slash;
    public SkillData fireBreath;
    public SkillData explosion;
    public SkillData gas;

    public MobData monkey;
    public MobData sparrow;
    public MobData lizard;
    public MobData squid;
    public MobData dear;
    public MobData snake;
    public MobData mouse;

    public static DataManager Instance;
    public GameObject LevelUpUI;

    public Transform playerCameraTransform;
    public Transform playerTrs;

    List<ChoiceData> upgradeList = new List<ChoiceData>();

    private void Awake()
    {
        Instance = this;
        SkillManager sm = GameObject.Find("SkillManager").GetComponent<SkillManager>();

        ChoiceData cd1 = new ChoiceData();
        ChoiceData cd2 = new ChoiceData();
        ChoiceData cd3 = new ChoiceData();
        ChoiceData cd4 = new ChoiceData();
        
        cd1.skill = slash;
        cd1.upgradeName = "Activate the Skill";
        cd1.detail = "Attack monsters in all directions";
        cd1.method = sm.ActivateSlash;

        cd2.skill = fireBreath;
        cd2.upgradeName = "Activate the Skill";
        cd2.detail = "Fire a strong flame in the forward range";
        cd2.method = sm.ActivateFireBreath;

        cd3.skill = explosion;
        cd3.upgradeName = "Activate the Skill";
        cd3.detail = "Install the bomb in the player's location";
        cd3.method = sm.ActivateExplosion;
        
        cd4.skill = gas;
        cd4.upgradeName = "Activate the Skill";
        cd4.detail = "Spray poison gas to random locations near the player";
        cd4.method = sm.ActivateGas;

        upgradeList.Add(cd1);
        upgradeList.Add(cd2);
        upgradeList.Add(cd3);
        upgradeList.Add(cd4);

        foreach (ChoiceData cd in upgradeList)
        {
            SkillData sd = cd.skill;
            sd.damage.upgradeName = "Increase damage";
            sd.coolTime.upgradeName = "Reduce cool time";
            sd.scale.upgradeName = "Expands the hitting range";
        }
        playerCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        playerTrs = GameObject.FindGameObjectWithTag("Player").transform;
        LevelUpUI.SetActive(true);
    }
    public MobData Get(ObjsInPool type)
    {
        switch (type)
        {
            case ObjsInPool.Mob_dear:
                return dear;
            case ObjsInPool.Mob_lizard:
                return lizard;
            case ObjsInPool.Mob_monkey:
                return monkey;
            case ObjsInPool.Mob_mouse:
                return mouse;
            case ObjsInPool.Mob_snake:
                return snake;
            case ObjsInPool.Mob_sparrow:
                return sparrow;
            case ObjsInPool.Mob_squid:
                return squid;
            default:
                Debug.Log("잘못된 타입");
                return null;
        }
    }
    

    public ChoiceData[] GetUpgradeList()
    {
        List<ChoiceData> cds = new List<ChoiceData>();

        foreach (ChoiceData cd in upgradeList)
        {

            if (cd.skill.isOn)
            {
                StepData[] sds = { cd.skill.damage, cd.skill.coolTime, cd.skill.scale };
                foreach (StepData sd in sds)
                {
                    if (sd.CanLevelUp())
                    {
                        ChoiceData nc = new ChoiceData();
                        nc.skill = cd.skill;
                        nc.upgradeName = sd.upgradeName;
                        nc.detail = $"{sd.values[sd.level]} to {sd.values[sd.level+1]}";
                        nc.method = sd.LevelUp;
                        cds.Add(nc);
                    }
                }
            }
            else
            {
                cds.Add(cd);
            }
        }
        
        ChoiceData[] cc = new ChoiceData[3];

        for (int i=0; i < 3; ++i)
        {
            int c = UnityEngine.Random.Range(0, cds.Count);
            cc[i] = cds[c];
            cds.RemoveAt(c);
        }
        return cc;
    }

    public void ExpUp(int x)
    {
        playerExp += x;
        if (playerExp >= levelExp.Get())
        {
            playerExp -= levelExp.Get();
            levelExp.LevelUp();
            LevelUpUI.SetActive(true);
        }
    }


    public void GameEnd(bool success)
    {

    }

}
