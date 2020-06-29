using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using zFrame.UI;

public class CharacterControllerMove0 : MonoBehaviour
{
    public  Joystick joystick;
    public float speed = 5;
    CharacterController controller;
    Vector3 direction;
    void Start()
    {
       
        controller = GetComponent<CharacterController>();

        joystick.OnValueChanged.AddListener(v =>
        {
            if (v.magnitude != 0)
            {
                 direction = new Vector3(v.x, 0, v.y);
               // controller.Move(direction * speed * Time.deltaTime);
                controller.SimpleMove(direction * speed);
                transform.rotation = Quaternion.LookRotation(new Vector3(v.x, 0, v.y));
            }
        });
    }
    public void SimpleMove(Vector3 targetPos)
    {
        transform.LookAt(targetPos);
        controller.SimpleMove(direction * speed);
    }
    //void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    Rigidbody body = hit.collider.attachedRigidbody;
    //    if (hit.collider.gameObject.tag== "InventroyItem")
    //    {
    //        Debug.Log("1111111111111111");
    //        int id = hit.collider.gameObject.GetComponent<Inventory_Item>().id;
    //        Inventory._instance. GetId(id, 1);
    //        Destroy(hit.collider.gameObject);
    //    }
      
    //}
}
