using BehaviorDesigner.Runtime;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskCategory("MyActions")]
    [TaskDescription("计算出更新的路径")]
    public class RandomPos : Action
    {

        RaycastHit hit;

        public override TaskStatus OnUpdate()
        {
            if (Physics.SphereCast(transform.position, 2, Vector3.right, out hit, 0, LayerMask.NameToLayer("Player")))
            {

                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}