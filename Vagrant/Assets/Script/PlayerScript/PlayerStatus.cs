using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public enum HeroType

{
    Swordman,
    Magician

}
public class PlayerStatus : MonoBehaviour
{

    public HeroType herotype;
    public int level = 1;//100+level*30
    public int hp = 100;//hp最大值
    public int mp = 100;//mp最大值
    public int coin = 200;
    public string Name="默认名称";
    public float hp_remain = 100;
    public float mp_remain = 100;
    public float exp = 0;//当前获得的经验
    public int attact = 20;
    public int attact_plus = 0;
    public int def = 20;
    public int def_plus = 0;
    public int speed = 20;
    public int speed_plus = 0;

    public int point_remain = 0;//剩余点数
    void Start()
    {
        string N = PlayerPrefs.GetString("Name");
        Name = N;
        GetExp(0);

    }
    public void GetEquip(int attactplu,int defplu,int speedplu)
    {
        attact += attactplu;
        def += defplu;
        speed += speed_plus;
    }
    //加血加蓝
    public void GetDrug(int hp,int mp)
    {
        hp_remain += hp;
        mp_remain += mp;
        if(hp_remain>this.hp)
        {
            hp_remain = this.hp;
        }
        if(mp_remain>this.mp)
        {
            mp_remain = this.mp;
        }
    }
    public void Getcoin(int coin)//获得金币
    {
        this.coin += coin;
    }
    public bool Getpluspoint(int num)//获得点数
    {
        if (point_remain >= num)

        {
            point_remain -= num;
            return true;
        }
        else
        {
            return false;
        }
          
        
    }
    public void GetExp(int exp)//获得经验
    {
        this.exp += exp;
        float total_exp = 100 + level * 30;
        while(this.exp>=total_exp)
        {
            //TODO 升级
            this.level++;
            this.exp -= total_exp;
            total_exp = 100 + level * 30; 
        }
        EXPBar._instance.SetValue(this.exp / total_exp);
    }
  
    public bool  TakeMP(int count)//使用蓝
    {
        if (mp_remain >= count)
        {
            mp_remain -= count;
            return true;
        }
        else
        {
            return false;
        }

    }
}




