using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour {

    GameObject obj;
    public bool leftward = true;
	// Use this for initialization
	void Start () {

        if (!leftward)
            transform.rotation = Quaternion.Euler(0F, 180F, 0F);
    }

	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Warrior" ||
            collision.gameObject.tag == "Boar" ||
            collision.gameObject.tag == "Bird")
        {
            obj = collision.gameObject;
            if (!obj.GetComponent<Reverser>())
            {
                var r = obj.AddComponent<Reverser>();
                r.leftward = leftward;
            }
            else
            {
                Destroy(obj.GetComponent<Reverser>());
                var r = obj.AddComponent<Reverser>();
                r.leftward = leftward;
            }
        }

    }
}
