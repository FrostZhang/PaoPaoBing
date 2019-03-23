using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class FightState : StateMachineBehaviour
{
    bool isfight;
    IAnimaEvent pa;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (pa == null)
        {
            pa = animator.transform.parent.GetComponent<IAnimaEvent>();
        }
        //animator.SetBool(Define.Anim.CANMOVE, false);
        pa.StateEvent(Define.Anim.CANMOVE, false);
        isfight = false;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        //animator.SetBool(Define.Anim.CANMOVE, true);
        animator.SetBool(Define.Anim.FIGHT, false);
        pa.StateEvent(Define.Anim.CANMOVE, true);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (stateInfo.normalizedTime > 0.5f && isfight == false)
        {
            pa.StateEvent(Define.Anim.FIGHT,true);
            isfight = true;
        }
    }
}
