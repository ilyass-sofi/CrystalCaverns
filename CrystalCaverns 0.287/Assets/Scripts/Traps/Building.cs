using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [SerializeField] private Sprite sprite;

    public Sprite Sprite
    {
        get { return sprite; }
        set { sprite = value; }
    }


    [SerializeField] private bool trigger;

    public bool Trigger
    {
        get { return trigger; }
        set { trigger = value; }
    }

    [SerializeField] private string type;

    public string Type
    {
        get { return type; }
        set { type = value; }
    }

    [SerializeField] private int price;

    public int Price
    {
        get { return price; }
        set { price = value; }
    }

    [SerializeField] private string description;

    public string Description
    {
        get { return description; }
        set { description = value; }
    }



    /// <summary>
    /// If the building collides with anything
    /// </summary>
    private bool buildable = true;

    /// <summary>
    /// Reference to own material
    /// </summary>
    private Material mat;

    /// <summary>
    /// Gets its own material so we can paint it when placed
    /// </summary>
    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
    }

    /// <summary>
    /// When it enters a trigger the building cannot get built
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {   
        if(other.tag != "Trigger")
            buildable = false;
    }

    /// <summary>
    /// Resets to true when exiting it
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        if (other.tag != "Trigger")
            buildable = true;
    }

    /// <summary>
    /// Returns if the building is buildable
    /// </summary>
    /// <returns></returns>
    public bool getBuildable()
    {
        return buildable;
    }

    /// <summary>
    /// Sets it own material to default
    /// </summary>
    public void setOwnMaterial()
    {
        GetComponent<Renderer>().material = mat;
    }
}


