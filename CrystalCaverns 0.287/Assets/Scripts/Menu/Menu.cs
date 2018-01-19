using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private PassThroughScene passThroughScene;

    [SerializeField] private GameObject backgroundPanel;
    [SerializeField] private GameObject modePanel;
    [SerializeField] private GameObject settingsPanel;


    [Header("Mage Pick")]
    [SerializeField] private GameObject magePick;
    [SerializeField] private MageAsset initialMage;
    [SerializeField] private GameObject backgroundSpell;
    private MageAsset selectedMage;


    [Header("Abilities")]
    [SerializeField] private GameObject passiveAbility;
    [SerializeField] private GameObject basicAttack;
    [SerializeField] private GameObject firstAbility;
    [SerializeField] private GameObject secondAbility;
    [SerializeField] private GameObject ultimateAbility;


    [Header("Building Pick")]
    [SerializeField] private GameObject trapsPick;
    [SerializeField] private GameObject render;
    [SerializeField] private GameObject backgroundTrap;
    [SerializeField] private BuildingAsset initialBuilding;
    [SerializeField] private GameObject buildingPicks;
    private List<BuildingAsset> buildingPicksAssets = new List<BuildingAsset>();
    private Text trapName;
    private Text trapDescription;
    private GameObject renderInstance;

    [Header("Settings")]
    [SerializeField] private GameObject keybindsPanel;
    [SerializeField] private GameObject audioPanel;
    [SerializeField] private GameObject videoPanel;


    private GameObject openPanel;


    private void Start()
    {
        trapName = backgroundTrap.transform.GetChild(0).GetComponent<Text>();
        trapDescription = backgroundTrap.transform.GetChild(1).GetComponent<Text>();

        SelectMage(initialMage);
        SelectBuilding(initialBuilding);
    }

    private void Update()
    {
        if (trapsPick.activeSelf && Input.GetMouseButton(0)) renderInstance.transform.Rotate(new Vector3(0, -Input.GetAxis("Mouse X"), 0) * Time.deltaTime * 300);
    }

    public void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void ToggleMode()
    {
        modePanel.SetActive(!modePanel.activeSelf);
        backgroundPanel.SetActive(!backgroundPanel.activeSelf);
    }

    public void ToggleModelMage()
    {
        modePanel.SetActive(!modePanel.activeSelf);
        magePick.SetActive(!magePick.activeSelf);
    }

    public void ToggleMageTraps()
    {
        magePick.SetActive(!magePick.activeSelf);
        trapsPick.SetActive(!trapsPick.activeSelf);
    }

    public void GoToSolo()
    {   
        if(buildingPicks.transform.childCount == 3)
        {
            passThroughScene.SelectedMage = selectedMage;
            GetSelectedBuildings();
            passThroughScene.SelectedBuildings = buildingPicksAssets.ToArray();
            SceneManager.LoadScene("Solo");
        }
        
    }

    public void GoToMpLobby()
    {
        SceneManager.LoadScene("MpLobby");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SelectMage(MageAsset mageAsset)
    {
        selectedMage = mageAsset;

        magePick.GetComponent<Image>().sprite = selectedMage.splashArt;

        // Set ability sprites
        passiveAbility.GetComponent<Image>().sprite = selectedMage.passiveSpell.sprite;
        basicAttack.GetComponent<Image>().sprite = selectedMage.basicAttack.sprite;
        firstAbility.GetComponent<Image>().sprite = selectedMage.firstSpell.sprite;
        secondAbility.GetComponent<Image>().sprite = selectedMage.secondSpell.sprite;
        ultimateAbility.GetComponent<Image>().sprite = selectedMage.ultimate.sprite;

        // Set On Pointer Event
        SetOnPointerEnterEvent(passiveAbility, selectedMage.passiveSpell);
        SetOnPointerEnterEvent(basicAttack, selectedMage.basicAttack);
        SetOnPointerEnterEvent(firstAbility, selectedMage.firstSpell);
        SetOnPointerEnterEvent(secondAbility, selectedMage.secondSpell);
        SetOnPointerEnterEvent(ultimateAbility, selectedMage.ultimate);

    }

    public void SelectSpell(SpellAsset spellAsset)
    {
        backgroundSpell.SetActive(true);
        backgroundSpell.transform.GetChild(0).GetComponent<Text>().text = spellAsset.spellName;
        backgroundSpell.transform.GetChild(1).GetComponent<Text>().text = spellAsset.description;
    }

    public void UnselectSpell()
    {
        backgroundSpell.SetActive(false);
    }

    private void SetOnPointerEnterEvent(GameObject spell, SpellAsset spellAsset)
    {
        EventTrigger eventTrigger = spell.GetComponent<EventTrigger>();

        if (eventTrigger.triggers.Count > 1)
            eventTrigger.triggers.RemoveAt(1);

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;

        entry.callback.AddListener((eventData) => { SelectSpell(spellAsset); });
        eventTrigger.triggers.Add(entry);

    }

    public void OpenPanels(string panel)
    {   
        if(openPanel)
        openPanel.SetActive(!openPanel.activeSelf);


        switch (panel)
        {   
            case "Keybinds":
                openPanel = keybindsPanel;
                keybindsPanel.SetActive(true);
                break;
            case "Video":
                openPanel = videoPanel;
                videoPanel.SetActive(true);
                break;
            case "Audio":
                openPanel = audioPanel;
                audioPanel.SetActive(true);
                break;
        }      
    }

    public void SelectBuilding(BuildingAsset buildingAsset)
    {
        trapName.text = buildingAsset.buildingName;
        trapDescription.text = buildingAsset.description;

        if (renderInstance) Destroy(renderInstance);
        renderInstance = Instantiate(buildingAsset.buildingPrefab, render.transform.position, buildingAsset.buildingPrefab.transform.rotation);
    }

    public void GetSelectedBuildings()
    {
        for (int i = 0; i < buildingPicks.transform.childCount; i++)
        {
            buildingPicksAssets.Add(buildingPicks.transform.GetChild(i).GetComponent<DragHandler>().buildingAsset);
        }

    }

   
}