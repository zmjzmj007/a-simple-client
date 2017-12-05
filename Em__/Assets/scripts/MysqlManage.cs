using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;
using System;

public class MysqlManage 
{ 
    private static string connStr = "data source=localhost;database=unityem__;user id=root;password=zmj007xtzoo7;pooling=false;charset=utf8;";

    public static void Insert(string sql, params MySqlParameter[] ps)
    {
        MySqlConnection conn = Conn();
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddRange(ps);
        cmd.ExecuteNonQuery();
        conn.Close();
        Debug.Log("注册成功");
    }//插入数据

    public static void Compare(string username,string pwd)
    {
        Dictionary<string,string> info= GetDic();

        if(info.ContainsKey(username))//有这个用户名
        {
            //判断密码是否正确
            if(pwd==info[username])//密码正确
            {
                Debug.Log("登陆成功");
            }
            else
            {
                Debug.Log("密码错误，登陆失败");
            }
        }
        else
        {
            Debug.Log("无此用户名");
        }
        
    }//判断是否登陆成功

    public static bool CompareUsernameIsUsed(string username)
    {
        Dictionary<string,string> info= GetDic();

        if(info.ContainsKey(username))
        {
            return true;
        }
        else
        {
            return false;
        }
    }//判断用户名是否存在
    
    private static Dictionary<string,string> GetDic()
    {

        MySqlConnection conn = Conn();
        MySqlCommand cmd = new MySqlCommand("select * from userinfo", conn);

        MySqlDataReader reader = cmd.ExecuteReader();
        Dictionary<string, string> info = new Dictionary<string, string>();
        while (reader.Read())
        {
            info.Add(reader.GetString("uname"), reader.GetString("upassword"));
        }
        conn.Close();
        return info;
    }//根据数据库来造字典

    private static MySqlConnection Conn()
    {
        try
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            return conn;
        }
        catch (Exception error)
        {
            throw new Exception("无法连接到数据库" + error.Message.ToString());
        }

    }//检查连接数据库是否成功

}






