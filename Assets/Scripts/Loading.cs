using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] private float delayLoading = 3f;
    [SerializeField] private string nameScene;
    private float timeElapsed;
    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > delayLoading) {
            SceneManager.LoadScene(nameScene);
        }
    }
}
