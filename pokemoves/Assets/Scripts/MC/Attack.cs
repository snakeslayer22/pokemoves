using System;
using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour{

    private Animator BodyAnimator;

    private Animator ArmsAnimator;

    [SerializeField] private float damage = 0;
    public static float MCdamage;
    [SerializeField] private float attackRange = 0;
    public static float MCAttackRange;

    private Transform upHitBox;
    private Transform downHitBox;
    private Transform rightHitBox;
    private Transform leftHitBox;

    static public bool up = false;
    static public bool down = false;
    static public bool right = false;
    static public bool left = false;

    public LayerMask enemyLayer;

    private bool resetTrigger;

    public static bool canAttack = true;

    private Transform arm;
    private Transform holdPoint;

    private bool rotationAllowed;
    private bool allowRotationOnce = false;

    public GameObject rangedAmmo;
    public Transform shootPoint;

    private void Start()
    {
        MCdamage = damage;
        MCAttackRange = attackRange;

        upHitBox = transform.Find("UpAttackHitBox");
        downHitBox = transform.Find("DownAttackHitBox");
        rightHitBox = transform.Find("RightAttackHitBox");
        leftHitBox = transform.Find("LeftAttackHitBox");

        ArmsAnimator = transform.Find("MC_Arms").GetComponent<Animator>();
        BodyAnimator = transform.Find("MC_Body").GetComponent<Animator>();

        arm = GameObject.FindGameObjectWithTag("arm").transform;
        holdPoint = arm.Find("HoldPoint");
    }

    void LateUpdate(){

        if (resetTrigger)
        {
            ArmsAnimator.ResetTrigger("Attack");
            BodyAnimator.ResetTrigger("Attack");
            resetTrigger = false;
        }

        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            if(MCAttackRange < 50)
            {
                if (right) AttackEnemyRight();
                else if (left) AttackEnemyLeft();
                else if (up) AttackEnemyUp();
                else if (down) AttackEnemyDown();
            }
            else if(MCAttackRange == 69)
            {
                if (up)
                {
                    rotationAllowed = true;
                    allowRotationOnce = true;
                }
                else if (down) {
                    rotationAllowed = true;
                    allowRotationOnce = true;
                };
                Shoot();
            }

            ArmsAnimator.SetTrigger("Attack");
            BodyAnimator.SetTrigger("Attack");
            resetTrigger = true;
        }
    }

    private void AttackEnemyRight()
    {
        HitEnemy(rightHitBox);
    }

    private void AttackEnemyLeft()
    {
        HitEnemy(leftHitBox);
    }

    private void AttackEnemyUp()
    {
        HitEnemy(upHitBox);
        rotationAllowed = true;
        allowRotationOnce = true;
    }

    private void AttackEnemyDown()
    {
        HitEnemy(downHitBox);
        rotationAllowed = true;
        allowRotationOnce = true;
    }

    private void FixedUpdate()
    {
        if (rotationAllowed)
        {
            if (up)
            {
                holdPoint.rotation = Quaternion.Euler(0, holdPoint.rotation.eulerAngles.y, holdPoint.rotation.eulerAngles.z + -9f);
            }

            if (down)
            {
                holdPoint.rotation = Quaternion.Euler(0, holdPoint.rotation.eulerAngles.y, holdPoint.rotation.eulerAngles.z - 5f);
                holdPoint.localPosition += new Vector3(0.0025f, -0.0005f, 0);
            }

            if (allowRotationOnce)
            {
                IEnumerator coroutine = waitHalfASecond();
                StartCoroutine(coroutine);

                allowRotationOnce = false;
            }
        }
    }

    private IEnumerator waitHalfASecond()
    {
        yield return new WaitForSeconds(0.25f);

        rotationAllowed = false;
        allowRotationOnce = true;
    }

    private void HitEnemy(Transform hitBox)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitBox.position, MCAttackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().HitEnemy(MCdamage);
        }

        canAttack = false;
        IEnumerator coroutine = waitForAttack();
        StartCoroutine(coroutine);
    }

    private IEnumerator waitForAttack()
    {
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
    }

    private void Shoot()
    {
        if(MCdamage == 1.25f) //bow
        {
            Instantiate(rangedAmmo, shootPoint.position, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!rightHitBox || !leftHitBox || !upHitBox || !downHitBox) return;

        Gizmos.DrawWireSphere(rightHitBox.position, MCAttackRange);
        Gizmos.DrawWireSphere(leftHitBox.position, MCAttackRange);
        Gizmos.DrawWireSphere(upHitBox.position, MCAttackRange);
        Gizmos.DrawWireSphere(downHitBox.position, MCAttackRange);
    }
}
