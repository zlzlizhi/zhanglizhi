using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diss : MonoBehaviour {

    private Rigidbody rbody;
    private Animation ani;
    private Transform ta;
    public static Diss diss;

    public Transform pos;
   // public float dissDistance = 5;
   // public float dieTime = 30f;
   // private Animation tagt;
    // Use this for initialization
    void Start()
    {
        diss = this;
        rbody = GetComponent<Rigidbody>();
        ani = GetComponent<Animation>();
        // ta = GameObject.FindWithTag("Player").transform;
       // ta = player._instance.transform;
      //  tagt = GameObject.FindWithTag("Player").animation;
    }

    // Update is called once per frame
    void Update()
    {
        ta = player._instance.transform;

        if (ta == null)
        {
            return;
        }

        float dis = Vector3.Distance(transform.position, ta.position);

        if (dis < 2f)
        {
            if (!ta.GetComponent<player>().isDie)//判断isdie的bool值；
            {
                ta.GetComponent<player>().Playdie();
            }
            ani.Play("idle");
        }
        else if (dis < 6f)
        {
            //先朝向玩家
            transform.LookAt(ta.position);
            //再追击玩家
            rbody.velocity = transform.forward * 2;
            ani.Play("move");
        }
        else
        {
            ani.Play("idle");
        }
    }

    public void dissdie()
    {
        gameObject.SetActive(false);
        Invoke("dissfuhuo", 30f);
    }

    public void dissfuhuo()
    {
        gameObject.transform.position = pos.position;//复活点
        gameObject.SetActive(true);       
    }
}