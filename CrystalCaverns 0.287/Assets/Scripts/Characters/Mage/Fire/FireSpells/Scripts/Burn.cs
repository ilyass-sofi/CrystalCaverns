using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour {

    private Character ch;
    private float nextTick = 0;
    private int currentTicks = 0;
    [SerializeField] private float damagePerSec = 1;
    [SerializeField] private float duration = 5;

    void Awake ()
    {
        ch = transform.parent.GetComponent<Character>();
        ch.IsBurning = true;
    }
	
	void Update ()
    {
        if (Time.time >= nextTick)
        {
            ch.Health = -damagePerSec;
            nextTick = Time.time + 1;
            currentTicks++;
            if (currentTicks >= duration)
            {
                ch.IsBurning = false;
                Destroy(gameObject);
            }
        }
		
	}

    public void addStack()
    {
        duration += 5;
    }
}
