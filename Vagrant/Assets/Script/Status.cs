using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public static Status _instance;
    bool isshow = false;
    private TweenPosition tw;
    private UILabel attackUILable;
    private UILabel defUILable;
    private UILabel speedUILable;
    private UILabel point_remainUILable;
    private UILabel summaryUILable;

    private GameObject attackbuttonGo;
    private GameObject defbuttonGo;
    private GameObject speedbuttonGo;
    private PlayerStatus ps;
    // Start is called before the first frame update
    private void Awake()
    {
        _instance = this;
        tw = this.gameObject.GetComponent<TweenPosition>();
        attackUILable = transform.Find("attack").GetComponent<UILabel>();
        defUILable = transform.Find("def").GetComponent<UILabel>();
        speedUILable = transform.Find("speed").GetComponent<UILabel>();
        point_remainUILable = transform.Find("point_remain").GetComponent<UILabel>();
        summaryUILable = transform.Find("summary").GetComponent<UILabel>();

        attackbuttonGo = transform.Find("attack_plusButton").gameObject;
        defbuttonGo = transform.Find("def_plusButton").gameObject;
        speedbuttonGo = transform.Find("speed_plusButton").gameObject;
       
    }

    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
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
    public void UpdateShow()//更新显示
    {
        attackUILable.text = ps.attact + "+" + ps.attact_plus;
        defUILable.text = ps.def + "+" + ps.def_plus;
        speedUILable.text = ps.speed + "+" + ps.speed_plus;
        point_remainUILable.text ="剩余点数：      " +ps.point_remain.ToString();
        summaryUILable.text = "伤害：" + (ps.attact + ps.attact_plus)
            + "  " + "防御：" + (ps.def + ps.def_plus)
            + "  " + "速度：" + (ps.speed + ps.speed_plus);
        if (ps.point_remain > 0)
        {
            attackbuttonGo.SetActive(true);
            defbuttonGo.SetActive(true);
            speedbuttonGo.SetActive(true);
        }
        else
        {
            attackbuttonGo.SetActive(false);
            defbuttonGo.SetActive(false);
            speedbuttonGo.SetActive(false);
        }

    }
    public void AttackplusButton()
    {
        if(ps.Getpluspoint(1))
        {
            ps.attact_plus++;
            UpdateShow();
            
        }
    }
    public void DefplusButton()
    {
        if (ps.Getpluspoint(1))
        {
            ps.def_plus++;
            UpdateShow();

        }
    }
    public void SpeedplusButton()
    {
        if (ps.Getpluspoint(1))
        {
            ps.speed_plus++;
            UpdateShow();

        }
    }
}
