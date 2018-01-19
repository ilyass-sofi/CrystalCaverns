using UnityEngine;

public class PilarNascent : Spell
{

    [SerializeField] private GameObject explosionPrefab;

    public override void Start()
    {
        base.Start();

        GameObject explosionEffect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosionEffect.GetComponent<ExplosionEffectEarth>().Damage = damage;
    }

}
