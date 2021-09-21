using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public abstract class ModuleBase
{
    /// <summary>
    /// Key:type
    /// Value：单例组件
    /// 这是全局性的，不能重复存在
    /// </summary>
    static Dictionary<Type, ISingletonModuleComponent> singletionComponents = new Dictionary<Type, ISingletonModuleComponent>();
    static Dictionary<Type, ISingletonDataComponent> singletonDataComponents = new Dictionary<Type, ISingletonDataComponent>();

    Dictionary<Type, IUpdateComponent> updateComponents = new Dictionary<Type, IUpdateComponent>();
    Dictionary<Type, IDataComponent> dataComponents = new Dictionary<Type, IDataComponent>();
    Dictionary<Type, IContorlComponent> contorlComponents = new Dictionary<Type, IContorlComponent>();

    List<Type> allEventTypes = new List<Type>();

    public abstract uint ModuleID { get; }

    public ModuleBase()
    {
        RegisterComponent();  // 注册组件
    }

    #region 单例组件

    public void TestAllSingletonModuleComponent()
    {
        var itr = singletionComponents.Values.GetEnumerator();
        while (itr.MoveNext())
        {
            MethodInfo methodInfo = itr.Current.GetType().GetMethod("TestToString",
                BindingFlags.Public |
                BindingFlags.Instance);

            if (methodInfo != null) 
                methodInfo.Invoke(itr.Current, null);
        }
        itr.Dispose();
    }

    public static CoroutineMonoComponent Coroutine
    {
        get { return (CoroutineMonoComponent)GetSingletonComponent<CoroutineMonoComponent>(); }
    }

    public static UIManagerComponet UIManager
    {
        get { return (UIManagerComponet)GetSingletonComponent<UIManagerComponet>(); }
    }

    public static ModuleBlackboard Blackboard
    {
        get { return (ModuleBlackboard)GetSingletonComponent<ModuleBlackboard>(); }
    }

    public static EventCallComponet EventCall
    {
        get { return (EventCallComponet)GetSingletonComponent<EventCallComponet>(); }
    }

    public static void AddSingletonModuleComponent<T>()where T:ISingletonModuleComponent,new()
    {
        if (!singletionComponents.ContainsKey(typeof(T)))
        {
            T obj = new T();
            obj.Initialize();
            singletionComponents.Add(typeof(T), obj);
        }
    }

    public static void RemoveSingletonModuleComponent<T>()where T:ISingletonModuleComponent
    {
        if (singletionComponents.ContainsKey(typeof(T)))
        {
            singletionComponents.Remove(typeof(T));
        }
    }

    public static T GetSingletonComponent<T>() where T : ISingletonModuleComponent
    {
        if (singletionComponents.ContainsKey(typeof(T)))
        {
            return (T)singletionComponents[typeof(T)];
        }
        return default(T);
    }


    public static void AddSingletonDataComponent<T>() where T : ISingletonDataComponent, new()
    {
        if (!singletionComponents.ContainsKey(typeof(T)))
        {
            singletonDataComponents.Add(typeof(T), new T());
        }
    }

    public static void RemoveSingletonDataComponent<T>() where T : ISingletonDataComponent
    {
        if (singletionComponents.ContainsKey(typeof(T)))
        {
            singletonDataComponents.Remove(typeof(T));
        }
    }

    public static T GetSingletonDataComponent<T>() where T : ISingletonDataComponent
    {
        if (singletionComponents.ContainsKey(typeof(T)))
        {
            return (T)singletonDataComponents[typeof(T)];
        }
        return default(T);
    }



    #endregion

    protected abstract void RegisterComponent();

    protected void Register<T>() where T : IModuleComponent
    {
        if (!allEventTypes.Contains(typeof(T)))
            allEventTypes.Add(typeof(T));
    }

    /// <summary>
    /// 顺序不要乱改
    /// </summary>
    public void InstantiateModule()
    {
        InstantiateSingletonDataComponent();
        InstantiateDataComponent();
        InstantiateEventComponent();
        InstantiateTCPEventComponent();
        InstantiateContorlComponent();

        InstantiatePanelComponet();
        InstantiateUpdateComponent();

        OnInstantiateModule();
    }

    public void UninstallModule()
    {
        UninstallPanelComponet();
        UninstallUpdateComponent();
        UninstallEventComponent();
        UninstallTCPEventComponent();
        UninstallDataComponent();
        UninstallContorlComponent();

        OnUninstallModule();
    }

    protected virtual void OnInstantiateModule()
    {

    }

    protected virtual void OnUninstallModule()
    {

    }

    public virtual void EnterModule()
    {

    }


    #region Get组件

    public T GetDataComponent<T>() where T : IDataComponent
    {
        if (dataComponents.ContainsKey(typeof(T)))
        {
            return (T)dataComponents[typeof(T)];
        }
        return default(T);
    }

    public T GetContorlComponent<T>() where T : IContorlComponent
    {
        if (contorlComponents.ContainsKey(typeof(T)))
        {
            return (T)contorlComponents[typeof(T)];
        }
        return default(T);
    }

    public T GetPanelComponent<T>() where T : BasePanelComponet
    {
        return UIManager.GetPanel<T>();
    }

    #endregion

    #region 实例化组件

    void InstantiatePanelComponet()
    {
        var itr = allEventTypes.GetEnumerator();
        while (itr.MoveNext())
        {
            if (itr.Current.BaseType == typeof(BasePanelComponet))
            {
                BasePanelComponet basePanel= UIManager.CreatePanel(itr.Current, this);
                if (!basePanel.showInInitialization)
                    basePanel.Hide();
                Debug.Log("注册 IPanelComponent");
            }
        }
        itr.Dispose();
    }

    void InstantiateUpdateComponent()
    {
        var itr = allEventTypes.GetEnumerator();
        while (itr.MoveNext())
        {
            if (typeof(IUpdateComponent).IsAssignableFrom(itr.Current))
            {
                if (!updateComponents.ContainsKey(itr.Current))
                    updateComponents.Add(itr.Current, (IUpdateComponent)Activator.CreateInstance(itr.Current));

                Debug.Log("注册 IUpdateComponent");
            }
        }
        itr.Dispose();

        var itr1 = singletonDataComponents.Keys.GetEnumerator();
        while (itr1.MoveNext())
        {
            if (typeof(IUpdateComponent).IsAssignableFrom(itr1.Current))
            {
                if (!updateComponents.ContainsKey(itr1.Current))
                    updateComponents.Add(itr1.Current, (IUpdateComponent)singletonDataComponents[itr1.Current]);
            }
        }
        itr1.Dispose();

        var itr2 = singletionComponents.Keys.GetEnumerator();
        while (itr2.MoveNext())
        {
            if (typeof(IUpdateComponent).IsAssignableFrom(itr2.Current))
            {
                if (!updateComponents.ContainsKey(itr2.Current))
                    updateComponents.Add(itr2.Current, (IUpdateComponent)singletionComponents[itr2.Current]);
            }
        }
        itr2.Dispose();
    }

    void InstantiateEventComponent()
    {
        var itr = allEventTypes.GetEnumerator();
        while (itr.MoveNext())
        {
            if (typeof(IEventComponent).IsAssignableFrom(itr.Current))
            {
                EventCall.Registered(itr.Current, this);
                Debug.Log("注册事件:"+ itr.Current.Name);
            }
        }
        itr.Dispose();
    }

    void InstantiateTCPEventComponent()
    {
        var itr = allEventTypes.GetEnumerator();
        while (itr.MoveNext())
        {
            if (typeof(ITCPEventComponent).IsAssignableFrom(itr.Current))
            {
                // TODO
            }
        }
        itr.Dispose();
    }

    void InstantiateDataComponent()
    {
        var itr = allEventTypes.GetEnumerator();
        while (itr.MoveNext())
        {
            if (typeof(IDataComponent).IsAssignableFrom(itr.Current))
            {
                if (!dataComponents.ContainsKey(itr.Current))
                {
                    IDataComponent dataComponent = (IDataComponent)Activator.CreateInstance(itr.Current);
                    dataComponents.Add(itr.Current, dataComponent);
                    dataComponent.Instantiate();
                }

                Debug.Log("注册 IDataComponent");
            }
        }
        itr.Dispose();
    }

    void InstantiateSingletonDataComponent()
    {
        var itr = allEventTypes.GetEnumerator();
        while (itr.MoveNext())
        {
            if (typeof(ISingletonDataComponent).IsAssignableFrom(itr.Current))
            {
                if (!singletonDataComponents.ContainsKey(itr.Current))
                {
                    ISingletonDataComponent dataComponent = (ISingletonDataComponent)Activator.CreateInstance(itr.Current);
                    singletonDataComponents.Add(itr.Current, dataComponent);
                    dataComponent.Instantiate();

                    Debug.Log("注册 SingletonDataComponent");
                }
            }
        }
        itr.Dispose();
    }

    void InstantiateContorlComponent()
    {
        var itr = allEventTypes.GetEnumerator();
        while (itr.MoveNext())
        {
            if (typeof(IContorlComponent).IsAssignableFrom(itr.Current))
            {
                if (!contorlComponents.ContainsKey(itr.Current))
                {
                    IContorlComponent contorlComponent = (IContorlComponent)Activator.CreateInstance(itr.Current);
                    contorlComponent.owner = this;
                    contorlComponents.Add(itr.Current, contorlComponent);
                }

                Debug.Log("注册 IContorlComponent");
            }
        }
        itr.Dispose();
    }


    #endregion

    #region 卸载组件

    void UninstallPanelComponet()
    {
        var itr = allEventTypes.GetEnumerator();
        while (itr.MoveNext())
        {
            if (itr.Current.BaseType  == typeof(BasePanelComponet)) 
            {
                UIManager.DestroyPanel(itr.Current);
            }
        }
        itr.Dispose();
    }

    void UninstallUpdateComponent()
    {
        var itr = allEventTypes.GetEnumerator();
        while (itr.MoveNext())
        {
            if (typeof(IUpdateComponent).IsAssignableFrom(itr.Current))
            {
                updateComponents.Remove(itr.Current);
            }
        }
        itr.Dispose();
    }

    void UninstallEventComponent()
    {
        var itr = allEventTypes.GetEnumerator();
        while (itr.MoveNext())
        {
            if (typeof(IEventComponent).IsAssignableFrom(itr.Current))
            {
                EventCall.Uninstall(itr.Current);
            }
        }
        itr.Dispose();
    }

    void UninstallTCPEventComponent()
    {
        var itr = allEventTypes.GetEnumerator();
        while (itr.MoveNext())
        {
            if (typeof(ITCPEventComponent).IsAssignableFrom(itr.Current))
            {
                // TODO
            }
        }
        itr.Dispose();
    }

    void UninstallDataComponent()
    {
        var itr = allEventTypes.GetEnumerator();
        while (itr.MoveNext())
        {
            if (typeof(IDataComponent).IsAssignableFrom(itr.Current))
            {
                dataComponents.Remove(itr.Current);
            }
        }
        itr.Dispose();
    }

    void UninstallContorlComponent()
    {
        var itr = allEventTypes.GetEnumerator();
        while (itr.MoveNext())
        {
            if (typeof(IContorlComponent).IsAssignableFrom(itr.Current))
            {
                contorlComponents.Remove(itr.Current);
            }
        }
        itr.Dispose();
    }

    #endregion

    public void Update()
    {
        var itr = updateComponents.Values.GetEnumerator();
        while (itr.MoveNext())
        {
            itr.Current.Update();
        }
        itr.Dispose();
    }


}
