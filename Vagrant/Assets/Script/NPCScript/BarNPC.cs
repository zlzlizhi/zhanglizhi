using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarNPC : MonoBehaviour
{
    public static BarNPC _instance;
    public TweenPosition TP;
    public UILabel Deslabel;
    public GameObject AcceptButton;
    public GameObject OkButton;
    public GameObject CancelButton;
    private bool isintask = false;
    public  int KillCount = 0;
    public UILabel PraiseLabel;
    public int num = 1;
    private PlayerStatus Playerstatus;
    // Start is called before the first frame update
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        Playerstatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseOver()
    {
        if(Input.GetMouseButton(0))
        {
            Showquest();
            if (isintask == true)
            {
                showTaskProgress();

            }
            else
            {
                ShowTaskDes();
            }
        }
    }
    void Showquest()
    {
        TP.gameObject.SetActive(true);

        TP.PlayForward();
       
    }
    void ShowTaskDes()
    {
        PraiseLabel.text = "老爷爷：勇敢的少年如果你帮我完成这个任务，我会给你一些奖励哦!加油！";
        if (num==1)
        {
            Deslabel.text = "\n任务：杀死10只小狼\n\n\n奖励：1000金币";
        }
       if(num==2)
        {
            Deslabel.text = "任务：杀死5只中狼\n\n\n奖励：1200金币";
        }
        OkButton.SetActive(false);
        AcceptButton.SetActive(true);
        CancelButton.SetActive(true);
   
    }
    void showTaskProgress()
    {
        if(num==1)
        {
            Deslabel.text = "任务：你已经杀死了" + KillCount + "\\10只小狼\n\n\n奖励：1000金币";
        }
        if(num==2)
        {
            Deslabel.text = "任务：你已经杀死了" + KillCount + "\\20只中狼\n\n\n奖励：1200金币";
        }
        Inventory._instance.AddCoin(1000);
        OkButton.SetActive(true);
        AcceptButton.SetActive(false);
        CancelButton.SetActive(false);
    }
    public void Killenemy()
    {
        KillCount++;
    }
    void Closequest()
    {
        TP.PlayReverse();
    }
   public  void OnClose()
    {
        Closequest();
    }
    public void OnAccept()
    {
     
        if (KillCount >= 1)
        {
            KillCount = 0;
        }
       
        isintask = true;
        showTaskProgress();
       
        
    }
    public void OnOk()
    {
       if(num==1)
       {
        if(KillCount>=10)
        {
            Playerstatus.Getcoin(1000);
            KillCount = 0; 
        }
            else
            {
                Closequest();
            }
        }
       if(num==2)
        {
            if (KillCount >= 20)
            {
                Playerstatus.Getcoin(1200);
                KillCount = 0;
            }
            else
            {
                Closequest();
            }
        }
       
    }
   
}
