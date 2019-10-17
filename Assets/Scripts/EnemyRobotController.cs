using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobotController : MonoBehaviour {
    public bool isGrounded = false;
    Animator anim;
    bool isCanShoot = true;
    public int HP = 10;
    bool isDie = false;
    public bool isFacingLeft;
    public GameObject Projectile; // object peluru
    public Vector2 projectileVelocity; // kecepatan peluru
    public Vector2 projectileOffset; // jarak posisi peluru dari posisi player
    public float cooldown = 1f; // jeda waktu menembak

    void Start()
    {
        anim = GetComponent<Animator>();
        if (!isDie && isCanShoot)
            InvokeRepeating("Fire", 2.0f, 4.0f);
    }
    
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (this.transform.position.y < -8f)
        {
            Destroy(this.gameObject);
        }
    }

    void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            isDie = true;
            anim.SetTrigger("dead");
            Data.score += 20;
            Destroy(this.gameObject, 2f);
        }
    }


    void Fire()
    {
        if (!isDie && isCanShoot)
        {
            GameObject bullet = Instantiate(Projectile, (Vector2)transform.position - projectileOffset * transform.localScale.x, Quaternion.identity);

            if (isFacingLeft)
            {
                bullet.GetComponent<SpriteRenderer>().flipX = true;
            }

            // Mengatur kecepatan projectile
            Vector2 velocity = new Vector2(projectileVelocity.x * transform.localScale.x, projectileVelocity.y);

            bullet.GetComponent<Rigidbody2D>().velocity = velocity * 1;
            StartCoroutine(CanShoot()); 
        }
    }

    IEnumerator CanShoot()
    {
        anim.SetTrigger("shoot");
        isCanShoot = false;
        yield return new WaitForSeconds(cooldown);
        anim.SetTrigger("idle");
        isCanShoot = true;
    }
}