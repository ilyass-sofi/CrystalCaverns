using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMage : Mage
{
    #region Spells Data

    // Basic Attack - Fireball
    private float basicProjectileSpeed = 1000;

    // First Spell - Explosion
    private float explosionSpeed = 1500;
    private float firstDmgMultiplier = 170.0f / 100.0f;

    // Second Spell - Fire Spiral
    private bool fireSpiralPlacement;
    private float spiralRange = 10;
    private float spiralDmgMultiplier = 140.0f / 100.0f;

    // Ultimate - Fire Wave
    private float ultDmgMultiplier = 230.0f / 100.0f;

    #endregion

    public override void Awake()
    {
        base.Awake();
        groundSprite = Resources.Load("Spells/ZoneFire") as GameObject;
    }

    public override void Update()
    {
        base.Update();

        if (combat && fireSpiralPlacement)
        {
            ZonePlacement(ref nextSecondSpellCd, ref secondSpellCd, ref fireSpiralPlacement, spiralRange, spiralDmgMultiplier, secondSpellPrefab);
        }
        else if (zone)
        ClearPlacementZone(ref fireSpiralPlacement);

    }


    public override void BasicAttack()
    {
        // Check if cd is off
        if ((Time.time >= nextBasicCd || godMode) && !zone)
        {
            // Add Cd
            nextBasicCd = Time.time + basicCd;
            // Spawn the prefab
            GameObject ball = Instantiate(basicAttackPrefab, shootPoint.position, shootPoint.rotation);
            // Make it look at the target
            ball.transform.LookAt(rayCont.NormalPosition);
            // Set the basic damage
            ball.GetComponent<FireBall>().Damage = Damage;
            // Impulse the projectile to the target
            ball.GetComponent<Rigidbody>().AddForce(ball.transform.forward * basicProjectileSpeed);
        }
    }

    public override void FirstSpell()
    {
        //Check if cd is off
        if (Time.time >= nextFirstSpellCd || godMode)
        {
            //Add Cd
            nextFirstSpellCd = Time.time + firstSpellCd;
            //Spawn the prefab
            GameObject explosion = Instantiate(firstSpellPrefab, shootPoint.position, shootPoint.rotation);
            //Make it look at the target
            explosion.transform.LookAt(rayCont.NormalPosition);
            //Set the damage multiplied
            explosion.GetComponent<Explosion>().Damage = Damage * firstDmgMultiplier;
            //Impulse the projectile to the target
            explosion.GetComponent<Rigidbody>().AddForce(explosion.transform.forward * explosionSpeed);
         }
    }

    public override void SecondSpell()
    {
        if (Time.time >= nextSecondSpellCd || godMode)
        {
            zonePlacementFlag = true;
            fireSpiralPlacement = true;
            if (zone) Destroy(zone);
            zone = Instantiate(groundSprite, transform.position + Vector3.down * 10, Quaternion.identity);
        }
    }

    public override void Ultimate()
    {
        //Check if cd is off
        if (Time.time >= nextUltimateCd || godMode)
        {
            //Add Cd
            nextUltimateCd = Time.time + ultimateCd;
            //Spawn the prefab
            GameObject ult = Instantiate(ultimatePrefab, shootPoint.position + new Vector3(0, 0.5f, 0), transform.rotation * Quaternion.Euler(0, 180f, 0));
            //Set the damage multiplied
            ult.GetComponent<FireWave>().Damage = Damage * ultDmgMultiplier;
            
        }
    }
    
   
}
