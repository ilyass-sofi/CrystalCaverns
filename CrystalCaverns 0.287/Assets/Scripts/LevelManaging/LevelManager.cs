using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance { get; private set; }
    [SerializeField] private GameObject spawners;
    private GameObject[] shops;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject buildingPanel;
    [SerializeField] private GameObject trapsPanel;
    [SerializeField] private GameObject spellsPanel;
    private Material openShopMat;
    private Material closedShopMat;
   

    private GameObject player; //Transform this into a list in multiplayer
    [SerializeField] private Text waveCountText;
    [SerializeField] private Text enemiesAliveText;
    [SerializeField] private Text enemiesAliveCount;
    [SerializeField] private Text timerBuildingText;
    [SerializeField] private GameObject hintText;
    [SerializeField] private float buildingTimeBase;
    private float buildingTime;

    private bool checking = false;
    private bool waveOver = false;
    private int waveCount = 1;
    private GameObject UIManager;
    [HideInInspector] public enum LevelPhase { Wave, Building };
    [HideInInspector] public LevelPhase levelCurrentPhase;
  


    private int enemiesAlive;

    public int EnemiesAlive
    {
        get { return enemiesAlive; }
        set
        {
            enemiesAlive = value;
            enemiesAliveCount.text = enemiesAlive.ToString();
        }
    }

    void Awake()
    {
        Instance = this;

    }

    void Start()
    {
        levelCurrentPhase = LevelPhase.Wave;
        buildingTime = buildingTimeBase;
        spawners.GetComponent<Spawn>().Manager(waveCount);
        //Loading up
        UIManager = GameObject.FindGameObjectWithTag("UIManager");
        shops = GameObject.FindGameObjectsWithTag("Shop");
        player = GameObject.FindGameObjectWithTag("Friendly");
        openShopMat = (Material)Resources.Load("Materials/GreenEmissive");
        closedShopMat = (Material)Resources.Load("Materials/RedEmissive");
    }

    public void SetBuildingPhase()
    {
        levelCurrentPhase = LevelPhase.Building;
        TogglePhase();
        timerBuildingText.gameObject.SetActive(true);
        timerBuildingText.text = (int)(buildingTime / 60) + ":" + (int)(buildingTime % 60);
        //SetSpawnerState(false);
    }

    private void Update()
    {
        //Check if the player is in building phase (after a wave)
        if (levelCurrentPhase == LevelPhase.Building)
        {
            //If he is then decrease the building time and adapt the text timer
            buildingTime -= Time.deltaTime;
            timerBuildingText.text = System.String.Format("{0:00}:{1:00}", (int)(buildingTime / 60), (int)(buildingTime % 60));

            
            

            if (Input.GetKeyDown(KeyCode.T))
            {
                buildingTime = 0;
            }

            //When the limit 0 is reached
            if (buildingTime <= 0)
            {
                //Go to the next wave, hide the text and set the spawners active again
                waveCount++;
                waveCountText.text = "Wave " + waveCount;
                levelCurrentPhase = LevelPhase.Wave;
                TogglePhase();
                timerBuildingText.gameObject.SetActive(false);
                buildingTime = buildingTimeBase;
                //SetSpawnerState(true);
                spawners.GetComponent<Spawn>().Manager(waveCount);
                ToggleCrosshair(true);
            }
        }

    }

    public void TogglePhase()
    {
        ToggleUI();
        player.GetComponent<Mage>().Combat = !player.GetComponent<Mage>().Combat;
        player.GetComponent<Builder>().BuildingState = !player.GetComponent<Builder>().BuildingState;
        player.GetComponent<RaycastController>().NormalRaycast = !player.GetComponent<RaycastController>().NormalRaycast;

        Material shopMat = null;

        if (levelCurrentPhase == LevelPhase.Building) shopMat = openShopMat;
        else if (levelCurrentPhase == LevelPhase.Wave) shopMat = closedShopMat;

        for (int i = 0; i < shops.Length; i++)
        {
            //Change material
            shops[i].GetComponent<Renderer>().material = shopMat;
            GameObject shopTrigger = shops[i].transform.GetChild(0).gameObject;
            shopTrigger.SetActive(!shopTrigger.activeSelf);
        }
        shopPanel.SetActive(false);
    }

    public void ToggleShopPanel()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
    }

    public void ToggleUI()
    {
        trapsPanel.SetActive(!trapsPanel.activeSelf);
        buildingPanel.SetActive(!buildingPanel.activeSelf);
        spellsPanel.SetActive(!spellsPanel.activeSelf);
        enemiesAliveText.gameObject.SetActive(!enemiesAliveText.gameObject.activeSelf);
        timerBuildingText.gameObject.SetActive(!timerBuildingText.gameObject.activeSelf);
        enemiesAliveCount.gameObject.SetActive(!enemiesAliveCount.gameObject.activeSelf);
        hintText.SetActive(!hintText.activeSelf);
    }

    public void SetShopItem(string name, string description, int price, Sprite sprite)
    {
        shopPanel.transform.GetChild(0).GetComponent<Text>().text = name;
        shopPanel.transform.GetChild(1).GetComponent<Text>().text = description;
        shopPanel.transform.GetChild(2).GetComponent<Text>().text = price + "";
        shopPanel.transform.GetChild(3).GetComponent<Image>().sprite = sprite;
    }

    public void AddItem(string name, int price)
    {
        player.GetComponent<Builder>().AddItemInventory(name, price);
    }

    public void ToggleCrosshair(bool value)
    {
        UIManager.GetComponent<Crosshair>().drawCrosshair = value;
    }

    

}
