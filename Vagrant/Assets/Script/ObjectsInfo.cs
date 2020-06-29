using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ObjectsInfo : MonoBehaviour
{
    public TextAsset objectInfolistText;
    public static ObjectsInfo _station;
    public Dictionary<int, ObjectInfo> objectsdic = new Dictionary<int, ObjectInfo>();
    // Start is called before the first frame update
    private void Awake()
    {
        ReadText();
    }
    void Start()
    {
        _station = this;
       
       
    }

    // Update is called once per frame
    void Update()
    {
        
    } 
    public ObjectInfo GetObjectinfoById(int id)
    {
        ObjectInfo info = null;
        objectsdic.TryGetValue(id, out info);//字典里通过ID查找到ObjectInfo 返回ObjectInfo的值
            return info;
    }
   
    public void ReadText()
    {
        string text = objectInfolistText.text;
        //string path = Application.streamingAssetsPath + "/OjbectsInfoList.txt";
        //string text = File.ReadAllText(path);
        string[] strArrary = text.Split('\n');//按列读取出文本信息
        
        foreach(string str in strArrary)
        {
           
            string[] proArrary = str.Split(',');
            ObjectInfo info = new ObjectInfo();
            info.id = int.Parse(proArrary[0]);
            info.name = proArrary[1];
          
            info.icon_name = proArrary[2];
            string str_type = proArrary[3];
           
            ObjectType type = ObjectType.Drug;
            switch (str_type)
            {
                case ("Drug"):
                    type = ObjectType.Drug;
                    break;
                case ("Equip"):
                    type = ObjectType.Equip;
                    break;
                case ("Mat"):
                    type = ObjectType.Mat;
                    break;
            }
            info.type = type;
          
            if (type == ObjectType.Drug)
            {
                info.hp = int.Parse(proArrary[4]);
                info.mp = int.Parse(proArrary[5]);
                info.price_sell = int.Parse(proArrary[6]);
                info.prive_buy = int.Parse(proArrary[7]);
            }
            else if (type == ObjectType.Equip)
            {
                info.attack = int.Parse(proArrary[4]);
                info.def = int.Parse(proArrary[5]);
                info.speed = int.Parse(proArrary[6]);
                info.price_sell = int.Parse(proArrary[9]);
                info.prive_buy = int.Parse(proArrary[10]);
                string str_dressType = proArrary[7];
                switch (str_dressType)
                {
                    case ("Headgear"):
                        info.dressType = DressType.Headgear;
                        break;
                    case ("Armor"):
                        info.dressType = DressType.Armor;
                        break;
                    case ("RightHand"):
                        info.dressType = DressType.RightHand;
                        break;
                    case ("LeftHand"):
                        info.dressType = DressType.LeftHand;
                        break;
                    case ("Shoe"):
                        info.dressType = DressType.Shoe;
                        break;
                    case ("Accessory"):
                        info.dressType = DressType.Accessory;
                        break;
                }
                string str_applicationType = proArrary[8];
                switch (str_applicationType)
                {
                    case ("Swordman"):
                        info.applicationType = ApplicationType.Swordman;
                        break;
                    case ("Magician"):
                        info.applicationType = ApplicationType.Magician;
                        break;
                    case ("Common"):
                        info.applicationType = ApplicationType.Common;
                        break;
                }


            }
            objectsdic.Add(info.id, info);
           
        }
    }
}
public enum ObjectType
{
    Drug,
    Equip,
    Mat
}
public enum DressType
{
    Headgear,
    Armor,
    RightHand,
    LeftHand,
    Shoe,
    Accessory

}

public enum ApplicationType
{
    Swordman,//剑士
    Magician,//魔法师
    Common//通用

}


public class ObjectInfo
{
    public int id;
    public string name;
    public string icon_name;
    public ObjectType type;
    public int hp;
    public int mp;
    public int price_sell;
    public int prive_buy;

    public int attack;
    public int def;
    public int speed;
    public ApplicationType applicationType;
    public DressType dressType;

}

