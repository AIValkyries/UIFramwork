using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventContorl1 : IEventComponent
{
    public ModuleBase owner { get; set; }

    public List<int> GetListener()
    {
        return new List<int>()
        {
            EventID.ShowPlayerInfo,
            EventID.UpdateAll,
            EventID.SubmitInfo,
            EventID.UpdatePlayerName,  // 可以在不同的 Event组件中同时注册一个事件
        };
    }

    public void Notify(int op, object param = null)
    {
        switch (op)
        {
            case EventID.ShowPlayerInfo:
                ShowAll();
                break;
            case EventID.UpdateAll:
                UpdateAll();
                break;
            case EventID.SubmitInfo:
                ButtonSubmitEvent();
                break;
            case EventID.UpdatePlayerName:
                UpdatePlayerName1();
                break;
        }
    }

    void UpdatePlayerName1()
    {
        PlayerGlobalDataComponent playerGlobalData = owner.GetDataComponent<PlayerGlobalDataComponent>();
        PlayerPanel panel = owner.GetPanelComponent<PlayerPanel>();

        panel.UpdatePlayerName1(playerGlobalData);
    }

    void ShowAll()
    {
        PlayerPanel panel = owner.GetPanelComponent<PlayerPanel>();
        PlayerDataComponent playerData = owner.GetDataComponent<PlayerDataComponent>();

        panel.ShowPlayerInfo(playerData);
    }

    void UpdateAll()
    {
        PlayerPanel panel = owner.GetPanelComponent<PlayerPanel>();
        PlayerDataComponent playerData = owner.GetDataComponent<PlayerDataComponent>();

        playerData.Mail = "99999999@qq.com";
        playerData.Sex = "女";
        playerData.Hometown = "冥王星";

        panel.ShowPlayerInfo(playerData);
    }


    void ButtonSubmitEvent()
    {
        Debug.Log("我收到了来自界面的按钮提交信息");
    }

}
