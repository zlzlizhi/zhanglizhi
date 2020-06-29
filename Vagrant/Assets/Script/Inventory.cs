using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region
//背包系统Inventory
//格子面板Inventory：存储的物品信息 新建一个类（物品信息）字典（ID，物品类）把所有格子存在列表里 在空格子了添加物品
//空格子Inventory_Item_Grid：格子ID为0 在格子里显示和清除物品图片和信息 改变自己的ID
//图片Inventory_Item 改变物品图片信息 拖拽和交换
#endregion
public class Inventory : MonoBehaviour
{
    public static Inventory _instance;
    private TweenPosition tween;
    private int coin_count = 1000;//金币数量
    public UILabel coincount;
    public List<Inventory_Item_Grid> itemGridList = new List<Inventory_Item_Grid>();//格子列表
    public GameObject InventoryItem;
    bool isPlay = false;
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        tween = this.GetComponent<TweenPosition>();
    }
   
    public void Show()
    {
        isPlay = true;
        tween.PlayForward();
    }
    public void Hide()
    {
        isPlay = false;
        tween.PlayReverse();
    }
    public void Isshow()
    {
        if (isPlay==false)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    // Update is called once per frame
    void Update()
    {
       
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            int a = Random.Range(2001,2011);
           
            GetId(a,1);
           
           
        }
    }
   
    public void GetId(int id,int number=1)//放到背包里
    {
        Inventory_Item_Grid grid = null;
        foreach(Inventory_Item_Grid temp in itemGridList)
        {
            if(temp.id==id)
            {
                grid = temp;
                break;
            }
        }
        if(grid!=null)
        {
            grid.Plusnumber(number);
        }
        else
        {
            foreach (Inventory_Item_Grid temp in itemGridList)
            {
                if(temp.id==0)
                {
                    grid = temp;
                    break;
                }
            }
            if(grid!=null)
            {
                GameObject itemGo = NGUITools.AddChild(grid.gameObject, InventoryItem);
                itemGo.transform.localPosition = Vector3.zero;
                grid.SetId(id,number);
            }
        }
    }
    public void AddCoin(int count)
    {
        coin_count += count;
        coincount.text = coin_count.ToString();//更新金币
    }
    public bool Getcoin(int count)
    {
        if(count<=coin_count)
        {
            coin_count = coin_count - count;
            coincount.text = coin_count.ToString();
            return true;
        }
       
            return false;
        
    }
}
