using UnityEngine;

public class ExplosionEffectEarth : Spell
{
    [SerializeField] private float slowPercentage;

    [SerializeField] private float slowDuration;

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
            Enemy enemy = colObj.GetComponent<Enemy>();
            enemy.Health = -damage;
            enemy.Slow(slowValue, slowDuration);
        }
    }
}
