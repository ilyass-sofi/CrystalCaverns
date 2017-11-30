using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    private Crosshair crosshair;
    public GameObject pausePanel;

    private void Start()
    {
        crosshair = GetComponent<Crosshair>();
        //Cursor locked on center and invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
       
    }


    public void TogglePause()
    {   
        //In Gameplay
        if (Time.timeScale == 0)
        {
            crosshair.drawCrosshair = true;
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Inputs.isInputEnabled = true;
        }
        //In Pause
        else
        {
            crosshair.drawCrosshair = false;
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Inputs.isInputEnabled = false;
        }

        //Toggle pause panel
        pausePanel.SetActive(!pausePanel.activeSelf);
    }
}
