using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class skillObj
{
    public int id;
    public string name;
    public string expln;
    public string iconPath;

    public skillObj(int _id,string _name,string _expln,string _iconPath)
    {
        id = _id;
        name = _name;
        expln = _expln;
        iconPath = _iconPath;
    }
}

public abstract class SkillBase {

    public bool isCd = false;

    public float CdTime;

    public abstract int getSkillId();
    public abstract void ExcuteSkill();
    public abstract void update();
}
