using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : CharacterController2D, IAnimaEvent, IHurt
{
    private Transform body;
    public float movespeed = 3;
    private Animator anim;
    protected bool canmove;

    protected void Start()
    {
        body = tr.GetChild(0);
        anim = body.GetComponent<Animator>();
        canmove = true;
        data = new RoleData() { hp = 100, atk = 2, def = 3, fightSpeed = 1 };
    }

    RaycastHit raycastHit;
    void Update()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") * 0.5f);
        if (canmove)
        {
            this.Move(ref dir, ref movespeed);

            if (dir.x > 0)
            {
                body.localEulerAngles = Vector2.zero;
            }
            else if (dir.x < 0)
            {
                body.localEulerAngles = new Vector3(0, 180, 0);
            }
            anim.SetFloat(Define.Anim.MOVE, dir.magnitude);
        }
        else
        {
            dir.y = -1;
        }
        if (Input.GetAxis("Fire1") > 0)
        {
            anim.SetBool("fight", true);
        }
        if (Input.GetAxis("Fire2") > 0)
        {
            anim.SetTrigger("hit");
        }
        body.localPosition = new Vector3(0, tr.position.y + tr.position.z * 0.5f, 0);
    }

    public void StateEvent(string statename, bool state)
    {
        switch (statename)
        {
            case Define.Anim.FIGHT:
                var hits = Physics.SphereCastAll(transform.position, 1, body.rotation * Vector2.right, 0.5f, 1 << LayerMask.NameToLayer("Enimy"));
                //Debug.Log(body.rotation * Vector2.right);
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].transform != tr)
                    {
                        IHurt ih = hits[i].transform.GetComponent<IHurt>();
                        if (ih != null)
                            ih.Hurt(tr, data);
                        Debugger.Game.Log("Palyer 攻击" + hits[i].transform.name);
                    }
                }
                break;
            case Define.Anim.CANMOVE:
                canmove = state;
                break;
        }
    }

    public void Hurt(Transform target, RoleData targetdata)
    {
        if ((data.hp -= targetdata.atk) > 0)
        {
            anim.SetTrigger(Define.Anim.HIT);
            ShowHpChange(targetdata.atk);
        }
    }
}