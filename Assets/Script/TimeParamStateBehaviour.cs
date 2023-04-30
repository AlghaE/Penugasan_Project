using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeParamStateBehaviour : StateMachineBehaviour
{
    public string ParamName;
    public bool SetDefaultParamName;

    public float Start, End;

    private bool _waitForExit;
    private bool _onTransitionExitTriggered;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _waitForExit = false;
        _onTransitionExitTriggered = false;
        animator.SetBool(ParamName, !SetDefaultParamName);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (CheckOnTransitionExit(animator, layerIndex))
        {
            OnStatetransitionExit(animator);
        }
        if(!_onTransitionExitTriggered && stateInfo.normalizedTime>=Start && stateInfo.normalizedTime <= End)
        {
            animator.SetBool(ParamName, SetDefaultParamName);
        }
    }
    private void OnStatetransitionExit(Animator animator)
    {
        animator.SetBool(ParamName, !SetDefaultParamName);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_onTransitionExitTriggered)
        {
            animator.SetBool(ParamName, !SetDefaultParamName);
        }
        
    }
    private bool CheckOnTransitionExit(Animator animator , int layerIndex)
    {
        if(!_waitForExit && animator.GetNextAnimatorStateInfo(layerIndex).fullPathHash == 0)
        {
            _waitForExit = true;
        }
        if(!_onTransitionExitTriggered && _waitForExit && animator.IsInTransition(layerIndex))
        {
            _onTransitionExitTriggered = true;
            return true;
        }
        return false;
    }

    
}
