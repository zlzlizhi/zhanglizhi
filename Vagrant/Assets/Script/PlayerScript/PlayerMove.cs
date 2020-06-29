using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController cc;
    private CharactersController div;
    public float speed = 3;
    // Start is called before the first frame update
    void Start()
    {
       cc = gameObject.GetComponent<CharacterController>();
        div = gameObject.GetComponent<CharactersController>();
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector3.Distance(div.tagerposition, transform.position);
        if(dis>=0.1f)
        {
           
             cc.SimpleMove(speed * transform.forward);
            // transform.Translate(speed * transform.forward);
          //  rigidbody.velocity = speed * transform.forward;
        }
    }
}
