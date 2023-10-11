using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    [SerializeField] private Transform thisGround;
    [SerializeField] private Transform pairGround1;
    [SerializeField] private Transform pairGround2;
    private Vector3 gap;
    [SerializeField] private bool isAxisX;
    [SerializeField] private bool isIncrease;
    // Start is called before the first frame update

    private void Awake()
    {
        if (isAxisX) gap = new Vector3(2*Mathf.Abs(thisGround.position.x - pairGround1.position.x), 0, 0);
        else gap = new Vector3(0,0,2 * Mathf.Abs(thisGround.position.z - pairGround1.position.z));
        if (!isIncrease) gap *= -1;
    }

    private void OnTriggerEnter(Collider other)
    {
        float a, b;
        if (isAxisX)
        {
            a = thisGround.position.x;
            b = pairGround1.position.x;
        }
        else
        {
            a = thisGround.position.z;
            b = pairGround1.position.z;
        }
        if (a<b != isIncrease)
        {
            pairGround1.position += gap;
            pairGround2.position += gap;
        }

    }
}
