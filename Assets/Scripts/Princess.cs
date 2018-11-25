using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Princess : MonoBehaviour {

    private Animator anim;
    private int randomTime;
    private float lastTime;
    public enum State {Sleep, Shout, Saved};
    public State state = State.Sleep;
    bool saved = false;
    GameObject cam;
    public GameObject fin,finObj;
    // Use this for initialization
    void Start () {
        randomTime = Random.Range(8, 15);
        lastTime = Time.fixedTime;
        anim = GetComponentInChildren<Animator>();
    }

    IEnumerator Return()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(1);
    }

	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case State.Sleep:
                float tempTime = Time.fixedTime;
                if (tempTime - lastTime >= randomTime)
                {
                    lastTime = tempTime;
                    randomTime = Random.Range(8, 15);
                    anim.SetTrigger("WantToShout");
                }
                break;
            case State.Shout:
                break;
            case State.Saved:
                break;
        }

        if (saved)
        {
            cam.transform.position = GameObject.Find("Warrior").transform.position + new Vector3(0F, 0.6F, -10F);
            cam.GetComponent<Camera>().orthographicSize = 2F;
            finObj.transform.position = GameObject.Find("Warrior").transform.position + new Vector3(0F, 1.6F);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Warrior")
            GetSaved();
    }

    public void GetSaved(){
        if (saved) return;
        cam = GameObject.Find("Main Camera");
        state = State.Saved;
        anim.SetTrigger("Saved");
        saved = true;
        Warrior wr = GameObject.Find("Warrior").GetComponent<Warrior>();
        wr.FindPrincess();
        StartCoroutine(Return());
        finObj = GameObject.Instantiate(fin, GameObject.Find("Warrior").transform.position + new Vector3(0F, 1.6F), Quaternion.identity);
    }
}
