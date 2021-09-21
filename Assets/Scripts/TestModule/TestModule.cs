using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestModule : ModuleBase
{
    public override uint ModuleID { get { return (int)ModuleIDMap.Test2; } }

    protected override void RegisterComponent()
    {
        Register<TestPanel>();
        Register<TestEventComponent>();
    }

    public override void EnterModule()
    {
        TestAllSingletonModuleComponent();

        UIManager.CreatePanel<TestPanel>(this);
    }

}
