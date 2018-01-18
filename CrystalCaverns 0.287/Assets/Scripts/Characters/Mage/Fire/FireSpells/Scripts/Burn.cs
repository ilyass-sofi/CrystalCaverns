using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour {

    private Character ch;

    private float timeLeft;
    private float ticksLeft = 4;

    [SerializeField] private float damagePerSec = 1;
    [SerializeField] private float baseDuration = 5;

    void Awake ()
    {
        name = "Burn";
        
        ch = transform.parent.GetComponent<Character>();
        ch.IsBurning = true;
        transform.localScale = new Vector3(ch.GetComponent<Collider>().bounds.size.x * 0.8f, ch.GetComponent<Collider>().bounds.size.y * 0.3f, ch.GetComponent<Collider>().bounds.size.z * 0.8f) ;

        timeLeft = baseDuration;
        ticksLeft = timeLeft - 1;
    }
	
	void Update ()
    {
        timeLeft -= Time.deltaTime;

        if(timeLeft < ticksLeft)
        {
            ticksLeft--;
            ch.Health = -damagePerSec;
        }
        else if(timeLeft <= 0)
        {
            ch.IsBurning = false;
            Destroy(gameObject);
        }
    }

    public float getTimeLeft()
    {
        return timeLeft;
    }

    public float getBaseDuration()
    {
        return baseDuration;
    }

    public void ResetEffect()
    {
        timeLeft = baseDuration;
        ticksLeft = baseDuration - 1;
    }
}
