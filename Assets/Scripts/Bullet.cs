using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private static int attackCounter = 0;
    void Start()
    {
        Destroy(this.gameObject, 10);
    }


    public static IEnumerator ComboAttack()
    {
        Debug.Log("Combo Attack Begin");
        yield return new WaitForSeconds(2);
        attackCounter = 0;
        Debug.Log("Combo Attack Done");
    }
        


    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            Destroy(this.gameObject);
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.SendMessage("TakeDamage", 5);

        } else if (col.gameObject.CompareTag("Player"))
        {
            if (attackCounter == 1)
            {
                Debug.Log("ComboAttack");
                col.gameObject.SendMessage("TakeDamage", 10);
                attackCounter = 0;
            } else {
                Debug.Log("Normal Attack");
                attackCounter += 1;
                col.gameObject.SendMessage("TakeDamage", 5);
                ComboAttack();
            }
        }
        Destroy(this.gameObject);
    }
}
