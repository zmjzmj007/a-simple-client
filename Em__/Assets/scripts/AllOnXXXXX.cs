using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class AllOnXXXXX : MonoBehaviour {
    public GameObject zhuce;
    public GameObject mainmenu;
    public GameObject denglu;
    public GameObject shezhi;
    public UIInput username;
    public UIInput pwd;
    public UIInput conPWD;
    public UIInput email;
    public UIInput phone;
    public UIInput LandingUsername;
    public UIInput LandingPWD;
    public UIButton resgiterButton;
    public UILabel isUsed;
    public UILabel checkpwd;
    private AudioSource bgm;
    private string _pwd;
    private string _conpwd;
    private string landingUsername;
    private string landingPWD;
    UserInfo info = new UserInfo();

    private void Start()
    {
        Screen.fullScreen = true;
        bgm = GetComponent<AudioSource>();
    }

    public void OnButtonRegisteredDown()
    {
        mainmenu.SetActive(false);
        zhuce.SetActive(true);
    }

    public void OnButtonRegisteredBackMenuDown()
    {
        zhuce.SetActive(false);
        mainmenu.SetActive(true);
    }

    public void OnButtonLandingDown()
    {
        mainmenu.SetActive(false);
        denglu.SetActive(true);
    }

    public void OnButtonLandingBackDown()
    {
        denglu.SetActive(false);
        mainmenu.SetActive(true);
    }

    public void OnButtonSetupDown()
    {
        mainmenu.SetActive(false);
        shezhi.SetActive(true);
    }

    public void OnButtonSetupBackDown()
    {
        shezhi.SetActive(false);
        mainmenu.SetActive(true);
    }

    public void OnSoundValueChange()
    {
        Camera.main.GetComponent<AudioSource>().volume = UIProgressBar.current.value;
    }

    public void OnisFull(bool isOn)
    {
        Screen.fullScreen = isOn;
    }

    public void OnButtonWeiXinDown()
    {
        Application.OpenURL("https://wx.qq.com/");
    }

    public void OnButtonWeiBoDown()
    {
        Application.OpenURL("www.weibo.com/");
    }

    public void GetUsername()//获取注册用户名
    {
        
        if( MysqlManage.CompareUsernameIsUsed(username.value))//若已经使用了用户名
        {
            //labe显示
            isUsed.enabled=true;
            resgiterButton.enabled = false;
            
        }
        else
        {
            //labe显示
            isUsed.enabled=false;
            resgiterButton.enabled = true;
            info.username = username.value;
        }



    }

    public void GetEmail()//获取注册email
    {
        if (_conpwd == _pwd)
        {
            info.pwd = _pwd;
        }
        else
        {
            Debug.Log("密码输入不一样");
        }
        info.email = email.value;
    }

    public void GetPhone()//获取注册手机号
    {
        int result;
        if( int.TryParse(phone.value,out result))
        {
            info.phone = result;
        }   
    }

    public void GetPWD()//获取注册密码
    {
        
        if(pwd.value.Length<=6)
        {
            checkpwd.enabled = true;
        }
        else
        {
            checkpwd.enabled = false;
            _pwd = pwd.value;
        }
    }

    public void GetConPWD()//获取注册确认密码
    {
        _conpwd = conPWD.value;
    }

    public void Registering()//注册页面的注册按钮
    {
        string sql = "insert into userinfo(uname,upassword,uemail,uphonenumber) values(@name,@pwd,@email,@phone)";
        MysqlManage.Insert(sql, new MySqlParameter("@name", info.username), new MySqlParameter("@pwd", info.pwd), new MySqlParameter("@email", info.email), new MySqlParameter("@phone", info.phone));
    }

    public void GetLandUsername()//登陆界面的获取用户名
    {
        landingUsername = LandingUsername.value;
    }

    public void GetLandPWD()//登陆界面的获取密码
    {
        landingPWD = LandingPWD.value;
    }

    public void Landing()//登陆界面的登录按钮
    {
        Debug.Log("landing");
        MysqlManage.Compare(landingUsername, landingPWD);
    }

    public void OnButtonExit()
    {
        Application.Quit();
    }

    
}
