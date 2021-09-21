using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainModule : ModuleBase
{
    public override uint ModuleID { get { return (int)ModuleIDMap.Main; } }

    protected override void RegisterComponent()
    {
        Register<PlayerPanel>();
        Register<PlayerPanel2>();
        Register<PlayerPanel3>();
        Register<PlayerEventContorl1>();
        Register<PlayerEventContorl2>();
        Register<PlayerDataComponent>();
        Register<PlayerGlobalDataComponent>();
    }

    protected override void OnInstantiateModule()
    {
        // KEY最好用单独的文件记录
        ModuleBase.Blackboard.AddVariables("Player", GetDataComponent<PlayerGlobalDataComponent>());

        EventCall.CallEvent(EventID.ShowPlayerInfo);

        TestAllSingletonModuleComponent();
    }


}
