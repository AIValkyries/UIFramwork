using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEventComponent : IEventComponent
{
    public ModuleBase owner { get; set; }

    public List<int> GetListener()
    {
        return new List<int>()
        {
            EventID.UpdatePlayerName
        };
    }

    public void Notify(int op, object param = null)
    {
        PlayerGlobalDataComponent component = (PlayerGlobalDataComponent)ModuleBase.Blackboard.GetobjectVariables("Player");

        if (op == EventID.UpdatePlayerName) 
        {
            TestPanel testPanel = owner.GetPanelComponent<TestPanel>();
            testPanel.ShowPlayerInfo(component.Name);
        }
    }



}
