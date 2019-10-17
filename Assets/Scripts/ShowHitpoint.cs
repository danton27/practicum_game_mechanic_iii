using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHitpoint : MonoBehaviour 
{
    // Update is called once per frame
    private void FixedUpdate() {
        GetComponent<Text>().text = NewPlayerController.showHealth();
    }    
}

