using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventContorl2 : IEventComponent
{
    public ModuleBase owner { get; set; }

    public List<int> GetListener()
    {
        return new List<int>()
        {
            EventID.UpdatePlayerName,
            EventID.UpdatePlayerAge,
            EventID.CallTestModule,
        };
    }

    public void Notify(int op, object param = null)
    {
        switch (op)
        {
            case EventID.UpdatePlayerName:
                UpdatePlayerName2();
                break;
            case EventID.UpdatePlayerAge:
                UpdatePlayerAge();
                break;
            case EventID.CallTestModule:
                CallTestModule();
                break;
        }
    }

    void UpdatePlayerAge()
    {
        PlayerPanel panel = owner.GetPanelComponent<PlayerPanel>();
        PlayerDataComponent playerData = owner.GetDataComponent<PlayerDataComponent>();
        playerData.Age = 20;
        panel.UpdatePlayerAge(playerData);
    }

    void UpdatePlayerName2()
    {
        PlayerGlobalDataComponent playerGlobalData = owner.GetDataComponent<PlayerGlobalDataComponent>();
        PlayerPanel panel = owner.GetPanelComponent<PlayerPanel>();

        panel.UpdatePlayerName2(playerGlobalData);
    }


    void CallTestModule()
    {
        ModuleBase moduleBase = ModuleBaseManager.CallModule(ModuleIDMap.Test2);
        moduleBase.TestAllSingletonModuleComponent();
    }


}
