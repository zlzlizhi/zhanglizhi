using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    public static WeaponItem _instance;

    private int Id;
    private ObjectInfo info;
    private UISprite name_iconn;//图片
    private UILabel effer;//效果
    private UILabel Buy;//售价
    private UILabel icon;//名称

    private void Awake()
    {
        _instance = this;
        name_iconn = transform.Find("name_icon").GetComponent<UISprite>();
        effer = transform.Find("effer (1)").GetComponent<UILabel>();
        Buy = transform.Find("Buy (1)").GetComponent<UILabel>();
        icon = transform.Find("icon").GetComponent<UILabel>();
     }
// Start is called before the first frame update
     void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    //更新装备商店的装备列表的显示
    public void Setid(int id)
    {
       info = ObjectsInfo._station.GetObjectinfoById(id);
      
        
        if (info == null)
        {
            Debug.Log("空");
        }
        Id = id;
        name_iconn.spriteName = info.icon_name;
      
        Buy.text = info.prive_buy.ToString();
       
        icon.text = info.name;
       if(info.attack>0)
        {
            effer.text = "+攻击 " + info.attack;
        }
       else if(info.speed>0)
        {
            effer.text = "+速度 " + info.speed;
        }
       else if(info.def>0)
        {
            effer.text = "+防御 " + info.def;
        }

    }
    public void BuyCliock()
    {
        ShopWeaponUI._instance.OnBuyClick(Id);
    }
}
