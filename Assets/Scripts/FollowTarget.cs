using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowTarget : MonoBehaviour
{
    public Transform player;
    public Transform Bg1;
    public Transform Bg2;
    public Transform Bg3;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneName == "Level2")
        {
            if (player.position.x != transform.position.x && player.position.x > -6f && player.position.x < 65f)
            {
                if (player.position.y < -9f)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), 0.1f);
                }else 
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, player.position.y, transform.position.z), 0.1f);        
                }
            }
        }
        else
        {            
            if (player.position.x != transform.position.x && player.position.x > -3.5f && player.position.x < 3f)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), 0.1f);
            }
        }

        Bg1.transform.position = new Vector3(transform.position.x * 1.0f, Bg1.transform.position.y);
        Bg2.transform.position = new Vector3(transform.position.x * 0.8f, Bg2.transform.position.y);
        Bg3.transform.position = new Vector3(transform.position.x * 0.9f, Bg3.transform.position.y);
    }
}
