using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class HitManager : MonoBehaviour
{
    public ObjsInPool type;
    private async void OnEnable()
    {
        await Task.Delay(1000);
        ObjManager.Instance.ReturnObj(gameObject,type);
    }
}
