using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class GameObjectController : MonoBehaviour
{
    #region 登录信息
    public InputField Log_name;
    public InputField Log_password;
    #endregion
    #region 注册信息
    public InputField register_name;
    public InputField register_password;
    public InputField tworegister_password;
    public Text text;
    #endregion
    public Canvas login;//登录页面
    public Canvas register;//注册页面
    //提示信息
    public Text tip;
    private Dictionary<string, string> allAccount;
    private void Awake()
    {
        allAccount = new Dictionary<string, string>();
        ReadJson();
    }
    // Start is called before the first frame update
    void Start()
    {
      
        register.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    #region 登录按钮
    public void Loginbtn()
    {
        string username = Log_name.text;
        string password = Log_password.text;
        if (username == "" || password == "")
        {
            tip.text = "用户名或密码为空,请重新输入";
            return;
        }
        else if(username=="123"&&password=="123")
        {
            tip.text = "登录成功！！";
            Login();
            return;
        }
        else if (allAccount.ContainsKey(username))
        {
            if (password == allAccount[username])
            {
                tip.text = "登录成功！！";
                Login();
                return;
            }
            else
            {
                tip.text = "密码错误！！";
                return;
            }
        }
        else
        {
            tip.text = "用户名错误";
        }
        Debug.Log(tip.text);
    }
    #endregion
    #region 登录页面注册按钮
    public void returnlogin()
    {
        login.gameObject.SetActive(false);
        register.gameObject.SetActive(true);
    }
    #endregion
    #region 注册页面注册按钮
    public void Signupbtn()
    {
        string name = register_name.text;
        string password = register_password.text;
        string password2 = tworegister_password.text;
        Debug.Log(name);
        if (password == "" || name == "")
        {
            print("用户名和密码不能为空!!");
            text.text = "用户名和密码不能为空!!!";
            return;

        }
        else if (allAccount.ContainsKey(name))
        {
            //如果用户名和已知的重复时
            print("该用户名已经被用了,请换一个吧!");
            text.text = "该用户名已经被用了,请换一个吧!";
            return;
        }
        else if (password != password2)
        {
            //如果两次输入密码不一致时
            print("两次输入的密码不一样");
            text.text = "两次输入的密码不一样!";
            return;
        }
        else
        {
            text.text = "注册成功!!";
            allAccount.Add(name, password);
            Save();
            print("注册成功!!");
           
          
        }


    }
    #endregion
    #region 注册返回按钮
    public void Return()
        {
        login.gameObject.SetActive(true);
        register.gameObject.SetActive(false);
    }
    #endregion
    #region 退出游戏按钮
    public void Quit()
    {
        Application.Quit();
    }
    #endregion
    //将数据存入Json文件中
    public void Save()
    {
        ////声明一个字符串类型来储存文件的位置

        string path = Application.streamingAssetsPath + "/RegisteJson.txt";

        FileInfo file = new FileInfo(path);//实例化
        if (!file.Exists)
        {
            StreamWriter sw = File.CreateText(path);
        }
        //开始写入数据
        //创建一个写入对象,用于写入数据
        StreamWriter writer = new StreamWriter(path);
        //将字典内容转换成Json可识别内容
        JsonData jd = JsonMapper.ToJson(allAccount);
        //写入数据
        writer.Write(jd);
        //关闭写入器
        writer.Close();
        //刷新储存文件
        file.Refresh();

        print("注册成功!!");
        return;
    }
    //登录
    public void Login()
    {
        Application.LoadLevel(1);
    }
  
    public void ReadJson()//读取文本里的用户信息
    {
        string path = Application.streamingAssetsPath + "/RegisteJson.txt";
        FileInfo file = new FileInfo(path);
        if (!file.Exists)
        {
            Debug.LogError("错误");
        }
        string all = File.ReadAllText(path);
        Debug.Log(all);
        allAccount = JsonMapper.ToObject<Dictionary<string, string>>(all);
    }

}


