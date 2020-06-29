using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_1 :SkillBase {

    float Timed = 0;

    public override void ExcuteSkill()
    {
        isCd = true;
       
        GameObject b = GameObject.Instantiate(Resources.Load("Prefabs/PlayerBomb")) as GameObject;
        b.GetComponent<Bomb>().bombPlayer = false;
        // b.transform.position = GameObject.FindGameObjectsWithTag("player").transform.position;
        b.transform.position = player._instance.transform.position;
    }

    public override int getSkillId()
    {
        return 0;
    }

    public override void update()
    {
        if(isCd)
        {
            Timed += Time.deltaTime;
            if (Timed >= CdTime)
            {
                isCd = false;
                Timed = 0;
            }
        }
    }
}
