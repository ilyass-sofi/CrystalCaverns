using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the building section of the game with a scanner and raycast
/// </summary>
public class Builder : MonoBehaviour {

    #region Variables

    [SerializeField]
    private bool buildingState = false;

    /// <summary>
    /// If the building scanner is activated
    /// </summary>
    private bool scanner = false;

    /// <summary>
    /// If the zone is free for building
    /// </summary>
    private bool freeZone = true;

    /// <summary>
    /// Cam reference
    /// </summary>
    public Camera cam;

    private Mage mage;

    /// <summary>
    /// Array of buildings prefabs
    /// </summary>
    private GameObject[] buildings = new GameObject[3];

    private BuildingAsset[] buildingAssets;

    /// <summary>
    /// Prefab selected
    /// </summary>
    private GameObject currentPrefab;

    /// <summary>
    /// Gameobject created
    /// </summary>
    private GameObject currentObject;

    /// <summary>
    /// Rotation that stays even if you create another object
    /// </summary>
    private float setRotation = 0;

    /// <summary>
    /// Green material that sets itself when the object is buildable
    /// </summary>
    public Material buildable;

    /// <summary>
    /// Red material that sets itself when the object is NOT buildable
    /// </summary>
    public Material notBuildable;

    /// <summary>
    /// Building range that the player has
    /// </summary>
    [SerializeField] public float buildingRange;

    /// <summary>
    /// Layer of the raycast, to make the raycast ignore the selected buildings
    /// </summary>
    private LayerMask layerMask = 9;

    Dictionary<string, int> buyingLimit = new Dictionary<string, int>();
    Dictionary<string, int> builtTraps = new Dictionary<string, int>();

    public Image[] buildingImages;
    private Text[] trapQuantity;

    private int selectedId;

    #endregion


    private void Start()
    {
        mage = GetComponent<Mage>();
        trapQuantity = new Text[buildingImages.Length];

        for (int i = 0; i < trapQuantity.Length; i++)
        {   
            trapQuantity[i] = buildingImages[i].gameObject.transform.GetChild(1).GetComponent<Text>();
            UpdateQuantityHUD(i);

        }
  
    }


    void Update ()
    {

        if (buildingState)
        {
            #region Build

            //Creates the selected building when clicked
            if (Input.GetMouseButtonDown(0) && currentObject != null && freeZone)
            {

                if (CheckCurrencyStock(selectedId))
                {
                    builtTraps[buildingAssets[selectedId].buildingName]++;
                    UpdateQuantityHUD(selectedId);

                    SetCustomBehaviour(buildingAssets[selectedId].buildingName);

                    if (!currentObject.GetComponent<Building>().Trigger)
                        currentObject.GetComponent<Collider>().isTrigger = false;

                    currentObject.AddComponent<Rigidbody>().isKinematic = true;
                    currentObject.GetComponent<Rigidbody>().useGravity = false;

                    //Sets its own material and create the building
                    currentObject.GetComponent<Building>().setOwnMaterial();
                    currentObject = Instantiate(currentPrefab, transform.position, currentPrefab.transform.rotation);
                    //Rotate
                    currentObject.transform.Rotate(currentObject.transform.rotation.x, currentObject.transform.rotation.y + setRotation, currentObject.transform.rotation.z);
                }

            }


            #endregion

            #region Raycast Building Scanner

            //if the player is building
            if (scanner && Time.timeScale != 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                //Send a raycast to the cam direction ignoring the object that you are building within a specified range
                
                if (Physics.Raycast(ray, out hit, 50, layerMask))
                {
                    currentObject.SetActive(true);
                    //If it hits a buildable object like the Ground and the building does not collide with other objects
                    if (hit.transform.gameObject.tag == "Ground")
                    {

                        currentObject.transform.position = new Vector3(Mathf.Round(hit.point.x), hit.point.y, Mathf.Round(hit.point.z));

                        if (currentObject.GetComponent<Building>().getBuildable() && Vector3.Distance(transform.position, hit.point) < buildingRange && builtTraps[buildingAssets[selectedId].buildingName] < buyingLimit[buildingAssets[selectedId].buildingName])
                        {
                            SetBuildable(true);
                        }
                        else SetBuildable(false);

                    }
                    else SetBuildable(false);
                }
                else currentObject.SetActive(false);
                
            }

            #endregion

        }
        else if (currentObject)
        {
            DeselectBuilding();
        }
    }

    public void SetBuildings(BuildingAsset[] _buildingAssets)
    {
        buildingAssets = _buildingAssets;
        for (int i = 0; i < buildingAssets.Length; i++)
        {
            buildings[i] = buildingAssets[i].buildingPrefab;
            buildingImages[i].sprite = buildingAssets[i].sprite;

            buyingLimit.Add(buildingAssets[i].buildingName, buildingAssets[i].buildingLimit);
            builtTraps.Add(buildingAssets[i].buildingName, 0);
        }

    }

    private void SetBuildable(bool value)
    {
        if (value)
        {
            currentObject.GetComponent<Renderer>().material = buildable;
            currentObject.transform.localScale = new Vector3(1, 1, 1);
            freeZone = true;
        }
        else
        {
            currentObject.GetComponent<Renderer>().material = notBuildable;
            currentObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            freeZone = false;
        }
    }

    private void SetCustomBehaviour(string buildingName)
    {
        switch (buildingName)
        {
            case "Torre Arcana":
                currentObject.transform.GetChild(0).gameObject.SetActive(true);
                currentObject.GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = true;
                break;
            case "Barricada":
                currentObject.GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = true;
                currentObject.GetComponent<Barricade>().enabled = true;
                break;
            case "Zarzas":
                currentObject.GetComponent<Brambles>().enabled = true;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Changes scanner building to the selected number
    /// </summary>
    /// <param name="id">Buildings id on the array</param>
    public void ChangeBuilding(int id)
    {
        selectedId = id;
        scanner = false;
        //Get the position of the last building and destroys it
        Vector3 tempPos = transform.position + Vector3.down * 10;
        if (currentObject)
        {
            tempPos = currentObject.transform.position;
            Destroy(currentObject);
        }
      
        //Create a new one with the selected prefab on that same position
        currentPrefab = buildings[id];
        currentObject = Instantiate(currentPrefab, tempPos, currentPrefab.transform.rotation);
        scanner = true;

    }

    /// <summary>
    /// Rotates Building 90 degrees
    /// </summary>
    public void RotateBuilding()
    {
        if (scanner)
        {
            //Rotate the building 90 degrees and save that rotation for the next created building
            currentObject.transform.Rotate(currentObject.transform.rotation.x, currentObject.transform.rotation.y + 90, currentObject.transform.rotation.z );
            if (setRotation >= 360)
            {
                setRotation = 0;
            }
            setRotation += 90;
        }
    }

    public void DeselectBuilding()
    {
        if (scanner)
        {
            scanner = false;
            Destroy(currentObject);
        }
    }

    public bool BuildingState
    {
        get { return buildingState; }
        set { buildingState = value;}
    }

    private bool CheckCurrencyStock(int id)
    {
        bool available = false;
        if (mage.Gold >= buildingAssets[id].cost && builtTraps[buildingAssets[id].buildingName] < buyingLimit[buildingAssets[id].buildingName])
        {
            mage.Gold -= buildingAssets[id].cost;
            available = true;
        }
        return available;
    }

    private void UpdateQuantityHUD(int position)
    {
        trapQuantity[position].text = builtTraps[buildingAssets[position].buildingName] + "/" + buyingLimit[buildingAssets[position].buildingName];
    }


}
