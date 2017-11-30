using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{

    private Dictionary<string, KeyCode> keys;

    [SerializeField] private Mage mage;
    [SerializeField] private Builder builder;
    [SerializeField] private UI ui;

    public static bool isInputEnabled = true;

    void Awake()
    {
        keys = GetComponent<InputManager>().getKeys();
    }

    void Update()
    {

        if (isInputEnabled)
        {
            if (Input.GetKeyDown(keys["BasicAttack"]))
            {
                if (mage.Combat) mage.BasicAttack();
            }

            if (Input.GetKeyDown(keys["FirstSpell"]))
            {
                if (mage.Combat) mage.FirstSpell();
                else builder.ChangeBuilding(0);
            }

            if (Input.GetKeyDown(keys["SecondSpell"]))
            {
                if (mage.Combat) mage.SecondSpell();
                else builder.ChangeBuilding(1);
            }

            if (Input.GetKeyDown(keys["Ultimate"]))
            {
                if (mage.Combat) mage.Ultimate();
                else builder.ChangeBuilding(2);
            }

            if (Input.GetKeyDown(keys["RotateBuilding"]))
            {
                builder.RotateBuilding();
            }

            if (Input.GetKeyDown(keys["DeselectBuilding"]))
            {
                builder.DeselectBuilding();
            }
        }


        if (Input.GetKeyDown(keys["TogglePause"]))
        {
            ui.TogglePause();
        }
    }
}
