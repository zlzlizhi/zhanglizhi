namespace zFrame.UI
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.Events;
    public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public float maxRadius = 100; //Handle 移动最大半径
        [SerializeField, EnumFlags]//EnumFlags 全选
        Direction activatedAxis = (Direction)(-1); //选择激活的轴向
        public GameObject player;
        public bool isRun=false;
        [SerializeField]
        bool showDirection = true;
        public JoystickEvent OnValueChanged = new JoystickEvent(); //Update驱动的事件
        public bool IsDraging { get { return fingerId != int.MinValue; } } //可供外部断言当前摇杆状态

        private RectTransform backGround, handle, direction; //摇杆背景、摇杆手柄、方向指引
        private Vector2 joystickValue = Vector2.zero;
        private Vector3 backGroundOriginLocalPostion,backGroundPressedPostion; //Background 的位置

        public PlayerAttack playattack;
       
        private bool isrun = false;
        private bool isidle = false;
        private int fingerId = int.MinValue; //当前触发摇杆的 pointerId ，预设一个永远无法企及的值
        [System.Serializable]
        public class JoystickEvent : UnityEvent<Vector2> { }
        [System.Flags]
        public enum Direction
        {
            Horizontal = 1 << 0,  //二进制 1<<0:1是二进制中的1,也就是0001,<<是左移符号,0是左移几个
            Vertical = 1 << 1     //1<<1  就是0001左移1位就是0010 ,UIRectCornerTopRight我们就传入十进制的2;
        }


        private void Awake()
        {
            backGround = transform.Find("BackGround") as RectTransform;
            handle = transform.Find("BackGround/Handle") as RectTransform;
            direction = transform.Find("BackGround/Direction") as RectTransform;
            direction.gameObject.SetActive(false);
            backGroundOriginLocalPostion = backGround.localPosition;
           
           



        }
        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playattack = player.GetComponent<PlayerAttack>();
        }

        void Update()
        {
           if(playattack.state==PlayerState.Death)
            {
                player.GetComponent<Animation>().Play("Death");
            }
           
           if(playattack.state==PlayerState.ControlWalk)
           {

              if(isidle)
             {
                player.GetComponent<Animation>().Play("Idle");
             }
              if(isrun)
             {
                player.GetComponent<Animation>().Play("Run");
             }
            }
            else if (playattack.state == PlayerState.NormalAttack)
            {
                if (playattack.attack_state == AttackState.Moving)
                {
                    player.GetComponent<Animation>().Play("Run");
                }
                if (playattack.attack_state == AttackState.Attack)
                {
                    player.GetComponent<Animation>().Play(playattack.aniname_now);
                }
            }

            if (!IsDraging) return;//仅当摇杆拖拽时驱动事件
            joystickValue.x = handle.anchoredPosition.x / maxRadius;
            joystickValue.y = handle.anchoredPosition.y / maxRadius;
            OnValueChanged.Invoke(joystickValue);
        }

        // 摇杆被触发，初始化摇杆
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (eventData.pointerId<-1||IsDraging) return; //适配 Touch：只响应一个Touch；适配鼠标：只响应左键
            fingerId = eventData.pointerId;
            backGroundPressedPostion = new Vector3() // As it is too long for trinocular operation so I create Vector3 like this.
            {
                x = eventData.position.x,
                y = eventData.position.y,
                z = (null == eventData.pressEventCamera) ? backGround.position.z :
                 eventData.pressEventCamera.WorldToScreenPoint(backGround.position).z //无奈，这个坐标转换不得不做啊,就算来来回回的折腾。
            };
            backGround.position = (null == eventData.pressEventCamera) ? backGroundPressedPostion : eventData.pressEventCamera.ScreenToWorldPoint(backGroundPressedPostion);
        }

        // 当鼠标拖拽时
        void IDragHandler.OnDrag(PointerEventData eventData)
        {
           
            if (fingerId != eventData.pointerId) return; //适配 Touch：只响应一个Touch
            Vector2 direction = eventData.position - (Vector2)backGroundPressedPostion; //得到方位盘中心指向光标的向量
            float radius = Mathf.Clamp(Vector3.Magnitude(direction), 0, maxRadius); //获取并锁定向量的长度 以控制 Handle 半径
            Vector2 localPosition = new Vector2()
            {
                x = (0 != (activatedAxis & Direction.Horizontal)) ? (direction.normalized * radius).x : 0, //确认是否激活水平轴向
                y = (0 != (activatedAxis & Direction.Vertical)) ? (direction.normalized * radius).y : 0       //确认是否激活水平轴向，激活就搞事情
            };
            handle.localPosition = localPosition;      //更新 Handle 位置
            UpdateDirectionArrow(localPosition);

            isrun = true;
            isidle = false;
               
          
           
          
        }

        // 更新指向器的朝向
        private void UpdateDirectionArrow(Vector2 position)
        {
            if (showDirection && position.magnitude != 0)
            {
                direction.gameObject.SetActive(true);
                direction.localEulerAngles = new Vector3(0, 0, Vector2.Angle(Vector2.right, position) * (position.y > 0 ? 1 : -1));
            }
            else if(direction.gameObject.activeSelf)
            {
                direction.gameObject.SetActive(false);
            }
        }

        // 当鼠标停止拖拽时,重置数据
        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
          
            if (fingerId != eventData.pointerId) return;  //适配 Touch：只响应一个Touch
            fingerId = int.MinValue;
            direction.gameObject.SetActive(false);
            backGround.localPosition = backGroundOriginLocalPostion;
            handle.localPosition = Vector3.zero;
            isrun = false;
            isidle =true;
          
         
                
           
        }
    }
} 
