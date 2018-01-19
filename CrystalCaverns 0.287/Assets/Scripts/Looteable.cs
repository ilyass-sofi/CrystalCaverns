using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looteable : MonoBehaviour {
    public enum objectTypes {Crystal,HpPack };
    public objectTypes type;

    private int cantidad;

    public int Crystal
    {
        get { return cantidad; }
        set { cantidad = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Friendly"))
        {
            other.transform.GetChild(1).GetComponent<LootCollector>().collectLoot(gameObject);
        }
    }
}
