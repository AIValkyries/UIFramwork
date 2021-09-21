using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

/// <summary>
/// 可以进行模块之间的通信
/// </summary>
public class EventCallComponet : ISingletonModuleComponent
{
    /// <summary>
    /// Key:事件ID
    /// Value:事件接口
    /// </summary>
    Dictionary<int, List<IEventComponent>> eventComponentMaps = new Dictionary<int, List<IEventComponent>>();

    /// <summary>
    /// 
    /// </summary>
    Dictionary<Type, IEventComponent> typeEvents = new Dictionary<Type, IEventComponent>();

    public void TestToString()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();

        System.Text.StringBuilder str0 = new System.Text.StringBuilder();
        System.Text.StringBuilder str1 = new System.Text.StringBuilder();

        var itr0 = eventComponentMaps.Keys.GetEnumerator();
        str0.Append("\n");
        while (itr0.MoveNext())
        {
            str0.Append("  事件ID=" + itr0.Current + "\n");

            str0.Append("  {\n");

            for (int i = 0; i < eventComponentMaps[itr0.Current].Count; i++)
            {
                if (i == eventComponentMaps[itr0.Current].Count - 1)
                {
                    str0.Append("    " + eventComponentMaps[itr0.Current][i].GetType().Name);
                }
                else
                {
                    str0.Append("    " + eventComponentMaps[itr0.Current][i].GetType().Name + ",\n");
                }
            }

            str0.Append("\n  }\n");
        }
        itr0.Dispose();

        var itr1 = typeEvents.Keys.GetEnumerator();
        while (itr1.MoveNext())
        {
            str1.Append("事件Type=" + itr1.Current + "\n");
        }
        itr1.Dispose();

        builder.Append(str0 + "\n");
        builder.Append(str1 + "\n");

        Debug.Log(builder.ToString());
    }

    public void Initialize()
    {

    }

    /// <summary>
    ///  
    /// </summary>
    public void Registered(Type type, ModuleBase moduleBase)
    {
        IEventComponent eventComponent = (IEventComponent)Activator.CreateInstance(type);
        if (!typeEvents.ContainsKey(type))
            typeEvents.Add(type, eventComponent);

        eventComponent.owner = moduleBase;

        List<int> events = eventComponent.GetListener();
        if (events == null)
            return;

        for (int i = 0; i < events.Count; i++) 
        {
            if (!eventComponentMaps.ContainsKey(events[i]))
            {
                eventComponentMaps.Add(events[i],new List<IEventComponent>());
            }

            if (!eventComponentMaps[events[i]].Contains(eventComponent))
            {
                eventComponentMaps[events[i]].Add(eventComponent);
            }
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public void Uninstall(Type type)
    {
        if (!typeEvents.ContainsKey(type))return;

        var eventComponent = typeEvents[type];

        List<int> events = eventComponent.GetListener();
        if (events == null) return;

        for (int i = 0; i < events.Count; i++)
        {
            if (eventComponentMaps.ContainsKey(events[i]))
            {
                if (eventComponentMaps[events[i]].Contains(eventComponent))
                {
                    eventComponentMaps[events[i]].Remove(eventComponent);
                }

                if (eventComponentMaps[events[i]].Count <= 0) 
                {
                    eventComponentMaps.Remove(events[i]);
                }
            }
        }
    }

    public void CallEvent(int eventID, params object[] arags)
    {
        if (eventComponentMaps.ContainsKey(eventID))
        {
            var itr = eventComponentMaps[eventID].GetEnumerator();
            while (itr.MoveNext())
            {
                itr.Current.Notify(eventID, arags);
            }
            itr.Dispose();
        }
    }

    public void Clear()
    {
        eventComponentMaps.Clear();
    }


}
