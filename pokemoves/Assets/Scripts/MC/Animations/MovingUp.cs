﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUp : StateMachineBehaviour{

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform arm;
        arm = GameObject.FindGameObjectWithTag("arm").transform;
        Transform holdPoint;
        holdPoint = arm.Find("HoldPoint");

        Attack.right = false;
        Attack.left = false;
        Attack.up = true;
        Attack.down = false;

        holdPoint.GetComponent<MoveHeldItem>().movingUp();
    }
}
