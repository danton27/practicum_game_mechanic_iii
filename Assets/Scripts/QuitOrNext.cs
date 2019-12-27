/*
 *   Copyright (c) 2019 NotSlimBoy
 *   All rights reserved.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitOrNext : MonoBehaviour
{
    public string NameScene = "";
    public GameObject panelWinLose;
    public GameObject winCond;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            player.SetActive(false);
            this.gameObject.SetActive(false);
            panelWinLose.SetActive(true);
            winCond.SetActive(true);
            // if (Data.score >= 120 )
            // {
            //     Debug.Log("EndPoint");
            // } else {
            //     Debug.Log("Your coin is not enough to go to the next stage!!");
            // }
            //SceneManager.LoadScene(NameScene);
        }
    }
}
