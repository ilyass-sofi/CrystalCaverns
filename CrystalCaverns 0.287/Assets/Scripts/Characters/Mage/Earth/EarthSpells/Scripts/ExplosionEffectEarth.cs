using UnityEngine;

public class ExplosionEffectEarth : Spell
{
    [SerializeField] private float slowPercentage;

    private float slowValue;
  
    public override void Start()
    {
        base.Start();
        slowValue = (100 - slowPercentage) / 100;
    }

    private void OnTriggerEnter(Collider other)
    {

        GameObject colObj = other.gameObject;
        if (colObj.CompareTag("Enemy"))
        {   
            Character enemy = colObj.GetComponent<Character>();
            enemy.Health = -damage;
            enemy.CurrentSpeed *= slowValue;
        }


    }
}
