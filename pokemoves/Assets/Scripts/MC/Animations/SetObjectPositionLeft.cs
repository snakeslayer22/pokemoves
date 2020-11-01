using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectPositionLeft : StateMachineBehaviour{

    Transform Arm;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Arm = GameObject.FindGameObjectWithTag("arm").transform;
        Arm.transform.localPosition = new Vector3(0.01f, 0.12f, -6);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Arm.transform.localPosition = new Vector3(0, 0, -6);
    }
}
