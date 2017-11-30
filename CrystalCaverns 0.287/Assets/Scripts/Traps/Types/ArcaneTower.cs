using System.Collections.Generic;
using UnityEngine;

public class ArcaneTower : MonoBehaviour
{
    [Header("Cannon Ball")]
    [Tooltip("The cannon ball game object")]
    [SerializeField]
    private GameObject cannonBall;
    [Tooltip("The cannon ball local speed")]
    [SerializeField]
    private float cannonBallSpeed;


    [Tooltip("The trap y dimension")]
    [SerializeField]
    private float yDimension;
    [Tooltip("The initial life of the trap")]
    [SerializeField]
    private float life;
  
    
    [Tooltip("The maximun range that the tower can throw the cannon ball")]
    [SerializeField]
    private float maxRange;
    [Tooltip("The minimun range that the tower can throw the cannon ball")]
    [SerializeField]
    private float minRange;

    [Header("Trap Current Stats")]
    [Tooltip("The current life of the trap")]
    [SerializeField]
    private float currentLife;
    public float CurrentLife
    {
        get { return currentLife; }
        set { currentLife = value; }
    }

    private float currentCoolDown = 0;
    [SerializeField] private float coolDown = 2;


    [Header("Damage")]
    [Tooltip("The damage that the enemy take")]
    [SerializeField]
    private float damage;
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    private Transform spawnCannonBallTr;
    private SphereCollider sphere;

    private List<GameObject>enemyList = new List<GameObject>();

    void Awake()
    {
        sphere = GetComponent<SphereCollider>();
    }

    void Start()
    {
        currentLife = life;
        sphere.radius = maxRange;
    }

    void Update()
    {
        CoolDownCheck();
        //pa las jajas
        //transform.parent.transform.Rotate(Vector3.up * 250 * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        //Destroy();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (!enemyList.Contains(other.gameObject))
            {
                enemyList.Add(other.gameObject);
            }
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (enemyList.Contains(other.gameObject))
            {
                enemyList.Remove(other.gameObject);
            }
        }
    }

    private void FireACannonBall(GameObject closestEnemy)
    {
       
        GameObject projectile = Instantiate(cannonBall, transform.position, transform.rotation);
        Projectile projClass = projectile.GetComponent<Projectile>();
        projClass.Speed = cannonBallSpeed;
        projClass.Damage = damage;
        projClass.Target = closestEnemy;

    }



    private void CoolDownCheck()
    {   
        //Reset closest enemy
        GameObject closestEnemy = null;
        
        //if Enemies do exist inside the trigger
        if(enemyList.Count > 0)
        {
           //If cooldown is over
            if (Time.time >= currentCoolDown)
            {   
                //Check the closest enemy
                closestEnemy = CheckClosestEnemy();
                //If he exists
                if (closestEnemy)
                {   
                    //Add the cooldown and shoot the closest enemy
                    currentCoolDown = Time.time + coolDown;
                    FireACannonBall(closestEnemy);
                }
            }
        }

    }


    private GameObject CheckClosestEnemy()
    {
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (!enemyList[i])
            {
                enemyList.Remove(enemyList[i]);
            }
            else
            {
                float distance = Vector3.Distance(transform.position, enemyList[i].transform.position);
              
                if (distance <  closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemyList[i];
                }
            }
                
        }

        return closestEnemy;
    }

    private void Destroy()
    {
        if (currentLife <= 0)
        {
            Destroy(gameObject);
        }
    }
}