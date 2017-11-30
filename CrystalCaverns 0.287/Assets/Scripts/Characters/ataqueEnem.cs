using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ataqueEnem : MonoBehaviour {

    private bool canAttack = true;
    private bool playerInside = false;
    private GameObject player = null;
    private Enemy enem;
    private float damage;


    void Start()
    {
        enem = GetComponentInParent<Enemy>();
        damage = enem.Damage;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Friendly")
        {
            player = coll.gameObject;
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Friendly") playerInside = false;
    }

    void Update()
    {
        if(playerInside && canAttack)
        {
            canAttack = false;
            player.transform.GetComponent<Character>().Health = -damage;
            StartCoroutine(pega());
        }
    }

    IEnumerator pega()
    {
            yield return new WaitForSeconds(enem.AttackSpeed);
            canAttack = true;
    }
}
