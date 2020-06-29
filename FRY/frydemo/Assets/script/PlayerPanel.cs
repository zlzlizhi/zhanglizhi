using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour {

    //设置玩家等级最高5级 升级增加学习 技能  的机会
    //等级  50 100 200 300 400
    //吃金币 获得10点经验
    public Text pName;
    public Text level;
    public Slider expSlider;
    [SerializeField]
    public float[] exp = new float[4];
    public float playerExps;
    public static PlayerPanel Instance;

    public float speed=0.1f;

    public void Start()
    {

        exp[0] = 100;
        exp[1] = 250;
        exp[2] = 450;
        exp[3] = 650;
    }

    private void Update()
    {
        try
        {
      if (player._instance.isDie)// GameObject.Find("player").GetComponent<player>().isDie


                return;
            expSlider.value -= speed * Time.deltaTime;
            if (expSlider.value <= 0)
            {

                // GameObject.Find("player").SendMessage("Playdie") ;
                player._instance.Playdie();
            }
        }
        catch(Exception e)
        {

        }
    }

    public void getExp(int value)
    {
        expSlider.value += 0.1f * value;
        /*
        int key = int.Parse(level.text) - 1;
        if (key == 4)
        {
            return;
        }        
        playerExps += value;
        if (exp[key] <= playerExps)
        {
            playerExps -= exp[key];
            key++;
            level.text = (key+1).ToString();//升级
            GameObject.Find("player").GetComponent<player>().playerlevelup();
            //EventUtilies.PLUEventInvoke();
        }
        if (key < 4)
        {
            expSlider.value = playerExps / exp[key];
        }
        */
    }
}
