using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsInfo : MonoBehaviour
{
    public static SkillsInfo _instance;
    public TextAsset skillInfoText;
    private Dictionary<int, SkillInfo> skillInfoDict = new Dictionary<int, SkillInfo>();
    private void Awake()
    {
        _instance = this;
        InitSkillInfoDict();//初始化技能字典信息
    }
   public  SkillInfo  GetSkillInfoByid(int id)
    {
        SkillInfo info = null;
        skillInfoDict.TryGetValue(id, out info);
        return info;
    }

    void InitSkillInfoDict()
    {
        string text = skillInfoText.text;
        string[] skillinfoArray = text.Split('\n');
        foreach(string skillinfostr in skillinfoArray)
        {
            string[] pa = skillinfostr.Split(',');
            SkillInfo info = new SkillInfo();
            info.id = int.Parse(pa[0]);
            info.name = pa[1];
          
            info.icon_name = pa[2];
            
            info.des = pa[3];
            string str_ApplyType = pa[4];
            switch (str_ApplyType)
            {
                case "Passive":
                    info.applyType = ApplyType.Passive;
                    break;
                case "Buff":
                    info.applyType = ApplyType.Buff;
                    break;
                case "SingleTarget":
                    info.applyType = ApplyType.SingleTarget;
                    break;
                case "MultiTarget":
                    info.applyType = ApplyType.MultiTarget;
                    break;

            }
            string str_ApplyProperty = pa[5];
            switch (str_ApplyProperty)
            {
                case "Attack":
                    info.applyProperty = ApplyProperty.Attack;
                    break;
                case "AttackSpeed":
                    info.applyProperty = ApplyProperty.AttackSpeed;
                    break;
                case "Def":
                    info.applyProperty = ApplyProperty.Def;
                    break;
                case "HP":
                    info.applyProperty = ApplyProperty.HP;
                    break;
                case "MP":
                    info.applyProperty = ApplyProperty.MP;
                    break;
                case "Speed":
                    info.applyProperty = ApplyProperty.Speed;
                    break;
            }
            info.applyValue = int.Parse(pa[6]);
            info.applyTime = int.Parse(pa[7]);
            info.mp = int.Parse(pa[8]);
            info.coldTime = int.Parse(pa[9]);
            switch (pa[10])
            {
                case "Swordman":
                    info.applicableRole = ApplicableRole.Swordman;
                    break;
                case "Magician":
                    info.applicableRole = ApplicableRole.Magician;
                    break;
            }
            info.level = int.Parse(pa[11]);

            switch(pa[12])
            {
                case "Enemy":
                    info.releaseType = ReleaseType.Enemy;
                    break;
                case "Position":
                    info.releaseType = ReleaseType.Position;
                    break;
                case "Self":
                    info.releaseType = ReleaseType.Self;
                    break;
            }
            info.distance = float.Parse(pa[13]);
            info.efx_name = pa[14];
            info.aniname = pa[15];
            info.anitime = float.Parse(pa[16]);
            skillInfoDict.Add(info.id, info);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
}
//适用角色
public enum ApplicableRole
{
    Swordman,
    Magician
}
//作用类型
public enum ApplyType
{
Passive,
Buff,
SingleTarget,
MultiTarget

}
//作用属性
public enum ApplyProperty
{
    Attack,
    Def,
    Speed,
    AttackSpeed,
    HP,
    MP
}
//释放类型
public enum ReleaseType
{
Self,
Enemy,
Position
}


public class SkillInfo
{
    public int id;
    public string name;
    public string icon_name;
    public string des;
    public ApplyType applyType;
    public ApplyProperty applyProperty;
    public int applyValue;
    public int applyTime;
    public int mp;
    public int coldTime;
    public ApplicableRole applicableRole;
    public int level;
    public ReleaseType releaseType;
    public float distance;
    public string efx_name;
    public string aniname;
    public float anitime;
}

