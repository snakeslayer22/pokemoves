using System;
using System.Collections;
using UnityEngine;

public class MoveHeldItem : MonoBehaviour{

    public static bool idle = false;
    public static bool walking = false;

    private bool currentlyUp = true;

    private float isUp = 0;

    private Transform shootPoint;

    private void Start()
    {
        shootPoint = GameObject.Find("ShootPoint").transform;
    }

    public void stopAndStartCoroutine()
    {
        StopAllCoroutines();

        currentlyUp = true;

        IEnumerator idleCoroutine = Idle();
        IEnumerator walkingCoroutine = Walking();

        if (Attack.up || Attack.down) walking = false;

        if (idle) StartCoroutine(idleCoroutine);
        if (walking) StartCoroutine(walkingCoroutine);
    }

    private IEnumerator Idle()
    {
        while (true)
        {
            moveHeldItemUp();
            currentlyUp = true;
            yield return new WaitForSeconds(0.5f);

            moveHeldItemDown();
            currentlyUp = false;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator Walking()
    {
        while (true)
        {
            moveHeldItemDown();
            currentlyUp = false;
            yield return new WaitForSeconds(0.75f);

            moveHeldItemUp();
            currentlyUp = true;
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void moveHeldItemUp()
    {
        if (currentlyUp) isUp = 0;
        else isUp = 1;
        transform.localPosition += new Vector3(0, isUp * 0.01f, 0);

        currentlyUp = true;
    }

    private void moveHeldItemDown()
    {
        if (currentlyUp) isUp = 1;
        else isUp = 0;
        transform.localPosition += new Vector3(0, isUp * -0.01f, 0);

        currentlyUp = false;
    }

    public void movingRight()
    {
        transform.localPosition = new Vector3(0.06f, -0.05f, 0.5f);
        transform.localRotation = Quaternion.Euler(0, 0, 0);

        shootPoint.GetComponent<MoveShootPoint>().movingRight();
    }

    public void movingUp()
    {
        transform.localPosition = new Vector3(-0.005f, 0.17f, 6.5f);
        transform.localRotation = Quaternion.Euler(0, 0, 45);

        shootPoint.GetComponent<MoveShootPoint>().movingUp();
    }

    public void movingLeft()
    {
        transform.localPosition = new Vector3(-0.06f, -0.05f, 0.5f);
        transform.localRotation = Quaternion.Euler(0, 180, 0);

        shootPoint.GetComponent<MoveShootPoint>().movingLeft();
    }

    public void movingDown()
    {
        transform.localPosition = new Vector3(-0.02f, 0.03f, -0.5f);
        transform.localRotation = Quaternion.Euler(0, 180, -135);

        shootPoint.GetComponent<MoveShootPoint>().movingDown();
    }
}
