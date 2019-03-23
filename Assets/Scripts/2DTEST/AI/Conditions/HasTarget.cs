using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskCategory("MyConditionals")]
    [TaskDescription("是否有目标")]
    public class HasTarget : Conditional
    {
        private SharedTransform target;

        public override void OnStart()
        {
            base.OnStart();
            target = this.Owner.GetVariable("target") as SharedTransform;
        }

        public override TaskStatus OnUpdate()
        {
            if (target.Value != null)
            {
                return TaskStatus.Failure;
            }
            return TaskStatus.Success;
        }
    }
}