using System;
using System.Collections;
using UnityEngine;

public class RangedAmmo : MonoBehaviour{

    public float shootSpeed = 0;

    [SerializeField] float destroyAfterTime = 3;

    private Rigidbody2D rb;

    private LayerMask enemy = 8;

    private Transform shootPoint;

    void Start()
    {
        shootPoint = GameObject.Find("ShootPoint").transform;

        rb = gameObject.GetComponent<Rigidbody2D>();
        transform.localRotation = Quaternion.Euler(0, 0, shootPoint.rotation.eulerAngles.z);

        if(transform.localRotation == Quaternion.Euler(0, 0, 0))
        {
            rb.velocity = new Vector3(shootSpeed, 0, 0);
        }
        else if(transform.localRotation == Quaternion.Euler(0, 0, 90))
        {
            rb.velocity = new Vector3(0, shootSpeed, 0);
        }
        else if (transform.localRotation == Quaternion.Euler(0, 0, 180))
        {
            rb.velocity = new Vector3(-shootSpeed, 0, 0);
        }
        else if (transform.localRotation == Quaternion.Euler(0, 0, 270))
        {
            rb.velocity = new Vector3(0, -shootSpeed, 0);
        }
    }

    public void updateSpriteRenderer(Sprite sprite)
    {
        transform.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    void Update()
    {
        destroyAfterTime -= Time.deltaTime;

        if (destroyAfterTime < 0)
        {
            Destroy(this.gameObject);
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
