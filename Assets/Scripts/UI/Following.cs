using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : MonoBehaviour {

    public Vector3 vec;
    GameObject w;
	// Use this for initialization
	void Start () {
        w = GameObject.Find("Warrior");
        Destroy(gameObject, 2.4F);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = w.transform.position + vec;
	}
}
