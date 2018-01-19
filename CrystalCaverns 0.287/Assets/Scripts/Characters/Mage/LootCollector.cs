using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCollector : MonoBehaviour
{
    private List<GameObject> loot = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if (loot.Count > 0)
        {
            foreach(GameObject obj in loot)
            {
                obj.transform.position = Vector3.MoveTowards(obj.transform.position, transform.position,0.5f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Loot"))
        {
            loot.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Loot"))
        {
            if (loot.Contains(other.gameObject))
            {
                loot.Remove(other.gameObject);
            }
        }

    }

    public void collectLoot(GameObject obj)
    {
        switch (obj.GetComponent<Looteable>().type)
        {
            case Looteable.objectTypes.Crystal:
                transform.GetComponentInParent<Mage>().Gold += obj.GetComponent<Looteable>().Crystal;
                loot.Remove(obj);
                Destroy(obj);
                break;
            case Looteable.objectTypes.HpPack:
                break;
        }
    }
}
