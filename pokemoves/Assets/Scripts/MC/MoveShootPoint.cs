using System;
using System.Collections;
using UnityEngine;

public class MoveShootPoint : MonoBehaviour{

    public void movingRight()
    {
        transform.localPosition = new Vector3(0.15f, -0.05f, 0.5f);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void movingLeft()
    {
        transform.localPosition = new Vector3(-0.15f, -0.05f, 0.5f);
        transform.localRotation = Quaternion.Euler(0, 0, 180);
    }

    public void movingUp()
    {
        transform.localPosition = new Vector3(0, 0.35f, 0.5f);
        transform.localRotation = Quaternion.Euler(0, 0, 90);
    }

    public void movingDown()
    {
        transform.localPosition = new Vector3(0, -0.3f, 0.5f);
        transform.localRotation = Quaternion.Euler(0, 0, -90);
    }
}
