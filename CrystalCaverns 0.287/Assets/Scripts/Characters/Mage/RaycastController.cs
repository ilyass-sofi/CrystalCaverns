using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    public LayerMask layers;

    private Camera cam;

    private bool normalRaycast = true;

    public bool NormalRaycast
    {
        get { return normalRaycast; }
        set { normalRaycast = value; }
    }

    private Vector3 normalPosition;

    public Vector3 NormalPosition
    {
        get { return normalPosition; }
        set { normalPosition = value; }
    }

    private GameObject normalTarget; 

    public GameObject NormalTarget
    {
        get { return normalTarget; }
        set { normalTarget = value; }
    }

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {

        if (normalRaycast)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 200, layers))
            {
                if (hit.transform.gameObject.CompareTag("Enemy"))
                {
                    normalTarget = hit.transform.gameObject;
                }
                else
                {
                    normalTarget = null;
                }
                
                normalPosition = hit.point;
            }
            else
            {
                normalPosition = ray.origin + (ray.direction * 100);
            }
        }

    }
}
