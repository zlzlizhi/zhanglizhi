using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShortCutType
{
    Skill,
    Drug,
    None
}

public class ShortcutGrid : MonoBehaviour
{
    public int id;
    public SkillInfo info;
    private UISprite icon;

    public UIButton but;
    public KeyCode keyCode;
    
    private ShortCutType type = ShortCutType.None;
    private ObjectInfo objectInfo;
    private PlayerStatus ps;
    private PlayerAttack pa;
    private bool bIsInCD;

    public float fCD;
    private float fTimeCount;

    public UISprite Mask;

    private void Awake()
    {
        icon = transform.Find("skillicon").GetComponent<UISprite>();
        icon.gameObject.SetActive(false);
        Mask.gameObject.SetActive(false);
       
    }

    void Start()
    {
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        pa = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerAttack>();
    }

    public void SetSkill(int id)
    {
        this.id = id;
       
        info = SkillsInfo._instance.GetSkillInfoByid(id);
        icon.gameObject.SetActive(true);
        icon.spriteName = info.icon_name;
        type = ShortCutType.Skill;
    }

    public void OnClick()
    {
        Debug.Log("OnCLick");
        if (type == ShortCutType.Drug)
        {
            //用药
            //OnDrugUse();
        }
        else if (type == ShortCutType.Skill)
        {
            Debug.Log(info.name);
            //释放技能
            //1,得到该技能需要的mp
            
            bool success = ps.TakeMP(info.mp);
            if (success == false)
            {

            }
            else
            {
                //2,获得mp之后，要去释放这个技能
                pa.UseSkill(info);
                fCD = info.anitime + 5;
                if (!bIsInCD && null != Mask)
                {
                    but.GetComponent<BoxCollider>().enabled=false;
                    bIsInCD = true;
                    StartCoroutine(CDWork());
                    Mask.gameObject.SetActive(true);
                   
                

                }

            }
        }
    }
    private IEnumerator CDWork()//CD
    {
        while (bIsInCD && null != Mask)
        {
            fTimeCount += Time.deltaTime;
            if (fTimeCount > fCD)
            {
                fTimeCount = 0;
                bIsInCD = false;
                Mask.gameObject.SetActive(false);
                but.GetComponent<BoxCollider>().enabled = false;
                but.isEnabled= true;
            }
            else
            {
               Mask.fillAmount = 1 - fTimeCount / fCD; //倒计时UI更新
                yield return 0;
            }
        }
    }


    //public void SetInventory(int id)
    //{
    //    this.id = id;
    //    objectInfo = ObjectsInfo._instance.GetObjectInfoById(id);
    //    if (objectInfo.type == ObjectType.Drug)
    //    {
    //        icon.gameObject.SetActive(true);
    //        icon.spriteName = objectInfo.icon_name;
    //        type = ShortCutType.Drug;
    //    }
    //}
    //public void OnDrugUse()
    //{
    //    bool success = Inventory._instance.MinusId(id, 1);
    //    if (success)
    //    {
    //        ps.GetDrug(objectInfo.hp, objectInfo.mp);
    //    }
    //    else
    //    {
    //        type = ShortCutType.None;
    //        icon.gameObject.SetActive(false);
    //        id = 0;
    //        info = null;
    //        objectInfo = null;
    //    }
    //}
}
