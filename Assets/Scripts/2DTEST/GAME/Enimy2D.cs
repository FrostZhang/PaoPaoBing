using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class Enimy2D : CharacterController2D, IHurt, IFSM, IAnimaEvent
{
    private Animator anim;
    private Transform body;

    public FSMController Fsm { get; set; }

    public RuntimeAnimatorController[] anims;

    public bool Canmove { get; protected set; }

    public Map.PlaceData placeData;

    protected override void Awake()
    {
        base.Awake();
        body = tr.GetChild(0);
        anim = body.GetComponent<Animator>();
    }

    protected void Start()
    {

    }

    public void IniData(RoleData data, RuntimeAnimatorController rac)
    {
        this.data = data;
        anim.runtimeAnimatorController = rac;

        FsmIni();
        GameApp.gameTimer.Delay(() => { BuildHub(); }, 0.5f);
        Canmove = true;
    }

    public void FsmIni()
    {
        Fsm = new FSMController(gameObject, transform);
        Fsm.onFsmEvent += OnFSMEvent;
        FreeMove freemove = new FreeMove(new Vector3(1, 0, 0.5f), 3f, 3f);
        FindTarget findTarget = new FindTarget(0.5f, 1.5f, 2.5f);
        LostTarget lost = new LostTarget(1.5f, 2, 4f);
        Idel idel = new Idel(1f, 3f);
        Vector2 v2 = new Vector2(1.5f, 0.25f);
        ShortRangeAttack att = new ShortRangeAttack(v2);
        MoveToTarget moveTo = new MoveToTarget(3f, v2);
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
                        Debug.Log(hits[i].transform.name);
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
        }
        if (!Fsm.variable[Define.FSMAI.TARGET])
        {
            Fsm.variable[Define.FSMAI.TARGET] = att;
        }
    }
}