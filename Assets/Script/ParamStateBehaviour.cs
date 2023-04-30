using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamStateBehaviour : StateMachineBehaviour
{
    public SetParamData[] ParamStateData;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach(SetParamData data in ParamStateData)
        {
            animator.SetBool(data.ParamName, data.SetDefaultState);
        }
    }
    [Serializable]
    public struct SetParamData
    {
        public string ParamName;
        public bool SetDefaultState;
    }

}
