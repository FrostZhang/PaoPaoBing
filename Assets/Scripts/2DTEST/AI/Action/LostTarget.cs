using BehaviorDesigner.Runtime;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskCategory("MyActions")]
    [TaskDescription("丢失目标")]
    public class LostTarget : Action
    {
        private SharedTransform target;

        public override void OnAwake()
        {
            base.OnAwake();
            target = this.Owner.GetVariable("target") as SharedTransform;

        }

        public override TaskStatus OnUpdate()
        {
            if (target.Value)
            {
                Vector3 dir = transform.position - target.Value.position;
                if (Mathf.Abs(dir.x) > 3.5f)
                {
                    target.Value = null;
                    return TaskStatus.Failure;
                }
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}