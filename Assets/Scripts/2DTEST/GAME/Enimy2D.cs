using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class Enimy2D : CharacterController2D, IHurt, IMapitem, IAnimaEvent
{
    public RuntimeAnimatorController runanim;
    public bool Canmove { get; protected set; }
    public Map.PlaceData mapdata { get; set; }

    public float freemoveDis = 3;
    public float freemovemaxtTime = 3;
    public float findInterval = 0.5f;
    public float finsSqr = 2.5f;
    public float lostMinT = 1.5f;
    public float lostMaxT = 2;
    public float lostDis = 4;
    public float idemin = 1;
    public float idemax = 3;
    public Vector2 atcRange = new Vector2(1.5f, 0.25f);
    public float moveTotargetMacTime = 3f;

    protected FSMController Fsm { get; set; }
    private Animator anim;
    private Transform body;

    protected override void Awake()
    {
        base.Awake();
        body = tr.GetChild(0);
        anim = body.GetComponent<Animator>();
        anim.runtimeAnimatorController = runanim;
    }

    protected void Start()
    {

    }

    public void IniData(RoleData data, RuntimeAnimatorController rac)
    {
        this.data = data;
        anim.runtimeAnimatorController = rac;

        BuildHub(); ShowHP(false);
        FsmIni();
        Canmove = true;
    }

    public void FsmIni()
    {
        if (Fsm == null)
        {
            Fsm = new FSMController(gameObject, transform);
            Fsm.onFsmEvent += OnFSMEvent;
            FreeMove freemove = new FreeMove(new Vector3(1, 0, 0.5f), freemoveDis, freemovemaxtTime);
            FindTarget findTarget = new FindTarget(findInterval, finsSqr);
            LostTarget lost = new LostTarget(lostMinT, lostMaxT, lostDis);
            Idel idel = new Idel(idemin, idemax);
            ShortRangeAttack att = new ShortRangeAttack(atcRange);
            MoveToTarget moveTo = new MoveToTarget(moveTotargetMacTime, atcRange);
            Fsm.AddState(idel);
            Fsm.AddState(freemove);
            Fsm.AddState(findTarget);
            Fsm.AddState(lost);
            Fsm.AddState(att);
            Fsm.AddState(moveTo);
            Fsm.Monitor(findTarget);
            Fsm.variable.Add(Define.FSMAI.TARGET, null);
            Fsm.variable.Add(Define.FSMAI.BODY, body);
            Fsm.variable.Add(Define.FSMAI.ANIMATOR, anim);
            Fsm.variable.Add(Define.FSMAI.SELF, this);
            Fsm.Initialization();
        }
        else
        {
            Fsm.variable[Define.FSMAI.TARGET] = null;
            Fsm.RunState(Define.FSMAI.IDLE);
        }
    }

    private void OnFSMEvent(FSM_State state, Component value)
    {
        switch (state.StateName)
        {
            case Define.FSMAI.ATTACK:
                anim.SetBool("fight", true);
                GameApp.gameTimer.Delay(() =>
                {
                    anim.SetBool("fight", false);
                }, 0.1f);
                break;
            default:
                break;
        }
    }

    void Update()
    {
        body.localPosition = new Vector3(0, tr.position.y + tr.position.z * 0.5f, 0);
        if (Canmove)
        {
            Fsm.UpdateState();
        }
    }

    public void StateEvent(string statename, bool state)
    {
        switch (statename)
        {
            case Define.Anim.FIGHT:
                var hits = Physics.SphereCastAll(transform.position, 1, body.rotation * Vector2.right, 0.5f, 1 << LayerMask.NameToLayer("Player"));
                //Debug.Log(body.rotation * Vector2.right);
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].transform != tr)
                    {
                        IHurt ih = hits[i].transform.GetComponent<IHurt>();
                        if (ih != null)
                            ih.Hurt(tr, data);
                        Debugger.Game.Log(hits[i].transform.name);
                    }
                }
                break;
            case Define.Anim.CANMOVE:
                Canmove = state;
                break;
        }
    }

    public void Hurt(Transform att, RoleData targetdata)
    {
        if ((data.hp -= targetdata.atk) > 0)
        {
            anim.SetTrigger("hit");
            ShowHpChange(targetdata.atk);
            if (!Fsm.variable[Define.FSMAI.TARGET])
            {
                Fsm.variable[Define.FSMAI.TARGET] = att;
                OnEnimyChange?.Invoke(this, new EnimyArg() { isdie = true });
            }
        }
        else
        {
            anim.StopPlayback();
            Canmove = false;
        }
    }

    public delegate void Enimy2DHandel(Enimy2D sender, EnimyArg e);
    public event Enimy2DHandel OnEnimyChange;

    public class EnimyArg : EventArgs
    {
        public bool isdie;
    }
}