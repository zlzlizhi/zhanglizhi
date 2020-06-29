using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour {

    static SkillManager _instance;

    public static SkillManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SkillManager();
            }
            return _instance;
        }
    }

    //skillBase 技能执行体
    //skillObj 技能数据体
    public Dictionary<int, SkillBase> idToSkillBase = new Dictionary<int, SkillBase>();
    public Dictionary<int, skillObj> idToSkillObj = new Dictionary<int, skillObj>();//技能表

    public List<SkillBase> SkillcdList = new List<SkillBase>();

    SkillManager()
    {
        SkillcdList.Clear();
        skillObj sk1 = new skillObj(0, "放炸弹", "在原地放一个炸弹，不会伤害到玩家", "");
        skillObj sk2 = new skillObj(1, "放炸弹2", "在原地放一个炸弹，不会伤害到玩家2", "");
        skillObj sk3 = new skillObj(2, "放炸弹3", "在原地放一个炸弹，不会伤害到玩家3", "");
        idToSkillObj.Add(sk1.id, sk1);
        idToSkillObj.Add(sk2.id, sk2);
        idToSkillObj.Add(sk3.id, sk3);

        SkillBase sb = new Skill_1();
        idToSkillBase.Add(sb.getSkillId(), sb);
    }

    public void Execute(int n)//玩家第n个技能
    {
        if (idToSkillBase[n].isCd)
        {
            Debug.Log("Skill is CDing");
            return;
        }
            
        if (!idToSkillBase.ContainsKey(n))
        {
            Debug.Log("SkillManager skill is null");
            return;
        }

        idToSkillBase[n].ExcuteSkill();
        SkillcdList.Add(idToSkillBase[n]);
    }

    private void Update()
    {
        foreach(SkillBase sb in SkillcdList)
        {
            sb.update();
            if(!sb.isCd)
            {
                SkillcdList.Remove(sb);
            }
        }
    }
}
