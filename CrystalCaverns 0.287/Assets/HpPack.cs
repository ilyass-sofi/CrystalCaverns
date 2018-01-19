using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPack : MonoBehaviour {

    [SerializeField] private float recoverHp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Friendly"))
        {
            if (other.transform.GetComponent<Character>().Health < other.transform.GetComponent<Character>().HealthMax)
            {
                other.transform.GetComponent<Character>().Health += recoverHp;
                Destroy(gameObject);
            }
        }
    }
}
