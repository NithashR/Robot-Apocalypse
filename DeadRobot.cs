using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadRobot : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<CapsuleCollider>().radius=0.05f;
        animator.gameObject.GetComponent<CapsuleCollider>().center= new Vector3 (0f, 1.2f, 0f);
        animator.gameObject.GetComponent<CapsuleCollider>().height=0.3f;
        //animator.gameObject.GetComponent<CapsuleCollider>().direction=2;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pointsmanager.totalp+=10;
        Destroy(animator.gameObject);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}



public class pointsmanager
{
    public static int totalp =0;
}
