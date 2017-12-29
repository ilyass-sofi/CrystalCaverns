using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private PassThroughScene passThroughScene;

    [SerializeField] private GameObject backgroundPanel;
    [SerializeField] private GameObject modePanel;
    [SerializeField] private GameObject magePick;
    [SerializeField] private GameObject trapsPick;
    [SerializeField] private GameObject settingsPanel;

    [SerializeField] private GameObject backgroundSpell;

    [SerializeField] private MageAsset initialMage;

    [Header("Settings")]
    [SerializeField] private GameObject keybindsPanel;
    [SerializeField] private GameObject audioPanel;
    [SerializeField] private GameObject videoPanel;

    [Header("Abilities")]
    [SerializeField] private GameObject passiveAbility;
    [SerializeField] private GameObject basicAttack;
    [SerializeField] private GameObject firstAbility;
    [SerializeField] private GameObject secondAbility;
    [SerializeField] private GameObject ultimateAbility;

    private MageAsset selectedMage;

    private GameObject openPanel;
    private Transform description;

    private void Start()
    {
        SelectMage(initialMage);
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

    //public void Passive()
    //{
    //    description = magePick.transform.GetChild(0).GetChild(0).GetChild(0);
    //    description.gameObject.SetActive(!description.gameObject.activeSelf);
    //}

    //public void Fireball()
    //{
    //    description = magePick.transform.GetChild(0).GetChild(1).GetChild(0);
    //    description.gameObject.SetActive(!description.gameObject.activeSelf);
    //}

    //public void Explosion()
    //{
    //    description = magePick.transform.GetChild(0).GetChild(2).GetChild(0);
    //    description.gameObject.SetActive(!description.gameObject.activeSelf);
    //}

    //public void TorrentOfFire()
    //{
    //    description = magePick.transform.GetChild(0).GetChild(3).GetChild(0);
    //    description.gameObject.SetActive(!description.gameObject.activeSelf);
    //}

    //public void WaveOfFire()
    //{
    //    description = magePick.transform.GetChild(0).GetChild(4).GetChild(0);
    //    description.gameObject.SetActive(!description.gameObject.activeSelf);
    //}

    public void ToggleMageTraps()
    {
        magePick.SetActive(!magePick.activeSelf);
        trapsPick.SetActive(!trapsPick.activeSelf);
    }

    public void Barricade()
    {
        description = trapsPick.transform.GetChild(0).GetChild(0).GetChild(0);
        description.gameObject.SetActive(!description.gameObject.activeSelf);
    }

    public void ArcaneTower()
    {
        description = trapsPick.transform.GetChild(0).GetChild(1).GetChild(0);
        description.gameObject.SetActive(!description.gameObject.activeSelf);
    }

    public void Brambles()
    {
        description = trapsPick.transform.GetChild(0).GetChild(2).GetChild(0);
        description.gameObject.SetActive(!description.gameObject.activeSelf);
    }

    public void BarricadePick()
    {
        description = trapsPick.transform.GetChild(1).GetChild(0).GetChild(0);
        description.gameObject.SetActive(!description.gameObject.activeSelf);
    }

    public void ArcaneTowerPick()
    {
        description = trapsPick.transform.GetChild(1).GetChild(1).GetChild(0);
        description.gameObject.SetActive(!description.gameObject.activeSelf);
    }

    public void BramblesPick()
    {
        description = trapsPick.transform.GetChild(1).GetChild(2).GetChild(0);
        description.gameObject.SetActive(!description.gameObject.activeSelf);
    }

    public void GoToSolo()
    {   
        
        passThroughScene.SelectedMage = selectedMage;
        SceneManager.LoadScene("Solo");
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
}