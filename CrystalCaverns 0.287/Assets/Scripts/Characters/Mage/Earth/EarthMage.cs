using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMage : Mage
{

    private RockCircle rockCircle;
    private float nextRockCd;
    private float rockCd = 3;


    public override void Awake()
    {
        base.Awake();
        rockCircle = Instantiate(Resources.Load("Spells/Earth/RockCircle") as GameObject,transform).GetComponent<RockCircle>() ;
    }

    public override void Update()
    {
        base.Update();

        if (Time.time >= nextRockCd && rockCircle.getCurrentRockCount() < rockCircle.getBaseRockCount())
        {
            nextRockCd = Time.time + rockCd;
            rockCircle.AddRock();
        }

    }

    public override void BasicAttack()
    {
        
        // Check if cd is off
        if (rockCircle.getCurrentRockCount() > 0)
        {
            rockCircle.RemoveRock();
            // Spawn the prefab
            GameObject ball = Instantiate(basicAttackPrefab, shootPoint.position, shootPoint.rotation);
            // Make it look at the target
            ball.transform.LookAt(rayCont.NormalTarget);
            // Set the basic damage
            ball.GetComponent<SharpRock>().Damage = Damage;
            // Impulse the projectile to the target
            ball.GetComponent<Rigidbody>().AddForce(ball.transform.forward * 1000);
        }
    }

    public override void FirstSpell()
    {
        throw new System.NotImplementedException();
    }

    public override void SecondSpell()
    {
        throw new System.NotImplementedException();
    }

    public override void Ultimate()
    {
        throw new System.NotImplementedException();
    }

   
}
