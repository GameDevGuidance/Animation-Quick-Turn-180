using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBusy : StateMachineBehaviour
{
    PlayerController player_controller;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(player_controller == null)
        {
            player_controller = animator.transform.GetComponent<PlayerController>();
        }

        player_controller.animation_busy = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float current_time = stateInfo.normalizedTime * stateInfo.length;
        if(current_time > .5f && animator.IsInTransition(0))
        {
            animator.rootRotation = player_controller.desired_rotation;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player_controller == null)
        {
            player_controller = animator.transform.GetComponent<PlayerController>();
        }

        player_controller.animation_busy = false;
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
