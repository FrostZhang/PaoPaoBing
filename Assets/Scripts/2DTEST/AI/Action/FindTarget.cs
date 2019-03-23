using BehaviorDesigner.Runtime;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskCategory("MyActions")]
    [TaskDescription("找目标")]
    public class FindTarget : Action
    {
        public SharedTransform body;
        private RaycastHit hit;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(gameObject);
        }

        public override TaskStatus OnUpdate()
        {
            var cs = Physics.OverlapSphere(transform.position, 2, LayerMask.GetMask("Player"));
            if (cs.Length > 0)
            {
                Debug.Log(cs[0].gameObject);
                this.Owner.GetVariable("target").SetValue(cs[0].transform);
                return TaskStatus.Failure;
            }
            return TaskStatus.Success;
        }
    }
}