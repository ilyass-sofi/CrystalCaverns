using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Mage : Character 
{   
    [SerializeField] protected MageAsset mageAsset;

    protected RaycastController rayCont;
    protected Transform shootPoint;
    protected GameObject groundSprite;
    protected GameObject zone;

    private enum BarName { Health, Gold, Shield }

    private int gold;

    [Tooltip("No Cooldown")]
    public bool godMode;
    protected bool shield;
    protected bool combat = true;
    protected bool zonePlacementFlag;

    private float maxShieldValue;

    private float shieldValue;

    


    #region HUD References

    private Image hpBar;
    private Image shieldBar;
    private Text healthText;
    private Text goldText;
    private Image firstSpellImg;
    private Image secondSpellImg;
    private Image ultSpellImg;

    #endregion

    #region Spell Prefabs

    protected GameObject passivePrefab;
    protected GameObject basicAttackPrefab;
    protected GameObject firstSpellPrefab;
    protected GameObject secondSpellPrefab;
    protected GameObject ultimatePrefab;

    #endregion

    #region Cooldown

    protected bool OnCdFirstSpell;
    protected bool OnCdSecondSpell;
    protected bool OnCdUltimateSpell;

    protected float basicCd;
    protected float firstSpellCd;
    protected float secondSpellCd;
    protected float ultimateCd;

    protected float nextBasicCd;
    protected float nextFirstSpellCd;
    protected float nextSecondSpellCd;
    protected float nextUltimateCd;

    #endregion


    public virtual void Awake()
    {
        MageAsset mageSelect = GameObject.Find("MenuManager").GetComponent<PassThroughScene>().SelectedMage;


        if (mageSelect != null) SetMageAsset(mageSelect);
        else SetMageAsset();


        rayCont = GetComponent<RaycastController>();
        shootPoint = transform.GetChild(0).GetChild(0);

        Gold = 5000;
        
    }

    public virtual void Update()
    {
        SpellsCooldownEffect();
    }

    public void SetMageAsset(MageAsset _mageAsset = null)
    {   
        if(_mageAsset != null)
        mageAsset = _mageAsset;

        GetHUDReferences();
        LoadPrefabSpells();
        SetBaseAttributes();
        // Fix images before doing this
        // SetHUD();
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
        //Get Shield
        if(shield) shieldBar = hpBarPanel.Find("ShieldBar").GetComponent<Image>();
        // Get GoldText
        goldText = GameObject.FindGameObjectWithTag("GoldText").GetComponent<Text>();
        // Get SpellBar
        Transform spellBar = GameObject.FindGameObjectWithTag("SpellBar").transform;
        // Get spells images from spellbar
        firstSpellImg = spellBar.Find("FirstAbility").GetComponent<Image>();
        secondSpellImg = spellBar.Find("SecondAbility").GetComponent<Image>();
        ultSpellImg = spellBar.Find("Ultimate").GetComponent<Image>();
    }

    /// <summary>
    /// Loads all the spell prefabs
    /// </summary>
    private void LoadPrefabSpells()
    {
        string path = "Spells/" + mageAsset.path + "/";
        passivePrefab = Resources.Load(path + mageAsset.passiveSpell.path) as GameObject;
        basicAttackPrefab = Resources.Load(path + mageAsset.basicAttack.path) as GameObject;
        firstSpellPrefab = Resources.Load(path + mageAsset.firstSpell.path) as GameObject;
        secondSpellPrefab = Resources.Load(path + mageAsset.secondSpell.path) as GameObject;
        ultimatePrefab = Resources.Load(path + mageAsset.ultimate.path) as GameObject;
    }

    /// <summary>
    /// Sets up all the HUD (sprites)
    /// </summary>
    private void SetHUD()
    {
        firstSpellImg.sprite = mageAsset.firstSpell.sprite;
        secondSpellImg.sprite = mageAsset.secondSpell.sprite;
        ultSpellImg.sprite = mageAsset.ultimate.sprite;
    }

    /// <summary>
    /// Loading atributes for the mage asset
    /// </summary>
    private void SetBaseAttributes()
    {
        //Stats
        HealthMax = mageAsset.baseHealth;
        Health = HealthMax;//this should be done on character
        Damage = mageAsset.baseDamage;

        //basicCd = mageAsset.passiveSpell.cooldown;
        basicCd = mageAsset.basicAttack.cooldown;
        firstSpellCd = mageAsset.firstSpell.cooldown;
        secondSpellCd = mageAsset.secondSpell.cooldown;
        ultimateCd = mageAsset.ultimate.cooldown;
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

    public float ShieldValue
    {
        get { return shieldValue; }
        set
        {
            shieldValue = value;
            Visual(BarName.Shield);
        }
    }

    public float MaxShieldValue
    {
        get { return maxShieldValue; }
        set { maxShieldValue = value;}
    }

    protected override void SetSpeed(float speedValue)
    {
        //GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>(). = speedValue;
    }

    protected override void SetHealth(float healthValue)
    {   
        if(shieldValue > 0)
        {   
            if(shieldValue < healthValue)
            {
                float remaining = healthValue - shieldValue;
                ShieldValue = 0;
                healthValue = remaining;
            }
            else
            {
                ShieldValue -= -healthValue;
                healthValue = 0;
            }

        }
        

        base.SetHealth(healthValue);
        Visual(BarName.Health);
    }

    protected override void GameOver()
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

            case BarName.Shield:
                shieldBar.fillAmount = ShieldValue / maxShieldValue;
                break;
        };
    }

    public abstract void BasicAttack();
    public abstract void FirstSpell();
    public abstract void SecondSpell();
    public abstract void Ultimate();

    // Zone Casting
    protected bool ZonePlacement(ref float nextCd, ref float baseCd, ref bool flag, float range, float damageMultiplier, GameObject prefab)
    {
        bool castMade = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //Send a raycast to the cam direction ignoring the object that you are building within a specified range
        if (Physics.Raycast(ray, out hit, 60, 9))
        {

            if (hit.transform.gameObject.CompareTag("Ground"))
            {
                zone.SetActive(true);
                //Place the zone
                zone.transform.position = hit.point;
                //Get his image for recoloring purposes
                GameObject zoneImage = zone.transform.GetChild(0).gameObject;

                //If the distance is under the range then paint it correctly
                if (Vector3.Distance(transform.position, hit.point) < range)
                {
                    zoneImage.GetComponent<SpriteRenderer>().color = Color.white;

                    if (Input.GetMouseButtonDown(0))
                    {

                        nextCd = Time.time + baseCd;
                        flag = false;
                        zonePlacementFlag = false;
                        GameObject spell = Instantiate(prefab, hit.point, prefab.transform.rotation);
                        spell.GetComponent<Spell>().Damage = Damage * damageMultiplier;
                        Destroy(zone);
                        castMade = true;

                    }
                }
                //If not then paint as another color
                else zoneImage.GetComponent<SpriteRenderer>().color = Color.black;

                if (Input.GetMouseButtonDown(1))
                {
                    ClearPlacementZone(ref flag);
                }

            }
            else zone.SetActive(false);
        }
        else zone.SetActive(false);

        return castMade;
    }

    protected void ClearPlacementZone(ref bool flag)
    {
        Destroy(zone);
        flag = false;
        zonePlacementFlag = false;
    }

    protected void ClearPlacementZone(ref bool[] flag)
    {
        Destroy(zone);

        for (int i = 0; i < flag.Length; i++)
        {
            flag[i] = false;
        }

        zonePlacementFlag = false;
    }
}
