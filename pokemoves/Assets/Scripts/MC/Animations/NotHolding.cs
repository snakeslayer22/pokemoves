using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotHolding : StateMachineBehaviour{

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform arm;
        arm = GameObject.FindGameObjectWithTag("arm").transform;
        Transform holdPoint;
        holdPoint = arm.Find("HoldPoint");

        Attack.right = false;
        Attack.left = false;
        Attack.up = false;
        Attack.down = false;

        MoveHeldItem.walking = false;
        MoveHeldItem.idle = false;
        holdPoint.GetComponent<MoveHeldItem>().stopAndStartCoroutine();
    }
}
