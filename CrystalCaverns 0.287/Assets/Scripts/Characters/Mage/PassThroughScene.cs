using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughScene : MonoBehaviour {

    private static MageAsset selectedMage;

    public MageAsset SelectedMage
    {
        get { return selectedMage; }
        set { selectedMage = value; }
    }

}
