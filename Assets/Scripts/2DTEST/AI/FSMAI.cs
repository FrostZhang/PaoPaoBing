//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class FSMAI
    {
        public static LayerMask onlyPlayer = LayerMask.GetMask("Player");

        public static Transform FindTarget(Vector3 pos, float radii, ref LayerMask mask)
        {
            var cs = Physics.OverlapSphere(pos, radii, mask);
            if (cs.Length > 0)
            {
                return cs[0].transform;
            }
            return null;
        }

        public static Vector3 RandomPos(ref Vector3 strengh, float dis)
        {
            Vector3 direction = new Vector3(Random.Range(-strengh.x, strengh.x), 0f, Random.Range(-strengh.z, strengh.z));
            direction *= dis;
            return direction;
        }

    }

    public class FreeMove : FSM_State
    {
        CharacterController2D cct;
        Animator anim;
        private Transform body;
        float maxdis;
        Vector3 strengh;
        float maxtime;
        Role role;

        private Vector3 target;
        private float temp;
        public FreeMove(Vector3 strengh, float maxdis, float maxtime)
        {
            StateName = Define.FSMAI.FREEMOVE;
            this.maxdis = maxdis;
            this.strengh = strengh;
            this.maxtime = maxtime;
        }

        public override void Initialization()
        {
            cct = fSMController.variable[Define.FSMAI.SELF] as CharacterController2D;
            role = fSMController.variable[Define.FSMAI.SELF] as Role;
            anim = fSMController.variable[Define.FSMAI.ANIMATOR] as Animator;
            body = fSMController.variable[Define.FSMAI.BODY] as Transform;
        }

        public override void Exit()
        {
            anim.SetFloat("move", 0);
        }

        public override void Start()
        {
            anim.SetFloat("move", 1);
            target = FSMAI.RandomPos(ref strengh, maxdis);
            if (target.x > 0)
            {
                body.localEulerAngles = Vector2.zero;
            }
            else if (target.x < 0)
            {
                body.localEulerAngles = new Vector3(0, 180, 0);
            }
            target += transform.position;
            temp = maxtime;
        }

        public override void Update()
        {
            if ((temp -= Time.deltaTime) < 0)
            {
                fSMController.RunState(Define.FSMAI.IDLE);
                return;
            }

            Vector3 dir = target - transform.position;
            if (dir.sqrMagnitude > 0.01)
            {
                if (cct.Move(dir.normalized * role.data.moveSpeed))
                {

                }
                else
                {
                    fSMController.RunState(Define.FSMAI.IDLE);
                }
            }
            else
            {
                fSMController.RunState(Define.FSMAI.IDLE);
            }
        }
    }

    public class FindTarget : FSM_State
    {
        float min;
        float max;

        float temp;
        float radii;
        Component target;
        public FindTarget(float max, float min, float radii)
        {
            this.max = Mathf.Max(max, min);
            this.min = Mathf.Min(max, min);
            this.radii = radii;

            StateName = Define.FSMAI.FINDTARGET;
        }

        public override void Exit()
        {

        }

        public override void Start()
        {
            temp = Random.Range(min, max);
        }

        public override void Update()
        {
            if ((temp -= Time.deltaTime) > 0)
            {
                return;
            }
            target = fSMController.variable[Define.FSMAI.TARGET];
            if (!target)
            {
                target = FSMAI.FindTarget(transform.position, radii, ref FSMAI.onlyPlayer);
            }
            if (target)
            {
                fSMController.variable[Define.FSMAI.TARGET] = target;
                fSMController.onFsmEvent.Invoke(this, target);
                fSMController.RunState(Define.FSMAI.MOVETOTARGET);
                fSMController.Monitor(Define.FSMAI.LOSTTARGET);
            }
            else
            {
                fSMController.Monitor(this);
            }
        }
    }

    public class LostTarget : FSM_State
    {
        float min;
        float max;

        float temp;
        float lostDis;

        Transform target;
        public LostTarget(float max, float min, float lostDis)
        {
            this.min = Mathf.Min(max, min);
            this.max = Mathf.Max(max, min);
            this.lostDis = lostDis;
            StateName = Define.FSMAI.LOSTTARGET;
        }

        public override void Exit()
        {

        }

        public override void Start()
        {
            temp = Random.Range(min, max);
            if (fSMController.variable.ContainsKey(Define.FSMAI.TARGET))
            {

                target = fSMController.variable[Define.FSMAI.TARGET] as Transform;
            }
        }

        public override void Update()
        {
            if ((temp-=Time.deltaTime)>0)
            {
                return;
            }
            float dir = transform.position.x - target.position.x;
            if (Mathf.Abs(dir) > lostDis)
            {
                fSMController.variable[Define.FSMAI.TARGET] = null;
                fSMController.Monitor(Define.FSMAI.FINDTARGET);
                fSMController.RunState(Define.FSMAI.IDLE);
            }
            else
            {
                fSMController.Monitor(this);
            }
        }
    }

    public class Idel : FSM_State
    {
        float max;
        float min;

        float temp;
        public Idel(float max, float min)
        {
            this.max = Mathf.Max(max, min);
            this.min = Mathf.Min(max, min);

            StateName = Define.FSMAI.IDLE;
        }

        public override void Exit()
        {

        }

        public override void Start()
        {
            temp = Random.Range(min, max);
        }

        Component target;
        public override void Update()
        {
            if ((temp -= Time.deltaTime) < 0)
            {

                fSMController.variable.TryGetValue(Define.FSMAI.TARGET, out target);
                if (target)
                    fSMController.RunState(Define.FSMAI.ATTACK);
                else
                    fSMController.RunState(Define.FSMAI.FREEMOVE);
            }
        }
    }

    //近程攻击
    public class ShortRangeAttack : FSM_State
    {
        Transform target;
        Vector2 dis;
        Transform body;
        public ShortRangeAttack(Vector2 dis)
        {
            this.dis = dis;
            StateName = Define.FSMAI.ATTACK;
        }

        public override void Initialization()
        {
            body = fSMController.variable[Define.FSMAI.BODY] as Transform;
        }

        public override void Exit()
        {

        }

        public override void Start()
        {
            target = fSMController.variable[Define.FSMAI.TARGET] as Transform;
        }

        public override void Update()
        {
            Vector3 dir = target.position - transform.position;
            if (Mathf.Abs(dir.x) <= dis.x && Mathf.Abs(dir.z) <= dis.y)
            {
                if (dir.x > 0)
                {
                    body.localEulerAngles = Vector2.zero;
                }
                else if (dir.x < 0)
                {
                    body.localEulerAngles = new Vector3(0, 180, 0);
                }
                fSMController.onFsmEvent(this, null);
                fSMController.RunState(Define.FSMAI.IDLE);
            }
            else
            {
                fSMController.RunState(Define.FSMAI.MOVETOTARGET);
            }
        }
    }

    public class MoveToTarget : FSM_State
    {
        Transform target;
        float maxtime;  //限定时间，防止一直追随玩家，或卡死原地踏步
        float temp;
        CharacterController2D cct;
        Animator anim;
        Transform body;
        Vector3 near;
        Role role;
        public MoveToTarget(float maxtime, Vector2 near)
        {
            this.maxtime = maxtime;
            StateName = Define.FSMAI.MOVETOTARGET;
            this.near = near;
        }

        public override void Initialization()
        {
            cct = fSMController.variable[Define.FSMAI.SELF] as CharacterController2D;
            role = fSMController.variable[Define.FSMAI.SELF] as Role;
            anim = fSMController.variable[Define.FSMAI.ANIMATOR] as Animator;
            body = fSMController.variable[Define.FSMAI.BODY] as Transform;
        }

        public override void Exit()
        {
            anim.SetFloat("move", 0);
        }

        public override void Start()
        {
            Component compent;
            fSMController.variable.TryGetValue(Define.FSMAI.TARGET, out compent);
            target = compent as Transform;
            temp = this.maxtime;
            anim.SetFloat("move", 1);
        }

        public override void Update()
        {
            if ((temp -= Time.deltaTime) < 0)
            {
                fSMController.RunState(Define.FSMAI.IDLE);
                return;
            }
            if (target)
            {
                Vector3 dir = target.position - transform.position;
                if (dir.x > 0)
                {
                    body.localEulerAngles = Vector2.zero;
                }
                else if (dir.x < 0)
                {
                    body.localEulerAngles = new Vector3(0, 180, 0);
                }
                if (Mathf.Abs(dir.x) > near.x || Mathf.Abs(dir.z) > near.y)
                {
                    if (cct.Move(dir.normalized * role.data.moveSpeed))
                    {

                    }
                    else
                    {
                        fSMController.RunState(Define.FSMAI.IDLE);
                    }
                }
                else
                {
                    fSMController.RunState(Define.FSMAI.ATTACK);
                }
            }
        }
    }
}