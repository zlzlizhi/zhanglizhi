using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public static EquipmentUI _instance;
    bool isshow = false;
    private TweenPosition tw;
    private GameObject Headgear;
    private GameObject Armor;
    private GameObject RightHand;
    private GameObject LeftHand;
    private GameObject Shoe;
    private GameObject Accessory;
    private PlayerStatus ps;
    public GameObject equipmentItem;


    public int attack;
    public int def;
    public int speed;
    // Start is called before the first frame update
    private void Awake()
    {
        _instance = this;
        tw = this.gameObject.GetComponent<TweenPosition>();
        Headgear = transform.Find("Headgear").gameObject;
        Armor = transform.Find("Armor").gameObject;
        RightHand = transform.Find("RightHand").gameObject;
        LeftHand = transform.Find("LeftHand").gameObject;
        Shoe = transform.Find("Shoe").gameObject;
        Accessory = transform.Find("Accessory").gameObject;

    }
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TranformStatus()
    {
        if (isshow)
        {
            tw.PlayReverse();
            isshow = false;
        }
        else
        {

            tw.PlayForward();
            isshow = true;
        }
    }
    public bool Dress(int id)

    {
        ObjectInfo info = ObjectsInfo._station.GetObjectinfoById(id);
        if (info.type != ObjectType.Equip)
        {
            return false;
        }
        if (ps.herotype == HeroType.Magician)
        {
            if (info.applicationType == ApplicationType.Swordman)
            {
                return false;
            }
        }
        if (ps.herotype == HeroType.Swordman)
        {
            if (info.applicationType == ApplicationType.Magician)
            {
                return false;
            }
        }

        GameObject parent = null;
        switch (info.dressType)
        {
            case DressType.Headgear:
                parent = Headgear;
                break;
            case DressType.Armor:
                parent = Armor;
                break;
            case DressType.RightHand:
                parent = RightHand;
                break;
            case DressType.LeftHand:
                parent = LeftHand;
                break;
            case DressType.Shoe:
                parent = Shoe;
                break;
            case DressType.Accessory:
                parent = Accessory;
                break;
        }
        EquipmentItem item = parent.GetComponentInChildren<EquipmentItem>();
        if (item != null)//已经穿戴
        {
            Inventory._instance.GetId(item.id, 1);
            item.SetInfo(info);
        }
        else
        {
            GameObject itemGo = NGUITools.AddChild(parent, equipmentItem);
            itemGo.transform.localPosition = Vector3.zero;
            itemGo.GetComponent<EquipmentItem>().SetInfo(info);
        }
        UpdateProperty();
        return true;
    }
    public void TakeOff(int id,GameObject go)
    {
        Inventory._instance.GetId(id);
        Destroy(go);
        UpdateProperty();
    }

    public void UpdateProperty()//穿上装备增加属性

    {
        this.attack = 0;
        this.def = 0;
        this.speed = 0;
        EquipmentItem headearItem = gameObject.GetComponentInChildren<EquipmentItem>();
        PlusProperty(headearItem);
        EquipmentItem armorItem = gameObject.GetComponentInChildren<EquipmentItem>();
        PlusProperty(armorItem);
        EquipmentItem rightHandItem = gameObject.GetComponentInChildren<EquipmentItem>();
        PlusProperty(rightHandItem);
        EquipmentItem leftHandItem = gameObject.GetComponentInChildren<EquipmentItem>();
        PlusProperty(leftHandItem);
        EquipmentItem shoeItem = gameObject.GetComponentInChildren<EquipmentItem>();
        PlusProperty(shoeItem);
        EquipmentItem accessoryItem = gameObject.GetComponentInChildren<EquipmentItem>();
        PlusProperty(accessoryItem);



    }
    public void PlusProperty(EquipmentItem item)
    {
      if(item!=null)
        {
            ObjectInfo equipinfo = ObjectsInfo._station.GetObjectinfoById(item.id);
            this.attack += equipinfo.attack;
            this.def += equipinfo.def;
            this.speed += equipinfo.speed;
        }
    }
}
