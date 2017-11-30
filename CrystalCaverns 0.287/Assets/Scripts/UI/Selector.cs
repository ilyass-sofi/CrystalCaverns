using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{

    private Vector3 big = new Vector3(1.25f,1.25f,1.25f);
    private Vector3 normal = new Vector3(1, 1, 1);

    public void MakeBigger()
    {
        transform.GetChild(0).localScale = big;
        transform.GetChild(1).localScale = big;
    }

    public void MakeNormal()
    {
        transform.GetChild(0).localScale = normal;
        transform.GetChild(1).localScale = normal;
    }
}
