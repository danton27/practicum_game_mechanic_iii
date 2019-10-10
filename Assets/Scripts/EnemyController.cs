using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool isGrounded = false; // Untuk mengecek karakter berada di ground
    public bool isFacingRight = false;
    private bool isHitPlayer = false;
    public Transform batas1; // digunakan untuk batas gerak ke kiri
    public Transform batas2; // digunakan untuk batas gerak ke kanan
    Animator anim;

    Rigidbody2D rigid;
    public int HP = 1;
    bool isDie = false;
    public static int enemyKilled = 0;

    float speed = 1; // kecepatan enemy bergerak

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded) 
        {
            if (!isHitPlayer) {
                if (isFacingRight)
                    MoveRight();
                else
                    MoveLeft();
            }
            
            if (transform.position.x >= batas2.position.x && isFacingRight)
                Flip();
            else if (transform.position.x <= batas1.position.x && !isFacingRight)
                Flip();
        }
    }

    void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            isDie = true;
            anim.SetBool("isDie", true);
            Destroy(this.gameObject, (float)0.5);
            Data.score += 20;
            enemyKilled++;
        }
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (col.gameObject.CompareTag("Player"))
        {
            isHitPlayer = true;
            Data.score -= 20;
        }
    }

    void OnCollisionStay2D (Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            anim.SetTrigger("idle");
            isGrounded = true;
        }
    }

    void OnCollisionExit2D (Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void MoveRight()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
        if (!isFacingRight)
        {
            Flip();
        }
    }

    void MoveLeft()
    {
        Vector3 pos = transform.position;
        pos.x -= speed * Time.deltaTime;
        transform.position = pos;
        if (isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        isFacingRight = !isFacingRight;
    }
}
