using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWeaponUI : MonoBehaviour
{
    public static ShopWeaponUI _instance;
    private TweenPosition tw;
    bool isshow = false;
    public int Buyid=0;
    public  GameObject numberDialog;//购买面板
    public UIInput numberInput;//输入购买个数
    public GameObject BuyOk;//确认购买面板
    public UILabel TwoLable;//确认面板上的提示
    public UIButton TwoOkbutton;

    [Header("列表预制体")]
    public GameObject Weaponitem;
    [Header("id集合")]
    public int[] WeaponitemidArrary;
    private bool istrue = false;
    public UIGrid grid;
    // Start is called before the first frame update
    private void Awake()
    {
        _instance = this;
        tw = gameObject.GetComponent<TweenPosition>();
        
       
    }
    void Start()
    {
       
        numberDialog.SetActive(false);
       BuyOk.SetActive(false);
       // ShowWeaponitem();

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
            if (istrue == false)
            {
                ShowWeaponitem();
                istrue = true;
            }
        }
        else
        {

            tw.PlayForward();
            isshow = true;
        }
    }
   public void ShowWeaponitem()
    {
        foreach(int id in WeaponitemidArrary)
        {
           
            GameObject itemGo = NGUITools.AddChild(grid.gameObject, Weaponitem);
            grid.AddChild(itemGo.transform);
             itemGo.GetComponent<WeaponItem>().Setid(id);
               
        }
    }

    //点击buy按钮时
    public void OnBuyClick(int id)
    {
        Buyid = id;
        numberDialog.SetActive(true);
        numberInput.value = "0";

    }
   
    int count;//购买个数
    int price;//购买的总价格

    //点击input上的OK 按钮时
    public void OnOkButton()
    {
        numberDialog.SetActive(false);
       BuyOk.SetActive(true);
        TwoOkbutton.gameObject.SetActive(true);
        count = int.Parse(numberInput.value);
        ObjectInfo info = ObjectsInfo._station.GetObjectinfoById(Buyid);
        price = info.prive_buy * count;
        TwoLable.text = "您确认要花费" + price.ToString() + "金币购买" + count.ToString() + "个" + info.name + "吗？";

    }
    //点击Buyok上的ok
    public void TwoOnOkButton()
    {
        TwoOkbutton.gameObject.SetActive(false);
        if (Inventory._instance.Getcoin(price))
        {
            if (count > 0)
            {
                Inventory._instance.GetId(Buyid, count);
                TwoLable.text = "购买成功";
            }
            else
            {
                TwoLable.text = "金币不足";
            }

        }

    }
    public void Onquit()//取消购买
    {
        BuyOk.SetActive(false);
        TwoLable.text = " ";
    }

}
