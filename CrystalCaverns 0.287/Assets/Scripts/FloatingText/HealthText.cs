using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    Character ch;
    TextMesh txt;
	void Start ()
    {
        ch = transform.parent.GetComponent<Character>();
        txt = GetComponent<TextMesh>();

    }
	
	void Update ()
    {
        txt.text = ch.Health.ToString("0.##");
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
