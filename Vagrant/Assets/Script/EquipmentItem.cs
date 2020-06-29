using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : MonoBehaviour
{
  
    public int id;
    private UISprite sprite;
    bool isHover = false;
    // Start is called before the first frame update
    private void Awake()
    {
        sprite = this.GetComponent<UISprite>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isHover)
        {
            if(Input.GetMouseButton(1))
            {
                EquipmentUI._instance.TakeOff(id,gameObject);
            }
        }
    }
    public void SetId(int id)//更改显示
    {
        this.id = id;
        ObjectInfo info = ObjectsInfo._station.GetObjectinfoById(id);
        SetInfo(info);
    }
    public void SetInfo(ObjectInfo info)//更改显示
    {
        this.id = info.id;
        sprite.spriteName = info.icon_name;
    }
    public void OnHover(bool isOver)
    {
        isHover = isOver;
    }
   
}
