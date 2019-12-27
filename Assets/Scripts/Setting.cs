using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    Text persenTeks;
    private float volume;
   // teks untuk persen

   void Start() 
   {
       persenTeks = GetComponent<Text>();
   }
   public void Volume (float value) {
       volume = Mathf.RoundToInt(value * 100);
       Audio.volume = volume;
       persenTeks.text = Audio.volume + "%";
   }

   
}
