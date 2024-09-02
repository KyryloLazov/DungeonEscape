using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaggerEnemy : StateMachineBehaviour
{

    private Moss_Giant giant;
    private Skeleton skeleton;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        giant = animator.transform.parent.GetComponent<Moss_Giant>();
        skeleton = animator.transform.parent.GetComponent<Skeleton>();

        if(giant != null)
        {
            giant.isStagger = true;
        }

        else if (skeleton != null)
        {
            skeleton.isStagger = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (giant != null)
        {
            giant.isStagger = false;
            giant.poise = giant.Maxpoise;
        }

        else if (skeleton != null)
        {
            skeleton.isStagger = false;
            skeleton.poise = skeleton.Maxpoise;
        }
    }
}
