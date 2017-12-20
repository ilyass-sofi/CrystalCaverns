using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour, ISpells
{
 
    private Mage mage;
    private RaycastController rayCont;
    private Transform cam;
    [SerializeField] private Transform shootPoint;

    [SerializeField] private GameObject basicAttackPrefab;
    [SerializeField] private GameObject firstSpellPrefab;
    [SerializeField] private GameObject secondSpellPrefab;
    [SerializeField] private GameObject ultimatePrefab;

    [SerializeField] private GameObject groundSprite;

    //Click - Fireball
    private float autoCd = 0.5f;
    private float nextAutoCd = 0;
    private float basicProjectileSpeed = 1000;

    //Q - Explosion
    private float firstCd = 6;
    private float nextFirstCd = 0;
    private float explosionSpeed = 1500;
    private float firstManaCost = 20;
    [SerializeField] private float firstDmgMultiplier = 170.0f / 100.0f;

    //E - Fire Spiral
    private float secondCd = 10;
    private float nextSecondCd = 0;
    private float spiralRange = 10;
    private float raycastRange = 60;
    private float secondManaCost = 35;
    private bool fireSpiralPlacement = false;
    [SerializeField] private float spiralDmgMultiplier = 140.0f / 100.0f;

    /// <summary>
    /// Layer of the raycast, to make the raycast ignore enemies
    /// </summary>
    private LayerMask layerMask = 9;
    //Zone for Spiral
    private GameObject zone;
    private Color32 placeableColor = new Color32(176, 255, 180, 50);
    private Color32 notPlaceableColor = new Color32(255, 148, 148, 50);

    //R - Fire Wave
    private float ultimateCd = 20;
    private float nextUltimateCd = 0;
    private float ultimateManaCost = 50;
    [SerializeField] private float ultDmgMultiplier = 230.0f / 100.0f;

    [Tooltip("No Cooldown")]
    [SerializeField]
    private bool godMode = true;

    private void Start()
    {
        mage = GetComponent<Mage>();
        //ch = GetComponent<Character>();
        rayCont = GetComponent<RaycastController>();
        cam = Camera.main.transform;
    }

    void Update()
    {
        //check combat
        if (fireSpiralPlacement)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //Send a raycast to the cam direction ignoring the object that you are building within a specified range

            if (Physics.Raycast(ray, out hit, raycastRange, layerMask))
            {


                //If it hits a buildable object like the Ground and the building does not collide with other objects
                if (hit.transform.gameObject.tag == "Ground")
                {
                    zone.SetActive(true);
                    //Place the zone
                    zone.transform.position = hit.point;
                    //Get his image for recoloring purposes
                    GameObject zoneImage = zone.transform.GetChild(0).gameObject;

                    //If the distance is under the range then paint it in green
                    if (Vector3.Distance(transform.position, hit.point) < spiralRange)
                    {

                        zoneImage.GetComponent<SpriteRenderer>().color = placeableColor;

                        if (Input.GetMouseButtonDown(0))
                        {
                            mage.Mana = -secondManaCost;
                            nextSecondCd = Time.time + secondCd;
                            fireSpiralPlacement = false;
                            GameObject fireSpiral = Instantiate(secondSpellPrefab, hit.point, cam.transform.parent.transform.rotation);
                            fireSpiral.GetComponent<FireSpiral>().Damage = mage.Damage * spiralDmgMultiplier;
                            Destroy(zone);

                        }
                    }
                    //If not then paint it red
                    else zoneImage.GetComponent<SpriteRenderer>().color = notPlaceableColor; 


                    if (Input.GetMouseButtonDown(1))
                    {
                        fireSpiralPlacement = false;
                        nextSecondCd = 0;
                        Destroy(zone);
                    }

                }
                else zone.SetActive(false);
            }
            else zone.SetActive(false);

        }



    }

    /// <summary>
    /// Casts a basic attack 
    /// </summary>
    public void BasicAttack()
    {
        //Check if cd is off
        if ((Time.time >= nextAutoCd || godMode) && !fireSpiralPlacement)
        {
            //Spawn the prefab
            GameObject ball = Instantiate(basicAttackPrefab, shootPoint.position, shootPoint.rotation);
            //Make it look at the target
            ball.transform.LookAt(rayCont.NormalTarget);
            //Set the basic damage
            ball.GetComponent<FireBall>().Damage = mage.Damage;
            //Impulse the projectile to the target
            ball.GetComponent<Rigidbody>().AddForce(ball.transform.forward * basicProjectileSpeed);
            //Add Cd
            nextAutoCd = Time.time + autoCd;
        }
    }

    /// <summary>
    /// Casts the first spell (explosion)
    /// </summary>
    public void FirstSpell()
    {
        //Check if cd is off and if mana is available, also if level has been atained
        if ((Time.time >= nextFirstCd || godMode) && mage.Mana >= firstManaCost )
        {
            //We first remove the mana
            mage.Mana = -firstManaCost;
            //Spawn the prefab
            GameObject explosion = Instantiate(firstSpellPrefab, shootPoint.position, shootPoint.rotation);
            //Make it look at the target
            explosion.transform.LookAt(rayCont.NormalTarget);
            //Set the damage multiplied
            explosion.GetComponent<Explosion>().Damage = mage.Damage * firstDmgMultiplier;
            //Impulse the projectile to the target
            explosion.GetComponent<Rigidbody>().AddForce(explosion.transform.forward * explosionSpeed);
            //Add Cd
            nextFirstCd = Time.time + firstCd;
        }
    }

    /// <summary>
    /// Casts the second spell (fire spiral)
    /// </summary>
    public void SecondSpell()
    {
        if ((Time.time >= nextSecondCd || godMode) && mage.Mana >= secondManaCost)
        {

            fireSpiralPlacement = true;
            if (zone) Destroy(zone);
            zone = Instantiate(groundSprite, transform.position + Vector3.down * 10, Quaternion.identity);

        }
    }

    /// <summary>
    /// Casts the ultimate (fire wave)
    /// </summary>
    public void Ultimate()
    {
        if ((Time.time >= nextUltimateCd || godMode)  && mage.Mana >= ultimateManaCost)
        {
            mage.Mana = -ultimateManaCost;
            GameObject ult = Instantiate(ultimatePrefab, shootPoint.position + new Vector3(0, 0.5f, 0), cam.transform.parent.transform.rotation);
            ult.GetComponent<FireWave>().Damage = mage.Damage * ultDmgMultiplier;
            ult.transform.rotation *= Quaternion.Euler(0, 180f, 0);
            nextUltimateCd = Time.time + ultimateCd;
        }
    }

    #region Spell Cooldown Getters

    /// <summary>
    /// Returns the cooldown of the first spell
    /// </summary>
    /// <returns></returns>
    public float getFirstCd()
    {
        return firstCd;
    }

    /// <summary>
    /// Returns the next time the player can cast his first spell
    /// </summary>
    /// <returns></returns>
    public float getNextFirstCd()
    {
        return nextFirstCd;
    }

    /// <summary>
    /// Returns the cooldown of the second spell
    /// </summary>
    /// <returns></returns>
    public float getSecondCd()
    {
        return secondCd;
    }

    /// <summary>
    /// Returns the next time the player can cast his second spell
    /// </summary>
    /// <returns></returns>
    public float getNextSecondCd()
    {
        return nextSecondCd;
    }

    /// <summary>
    /// Returns the cooldown of the ultimate
    /// </summary>
    /// <returns></returns>
    public float getUltimateCd()
    {
        return ultimateCd;
    }

    /// <summary>
    /// Returns the next time the player can cast his ultimate
    /// </summary>
    /// <returns></returns>
    public float getNextUltimateCd()
    {
        return nextUltimateCd;
    }

    #endregion
}
