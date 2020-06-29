using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{
    ControlWalk ,  // 控制移动
    NormalAttack,     //  普通攻击
    SkillAttack,     //  技能攻击
    Death
}
public enum AttackState {//攻击时候的状态
    Moving,
    Idle,
    Attack
}

public class Tags
{
    public static string enemy = "Enemy";
    public static string player = "Player";
}


public class PlayerAttack : MonoBehaviour
{
    public PlayerState state = PlayerState.ControlWalk;

    public string aniname_normalattack;//普通攻击动画
    public  float time_normalattack;//普通攻击时间
    public string aniname_idle;
    public string aniname_now;
    public  float timer = 0;
    public float rate_normalattack = 1f;
    public float min_distance = 5;//默认攻击的最小距离
    private Transform target_normalattack;
    private  CharacterControllerMove0 playmove;

    public AudioClip playerMiss;
    private float miss = 0.05f;
    private GameObject hudtextFollow;//主角下的HUDtext
    private GameObject hudtextGO;//接收预制体的物体
    public GameObject hudtextPrefab;//hud的预制体
    private HUDText hudtext;
    private UIFollowTarget follwTarget;

    public AttackState attack_state = AttackState.Idle;
    private bool isshowEff = false;
    public GameObject effprefeb;
    public PlayerStatus ps;

    public GameObject body;
    private Color normal;
    public string aniname_death;

    public GameObject[] efxArray;
    private Dictionary<string, GameObject> efxDict = new Dictionary<string, GameObject>();

    public bool isLockingTarget = false;//是否正在选择目标
    private SkillInfo info = null;
    public GameObject Deathsprite;



    private void Awake()
    {
        playmove = gameObject.GetComponent<CharacterControllerMove0>();
        hudtextFollow = transform.Find("HUDText").gameObject;
        ps = gameObject.GetComponent<PlayerStatus>();

        normal = body.GetComponent<Renderer>().material.color;

        foreach (GameObject go in efxArray)
        {
            efxDict.Add(go.name, go);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        //HUD
        hudtextGO = NGUITools.AddChild(HUDTextParent._instance.gameObject, hudtextPrefab);
        hudtext = hudtextGO.GetComponent<HUDText>();
        follwTarget = hudtextGO.GetComponent<UIFollowTarget>();
        follwTarget.target = hudtextFollow.transform;
        follwTarget.gameCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //射线检测
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray, out hitInfo);
            if(isCollider&&hitInfo.collider.tag=="Enemy")//当我们点击到一个敌人时
            {
                timer = 0;
                target_normalattack = hitInfo.collider.transform;
                 state = PlayerState.NormalAttack;//攻击模式
                isshowEff = false;
            }
            else{
                state = PlayerState.ControlWalk;//行走动画
               
                target_normalattack = null;
              
            }
        }
        if (state == PlayerState.NormalAttack)
        {
            if (target_normalattack == null)
            {
                state = PlayerState.ControlWalk;
            }
            else
            {



                float dis = Vector3.Distance(transform.position, target_normalattack.position);
                if (dis <= min_distance)
                {

                    //攻击
                    transform.LookAt(target_normalattack.position);
                    attack_state = AttackState.Attack;//播放动画


                    if (isLockingTarget)
                    {
                        OnLockTarget();//单个攻击
                        return;
                    }


                    timer += Time.deltaTime;
                    if (timer >= time_normalattack)
                    {
                        aniname_now = aniname_idle;
                        if (isshowEff == false)
                        {
                            isshowEff = true;
                            GameObject.Instantiate(effprefeb, target_normalattack.position, Quaternion.identity);
                            
                            target_normalattack.GetComponent<WolfBaby>().TakeDamage(GetAttack());
                        }
                    }
                    if (timer >= (1f / rate_normalattack))
                    {
                        isshowEff = false;
                        timer = 0;
                        aniname_now = aniname_normalattack;
                    }


                }
                else
                {
                    //走向敌人
                    attack_state = AttackState.Idle;
                    playmove.SimpleMove(target_normalattack.position);
                }
            }
        }
        else if(state==PlayerState.Death)
        {
            return;
        }
       
    }
    int GetAttack()
    {
        return ps.attact + ps.attact_plus + EquipmentUI._instance.attack;
    }
    public void TakeDamage(int attack)
    {
        float def = EquipmentUI._instance.def + ps.def + ps.def_plus;
        float temp = attack * (200-def) / 200;
        if (temp < 1)
            temp = 1;
        float value = Random.Range(0f, 1f);
        if (value < miss)
        {
            AudioSource.PlayClipAtPoint(playerMiss, transform.position);
            hudtext.Add("Miss", Color.gray, 1);
        }
        else
        {
            hudtext.Add("-" + attack, Color.red, 1);
            ps.hp_remain -= (int)temp;
            HeadstatusUI._instance.UpdateShow();//更新血条；
            if (ps.hp_remain<=0)
            {
                state = PlayerState.Death;
                Deathsprite.SetActive(true);
                GameObject.Destroy(gameObject);
                GameObject.Destroy(hudtextGO);
            }

         }
        }

    IEnumerator ShowBodyRed()
    {
        body.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(1f);
        body.GetComponent<Renderer>().material.color = normal;
    }

    void OnDestroy()
    {
        GameObject.Destroy(hudtextGO);

    }

    public void UseSkill(SkillInfo info)
    {
        if (ps.herotype == HeroType.Magician)
        {
            if (info.applicableRole == ApplicableRole.Swordman)
            {
                return;
            }
        }
        if (ps.herotype == HeroType.Swordman)
        {
            if (info.applicableRole == ApplicableRole.Magician)
            {
                return;
            }
        }
        switch (info.applyType)
        {
            case ApplyType.Passive:
                StartCoroutine(OnPassiveSkillUse(info));
                break;
            case ApplyType.Buff:
                StartCoroutine(OnBuffSkillUse(info));
                break;
            case ApplyType.SingleTarget:

                state = PlayerState.SkillAttack;
                //CursorManager._instance.SetLockTarget();
                isLockingTarget = true;
                this.info = info;
               // OnLockTarget();
            //   OnSingleTargetSkillUse(info);
                break;
            case ApplyType.MultiTarget:
                OnMultiTargetSkillUse(info);
                break;
        }

    }
    //处理增益技能
    IEnumerator OnPassiveSkillUse(SkillInfo info)
    {
        state = PlayerState.SkillAttack;
        GetComponent<Animation>().CrossFade(info.aniname);
        yield return new WaitForSeconds(info.anitime);
       
        state = PlayerState.ControlWalk;
        int hp = 0, mp = 0;
        if (info.applyProperty == ApplyProperty.HP)
        {
            hp = info.applyValue;
        }
        else if (info.applyProperty == ApplyProperty.MP)
        {
            mp = info.applyValue;
        }

        ps.GetDrug(hp, mp);
        //实例化特效
        GameObject prefab = null;
        efxDict.TryGetValue(info.efx_name, out prefab);
        GameObject.Instantiate(prefab, transform.position, Quaternion.identity);
        
    }
    //处理增强技能
    IEnumerator OnBuffSkillUse(SkillInfo info)
    {
        state = PlayerState.SkillAttack;
        GetComponent<Animation>().CrossFade(info.aniname);
        yield return new WaitForSeconds(info.anitime);
        state = PlayerState.ControlWalk;

        //实例化特效
        GameObject prefab = null;
        efxDict.TryGetValue(info.efx_name, out prefab);
        GameObject.Instantiate(prefab, transform.position, Quaternion.identity);
       
        switch (info.applyProperty)
        {
            case ApplyProperty.Attack:
                ps.attact *= (int)(info.applyValue / 100f);
                break;
            case ApplyProperty.AttackSpeed:
                rate_normalattack *= (info.applyValue / 100f);
                break;
            case ApplyProperty.Def:
                ps.def *= (int)(info.applyValue / 100f);
                break;
            case ApplyProperty.Speed:
                //move.speed *= (info.applyValue / 100f);
                break;
        }
        yield return new WaitForSeconds(info.applyTime);
        switch (info.applyProperty)
        {
            case ApplyProperty.Attack:
                ps.attact /= (int)(info.applyValue / 100f);
                break;
            case ApplyProperty.AttackSpeed:
                rate_normalattack /= (info.applyValue / 100f);
                break;
            case ApplyProperty.Def:
                ps.def /= (int)(info.applyValue / 100f);
                break;
            case ApplyProperty.Speed:
                //move.speed /= (info.applyValue / 100f);
                break;
        }
    }
    ////准备选择目标
    //void OnSingleTargetSkillUse(SkillInfo info)
    //{
    //    Debug.Log("释放" + info.name);
    //    state = PlayerState.SkillAttack;

    //    isLockingTarget = true;
    //    this.info = info;

    //    CursorManager._instance.SetLockTarget();
    //}
    //选择目标完成，开始技能的释放
    void OnLockTarget()
    {
        isLockingTarget = false;
        switch (info.applyType)
        {
            case ApplyType.SingleTarget:
                StartCoroutine(OnLockSingleTarget());
                break;
            //case ApplyType.MultiTarget:
            //    StartCoroutine(OnLockMultiTarget());
            //    break;
        }
    }

    IEnumerator OnLockSingleTarget()
    {//target_normalattack
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hitInfo;
        //bool isCollider = Physics.Raycast(ray, out hitInfo);
        if (target_normalattack!=null)//isCollider && hitInfo.collider.tag == Tags.enemy
        {//选择了一个敌人
            Debug.Log(target_normalattack);
            GetComponent<Animation>().CrossFade(info.aniname);
            yield return new WaitForSeconds(info.anitime);
            //state = PlayerState.ControlWalk;
            //实例化特效
            GameObject prefab = null;
            efxDict.TryGetValue(info.efx_name, out prefab);
            GameObject.Instantiate(prefab, target_normalattack.position, Quaternion.identity);

            target_normalattack.GetComponent<WolfBaby>().TakeDamage((int)(GetAttack() * (info.applyValue / 100f)));
            
            state = PlayerState.NormalAttack;
        }
        else
        {
           
            state = PlayerState.NormalAttack;
        }
        //CursorManager._instance.SetNormal();
    }

    void OnMultiTargetSkillUse(SkillInfo info)
    {
        state = PlayerState.SkillAttack;
        //CursorManager._instance.SetLockTarget();
        //isLockingTarget = true;
        
            //球形射线检测,得到主角半径2米范围内所有的物件
            Collider[] cols = Physics.OverlapSphere(this.transform.position, 2f);
            //判断检测到的物件中有没有Enemy
            if (cols.Length > 0)
            {

                for (int i = 0; i < cols.Length; i++)
                {
           
                    //判断是否是怪物
                    if (cols[i].tag.Equals("Enemy"))
                    {
                    GetComponent<Animation>().CrossFade(info.aniname);
                    //实例化特效
                    GameObject prefab = null;
                    efxDict.TryGetValue(info.efx_name, out prefab);
                    GameObject go = GameObject.Instantiate(prefab, cols[i].transform.position + Vector3.up * 0.5f, Quaternion.identity) as GameObject;
                    cols[i].GetComponent<WolfBaby>().TakeDamage((int)(GetAttack() * (info.applyValue / 100f)));
                    Debug.Log("范围攻击");
                }
            }
            }
        else
        {

            state = PlayerState.NormalAttack;
        }

        this.info = info;
    }
    //IEnumerator OnLockMultiTarget()
    //{
    //    CursorManager._instance.SetNormal();
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hitInfo;
    //    bool isCollider = Physics.Raycast(ray, out hitInfo, 11);
    //    if (isCollider)
    //    {
    //        GetComponent<Animation>().CrossFade(info.aniname);
    //        yield return new WaitForSeconds(info.anitime);
    //        state = PlayerState.ControlWalk;

    //        //实例化特效
    //        GameObject prefab = null;
    //        efxDict.TryGetValue(info.efx_name, out prefab);
    //        GameObject go = GameObject.Instantiate(prefab, hitInfo.point + Vector3.up * 0.5f, Quaternion.identity) as GameObject;
    //        go.GetComponent<MagicSphere>().attack = GetAttack() * (info.applyValue / 100f);
    //    }
    //    else
    //    {
    //        state = PlayerState.ControlWalk;
    //    }

    //}
}
