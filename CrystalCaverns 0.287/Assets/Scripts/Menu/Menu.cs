using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    [SerializeField] private GameObject backgroundPanel;
    [SerializeField] private GameObject modePanel;
    [SerializeField] private GameObject magePick;
    [SerializeField] private GameObject trapsPick;
    [SerializeField] private GameObject settingsPanel;

    [SerializeField] private GameObject keybindsPanel;
    [SerializeField] private GameObject audioPanel;
    [SerializeField] private GameObject videoPanel;


    private GameObject openPanel;
    private Transform description;

    void Start ()
    {
        //openPanel = keybindsPanel;
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

    //public void ActiveOrDesactive()
    //{
    //    Transform description = null;

    //    switch (gameObject.tag)
    //    {
    //        case "Passive":

    //            description = magePick.transform.GetChild(0).GetChild(0).GetChild(0);
    //            break;

    //        case "Fireball":

    //            description = magePick.transform.GetChild(0).GetChild(1).GetChild(0);
    //            break;

    //        case "Explosion":

    //            description = magePick.transform.GetChild(0).GetChild(2).GetChild(0);
    //            break;

    //        case "TorrentOfFire":

    //            description = magePick.transform.GetChild(0).GetChild(3).GetChild(0);
    //            break;

    //        case "WaveOfFire":

    //            description = magePick.transform.GetChild(0).GetChild(4).GetChild(0);
    //            break;
    //    }

    //    description.gameObject.SetActive(!description.gameObject.activeSelf);
    //}

    public void Passive()
    {
        description = magePick.transform.GetChild(0).GetChild(0).GetChild(0);
        description.gameObject.SetActive(!description.gameObject.activeSelf);
    }

    public void Fireball()
    {
        description = magePick.transform.GetChild(0).GetChild(1).GetChild(0);
        description.gameObject.SetActive(!description.gameObject.activeSelf);
    }

    public void Explosion()
    {
        description = magePick.transform.GetChild(0).GetChild(2).GetChild(0);
        description.gameObject.SetActive(!description.gameObject.activeSelf);
    }

    public void TorrentOfFire()
    {
        description = magePick.transform.GetChild(0).GetChild(3).GetChild(0);
        description.gameObject.SetActive(!description.gameObject.activeSelf);
    }

    public void WaveOfFire()
    {
        description = magePick.transform.GetChild(0).GetChild(4).GetChild(0);
        description.gameObject.SetActive(!description.gameObject.activeSelf);
    }

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

    //public void BeginDrag()
    //{
    //    barricade = trapsPick.transform.GetChild(0).GetChild(0).gameObject;
    //    barricade.transform.SetParent(barricade.transform.parent.parent);
    //    barricade.transform.GetChild(0).gameObject.SetActive(false);
    //    //GameObject duplicate = Instantiate(barricade, trapsPick.transform.GetChild(0));
    //    //Destroy(barricade);
    //    //duplicate.transform.GetChild(0).gameObject.SetActive(false);
    //}

    //public void Drag()
    //{
    //    Vector3 screenPoint = Input.mousePosition;
    //    barricade.transform.position = screenPoint;
    //}

    //public void Drop()
    //{
    //    barricade.transform.SetParent(barricade.transform.parent.parent);
    //    GameObject pick1 = trapsPick.transform.GetChild(1).GetChild(0).gameObject;
    //    Destroy(pick1);
    //}

    public void GoToSolo()
    {
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