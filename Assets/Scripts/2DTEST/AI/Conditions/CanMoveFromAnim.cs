using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskCategory("MyConditionals")]
    [TaskDescription("动画是否禁止人物移动")]
    public class CanMoveFromAnim : Conditional
    {
        public SharedTransform body;
        Animator anim;
        public override void OnAwake()
        {
            base.OnAwake();
            anim = body.Value.GetComponent<Animator>();
        }
        public override TaskStatus OnUpdate()
        {
            if (anim.GetBool("canmove"))
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}