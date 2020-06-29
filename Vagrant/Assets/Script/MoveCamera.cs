using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private float speed=10;
    private float endX = 80;
    public Vector3 dis;
    float a;
    private GameObject player;
    bool isrotate = false;
    // Start is called before the first frame update
    void Start()
    {
        //if (transform.position.x > endX)
        //{
        //    transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //}
        player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(player.transform.position);
        dis = gameObject.transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      
      
    }
    private void FixedUpdate()
    {
        gameObject.transform.position = dis + player.transform.position;
        gameObject.transform.LookAt(player.transform.position);
     //   Rotate();
     //   ScrollWheel();
     
    }
    public void ScrollWheel()
    {
        // a = Camera.main.fieldOfView;
        a = dis.magnitude;
        a += Input.GetAxis("Mouse ScrollWheel") * speed;
        // transform.position = new Vector3(transform.position.x, transform.position.y + a, transform.position.z);
        // Camera.main.fieldOfView = a;
        // transform.Translate(Vector3.forward * a * 3f);
        a = Mathf.Clamp(a, 2, 18);
        dis = dis.normalized * a;
    }
    public void Rotate()
    {
      
        if(Input.GetMouseButtonDown(1))
        {
            isrotate = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isrotate = false;
        }
        float mouse_x = Input.GetAxis("Mouse X");
        float mouse_y = Input.GetAxis("Mouse Y");
        if(isrotate)
        {

            transform.RotateAround(player.transform.position, player.transform.up, mouse_x*5f);
            Vector3 origposition = transform.position;
            Quaternion origRotate = transform.rotation;
            transform.RotateAround(player.transform.position, player.transform.right, mouse_y * 5f);
            float x = transform.eulerAngles.x;
            if(x<10||x>80)//限制旋转大小
            {
                transform.position = origposition;
                transform.rotation = origRotate;
            }


        }
        dis = transform.position - player.transform.position;
    }
}
