using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

    /// <summary>
    /// Keybinds dictionnary with a name and their respective key
    /// </summary>
    public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    /// <summary>
    /// Color of keybind when not selected
    /// </summary>
    private Color32 normal = new Color32(0, 149, 255, 255);

    /// <summary>
    /// Color of keybind when selected
    /// </summary>
    private Color32 selected = new Color32(137, 203, 255, 255);

    /// <summary>
    /// Current Pressed Key
    /// </summary>
    private GameObject currentKey;

    /// <summary>
    /// Text reference on the keybinds
    /// </summary>
    public Text basicAttack, firstSpell, secondSpell, ultimate, rotateBuilding, deselectBuilding, togglePause;

    /// <summary>
    /// State text that displays the state of the keybinds
    /// </summary>
    public Text stateText;

    void Start ()
    {
        //To add a key just copy paste, and put a name and a default key
        keys.Add("BasicAttack", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("BasicAttack", KeyCode.Mouse0.ToString())));
        keys.Add("FirstSpell", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("FirstSpell", "Q")));
        keys.Add("SecondSpell", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SecondSpell", "E")));
        keys.Add("Ultimate", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Ultimate", "R")));

        keys.Add("RotateBuilding", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RotateBuilding", "F")));
        keys.Add("DeselectBuilding", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("DeselectBuilding", KeyCode.Mouse1.ToString())));
        keys.Add("TogglePause", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("TogglePause", "Escape")));

        //Text Reference set to the key, make click set to Left Click ..
        basicAttack.text = keys["BasicAttack"].ToString();
        firstSpell.text = keys["FirstSpell"].ToString();
        secondSpell.text = keys["SecondSpell"].ToString();
        ultimate.text = keys["Ultimate"].ToString();

        rotateBuilding.text = keys["RotateBuilding"].ToString();
        deselectBuilding.text = keys["DeselectBuilding"].ToString();
        togglePause.text = keys["TogglePause"].ToString();
        
    }

    private void OnGUI()
    {   
        //When we're changing keys
        if (currentKey != null)
        {   
            //We take the key value
            Event e = Event.current;
            if (e.isKey)
            {
                //Check if the key has not been taken
                if (!keys.ContainsValue(e.keyCode) && e.keyCode != KeyCode.None)
                {
                    //Set the new key on its value
                    string keyName = null;
                    keys[currentKey.name] = e.keyCode;
                    switch (keys[currentKey.name])
                    {
                        case KeyCode.Alpha0:
                            keyName = "0";
                            break;
                        case KeyCode.Alpha1:
                            keyName = "1";
                            break;
                        case KeyCode.Alpha2:
                            keyName = "2";
                            break;
                        case KeyCode.Alpha3:
                            keyName = "3";
                            break;
                        case KeyCode.Alpha4:
                            keyName = "4";
                            break;
                        case KeyCode.Alpha5:
                            keyName = "5";
                            break;
                        case KeyCode.Alpha6:
                            keyName = "6";
                            break;
                        case KeyCode.Alpha7:
                            keyName = "7";
                            break;
                        case KeyCode.Alpha8:
                            keyName = "8";
                            break;
                        case KeyCode.Alpha9:
                            keyName = "9";
                            break;
                        case KeyCode.Mouse0:
                            keyName = "Left Click";
                            break;
                        case KeyCode.Mouse1:
                            keyName = "Right Click";
                            break;
                        case KeyCode.Mouse2:
                            keyName = "Middle Click";
                            break;
                        default:
                            keyName = e.keyCode.ToString();
                            break;
                    }
                    keys[currentKey.name] = e.keyCode;
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = keyName;
                    currentKey.transform.parent.GetComponent<Image>().color = normal;
                    StateText("changed", keyName);
                    currentKey = null;
                }
                else
                {
                    StateText("taken");
                }
            }

            //Same for mouse
            if (e.isMouse)
            {
               
                switch (e.button)
                {
                    case 0:
                        if (!keys.ContainsValue(KeyCode.Mouse0))
                        {
                            keys[currentKey.name] = KeyCode.Mouse0;
                            currentKey.transform.GetChild(0).GetComponent<Text>().text = "Left Click";
                            StateText("changed", "Left Click");
                        }
                        else
                        {
                            StateText("taken");
                        }
                        break;
                    case 1:
                        if (!keys.ContainsValue(KeyCode.Mouse1))
                        {
                            keys[currentKey.name] = KeyCode.Mouse1;
                            currentKey.transform.GetChild(0).GetComponent<Text>().text = "Right Click";
                            StateText("changed", "Right Click");
                        }
                        else
                        {
                            StateText("taken");
                        }

                        break;

                }

                currentKey.transform.parent.GetComponent<Image>().color = normal;
                currentKey = null;
                
                   
            }
        }
    }

    /// <summary>
    /// Changes the selected key to the new value
    /// </summary>
    /// <param name="clicked"></param>
    public void ChangeKey(GameObject clicked)
    {
        if (currentKey != null)
        {
            currentKey.transform.parent.GetComponent<Image>().color = normal;
        }

        currentKey = clicked;
        currentKey.transform.parent.GetComponent<Image>().color = selected;
    }

    /// <summary>
    /// Save keys into player prefs
    /// </summary>
    public void SaveKeys()
    {
        
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
            Debug.Log(key.Key +" "+  key.Value.ToString());
        }

        PlayerPrefs.Save();
        StateText("save");
      
    }

    /// <summary>
    /// Displays the state of the keybinds
    /// </summary>
    /// <param name="state"></param>
    /// <param name="key"></param>
    public void StateText(string state, string key = "" )
    {
        switch (state)
        {
            case "save":
                stateText.text = "Keybinds have been saved!";
                stateText.color = Color.green;
                break;
            case "taken":
                stateText.text = "Keybind has already been taken.";
                stateText.color = Color.red;
                break;
            case "changed":
                stateText.text = currentKey.name + " is now " + key;
                stateText.color = Color.yellow;
                break;
        }
    }

    /// <summary>
    /// Returns the input keys
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, KeyCode> getKeys()
    {
        return keys;
    }
}
