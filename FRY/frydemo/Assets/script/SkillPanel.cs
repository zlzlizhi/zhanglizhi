using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour {

    //public SkillBase[] skills = new SkillBase[3];

    public List<int> skills = new List<int>();
    public List<Image> skillHide = new List<Image>();

    public bool isSkillRun = false;

    void Update () {
        if(isSkillRun)
        {
            return;
        }
		if(Input.GetKeyDown(KeyCode.J))
        {
            SkillManager.instance.Execute(skills[0]);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SkillManager.instance.Execute(skills[1]);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SkillManager.instance.Execute(skills[2]);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SkillManager.instance.Execute(skills[3]);
        }


    }

    public void setCd(int n, float time)
    {
        skillHide[n].fillAmount = 1;
        StartCoroutine("sklcd", time);
    }
    
    IEnumerator sklcd(float n)
    {
        float z = n;
        while (z > 0)
        {
            z -= Time.deltaTime;

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
