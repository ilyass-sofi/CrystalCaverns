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

    private Vector3 normalTarget;

    public Vector3 NormalTarget
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
            //LayerMask.LayerToName(3);
            if (Physics.Raycast(ray, out hit, 200, layers))
            {
                
                normalTarget = hit.point;
            }
            else
            {
                normalTarget = ray.origin + (ray.direction * 100);
            }
        }

    }
}
