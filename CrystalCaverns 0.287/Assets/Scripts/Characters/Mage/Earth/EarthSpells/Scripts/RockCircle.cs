using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCircle : MonoBehaviour {

    private GameObject[] rocks;
    private int rockCount = 5;

	void Awake ()
    {   
        rocks = new GameObject[rockCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            rocks[i] = transform.GetChild(i).gameObject;
        }

    }
	
	void Update ()
    {
        transform.Rotate(Vector3.up, 30 * Time.deltaTime);
	}

    public void AddRock()
    {   
        bool rockFound = false;

        for (int i = 0; i < rocks.Length && !rockFound; i++)
        {
            if (!rocks[i].activeSelf)
            {
                rocks[i].SetActive(true);
                rockFound = true;
                rockCount++;
            }
        }
    }

    public void RemoveRock()
    {
        bool rockFound = false;

        for (int i = 0; i < rocks.Length && !rockFound; i++)
        {
            if (rocks[i].activeSelf)
            {
                rocks[i].SetActive(false);
                rockFound = true;
                rockCount--;
            }
        }
    }

    public int getBaseRockCount()
    {
        return rocks.Length;
    }

    public int getCurrentRockCount()
    {
        return rockCount;
    }
}
