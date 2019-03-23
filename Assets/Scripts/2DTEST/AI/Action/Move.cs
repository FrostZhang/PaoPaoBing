using BehaviorDesigner.Runtime;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskCategory("MyActions")]
    [TaskDescription("自由走动")]
    public class Move : Action
    {
        CharacterController2D cct;
        public Animator anim;
        public SharedTransform body;
        Vector3 targetpos;

        [Tooltip("Random距离")]
        public float maxdis;

        [Tooltip("存放目标地的变量")]
        public SharedVector3 destination;

        public override void OnAwake()
        {
            base.OnAwake();
            cct = transform.GetComponent<CharacterController2D>();
            anim = body.Value.GetComponent<Animator>();
        }

        public override void OnBehaviorComplete()
        {
            base.OnBehaviorComplete();
        }

        private void RandomPos()
        {
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-0.3f, 0.3f));
            direction *= maxdis;
            targetpos = transform.position + direction;

            if (direction.x > 0)
            {
                body.Value.localEulerAngles = Vector2.zero;
            }
            else if (direction.x < 0)
            {
                body.Value.localEulerAngles = new Vector3(0, 180, 0);
            }
        }

        public override void OnStart()
        {
            base.OnStart();
            RandomPos();
            anim.SetFloat("move", 1);
        }

        public override void OnEnd()
        {
            base.OnEnd();
            anim.SetFloat("move", 0);
        }

        public override TaskStatus OnUpdate()
        {
            Vector3 dir = targetpos - transform.position;
            if (dir.sqrMagnitude > 0.01)
            {
                if (cct.Move(dir.normalized))
                {
                    return TaskStatus.Running;
                }
            }
            return TaskStatus.Success;
        }
    }
}