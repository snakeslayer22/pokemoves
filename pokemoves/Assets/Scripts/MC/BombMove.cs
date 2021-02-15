using System;
using System.Collections;
using UnityEngine;

public class BombMove : MonoBehaviour{

    [SerializeField] float throwSpeed = 0;

    [SerializeField] float explodeAfterTime = 3;

    private Rigidbody2D rb;

    private LayerMask enemy = 8;

    [SerializeField] float explosionRange = 0;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        if (Attack.right)
        {
            rb.AddForce(Vector3.right * throwSpeed, ForceMode2D.Impulse);
        }
        else if (Attack.left)
        {
            rb.AddForce(Vector3.left * throwSpeed, ForceMode2D.Impulse);
        }
        else if (Attack.up)
        {
            rb.AddForce(Vector3.up * throwSpeed, ForceMode2D.Impulse);
        }
        else if (Attack.down)
        {
            rb.AddForce(Vector3.down * throwSpeed, ForceMode2D.Impulse);
        }
    }

    public void updateSpriteRenderer(Sprite sprite)
    {
        transform.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    void Update()
    {
        explodeAfterTime -= Time.deltaTime;

        if (explodeAfterTime < 0)
        {
            explode();
            //play explosion
            Destroy(gameObject);
        }
    }

    private void explode()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRange, enemy);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().HitEnemy(Attack.MCdamage);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == enemy)
        {
            col.gameObject.GetComponent<Enemy>().HitEnemy(Attack.MCdamage);
            Debug.Log("it hit the enemy");
        }

        Destroy(this.gameObject);
    }
}
