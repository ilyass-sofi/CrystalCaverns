using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    [SerializeField] private GameObject shopPrefab;
    private LevelManager lvlManager;

    private bool playerIn = false;
    private Mage player = null;

    private string itemName;
    private string itemDesc;
    private int itemPrice;
    private Sprite itemSprite;

    void Start ()
    {   
        lvlManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();

        itemName = shopPrefab.name;

        Building shopBuilding = shopPrefab.GetComponent<Building>();
        itemDesc = shopBuilding.Description;
        itemPrice = shopBuilding.Price;
        itemSprite = shopBuilding.Sprite;
    }

    private void Update()
    {
        if (playerIn && Input.GetKeyDown(KeyCode.B))
        {
            BuyItem();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Friendly")
        {
            playerIn = true;
            player = other.GetComponent<Mage>();
            lvlManager.ToggleShopPanel();
            lvlManager.SetShopItem(itemName, itemDesc, itemPrice, itemSprite);
            lvlManager.ToggleCrosshair(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Friendly")
        {
            playerIn = false;
            player = null;
            lvlManager.ToggleShopPanel();
            lvlManager.ToggleCrosshair(true);
        }
    }

    public void BuyItem()
    {
        if(player.Gold >= itemPrice)
        {
            lvlManager.AddItem(shopPrefab.GetComponent<Building>().Type, itemPrice);
        }
        
    }
}
