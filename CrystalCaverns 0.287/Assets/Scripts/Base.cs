using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {

    [SerializeField] private TextMesh lifes;

    private int health = 10;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    private int healthMax = 10;

    public int HealthMax
    {
        get { return healthMax; }
        set { healthMax = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject colObj = other.gameObject;
        string colTag = colObj.tag;
        if (colTag == "Enemy")
        {
            health -= 1;
            colObj.GetComponent<Enemy>().Kill();
            lifes.text = health + "";
            if (health <= 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
            }
        }
    }
}
