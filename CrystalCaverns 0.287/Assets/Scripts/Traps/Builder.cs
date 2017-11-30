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

    /// <summary>
    /// Array of buildings prefabs
    /// </summary>
    public GameObject[] buildings;

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

    Dictionary<string, int> inventory = new Dictionary<string, int>();
    Dictionary<string, int> buyingLimit = new Dictionary<string, int>();
    Dictionary<string, int> builtTraps = new Dictionary<string, int>();

    public Text firstTrapQuantity;
    public Text secondTrapQuantity;
    public Text thirdTrapQuantity;

    #endregion

    private void Start()
    {   
        //Add only selected traps, store the names
        inventory.Add("Barricade", 0);
        inventory.Add("Brambles", 0);
        inventory.Add("ArcaneTower", 0);

        //Buying and building Limit 
        buyingLimit.Add("Barricade", 20);
        buyingLimit.Add("Brambles", 12);
        buyingLimit.Add("ArcaneTower", 2);

        //Traps builts, do not change
        builtTraps.Add("Barricade", 0);
        builtTraps.Add("Brambles", 0);
        builtTraps.Add("ArcaneTower", 0);

    }


    void Update ()
    {

        if (buildingState)
        {

            //Creates the selected building when clicked and the an object has been sel
            if (Input.GetMouseButtonDown(0) && currentObject != null && freeZone)
            {
                string name = currentObject.GetComponent<Building>().Type;
                if (inventory[name] > 0 && builtTraps[name] < buyingLimit[name])
                {
                    inventory[name]--;
                    builtTraps[name]++;
                    UpdateQuantityHUD(name);
                    //Set traps custom behaviour here (You have to set the type on the Building class of the trap)
                    switch (name)
                    {
                        case "ArcaneTower":
                            currentObject.transform.GetChild(0).gameObject.SetActive(true);
                            currentObject.GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = true;
                            break;
                        case "Barricade":
                            currentObject.GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = true;
                            currentObject.GetComponent<Barricade>().enabled = true;
                            break;
                        case "Brambles":
                            currentObject.GetComponent<Brambles>().enabled = true;
                            break;
                        default:
                            break;
                    }
                    //Enable the scanner option and adds components to make it a real object
                    scanner = true;


                    if (!currentObject.GetComponent<Building>().Trigger)
                        currentObject.GetComponent<Collider>().isTrigger = false;

                    currentObject.AddComponent<Rigidbody>().isKinematic = true;
                    currentObject.GetComponent<Rigidbody>().useGravity = false;

                    //Build behind objects disable option
                    //currentObject.layer = 0;

                    //Sets its own material and create the building
                    currentObject.GetComponent<Building>().setOwnMaterial();
                    currentObject = Instantiate(currentPrefab, transform.position, currentPrefab.transform.rotation);
                    //Rotate
                    currentObject.transform.Rotate(currentObject.transform.rotation.x, currentObject.transform.rotation.y + setRotation, currentObject.transform.rotation.z);
                }



            }

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
                    if (hit.transform.gameObject.tag == "Ground" && currentObject.GetComponent<Building>().getBuildable() && Vector3.Distance(transform.position, hit.point) < buildingRange && inventory[currentObject.GetComponent<Building>().Type] > 0)
                    {
                        //Set the material to buildable green and position it on that spot but with rounded position
                        currentObject.GetComponent<Renderer>().material = buildable;
                        currentObject.transform.position = new Vector3(Mathf.Round(hit.point.x), hit.point.y, Mathf.Round(hit.point.z));
                        freeZone = true;

                    }
                    else
                    {
                        currentObject.GetComponent<Renderer>().material = notBuildable;
                        currentObject.transform.position = new Vector3(Mathf.Round(hit.point.x), 0.1f, Mathf.Round(hit.point.z));
                        freeZone = false;
                    }


                }
                else
                {
                    currentObject.SetActive(false);
                }


            }

            #endregion

        }
        else if (currentObject)
        {
            scanner = false;
            Destroy(currentObject);
        }
    }

    /// <summary>
    /// Changes scanner building to the selected number
    /// </summary>
    /// <param name="id">Buildings id on the array</param>
    public void ChangeBuilding(int id)
    {
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
        //currentObject.transform.Rotate(currentObject.transform.rotation.x - 90, currentObject.transform.rotation.y, currentObject.transform.rotation.z + 90);
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

    public void AddItemInventory(string name,int price)
    {
        if(inventory[name] < buyingLimit[name])
        {
            GetComponent<Mage>().Gold -= price;
            inventory[name]++;
            UpdateQuantityHUD(name);
        }
    }

    public void UpdateQuantityHUD(string name)
    {
        switch (name)
        {
            //use the stored names instead of hard coded values
            case "Barricade":
                firstTrapQuantity.text = inventory[name] + "/" + buyingLimit[name];
                break;
            case "Brambles":
                secondTrapQuantity.text = inventory[name] + "/" + buyingLimit[name];
                break;
            case "ArcaneTower":
                thirdTrapQuantity.text = inventory[name] + "/" + buyingLimit[name];
                break;
            default:
                break;
        }
    }


}
