using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : MonoBehaviour {
    private Animator anim;

    WarriorFoot Foot;
    public Vector2 vel = Vector2.zero;
    public enum State { Sleep, Move, Stun };
    public State state = State.Sleep;
    public float stunTime = 0F;
    public bool reversed = false;

    public Vector2 CalcVel()
    {
        Vector2 rideVel = Vector2.zero;
        if (Foot.StepOn)
        {
            if (Foot.StepOn.tag == "Boar")
            {
                rideVel = Foot.StepOn.GetComponent<Boar>().CalcVel();
            }
        }
        return vel + rideVel;
    }

    Vector2 BumpVec()
    {
        return reversed ? new Vector2(2.5F, 4F) : new Vector2(-2.5F, 4F);
    }
    Vector2 RebVec()
    {
        return reversed ? new Vector2(-2.5F, 4F) : new Vector2(2.5F, 4F);
    }

    float lastBumpTime = -1000F;
    public void Bump(GameObject obj)
    {
        if (Time.time < lastBumpTime + 0.3F)
            return;
        if (state == State.Sleep)
            return;
            
        Debug.Log("Bump!" + gameObject.name + " " + obj.name);
        switch (obj.tag)
        {
            case "Warrior":

                obj.GetComponent<Warrior>().stunTime = 0.7F;
                obj.GetComponent<Warrior>().vel = BumpVec();

                stunTime = 1F;
                state = State.Stun;
                vel = RebVec();
                break;
            case "Boar":
                obj.GetComponent<Boar>().stunTime = 1F;
                obj.GetComponent<Boar>().state = State.Stun;
                obj.GetComponent<Boar>().vel = BumpVec();

                stunTime = 1F;
                state = State.Stun;
                vel = RebVec();
                break;
            case "Ground":
                if (!Foot.StepOn)
                    return;
                if (state != State.Move)
                    return;
                anim.SetTrigger("Sleeping");
                state = State.Sleep;
                vel = Vector2.zero;
                break;
        }


        lastBumpTime = Time.time;


    }

    void Move()
    {
        if (Foot.StepOn)
        {
            anim.SetBool("Landing", true);
            if (vel.y<0)
                vel.y = 0F;
        }
        else
            vel.y -= 8F * Time.deltaTime;

        if (stunTime > 0F)
            stunTime -= Time.deltaTime;
        switch (state)
        {
            case State.Sleep:
                anim.SetTrigger("Sleeping");
                break;
            case State.Move:
                vel.x = reversed ? 2.5F : -2.5F;
                break;
            case State.Stun:
                if (stunTime <= 0F)
                    state = State.Move;
                break;
        }
        GetComponent<Rigidbody2D>().velocity = CalcVel();
        if (Foot.StepOn && Foot.StepOn.tag == "Boar")
        {
            state = State.Sleep;
            anim.SetTrigger("Sleeping");
            vel = Vector2.zero;
            Vector3 dPos = Foot.StepOn.transform.position + Vector3.up * 0.6F - transform.position;
            if (dPos.magnitude > 0.01F)
                dPos -= 5F * Time.deltaTime * dPos;
            transform.position = Foot.StepOn.transform.position + Vector3.up * 0.6F - dPos ;
        }

    }

	// Use this for initialization
	void Start ()
    {
        Foot = GetComponentInChildren<WarriorFoot>();
        anim = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (state == State.Sleep)
        {
            if (!(Foot.StepOn && Foot.StepOn.tag == "Boar"))
            {
                RaycastHit2D s = Physics2D.Raycast(transform.position + new Vector3(-1F, -0.1F), Vector3.left, 2.4F);
                if (s && s.transform.gameObject.tag == "Warrior")
                {
                    //state = State.Move;
                    anim.SetTrigger("SeeWarrior");
                    anim.SetBool("Landing", false);
                }
            }
        }
        Move();

        if (reversed)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.identity;

    }

    float lastCollisionTime = -1000F;
    public void Collision() {
        if (Time.time < lastCollisionTime + 0.5F)
            return;
        anim.SetTrigger("Collision");
        lastCollisionTime = Time.time;
    }

    public void BeginDashing(){
        state = State.Move;
    }
}
