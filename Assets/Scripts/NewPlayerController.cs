using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewPlayerController : MonoBehaviour
{
    public GameObject loseCond;
    public static int HP = 20;
    public int ammo = 0;
    bool isJump = true;
    bool isDead = false;
    bool isFacingRight = true;
    bool isFacingLeft = false;
    static int attacked = 0;
    int idMove = 0;
    Animator anim;
    public GameObject Projectile; // object peluru
    public Vector2 projectileVelocity; // kecepatan peluru
    public Vector2 projectileOffset; // jarak posisi peluru dari posisi player
    public float cooldown = 0.5f; // jeda waktu untuk menembak
    public static bool isCanShoot = false; // memastikan untuk kapan dapat menembak
    
    
    // Use this for initialization
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Jump "+isJump);
        if (!isDead)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                Idle();
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                Idle();
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (Data.ammo > 0)
                    Fire();
            }
            Move();
            Dead();
        }

    }

    public static string showHealth()
    {
        return HP.ToString();
    }

    void TakeDamage(int damage)
    {
        switch(attacked)
        {
            case 1:
                HP -= (damage * 2);
                attacked = 0;
                break;
            case 0:
                HP -= damage;
                break;
        }

        switch(HP)
        {
            case 5:
                Data.ammo += 3;
                Debug.Log("Angry mode active, ammo + 3");
                break;
            case 0:
                isDead = true;
                anim.SetTrigger("dead");
                this.gameObject.SetActive(false);
                loseCond.SetActive(true);
                break;
        }
        if (HP < 0) {
            isDead = true;
            anim.SetTrigger("dead");
            this.gameObject.SetActive(false);
            loseCond.SetActive(true);
        }
        attacked = 1;
        StartCoroutine(BeginAttackedCombo());
    }

    IEnumerator BeginAttackedCombo()
    {
        yield return new WaitForSeconds(2);
        attacked = 0;
    }

    public void Fire()
    {
        if (Data.ammo == 0) {
            return ;
        }
        if (isCanShoot)
        {
            Data.ammo -= 1;

            // Membuat projectile baru
            GameObject bullet = Instantiate(Projectile, (Vector2)transform.position - projectileOffset * transform.localScale.x, Quaternion.identity);

            if (isFacingLeft)
            {
                bullet.GetComponent<SpriteRenderer>().flipX = true;
            } else if (isFacingRight)
            {
                bullet.GetComponent<SpriteRenderer>().flipX = false;
            }

            // Mengatur kecepatan dari projectile
            Vector2 velocity = new Vector2(projectileVelocity.x * transform.localScale.x, projectileVelocity.y);
            bullet.GetComponent<Rigidbody2D>().velocity = velocity * 1;

            // //  Menyesuaikan scale dari projectile dengan scale karakter
            // Vector3 scale = transform.localScale;
            // bullet.transform.localScale = scale * -1;

            StartCoroutine(CanShoot());
        }
    }

    IEnumerator CanShoot()
    {
        anim.SetTrigger("shoot");
        isCanShoot = false;
        yield return new WaitForSeconds(cooldown);
        isCanShoot = true;
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            isDead = true;
            anim.SetTrigger("dead");
            this.gameObject.SetActive(false);
            loseCond.SetActive(true);
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        // Kondisi ketika menyentuh tanah
        if (isJump)
        {
            anim.ResetTrigger("jump");
            if (idMove == 0) anim.SetTrigger("idle");
            isJump = false;
        }
 
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Kondisi ketika menyentuh tanah
        anim.SetTrigger("jump");
        anim.ResetTrigger("run");
        anim.ResetTrigger("idle");
        isJump = true;
    }
    
    public void MoveRight()
    {
        idMove = 1;
        isFacingRight = true;
        isFacingLeft = false;
    }
    
    public void MoveLeft()
    {
        idMove = 2;
        isFacingRight = false;
        isFacingLeft = true;
    }
    
    private void Move()
    {
        if (idMove == 1 && !isDead)
        {
            // Kondisi ketika bergerak ke kekanan
            if (!isJump) anim.SetTrigger("run");
            transform.Translate(1 * Time.deltaTime * 4f, 0, 0);
            transform.localScale = new Vector3(0.338468f, 0.338468f, 1f);
        }
        if (idMove == 2 && !isDead)
        {
            // Kondisi ketika bergerak ke kiri
            if (!isJump) anim.SetTrigger("run");
            transform.Translate(-1 * Time.deltaTime * 4f, 0, 0);
            transform.localScale = new Vector3(-0.338468f, 0.338468f, 1f);
        }
    }
    
    public void Jump()
    {
        if (!isJump)
        {
            // Kondisi ketika Loncat           
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300f);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Coin"))
        {
            Data.score += 15;
            Destroy(collision.gameObject);
        } else if (collision.transform.tag.Equals("Ammo"))
        {
            isCanShoot = true;
            Data.ammo += 3;
            Destroy(collision.gameObject);
        }
    }
    
    public void Idle()
    {
        // kondisi ketika idle/diam
        if (!isJump)
        {
            anim.ResetTrigger("jump");
            anim.ResetTrigger("run");
            anim.SetTrigger("idle");
        }
        idMove = 0;
    }
    
    private void Dead()
    {
        if (!isDead)
        {
            if (transform.position.y < -7f)
            {
                // kondisi ketika jatuh
                isDead = true;
                HP = 0;
                Destroy(this.gameObject);
                loseCond.SetActive(true);
            }
        }
    }
}
