using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeMap : MonoBehaviour {
    public SuperTextMesh wacth;

	// Use this for initialization
	void Start ()
    {
        var a= Resources.Load<Player2D>("Model/Player");
        if (a)
        {
            Instantiate(a);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpWacth();

    }

    private void UpWacth()
    {
        wacth.text = DateTime.Now.ToShortTimeString();
    }
}
