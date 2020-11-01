using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLeft : StateMachineBehaviour{

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform arm;
        arm = GameObject.FindGameObjectWithTag("arm").transform;
        Transform holdPoint;
        holdPoint = arm.Find("HoldPoint");

        Attack.right = false;
        Attack.left = true;
        Attack.up = false;
        Attack.down = false;

        holdPoint.GetComponent<MoveHeldItem>().movingLeft();
    }
}
