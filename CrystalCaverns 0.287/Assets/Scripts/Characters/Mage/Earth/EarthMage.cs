using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMage : Mage
{
    #region Spell Data

    // Rock Circle 
    private RockCircle rockCircle;
    private float nextRockCd;
    private float rockCd = 3;

    // Basic Attack - Sharp Rock
    private float basicProjectileSpeed = 1000;

    // First Spell - Pilar of the Nascent
    private bool earthPilarPlacement;
    private float pilarRange = 10;
    private float pilarDmgMultiplier = 140.0f / 100.0f;

    // Second Spell - QuickSand
    private bool quickSandPlacement;
    private float quickSandRange = 10;
    private float quickSandDmgMultiplier = 60.0f / 100.0f;

    #endregion

    public override void Awake()
    {
        base.Awake();
        rockCircle = Instantiate(Resources.Load("Spells/Earth/RockCircle") as GameObject,transform).GetComponent<RockCircle>();
        groundSprite = Resources.Load("Spells/ZoneEarth") as GameObject;
    }

    public override void Update()
    {
        base.Update();

        // Rock Regeneration
        if (Time.time >= nextRockCd && rockCircle.getCurrentRockCount() < rockCircle.getBaseRockCount())
        {
            nextRockCd = Time.time + rockCd;
            rockCircle.AddRock();
        }

        if (combat && zonePlacementFlag)
        {
            if(earthPilarPlacement) ZonePlacement(ref nextFirstSpellCd, ref firstSpellCd, ref earthPilarPlacement, pilarRange, pilarDmgMultiplier, firstSpellPrefab);

            else if(quickSandPlacement) ZonePlacement(ref nextSecondSpellCd, ref secondSpellCd, ref quickSandPlacement, quickSandRange, quickSandDmgMultiplier, secondSpellPrefab);
        }
        else if (zone)
        {
            bool[] flags = { earthPilarPlacement, quickSandPlacement };
            ClearPlacementZone(ref flags);
        }

    }

    public override void BasicAttack()
    {
         
        // Check if cd is off
        if ((rockCircle.getCurrentRockCount() > 0 || godMode) && !zonePlacementFlag)
        {
            rockCircle.RemoveRock();
            // Spawn the prefab
            GameObject ball = Instantiate(basicAttackPrefab, shootPoint.position, shootPoint.rotation);
            // Make it look at the target
            ball.transform.LookAt(rayCont.NormalTarget);
            // Set the basic damage
            ball.GetComponent<SharpRock>().Damage = Damage;
            // Impulse the projectile to the target
            ball.GetComponent<Rigidbody>().AddForce(ball.transform.forward * basicProjectileSpeed);
        }
    }

    public override void FirstSpell()
    {
        if ((Time.time >= nextFirstSpellCd || godMode) && !zonePlacementFlag)
        {
            zonePlacementFlag = true;
            earthPilarPlacement = true;
            if (zone) Destroy(zone);
            zone = Instantiate(groundSprite, transform.position + Vector3.down * 10, Quaternion.identity);
        }
    }

    public override void SecondSpell()
    {
        if ((Time.time >= nextSecondSpellCd || godMode) && !zonePlacementFlag)
        {
            zonePlacementFlag = true;
            quickSandPlacement = true;
            if (zone) Destroy(zone);
            zone = Instantiate(groundSprite, transform.position + Vector3.down * 10, Quaternion.identity);
            zone.transform.localScale *= 2.5f;
        }
    }

    public override void Ultimate()
    {
        throw new System.NotImplementedException();
    }
    

}
