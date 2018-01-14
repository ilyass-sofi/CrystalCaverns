using UnityEngine;
using UnityEngine.SceneManagement;

public class PassThroughScene : MonoBehaviour {

    [SerializeField] private MageAsset defaultMage;

    static MageAsset selectedMage;

    public MageAsset SelectedMage
    {
        get { return selectedMage; }
        set { selectedMage = value; }
    }

    private void Awake()
    {
        
        if(SceneManager.GetActiveScene().name == "Solo")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Friendly");

            if(selectedMage == null)
            {
                selectedMage = defaultMage;
            }

            switch (selectedMage.mageName)
            {
                case "Fire":
                    player.AddComponent<FireMage>();
                    break;
                case "Earth":
                    player.AddComponent<EarthMage>();
                    break;
                default:
                    break;
            }
           

            player.GetComponent<Mage>().SetMageAsset(selectedMage);


        }
    }

}
