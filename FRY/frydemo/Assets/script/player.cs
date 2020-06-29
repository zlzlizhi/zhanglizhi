using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using UnityEngine.SceneManagement;

public class player : MonoBehaviour {
    public static player _instance;
    private Rigidbody rbody;
    private Animation anim;
    private int score = 0;
    public Text text;
    public Text Destext;
    public Text Wintext;
    public Image WinImage;
    public Text LevelUptext;
    public bool isDie = false;

    public GameObject overPanel;
    public PlayerPanel playerPanel;

    void Start()
    {
        _instance = this;
        rbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
        //EventUtilies.PLUEvent += playerlevelup;
    }

    public void playerlevelup()
    {
        Color c = new Color();
        float temp = LevelUptext.GetComponent<Text>().color.a;
        c.b = LevelUptext.GetComponent<Text>().color.b;
        c.g = LevelUptext.GetComponent<Text>().color.g;
        c.r = LevelUptext.GetComponent<Text>().color.r;
        c.a = 1;
        LevelUptext.GetComponent<Text>().color = c;
        StartCoroutine("jianbian", LevelUptext.GetComponent<Text>());
    }

    void Update()
    {
        if (isDie)
            return;
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector3 dir = new Vector3(h, 0, v);
        if (dir != Vector3.zero)
        {
        
        transform.rotation = Quaternion.LookRotation(dir);
  
    rbody.velocity = dir * 5;
   
        anim.Play("run");
         
        }
       else
        {
            anim.Play("idle");
        }
        
    }

    public void PlayerGetGold()
    {
        getScore(1);
        getExp(1);
    }

#region 玩家杀怪

    public void PlayerSkillDiss()
    {
        getScore(10);
        getExp(3);
        PlayWin();
    }

    private void PlayWin()

    {
        Color c = new Color();
        float temp = Wintext.GetComponent<Text>().color.a;
        c.b = Wintext.GetComponent<Text>().color.b;
        c.g = Wintext.GetComponent<Text>().color.g;
        c.r = Wintext.GetComponent<Text>().color.r;
        c.a = 1;


        Color c2 = new Color();
        float temp2 = WinImage.GetComponent<Image>().color.a;
        c2.b = WinImage.GetComponent<Image>().color.b;
        c2.g = WinImage.GetComponent<Image>().color.g;
        c2.r = WinImage.GetComponent<Image>().color.r;
        c2.a = 1;

        Wintext.GetComponent<Text>().color = c;
        WinImage.GetComponent<Image>().color = c2;
        StartCoroutine("jianbian");
    }
    #endregion

#region 玩家死亡
    public void Playdie()
    {
       Destext.enabled=true;//SetActive(true)
        isDie = true;
        anim.Play("die");
        Invoke("destroyPlayer", anim.GetClip("die").length);       
    }
    private void destroyPlayer()
    {
        gameObject.SetActive(false);
        //显示最后得分
        overPanel.transform.GetChild(2).GetComponent<Text>().text = text.text;
      overPanel.SetActive(true);
       // overPanel.enabled = true;
    }
#endregion

    
   /* IEnumerator jianbian(Text txt)
    {

        while(txt.color.a>0)
        {
            Color c = new Color();
            float temp = txt.color.a;
            c.b = txt.color.b;
            c.g = txt.color.g;
            c.r = txt.color.r;
            c.a = temp - 0.05f;
            txt.color = c;
            yield return new WaitForSeconds(0.01f);
        }       
    }*/
    
    IEnumerator jianbian()
    {

        while (Wintext.GetComponent<Text>().color.a > 0)
        {
            Color c = new Color();
            float temp = Wintext.GetComponent<Text>().color.a;
            c.b = Wintext.GetComponent<Text>().color.b;
            c.g = Wintext.GetComponent<Text>().color.g;
            c.r = Wintext.GetComponent<Text>().color.r;
            c.a = temp - 0.05f;
             Color c2 = new Color();
            float temp2 = WinImage.GetComponent<Image>().color.a;
            c2.b = WinImage.GetComponent<Image>().color.b;
            c2.g = WinImage.GetComponent<Image>().color.g;
            c2.r = WinImage.GetComponent<Image>().color.r;
            c2.a = temp2 - 0.05f;
            Wintext.GetComponent<Text>().color = c;
            WinImage.GetComponent<Image>().color = c2;
            yield return new WaitForSeconds(0.02f);
        }
    }
    //得分
    public void getScore(int value)
    {
        score += value;
        text.text = score.ToString();
    }
    //得到能量
    public void getExp(int value)
    {
        playerPanel.getExp(value);
     
    }
    //进入新游戏
    public void OkClick()
    {
        SceneManager.LoadScene("wwwwfryafry");
    }
    public void NoClick()
    {
   Application.Quit();
    }
}
