using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] protected float timeDestroy;

    protected float damage;
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public virtual void Start()
    {   
        if(timeDestroy > 0) Destroy(gameObject, timeDestroy);
    }
}
