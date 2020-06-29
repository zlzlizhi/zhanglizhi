using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WolfState
{
    Idle,
    attack,
    Death,
    Walk

}

public class WolfBaby : MonoBehaviour
{
    private Animation anim;
    public WolfState state = WolfState.Idle;
    private CharacterController cc;
    public int attact ;//小狼的伤害
    public float speed ;//小狼死亡速度
    public int exp;//得到的经验值
    public string Animation_Idle;
    public string Animation_Walk ;
    public string Animation_Now ;
    public float time ;
    private float timer = 0;
    private float miss = 0.2f;
    public int Hp ;
    private Color normol;
    private bool isattack = false;
    public AudioClip BabyMiss;
    private GameObject hudtextFollow;//小狼下的HUDtext
    private GameObject hudtextGO;//接收预制体的物体
    public GameObject hudtextPrefab;//hud的预制体
    private HUDText hudtext;
    private UIFollowTarget follwTarget;
    public GameObject Baby;
    private Transform target;//小狼的攻击对象
    private PlayerStatus ps;
    public string aniname_normalattack;
    public float time_normalattack;

    public string aniname_caryattack;//="WolfBaby-Attack2"
    public float time_caryattack;
    public float attack_rate;
    public float attack_timer = 0;
    public string aniname_attack_now;//= "WolfBaby-Attack1"
    private float crazyattack_rate = 0.25f;
    public string aniname_Death;
    private float maxdis = 5;
    private float mindis = 2;
    float dis;
    public WolfSpawn spawn;
    public GameObject Item;//掉落的物品
    GameObject itemGo;
    // Start is called before the first frame update
    void Awake()
    {
        anim = gameObject.GetComponent<Animation>();
        cc = this.GetComponent<CharacterController>();
        hudtextFollow = transform.Find("HUDText").gameObject;
       
         normol=Baby.GetComponent<Renderer>().material.color;
      
       

    }
    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //hudtextGO = GameObject.Instantiate(hudtextPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        //hudtextGO.transform.parent = HUDTextParent._instance.gameObject.transform;
        hudtextGO = NGUITools.AddChild(HUDTextParent._instance.gameObject, hudtextPrefab);
        hudtext = hudtextGO.GetComponent<HUDText>();
        follwTarget = hudtextGO.GetComponent<UIFollowTarget>();
        follwTarget.target = hudtextFollow.transform;
        follwTarget.gameCamera = Camera.main;
       // follwTarget.uiCamera = UICamera.current.GetComponent<Camera>();
      //  follwTarget.uiCamera = UICamera.currentCamera;
      
    }
   
    // Update is called once per frame
    void Update()
    {
        if(target!=null)
        {
             dis = Vector3.Distance(transform.position, target.position);
            if(dis<2)
            {
                state = WolfState.attack;
            }
        }
      
       
        if (state == WolfState.Death)
        {
            anim.Play(aniname_Death);
        }
        else if (state == WolfState.attack)//自动攻击
        {
             AutoAttack();
           
        }
        else
        {

            anim.Play(Animation_Now);
            if (Animation_Now == Animation_Walk)
            {
                cc.SimpleMove(transform.forward * speed);

            }
            timer += Time.deltaTime;
            if (timer >= time)
            {
                timer = 0;
                RandomState();
            }

            //巡逻

        }
       
    }
    public void RandomState()
    {
        int value = Random.Range(0, 2);
        if (value == 0)
        {
            Animation_Now = Animation_Idle;
        }

        else
        {
            if (Animation_Now != Animation_Walk)
            {
                transform.Rotate(transform.up * Random.Range(0, 360));
            }
            Animation_Now = Animation_Walk;

        }

    }
    public void TakeDamage(int Attack)//受到伤害
    {
        if (state == WolfState.Death) return;
       
        state = WolfState.attack;
        float value = Random.Range(0f, 1f);
        if (value < miss)
        {
            AudioSource.PlayClipAtPoint(BabyMiss, transform.position);
            hudtext.Add("Miss", Color.gray, 1);
        }
        else
        {
            Hp -= Attack;
            hudtext.Add("-"+Attack, Color.red, 1);
            StartCoroutine(ShowBabyRed());
            if (Hp <= 0)
            {
                state = WolfState.Death;
                spawn.Misnumber();
                BarNPC._instance.Killenemy();//任务中狼的数量
                ps.GetExp(exp);//得到经验
                SetItem(RangeID());
                //Invoke("GetItem",1f);
                StartCoroutine(GetItem(RangeID()));
                GameObject.Destroy(hudtextGO);
                Destroy(this.gameObject, 1.2f);

            }

        }
    }
  void   AutoAttack()//自动攻击

    {
        if (target != null)
        {
            PlayerState playerstate = target.GetComponent<PlayerAttack>().state;
            if(playerstate==PlayerState.Death)
            {
                target = null;
                state = WolfState.Idle;
            }
            //自动攻击
            float dis = Vector3.Distance(target.position, transform.position);
            if (dis >= maxdis)//停止自动攻击
            {
                target = null;
                state = WolfState.Idle;

            }
            else if (dis < mindis)
            {
                //自动攻击
                attack_timer += Time.deltaTime;
                transform.LookAt(target);
                anim.Play(aniname_normalattack);
              
                if(attack_timer > time_normalattack)

                {
                   
                    aniname_attack_now = Animation_Idle;
                }
                //    if (aniname_attack_now == aniname_normalattack)
                //    {
                //        if (attack_timer > time_normalattack)

                //        {
                //            //产生伤害
                //            aniname_attack_now = Animation_Idle;
                //        }
                //    }
                //    else if (aniname_attack_now == aniname_caryattack)
                //    {
                //        //产生伤害
                //        aniname_attack_now = Animation_Idle;
                //    }
                if (attack_timer > (1f / attack_rate))
                {
                    //  RandomAttack();
                    //产生伤害
                    anim.Play(aniname_normalattack);
                    target.GetComponent<PlayerAttack>().TakeDamage(attact);
                    attack_timer = 0;
                }
            }

               
            else
            {
                //朝着角色移动
                transform.LookAt(target);
                cc.SimpleMove(transform.forward * speed);
                anim.Play(Animation_Walk);
            }
        }
        else
            state = WolfState.Idle;
      }
    //void RandomAttack()//攻击方式
    //{
    //    float  value = Random.Range(0, 1);
    //    if (value <=crazyattack_rate )
    //    {
    //        aniname_attack_now = aniname_caryattack;
    //    }

    //    else
    //    {
    //        aniname_attack_now = aniname_normalattack;

    //    }
    //}
    IEnumerator ShowBabyRed()
    {
        if(Baby!=null)
        {
            Baby.GetComponent<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(1f);
            Baby.GetComponent<Renderer>().material.color = normol;
        }
       
    }
    //private void OnDestroy()
    //{
    //    spawn.Misnumber();
    //    BarNPC._instance.Killenemy();//得到金币
    //    ps.GetExp(120);
    //    GameObject.Destroy(hudtextGO);
    //}
    public int RangeID()
    {
       
        int b = Random.Range(2001, 2023);//随机得到物品
        return b;
    }
  public void SetItem(int value)
      {
        GameObject aa = gameObject.transform.parent.gameObject;
        Vector3 pos = transform.position;
        pos.z += Random.Range(-5, 5);
        pos.x += Random.Range(-5, 5);
         itemGo = GameObject.Instantiate(Item, pos, Quaternion.identity) as GameObject;
        // Go.transform.parent = spawn.transform;
        //  GameObject  = NGUITools.AddChild(aa, Item);      
        itemGo.GetComponent<Inventory_Item>().SetId(value);
    }
    IEnumerator GetItem(int value)
    {
        
        yield return new WaitForSeconds(1f);
        Inventory._instance.GetId(value, 1);
        Destroy(itemGo);
    }
}
