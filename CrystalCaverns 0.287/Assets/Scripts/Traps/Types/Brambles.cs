using UnityEngine;

public class Brambles : MonoBehaviour
{
    [Header("Trap Stats")]
    [Tooltip("The initial life of the trap")]
    [SerializeField]
    private float life;
    [Tooltip("The life that the trap lose per time")]
    [SerializeField]
    private float loseLife;
    [Tooltip("In that time the trap lose life")]
    [SerializeField]
    private float LoseLifePT;

    [Header("Trap Current Stats")]
    [Tooltip("The current life of the trap")]
    [SerializeField]
    private float currentLife;
    [Tooltip("When the trap begins to lose time??")]
    [SerializeField]
    private float currentLoseLifePT;

    [Header("Velocty Ruduction")]
    [Tooltip("The percentage velocity that enemy lose per a time")]
    [SerializeField]
    private float velocityLosePercent;

    public float VelocityLosePercent
    {
        get { return velocityLosePercent; }
        set { velocityLosePercent = value; }
    }

    [Tooltip("The seconds that the enemy will be slowed down")]
    [SerializeField]
    private float secondsSD;

    public float SecondsSD
    {
        get { return secondsSD; }
        set { secondsSD = value; }
    }


    void Start()
    {
        currentLife = life;
        currentLoseLifePT = LoseLifePT;
        //Here will be the sound that it make this trap
    }


    void OnTriggerEnter(Collider other)  // Invoke "SlowDown(Collider enemy)"
    {
        SlowDown(other);
    }


    void OnTriggerExit(Collider other)  // Invoke "SlowDown(Collider enemy)"
    {
        NormalVelocity(other);
    }

    /// <summary>
    ///  The trap with the tag "VelocityReduction" makes a slow down in the enemy that enter in that trap
    /// </summary>
    private void SlowDown(Collider enemy)  //
    {
        if (enemy.tag == "Enemy")
        {
            Character enemyStatsController = enemy.GetComponent<Character>();
            enemyStatsController.CurrentSpeed = (100 - VelocityLosePercent) * enemyStatsController.CurrentSpeed / 100;
            
        }
    }

    /// <summary>
    /// The trap with the tag "VelocityReduction" makes a slow down in the enemy that enter in that trap
    /// </summary>
    private void NormalVelocity(Collider enemy)  
    {
        if (enemy.tag == "Enemy")
        {
            Character enemyStatsController = enemy.GetComponent<Character>();
            enemyStatsController.CurrentSpeed = enemyStatsController.BaseSpeed;
        }
    }

    
}
