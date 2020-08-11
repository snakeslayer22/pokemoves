using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    [SerializeField]
    private Animator BodyAnimator;

    [SerializeField]
    private Animator ArmsAnimator;

    [SerializeField]
    private float speed = 4;
    private Vector2 movement;
    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        ArmsAnimator.ResetTrigger("Attack");
        BodyAnimator.ResetTrigger("Attack");

        BodyAnimator.SetFloat("HorizontalSpeed", movement.x);
        BodyAnimator.SetFloat("VerticalSpeed", movement.y);

        ArmsAnimator.SetFloat("HorizontalSpeed", movement.x);
        ArmsAnimator.SetFloat("VerticalSpeed", movement.y);

        if (Input.GetKeyDown("space"))
        {
            ArmsAnimator.SetBool("Holding", true);
            BodyAnimator.SetBool("Holding", true);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            ArmsAnimator.SetBool("Holding", false);
            BodyAnimator.SetBool("Holding", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            ArmsAnimator.SetTrigger("Attack");
            BodyAnimator.SetTrigger("Attack");
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }
}
