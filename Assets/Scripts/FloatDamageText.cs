using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;
public class FloatDamageText : MonoBehaviour
{
    public ObjsInPool type;
    public RectTransform text;
    public TMP_Text t;
    private float moveRad = 2f * Mathf.PI * 0.05f / 3f;
    private float maxH = 0.3f;
    private int cnt = 20;

    private async void OnEnable()
    {
        int c = 0;
        while (++c < cnt)
        {
            text.localPosition = new Vector3(0, maxH * Mathf.Sin(moveRad * c), 0);
            await Task.Delay(25);
        }
        ObjManager.Instance.ReturnObj(gameObject, type);
    }

}
