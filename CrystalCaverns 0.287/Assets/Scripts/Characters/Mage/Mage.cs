using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Mage : Character 
{
    protected RaycastController rayCont;
    protected Transform shootPoint;

    protected GameObject groundSprite;
    protected GameObject zone;

    private enum BarName { Health, Gold }

    private int gold;

    [Tooltip("No Cooldown")]
    public bool godMode;

    protected bool combat = true;

    #region HUD References

    private Image hpBar;
    private Text healthText;
    private Text goldText;
    private Image firstSpellImg;
    private Image secondSpellImg;
    private Image ultSpellImg;

    #endregion

    #region Spell Prefabs

    [SerializeField] protected GameObject passivePrefab;
    [SerializeField] protected GameObject basicAttackPrefab;
    [SerializeField] protected GameObject firstSpellPrefab;
    [SerializeField] protected GameObject secondSpellPrefab;
    [SerializeField] protected GameObject ultimatePrefab;

    #endregion

    #region Cooldown

    protected bool OnCdFirstSpell;
    protected bool OnCdSecondSpell;
    protected bool OnCdUltimateSpell;

    [SerializeField] protected float basicCd;
    [SerializeField] protected float firstSpellCd;
    [SerializeField] protected float secondSpellCd;
    [SerializeField] protected float ultimateCd;

    protected float nextBasicCd;
    protected float nextFirstSpellCd;
    protected float nextSecondSpellCd;
    protected float nextUltimateCd;

    #endregion

    public virtual void Awake()
    {
        GetHUDReferences();
    }

    void Start()
    {
        rayCont = GetComponent<RaycastController>();
        shootPoint = transform.GetChild(0).GetChild(0);

        HealthMax = 100;
        Health = HealthMax;
        Damage = 35;
        Gold = 5000;
    }

    public virtual void Update()
    {
        SpellsCooldownEffect();
    }

    private void SpellsCooldownEffect()
    {   

        if (Time.time < nextFirstSpellCd)
        {
            if (OnCdFirstSpell)
            {
                OnCdFirstSpell = false;
                firstSpellImg.fillAmount = 0;
            }
            firstSpellImg.fillAmount += 1.0f / firstSpellCd * Time.deltaTime;
            firstSpellImg.color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1));
        }
        else
        {
            OnCdFirstSpell = true;
            firstSpellImg.color = Color.white;
        }

        if (Time.time < nextSecondSpellCd)
        {
            if (OnCdSecondSpell)
            {
                OnCdSecondSpell = false;
                secondSpellImg.fillAmount = 0;
            }
            secondSpellImg.fillAmount += 1.0f / secondSpellCd * Time.deltaTime;
            secondSpellImg.color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1));
        }
        else
        {
            OnCdSecondSpell = true;
            secondSpellImg.color = Color.white;
        }

        if (Time.time < nextUltimateCd)
        {
            if (OnCdUltimateSpell)
            {
                OnCdUltimateSpell = false;
                ultSpellImg.fillAmount = 0;
            }
            ultSpellImg.fillAmount += 1.0f / ultimateCd * Time.deltaTime;
            ultSpellImg.color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1));
        }
        else
        {
            OnCdUltimateSpell = true;
            ultSpellImg.color = Color.white;
        }
    }

    /// <summary>
    /// Gets all the references of the HUD (health text, spell images ...)
    /// </summary>
    private void GetHUDReferences()
    {
        // Get HealthBarPanel
        Transform hpBarPanel = GameObject.FindGameObjectWithTag("HealthBar").transform;
        // Get HealthBar and HealthText
        hpBar = hpBarPanel.Find("HealthBar").GetComponent<Image>();
        healthText = hpBarPanel.Find("HealthText").GetComponent<Text>();
        // Get GoldText
        goldText = GameObject.FindGameObjectWithTag("GoldText").GetComponent<Text>();
        // Get SpellBar
        Transform spellBar = GameObject.FindGameObjectWithTag("SpellBar").transform;
        // Get spells images from spellbar
        firstSpellImg = spellBar.Find("FirstAbility").GetComponent<Image>();
        secondSpellImg = spellBar.Find("SecondAbility").GetComponent<Image>();
        ultSpellImg = spellBar.Find("Ultimate").GetComponent<Image>();
    }

    public bool Combat
    {
        get { return combat; }
        set { combat = value; }
    }

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

    /// <summary>
    /// El nobre que se pasa es el nombre de la barra que se quiere cambiar
    /// </summary>
    /// <param name="name"></param>
    void Visual(BarName name)
    {
        switch (name)
        {
           
            case BarName.Health:
                hpBar.fillAmount = Health / HealthMax;
                healthText.text = (int)Health + "/" + HealthMax;
                break;

           
            case BarName.Gold:
                goldText.text = gold.ToString();
                break;
        };
    }

    public abstract void BasicAttack();
    public abstract void FirstSpell();
    public abstract void SecondSpell();
    public abstract void Ultimate();

}
