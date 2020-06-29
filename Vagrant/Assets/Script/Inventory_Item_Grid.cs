using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Item_Grid : MonoBehaviour
{
    public int id;//id 不为0 就存在图片
    public int num = 0;
    public  UILabel UILabel;//显示物品数量
    private ObjectInfo info;
    // Start is called before the first frame update
    void Start()
    {
        UILabel = gameObject.GetComponentInChildren<UILabel>().gameObject.GetComponent<UILabel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //显示图片和数字
    public void SetId(int id,int num)
    {
        this.id = id;
        info = ObjectsInfo._station.GetObjectinfoById(id);
        Inventory_Item item = gameObject.GetComponentInChildren<Inventory_Item>();
        item.SetIconName(id, info.icon_name);
        this.num = num;
        UILabel.enabled = true;
        UILabel.text = num.ToString();
    }
    ////显示图片
   
    public void Plusnumber(int num)
    {
        this.num += num;
        UILabel.text = this.num.ToString();

    }
  
    public bool MinusNumber(int num=1)
    {
        if(num<=this.num)
        {
            this.num -= num;
          
            if (this.num==0)
            {
                ClearInfo();
                Destroy(this.GetComponentInChildren<Inventory_Item>().gameObject);
                InventroyDes._instance.gameObject.SetActive(false);
            }
            UILabel.text = this.num.ToString();
            return true;
           
        }
        return false;
       
    }
    public void ClearInfo()
    {
       
        id = 0;
        info = null;
        num = 0;
        UILabel.enabled = false;
       
    }
}
