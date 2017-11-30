using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoSettings : MonoBehaviour
{

    int[] resolutionsWidth = new int[] { 800, 1024, 1024, 1280, 1360, 1600, 1920 };
    int[] resolutionsHeight = new int[] { 600, 600, 768, 720, 768, 900, 1080 };
    bool fullscreen = true;

    public void SetResolution(int level)
    {
        Screen.SetResolution(resolutionsWidth[level], resolutionsHeight[level], fullscreen);
        
    }

    public void SetRefreshRate()
    {
        //DO
    }

    public void SetFullscreen(bool value)
    {
        Screen.fullScreen = value;
        fullscreen = value;
    }

    public void SetQuality(int level)
    {
        QualitySettings.SetQualityLevel(level);
     
    }

    public void ToggleShadows(bool value)
    {
      
        if (value) QualitySettings.shadowDistance = 300;
        else QualitySettings.shadowDistance = 0;
    }
}
