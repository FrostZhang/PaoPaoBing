using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurfaceMenu : SurfaceChild {
    public Button bagbtn;

	// Use this for initialization
	void Start ()
    {
        bagbtn.onClick.AddListener(() =>{
            GameApp.ui.game.Open<SurfaceBag>();
        });
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
