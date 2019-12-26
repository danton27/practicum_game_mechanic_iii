using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCond : MonoBehaviour
{
    // Start is called before the first frame update
    public void goToMainMenu() {
        SceneManager.LoadScene("Menu");
    }
}
