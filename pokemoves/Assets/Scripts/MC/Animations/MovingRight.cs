﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRight : StateMachineBehaviour{

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform arm;
        arm = GameObject.FindGameObjectWithTag("arm").transform;
        Transform holdPoint;
        holdPoint = arm.Find("HoldPoint");

        Attack.right = true;
        Attack.left = false;
        Attack.up = false;
        Attack.down = false;

        holdPoint.GetComponent<MoveHeldItem>().movingRight();
    }
}
