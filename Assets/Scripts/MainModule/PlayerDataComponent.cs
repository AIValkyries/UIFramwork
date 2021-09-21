using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataComponent : IDataComponent
{
    public int Age { get; set; }
    public string Hometown { get; set; }
    public string Mail { get; set; }
    public string Sex { get; set; }

    /// <summary>
    /// 爱好
    /// </summary>
    public List<string> Hobby { get; set; }

    public void Instantiate()
    {
        Age = 10;
        Hometown = "火星";
        Mail = "123456789@qq.com";
        Sex = "男";
        Hobby = new List<string>();
        Hobby.Add("二次元");
        Hobby.Add("游戏");
        Hobby.Add("哲学");
        Hobby.Add("艺术");
    }

    public IDataComponent Clone()
    {
        PlayerDataComponent player = new PlayerDataComponent();

        player.Age = Age;
        player.Hometown = Hometown;
        player.Mail = Mail;
        player.Sex = Sex;
        player.Hobby = Hobby;

        return player;
    }

}
