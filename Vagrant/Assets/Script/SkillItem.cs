using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour
{
    public int id;
    private SkillInfo info;
    private UISprite iconname_sprite;
    private UILabel namelabel;
    private UILabel applytypelabel;
    private UILabel deslabel;
    private UILabel mplabel;
    private GameObject iconmask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitProperty()
    {
        iconname_sprite = transform.Find("icon_name").GetComponent<UISprite>();
        namelabel = transform.Find("property/name_bg/name").GetComponent<UILabel>();
        applytypelabel = transform.Find("property/applytype_bg/applytype").GetComponent<UILabel>();
        deslabel = transform.Find("property/des_bg/des").GetComponent<UILabel>();
        mplabel = transform.Find("property/mp_bg/mp").GetComponent<UILabel>();
        iconmask = transform.Find("iconmask").gameObject;
        iconmask.SetActive(false);
    }
    public void UpdataShow(int level)
    {
        if(level<info.level)
        {
            iconmask.SetActive(true);
           // iconname_sprite.GetComponent<SkillItemicon>().enabled = true;
            iconmask.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            iconmask.SetActive(false);
          //  iconname_sprite.GetComponent<SkillItemicon>().enabled = false;
            iconmask.GetComponent<BoxCollider>().enabled = false;
        }

    }

    //通过调用这个方法来更新显示
    public void GetSkillId(int id)
    {
        InitProperty();
        this.id = id;
       
        info = SkillsInfo._instance.GetSkillInfoByid(id);
        if(info==null)
        {
            Debug.Log("空");
        }
        iconname_sprite.spriteName = info.icon_name;
        namelabel.text = info.name;
       
        switch (info.applyType)
        {
            case ApplyType.Passive:
                applytypelabel.text = "增益";
                break;
            case ApplyType.Buff:
                applytypelabel.text = "增加";
                break;
            case ApplyType.SingleTarget:
                applytypelabel.text = "单个目标";
                break;
            case ApplyType.MultiTarget:
                applytypelabel.text = "群体技能";
                break;

        }
        deslabel.text = info.des;
        mplabel.text = info.mp + "MP";
      
    }
}
