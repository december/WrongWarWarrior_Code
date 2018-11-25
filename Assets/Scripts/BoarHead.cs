using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarHead : MonoBehaviour {

    Boar boar;
	// Use this for initialization
	void Start () {
        boar = transform.parent.gameObject.GetComponent<Boar>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        boar.Bump(collision.gameObject);
        gameObject.transform.parent.GetComponent<Boar>().Collision();
    }
}
