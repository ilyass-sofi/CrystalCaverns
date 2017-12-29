using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected bool isDead = false;
    protected bool isBurning = false;
    protected string nombre;
    protected string id;
    protected float health;
    protected float healthMax;
    protected float range;
    protected float damage;
    protected float currentSpeed;
    protected float attackSpeed;
    protected float baseSpeed;


    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    public bool IsBurning
    {
        get { return isBurning; }
        set { isBurning = value; }
    }

    public float HealthMax
    {
        get { return healthMax; }
        set { healthMax = value; }
    }

    public float Range
    {
        get { return range; }
        set { range = value; }
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public float CurrentSpeed
    {
        get { return currentSpeed; }
        set { SetSpeed(value); }
    }

    public float BaseSpeed
    {
        get { return baseSpeed; }
        set { baseSpeed = value; }
    }

    public float AttackSpeed
    {
        get { return attackSpeed; }
        set { attackSpeed = value; }
    }

    /// <summary>
    /// get: devuelve el valor de la salud actual
    /// set: suma a la salud actual el valor dado y si la salud es menor que 0 mueres
    /// </summary>
    public float Health
    {
        get { return health; }
        set { SetHealth(value); }

    }

    #region Change on sons

    protected virtual void SetHealth(float healthValue)
    {
        if (!isDead)
        {
            if (health + healthValue > healthMax) health = healthMax;
            else if (health + healthValue <= healthMax && health + healthValue > 0) health += healthValue; 
            else
            {
                health = 0;
                isDead = true;
                GameOver();
            }
        }
    }

    protected abstract void SetSpeed(float speedValue);

    protected abstract void GameOver();
    
    #endregion

}
