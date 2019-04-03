using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMap : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GameApp.ui.app.Open<SurfaceLoading>();
	}
}
