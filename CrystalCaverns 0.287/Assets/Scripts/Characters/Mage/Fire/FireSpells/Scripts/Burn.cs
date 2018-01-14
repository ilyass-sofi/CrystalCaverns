using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour {

    private Character ch;
    private float nextTick = 0;
    private int currentTicks = 0;
    [SerializeField] private float damagePerSec = 1;
    [SerializeField] private float baseDuration = 5;
    private float currentDuration;

    void Awake ()
    {
        name = "Burn";
        
        ch = transform.parent.GetComponent<Character>();
        ch.IsBurning = true;
        transform.localScale = new Vector3(ch.GetComponent<Collider>().bounds.size.x * 0.8f, ch.GetComponent<Collider>().bounds.size.y * 0.3f, ch.GetComponent<Collider>().bounds.size.z * 0.8f) ;
        currentDuration = baseDuration;
    }
	
	void Update ()
    {
        if (Time.time >= nextTick)
        {
            ch.Health = -damagePerSec;
            nextTick = Time.time + 1;
            currentTicks++;
            if (currentTicks >= currentDuration)
            {
                ch.IsBurning = false;
                Destroy(gameObject);
            }
        }
		
	}

    public void AddStack()
    {
        currentDuration += 5;
    }

    public void ResetEffect()
    {
        currentTicks = 0;
    }
}
