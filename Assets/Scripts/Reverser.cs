using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverser : MonoBehaviour {

    public enum State {First, Second, Third };
    float alpha = 1F, sTime = 0.2F;
    State state = State.First;
    Vector3 startPos;
    public bool leftward = true;
    void Reverse()
    {
        if (gameObject.tag == "Warrior")
        {
            GetComponent<Warrior>().reversed = !GetComponent<Warrior>().reversed;
            if(GetComponent<Warrior>().reversed)
                GameObject.Instantiate(GetComponent<Warrior>().note2, new Vector3(100, 0), Quaternion.identity);
            else
                GameObject.Instantiate(GetComponent<Warrior>().note3, new Vector3(100, 0), Quaternion.identity);
        }
        if (gameObject.tag == "Boar")
            GetComponent<Boar>().reversed = !GetComponent<Boar>().reversed;
        if (gameObject.tag == "Bird")
            GetComponent<Bird>().reversed = !GetComponent<Bird>().reversed;
    }

    // Use this for initialization
    void Start () {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        switch (state)
        {
            case State.First:
                alpha -= 3F * Time.deltaTime;
                GetComponent<SpriteRenderer>().color = new Color(1F, 1F, 1F, alpha);
                if (alpha <= 0F)
                {
                    state = State.Second;
                    Reverse();
                }
                break;
            case State.Second:
                sTime -= Time.deltaTime;
                if (sTime <= 0F)
                {
                    state = State.Third;
                    gameObject.transform.position = startPos + (leftward ? 0.1F : -0.1F) * Vector3.left;
                }
                break;
            case State.Third:
                alpha += 3F * Time.deltaTime;
                GetComponent<SpriteRenderer>().color = new Color(1F, 1F, 1F, alpha);
                if (alpha >= 1F)
                    Destroy(this);
                break;

        }
    }
}
