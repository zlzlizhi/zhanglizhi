using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    public static SkillUI _instance;
    bool isshow = false;
    public UIGrid grid;
    public GameObject skillitemprefab;
    private TweenPosition tw;
    private PlayerStatus ps;
    public int[] magicianSkillIdList;
    public int[] swordmanSkillIdList;
    private void Awake()
    {
        _instance = this;
        tw = this.gameObject.GetComponent<TweenPosition>();
       
    }
    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        int[] idlist = null;
        switch (ps.herotype)
        {
            case HeroType.Magician:
                idlist = magicianSkillIdList;
                break;
            case HeroType.Swordman:
                idlist = swordmanSkillIdList;
                break;

        }

        foreach (int id in idlist)
        {
            GameObject go = NGUITools.AddChild(grid.gameObject, skillitemprefab);
             grid.AddChild(go.transform);
           // go.transform.parent = grid.transform;
            go.GetComponent<SkillItem>().GetSkillId(id);
        }
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
            UpdateShow();
            tw.PlayForward();
            isshow = true;
           
        }
    }
    public void UpdateShow()
    {
        SkillItem[] items = this.GetComponentsInChildren<SkillItem>();
      
        foreach(SkillItem item in items )
        {
            item.UpdataShow(ps.level);
        }
    }
}
