using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDurg : MonoBehaviour
{
    public static ShopDurg _instance;
    bool isshow = false;
    private TweenPosition tw;
    public int buy_id;
    public GameObject numberDialog;//购买面板
    public UIInput numberInput;//输入购买个数
    public GameObject BuyOk;//确认购买面板
    public UILabel TwoLable;//确认面板上的提示
    private UIButton TwoOkbutton;
    // Start is called before the first frame update
    private void Awake()
    {
        _instance = this;
        tw = this.gameObject.GetComponent<TweenPosition>();
        TwoOkbutton = transform.Find("BuyOk/TwoOk").GetComponent<UIButton>();
    }

    // Start is called before the first frame update
    void Start()
    {
        numberDialog.SetActive(false);
        BuyOk.SetActive(false);
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

            tw.PlayForward();
            isshow = true;
        }
    }
    public void OnBuyId1001()
    {
        BuyId(1001);
    }
    public void OnBuyId1002()
    {
        BuyId(1002);
    }
    public void OnBuyId1003()
    {
        BuyId(1003);
    }
    public void BuyId(int id)
    {
        ShowNumberDialog();
        buy_id = id;
       
    }
    int count;//购买个数
    int price;//购买的总价格
    public void OnOkButton()
    {
        numberDialog.SetActive(false);
        BuyOk.SetActive(true);
        TwoOkbutton.gameObject.SetActive(true);
         count = int.Parse(numberInput.value);
        ObjectInfo info = ObjectsInfo._station.GetObjectinfoById(buy_id);
        price = info.prive_buy * count;
        TwoLable.text = "您确认要花费" + price.ToString() + "金币购买" + count.ToString() + "个" + info.name + "吗？";

    }
    public void TwoOnOkButton()
    {
        TwoOkbutton.gameObject.SetActive(false);
        if (Inventory._instance.Getcoin(price))
        {
            if (count > 0)
            {
                Inventory._instance.GetId(buy_id, count);
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
    void ShowNumberDialog()

    {
        numberDialog.SetActive(true);
        numberInput.value = "0";
    }
}
