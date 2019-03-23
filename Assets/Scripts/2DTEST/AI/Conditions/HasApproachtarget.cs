using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskCategory("MyActions")]
    [TaskDescription("是否有目标")]
    public class HasApproachtarget : Conditional
    {
        private SharedTransform target;

        public override void OnStart()
        {
            base.OnStart();
            target = this.Owner.GetVariable("target") as SharedTransform;
        }

        public override TaskStatus OnUpdate()
        {
            Vector3 dir = target.Value.position - transform.position;
            if (Mathf.Abs(dir.x) > 1.5f || Mathf.Abs(dir.z) > 0.25f)
            {
                return TaskStatus.Failure;
            }
            return TaskStatus.Success;
        }
    }
}
