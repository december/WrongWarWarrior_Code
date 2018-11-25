using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    private Animator anim;

    public float vel = 2F;
    public bool reversed = false;
    public bool stunned = false;

    public Vector2 CalcVel()
    {
        return (reversed?-vel:vel)*Vector2.left;
    }
	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (reversed)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.identity;
        if (!stunned)
        {
            if (reversed)
            {
                transform.position += Vector3.right * vel * Time.deltaTime;
                if (transform.position.x >= 10)
                    transform.position += Vector3.left * 20F;
            }
            else
            {
                transform.position += Vector3.left * vel * Time.deltaTime;
                if (transform.position.x <= -10)
                    transform.position += Vector3.right * 20F;
            }
        }
    }

    public void GetStepped(){
        anim.SetTrigger("Stepped");
        //stunned = true;
    }

}
