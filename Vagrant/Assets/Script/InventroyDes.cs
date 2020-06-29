using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventroyDes : MonoBehaviour
{
    public UILabel UILabel;
    public static InventroyDes _instance;
    private float timer = 0;
    public ObjectInfo Info;
    private PlayerStatus ps;
    private int Uid;
    Inventory_Item Inventory_Item;
    // Start is called before the first frame update
    private void Awake()
    {
        _instance = this;
        UILabel = gameObject.GetComponentInChildren<UILabel>();
        this.gameObject.SetActive(false);
       
      
    }
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (this.gameObject.activeInHierarchy == true)
        //{
        //    timer -= Time.deltaTime;
        //    if (timer <= 0)
        //    {
        //        this.gameObject.SetActive(false);
        //    }
        //}
    }
    public void Show(int id,Inventory_Item inventory_Item)
        
    {
        Inventory_Item = inventory_Item;
        this.gameObject.SetActive(true);
        timer = 0.1f;
        Uid = id;
        ObjectInfo info = ObjectsInfo._station.GetObjectinfoById(id);
        Info=info;
        string str = "";
        switch (info.type)
        {
            case ObjectType.Drug:
                str = GetDrugdes(info);
                 break;
            case ObjectType.Equip:
                str = GetEquipdes(info);
                break;

        }
        
        UILabel.text = str;


    }
   
   public string GetDrugdes(ObjectInfo info)
    {
        string str = "";
        str += "名称: " + info.name + "\n";
        str += "+HP: " + info.hp + "\n";
        str += "+MP: " + info.mp + "\n";
        str += "出售价: " + info.price_sell + "\n";
        str += "购买价: " + info.prive_buy + "\n";
        return str;
    }
    public string GetEquipdes(ObjectInfo info)
    {
        string str = "";
        str += "名称: " + info.name + "\n";
        switch (info.dressType)
        {
            case DressType.Headgear:
                str += "穿戴类型：头盔\n";
                break;
            case DressType.Armor:
                str += "穿戴类型：盔甲\n";
                break;
            case DressType.RightHand:
                str += "穿戴类型：右手\n";
                break;
            case DressType.LeftHand:
                str += "穿戴类型：左手\n";
                break;
            case DressType.Shoe:
                str += "穿戴类型：鞋子\n";
                break;
            case DressType.Accessory:
                str += "穿戴类型：饰品\n";
                break;

        }
        switch (info.applicationType)
        {
            case ApplicationType.Magician:
                str += "适用类型：魔法师\n";
                break;
            case ApplicationType.Swordman:
                str += "适用类型：剑士\n";
                break;
            case ApplicationType.Common:
                str += "适用类型：通用\n";
                break;

        }
        str += "伤害值：" + info.attack+"\n";
        str += "防御值：" + info.def + "\n";
        str += "速度值：" + info.speed + "\n";
        str += "出售价: " + info.price_sell + "\n";
        str += "购买价: " + info.prive_buy + "\n";
        return str;
    }
    public void Use()
    {
       
        ObjectInfo Info = ObjectsInfo._station.GetObjectinfoById(Uid);
        if(Info.type==ObjectType.Drug)//使用药品
        {
            Inventory_Item.Use();

        }

      
            bool sccess = EquipmentUI._instance.Dress(Uid);//使用装备
            if (sccess)
            {
                Inventory_Item.Use();
            }
        
            //switch (Info.type)
            //{
            //    case ObjectType.Drug:
            //        ps.GetDrug(Info.hp, Info.mp);
            //        break;
            //    case ObjectType.Equip:
            //        ps.GetEquip(Info.attack, Info.def, Info.speed);
            //        break;
            //}


     }
}
