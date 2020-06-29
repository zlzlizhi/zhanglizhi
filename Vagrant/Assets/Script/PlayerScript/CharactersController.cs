using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersController : MonoBehaviour
{
    public GameObject exf;
    private bool isDown=false;
    public  Vector3 tagerposition;
    // Start is called before the first frame update
    void Start()
    {
        tagerposition = this.transform.position;
    }

    // Update is called once per frame UICamera.hoveredObject==null
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&& UICamera.isOverUI == false) //点击鼠标左键

        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //屏幕坐标转射线
            RaycastHit hit;                 //射线对象是：结构体类型（存储了相关信息）
            bool isHit = Physics.Raycast(ray, out hit);  //发出射线检测到了碰撞  isHit返回的是 一个bool值 有碰撞器
            if (isHit && hit.collider.tag=="ground")
            {
                isDown = true;
                show(hit.point);
                TargetLookat(hit.point);
            }
            
        }
        if(Input.GetMouseButtonUp(0))
        {
            isDown = false;
        }
        if(isDown)//如果鼠标一直不抬起 人物一直朝向鼠标移动的方向
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //屏幕坐标转射线
            RaycastHit hit;                 //射线对象是：结构体类型（存储了相关信息）
            bool isHit = Physics.Raycast(ray, out hit);  //发出射线检测到了碰撞  isHit返回的是 一个bool值 有碰撞器
            if (isHit && hit.collider.tag == "ground")
            {
                TargetLookat(hit.point);
            }
        }
    }
    public void show(Vector3 position)
    {
        position = new Vector3(position.x, position.y + 0.2f, position.z);
        GameObject.Instantiate(exf, position, Quaternion.identity) ;
    }
    public void TargetLookat(Vector3 hit)

    {
        tagerposition = hit;
        tagerposition = new Vector3(tagerposition.x, transform.position.y, tagerposition.z);
        transform.LookAt(tagerposition);
    }
   
  

}
