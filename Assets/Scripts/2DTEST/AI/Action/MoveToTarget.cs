using BehaviorDesigner.Runtime;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskCategory("MyActions")]
    [TaskDescription("走向目标")]
    public class MoveToTarget : Action
    {
        CharacterController2D cct;
        public Animator anim;
        public SharedTransform body;

        SharedTransform target;
        Vector3 dir;
        public override void OnAwake()
        {
            base.OnAwake();
            cct = transform.GetComponent<CharacterController2D>();
            anim = body.Value.GetComponent<Animator>();
            target = this.Owner.GetVariable("target") as SharedTransform;
        }

        public override void OnStart()
        {
            base.OnStart();
            anim.SetFloat("move", 1);
        }

        public override void OnEnd()
        {
            base.OnEnd();
            anim.SetFloat("move", 0);
        }

        public override TaskStatus OnUpdate()
        {
            if (target.Value)
            {
                dir = target.Value.position - transform.position;
                if (dir.x > 0)
                {
                    body.Value.localEulerAngles = Vector2.zero;
                }
                else if (dir.x < 0)
                {
                    body.Value.localEulerAngles = new Vector3(0, 180, 0);
                }
                if (Mathf.Abs(dir.x) > 1.5f || Mathf.Abs(dir.z) > 0.25f)
                {
                    if (cct.Move(dir.normalized))
                    {
                        return TaskStatus.Running;
                    }
                }
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}