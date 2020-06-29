using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpPanel : MonoBehaviour {

    public Text nameTxt;
    public Text explTxt;

    public bool isSelect = false;
    public Transform pot;
    public Transform p1;
    public Transform p2;
    public Transform p3;
    //技能名称 介绍 图标

    void Update() {
        if (!isSelect)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (pot.position == p1.position)
            {
                isSelect = false;
                studySkill(0);
            }
            else
            {

            }
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (pot.position == p1.position)
            {
                isSelect = false;
                studySkill(0);
            }
            else
            {

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (pot.position == p1.position)
            {
                isSelect = false;
                studySkill(0);
            }
            else
            {

            }
        }
    }


    public void studySkill(int n)
    {
        gameObject.SetActive(false);
        //
    }

    public void showSkill(int n)
    {
        skillObj skl = SkillManager.instance.idToSkillObj[n];
        nameTxt.text = skl.name;
        explTxt.text = skl.expln;
    }
}
