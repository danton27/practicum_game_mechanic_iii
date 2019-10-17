using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAmmo : MonoBehaviour 
{
    // Update is called once per frame
    private void FixedUpdate() {
        GetComponent<Text>().text = Data.ammo.ToString("00");
    }    
}

