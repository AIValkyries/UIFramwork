using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonModule : ModuleBase
{
    public override uint ModuleID { get { return (int)ModuleIDMap.Common; } }

    protected override void RegisterComponent()
    {
        AddSingletonModuleComponent<EventCallComponet>();
        AddSingletonModuleComponent<ModuleBlackboard>();
        AddSingletonModuleComponent<UIManagerComponet>();
        AddSingletonModuleComponent<CoroutineMonoComponent>();

        Register<MsgBox>();
    }

    public override void EnterModule()
    {
        Debug.Log("我是通用的模块一般我都是最开始初始化的!");
    }


    public static MsgBox ShowMsgBox(MsgData data)
    {
        ModuleBase moduleBase = ModuleBaseManager.GetModuleBase(ModuleIDMap.Common);
        Blackboard.RemoveobjectVariables(BlackboardKey.MSG_DATA_KEY);
        Blackboard.AddVariables(BlackboardKey.MSG_DATA_KEY, data);

        return UIManager.CreatePanel<MsgBox>(moduleBase);
    }


}
