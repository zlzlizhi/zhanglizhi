using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Item : UIDragDropItem
{
    public UISprite sprite;
    public  bool isHover = false;
    public  int id;
    



    void Awake()
    {
        base.Awake();
        sprite= gameObject.GetComponent<UISprite>();
       
    }
    #region //拖拽
    protected override void OnDragDropRelease(GameObject surface)///拖拽
    {
        base.OnDragDropRelease(surface);
        if (surface != null)
        {
            if (surface.tag == "InventoryItemGrid")
            {
                if (surface == this.transform.parent.gameObject)//拖放到自己的格子里 重置位置
                {
                    RestPosition();
                }
                else
                {
                    Inventory_Item_Grid oldParent = this.transform.parent.GetComponent<Inventory_Item_Grid>();
                    this.transform.parent = surface.transform;//把当前物体所在格子的位置改为拖到格子的位置(空格子里没有物品 把当前物品移到空格子里)
                    Inventory_Item_Grid newparent = surface.GetComponent<Inventory_Item_Grid>();
                    newparent.SetId(oldParent.id, oldParent.num);
                    oldParent.ClearInfo();
                }
            }
            else if (surface.tag == "InventroyItem")
            {
                Inventory_Item_Grid grid1 = this.transform.parent.GetComponent<Inventory_Item_Grid>();
                Inventory_Item_Grid grid2 = surface.transform.parent.GetComponent<Inventory_Item_Grid>();
                int id = grid1.id;
                int num = grid1.num;
                grid1.SetId(grid2.id, grid2.num);
                grid2.SetId(id, num);
            }

        }
        RestPosition();
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
       
        if(isHover)
        {
        // InventroyDes._instance.Show(id);
            if(Input.GetMouseButtonDown(1))
            {
                bool sccess = EquipmentUI._instance.Dress(id);
                if(sccess)
                {
                    transform.parent.GetComponent<Inventory_Item_Grid>().MinusNumber(1);
                }
            }
        }
        


    }
    public void RestPosition()
    {
        transform.localPosition = Vector3.zero;
    }
    public void SetId(int id)
    {

        ObjectInfo info = ObjectsInfo._station.GetObjectinfoById(id);
        sprite.spriteName = info.icon_name;//通过名字改变图片 
        this.id = id;
    }

    public void SetIconName(int id, string icon_name)//直接改变图片
    {
       
        sprite.spriteName = icon_name;
        this.id = id;
    }
    public void OnHoverOver()
    {
        isHover = true;
       
    }
    public void OnHoverQuit()
    {
        isHover = false;
    }
    

    public void OnShow()//点击
    {
      
            InventroyDes._instance.Show(id,this);
      
    }
    public void Use()
    {

        
        transform.parent.GetComponent<Inventory_Item_Grid>().MinusNumber(1);




    }
}
