using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//行走
[RequireComponent(typeof(Rigidbody))]
public class CharacterController2D : Role
{
    private Rigidbody rb;
    private RaycastHit raycastHit;
    public Transform tr { get; protected set; }

    private void Awake()
    {
        tr = transform;
        rb = tr.GetComponent<Rigidbody>();
    }

    public void Move(ref Vector3 dir, ref float movespeed)
    {
        Debug.DrawRay(tr.position, dir, Color.red, 0.1f);
        if (!rb.SweepTest(dir, out raycastHit, 0.1f, QueryTriggerInteraction.Ignore))
        {
            rb.MovePosition(tr.position + dir * movespeed * Time.deltaTime);
        }
        else
        {
            Debug.Log(raycastHit.transform.name);
        }
    }

    public bool Move(Vector3 motion)
    {
        Debug.DrawRay(tr.position, motion, Color.red, 0.1f);
        if (!rb.SweepTest(motion, out raycastHit, 0.1f, QueryTriggerInteraction.Ignore))
        {
            rb.MovePosition(tr.position + motion * Time.deltaTime);
            return true;
        }
        return false;
    }
}
