using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mage : Character 
{
    private ISpells spells;
    private enum BarName { Mana, Health, Exp, Gold }

    private float manaMax = 100000;
    private float mana;
   
    //private float level = 1;
    //private float exp;
    //private float expLvlUp = 100;

    //private float newLvlExp = 1.5f;//aumento de experiencia necesaria para subir un nivel

    private int gold;

    [SerializeField] private bool combat;

    //[SerializeField] private Image expBar;
    [SerializeField] private Image hpBar;
    [SerializeField] private Image manaBar;

    //[SerializeField] private Text lvlText;
    //[SerializeField] private Text expText;
    [SerializeField] private Text hpText;
    [SerializeField] private Text manaText;
    [SerializeField] private Text goldText;

    [SerializeField] private Image cdFirstSpellImg;
    bool OnCdFirstSpell = false;

    [SerializeField] private Image cdSecondSpellImg;
    bool OnCdSecondSpell = false;

    [SerializeField] private Image cdUltimateSpellImg;
    bool OnCdUltimateSpell = false;

    void Awake()
    {
        GetSpellsInterface();
    }

    void Start()
    {
        HealthMax = 100;
        Health = HealthMax;
        Damage = 35;
        Gold = 5000;
        Mana = manaMax;
    }

    void Update()
    {
        #region UI Spell Cooldown Effect

        if (Time.time < getNextFirstCd())
        {
            if (OnCdFirstSpell)
            {
                OnCdFirstSpell = false;
                cdFirstSpellImg.fillAmount = 0;
            }
            cdFirstSpellImg.fillAmount += 1.0f / getFirstCd() * Time.deltaTime;
            cdFirstSpellImg.color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1));
        }
        else
        {
            OnCdFirstSpell = true;
            cdFirstSpellImg.color = Color.white;
        }

        if (Time.time < getNextSecondCd())
        {
            if (OnCdSecondSpell)
            {
                OnCdSecondSpell = false;
                cdSecondSpellImg.fillAmount = 0;
            }
            cdSecondSpellImg.fillAmount += 1.0f / getSecondCd() * Time.deltaTime;
            cdSecondSpellImg.color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1));
        }
        else
        {
            OnCdSecondSpell = true;
            cdSecondSpellImg.color = Color.white;
        }

        if (Time.time < getNextUltimateCd())
        {
            if (OnCdUltimateSpell)
            {
                OnCdUltimateSpell = false;
                cdUltimateSpellImg.fillAmount = 0;
            }
            cdUltimateSpellImg.fillAmount += 1.0f / getUltimateCd() * Time.deltaTime;
            cdUltimateSpellImg.color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1));
        }
        else
        {
            OnCdUltimateSpell = true;
            cdUltimateSpellImg.color = Color.white;
        }
        #endregion

    }

    public bool Combat
    {
        get { return combat; }
        set { combat = value; }
    }

    ///// <summary>
    ///// Get: Returns current experience points
    ///// Set: Add the provided value to the current experience, if it's above or equal to the limit then call level up
    ///// </summary>
    //public float Experience
    //{
    //    get { return exp; }
    //    set
    //    {
    //        exp += value;
    //        if (exp >= expLvlUp)
    //        {
    //            exp -= expLvlUp;
    //            LvlUp();
    //        }
    //        Visual(BarName.Exp);
    //    }
    //}

    /// <summary>
    /// get: devuelve el valor del mana actual
    /// </summary>
    public float Mana
    {
        get { return mana; }
        set
        {
            if (mana + value > manaMax) mana = manaMax;
            else if (mana + value <= manaMax && mana + value >= 0) mana += value;
            Visual(BarName.Mana);
        }
    }

    //public float Level
    //{
    //    get { return level; }
    //}

    /// <summary>
    /// Player's gold
    /// </summary>
    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            Visual(BarName.Gold);
        }
    }

    //private void LvlUp()
    //{
    //    expLvlUp *= 1.3f;
    //    level++;
    //    lvlText.text = level.ToString();
    //    LvlUpBoost();
    //}

    protected override void SetSpeed(float speedValue)
    {
        //GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>(). = speedValue;
    }

    protected override void SetHealth(float healthValue)
    {
        base.SetHealth(healthValue);
        Visual(BarName.Health);
    }

    protected override void dead()
    {
        //pantalla Game Over
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
       
    }
    ///// <summary>
    ///// acciones que ocurren al subir de nivel
    ///// </summary>
    //private void LvlUpBoost()
    //{
    //    HealthMax *= 1.3f;
    //    manaMax *= 1.3f;
    //    Damage *= 1.3f;
    //    Health = HealthMax;
    //    mana = manaMax;
    //    //funcion que resetea todos los cds
    //}

    /// <summary>
    /// El nobre que se pasa es el nombre de la barra que se quiere cambiar
    /// </summary>
    /// <param name="name"></param>
    void Visual(BarName name)
    {
        switch (name)
        {
            //case BarName.Exp:
            //    expBar.transform.localScale = new Vector3(exp / expLvlUp, 0.1f, 1);
            //    expText.text = (int)exp + "/" + expLvlUp;
            //    break;

            case BarName.Health:
                hpBar.transform.localScale = new Vector3(Health / HealthMax, 0.1f, 1);
                hpText.text = (int)Health + "/" + HealthMax;
                break;

            case BarName.Mana:
                manaBar.transform.localScale = new Vector3(mana / manaMax, 0.1f, 1);
                manaText.text = (int)mana + "/" + manaMax;
                break;
            case BarName.Gold:
                goldText.text = gold.ToString();
                break;
        };
    }

    #region Spells Interface

    /// <summary>
    /// Get the spell interface script
    /// </summary>
    public void GetSpellsInterface()
    {
        spells = GetComponent<ISpells>();
    }
    

    public void BasicAttack()
    {
        if (combat)
            spells.BasicAttack();
    }


    public void FirstSpell()
    {
        if (combat)
            spells.FirstSpell();
    }

    public void SecondSpell()
    {
        if (combat)
            spells.SecondSpell();
    }

    public void Ultimate()
    {
        if (combat)
            spells.Ultimate();
    }

    public float getFirstCd()
    {
        return spells.getFirstCd();
    }

    public float getNextFirstCd()
    {
        return spells.getNextFirstCd();
    }

    public float getSecondCd()
    {
        return spells.getSecondCd();
    }

    public float getNextSecondCd()
    {
        return spells.getNextSecondCd();
    }

    public float getUltimateCd()
    {
        return spells.getUltimateCd();
    }

    public float getNextUltimateCd()
    {
        return spells.getNextUltimateCd();
    }


    #endregion
}
