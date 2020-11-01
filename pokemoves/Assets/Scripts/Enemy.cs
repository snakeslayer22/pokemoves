using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float damage = 0;
    public float EnemyDamage;

    private Transform player;
    private Transform playerCollider1;
    private Transform playerCollider2;
    private Animator animator;

    [SerializeField] private float speed = 0;
    [SerializeField] private float stoppingDistance = 0;
    [SerializeField] private float sight = 0;

    [SerializeField] private float enemyHealth = 0;

    private bool canDo = true;

    private bool Attacked;
    private bool moveBack;
    private bool moveForward;

    [SerializeField] private bool slime;
    [SerializeField] private bool wolf;

    private float enemyNumber = 0;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerCollider1 = player.Find("MCCollider1").GetComponent<Transform>();
        playerCollider2 = player.Find("MCCollider2").GetComponent<Transform>();
        animator = this.GetComponent<Animator>();

        if (slime) enemyNumber = 1;
        else if (wolf) enemyNumber = 2;

        animator.SetFloat("Enemy", enemyNumber);
    }

    private void FixedUpdate()
    {
        if (canDo)
        {
            if (Vector2.Distance(transform.position, playerCollider1.position) < sight || Vector2.Distance(transform.position, playerCollider2.position) < sight)
            {
                animator.SetBool("Idle", false);

                if (Vector2.Distance(transform.position, playerCollider1.position) > stoppingDistance && Vector2.Distance(transform.position, playerCollider2.position) > stoppingDistance)
                {
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
                }
                else
                {
                    Attack();
                }
            }
            else
            {
                animator.SetBool("Idle", true);
            }
        }

        if (Attacked)
        {
            if (moveBack)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -0.07f);
            }

            if (moveForward)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.07f);
            }
        }
    }

    public void HitEnemy(float damage)
    {
        enemyHealth -= damage;
        Attacked = true;
        IEnumerator coroutine = constatlyMoveBackAndStay();
        StartCoroutine(coroutine);

        if (enemyHealth < 1)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        animator.SetFloat("HorizontalSpeed", (player.position.x - transform.position.x));
    }

    private void Attack()
    {
        canDo = false;
        IEnumerator coroutine = waitForAttackAnimation();
        StartCoroutine(coroutine);
        attackAnimation();
        Attacked = true;
        HealthBar.SetHealth(damage);
    }

    private IEnumerator waitForAttackAnimation()
    {
        yield return new WaitForSeconds(0.75f);
        canDo = true;
    }

    private void attackAnimation()
    {
        IEnumerator coroutine = constatlyMoveBack();
        StartCoroutine(coroutine);
    }

    private IEnumerator constatlyMoveBack()
    {
        moveForward = false;
        moveBack = true;
        yield return new WaitForSeconds(0.08f);
        IEnumerator coroutine = constatlyMoveForward();
        StartCoroutine(coroutine);
    }

    private IEnumerator constatlyMoveForward()
    {
        moveBack = false;
        moveForward = true;
        yield return new WaitForSeconds(0.08f);
        Attacked = false;
    }

    private IEnumerator constatlyMoveBackAndStay()
    {
        moveForward = false;
        moveBack = true;
        yield return new WaitForSeconds(0.12f);
        Attacked = false;
    }
}
