using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceContainer : MonoBehaviour {

    public Transform tr;
    public GameObject go;

    public List<SuefaceChild> suefaces;

    private void Awake()
    {
        tr = transform;
        go = gameObject;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}