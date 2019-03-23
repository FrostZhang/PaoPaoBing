using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Hitstate : StateMachineBehaviour
{
    IAnimaEvent pa;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (pa == null)
        {
            pa = animator.transform.parent.GetComponent<IAnimaEvent>();
        }
        pa.StateEvent(Define.Anim.CANMOVE, false);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        pa.StateEvent(Define.Anim.CANMOVE, true);
    }
}
