using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {

    private Texture2D crosshairTexture;
    private Rect crosshairPos;
    private Rect crosshairPosEditor;
    public bool drawCrosshair = true;
    public Color crosshairColor;

    private void Awake()
    {
        
        crosshairTexture = (Texture2D) Resources.Load("Crosshair/Crosshair");
        
    }

    private void Start()
    {
        //Create crosshair on center
        crosshairPos = new Rect((Screen.width - crosshairTexture.width / 2) / 2, (Screen.height -
        crosshairTexture.height / 2) / 2, crosshairTexture.width / 2, crosshairTexture.height / 2);

        crosshairPosEditor = new Rect((Screen.width - crosshairTexture.width / 2) / 2, (Screen.height - 50 -
        crosshairTexture.height / 2) / 2, crosshairTexture.width / 2, crosshairTexture.height / 2);
    }

    private void OnGUI()
    {
        if (drawCrosshair)
        {
            
            //Draw Crosshair
            if (Application.isEditor)
            {
                GUI.DrawTexture(crosshairPosEditor, crosshairTexture, 0, true, 0, crosshairColor, 0, 0);
            }
            else
            {
                GUI.DrawTexture(crosshairPos, crosshairTexture, 0, true, 0, crosshairColor, 0, 0);
            }
           



        }
    }

    
}
