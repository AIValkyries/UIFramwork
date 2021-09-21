using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>
/// ui manager 
/// </summary>
public class UIManagerComponet : ISingletonModuleComponent, IUpdateComponent
{
    /// <summary>
    /// Key:具体模块类型
    /// value:模块所有的panel
    /// </summary>
    static Dictionary<Type, List<BasePanelComponet>> modulePanels = new Dictionary<Type, List<BasePanelComponet>>();

    /// <summary>
    /// KEY:uipanel的类型
    /// Value：模块的类型
    /// </summary>
    static Dictionary<Type, Type> panelToModuleTypes = new Dictionary<Type, Type>();

    /// <summary>
    /// Key:ui panel的类型
    /// Value：具体的panel
    /// </summary>
    static Dictionary<Type, BasePanelComponet> panelDic = new Dictionary<Type, BasePanelComponet>();

    /// <summary>
    /// Key:canvas 类型
    /// Value：
    /// </summary>
    static Dictionary<CanvasType, Canvas> allCanvas = new Dictionary<CanvasType, Canvas>();

    /// <summary>
    /// 每个Canvas的层级
    /// </summary>
    static Dictionary<CanvasType, List<GameObject>> canvasLayers = new Dictionary<CanvasType, List<GameObject>>();

    static Camera uiCamera;
    static Camera uICamera3D;
    static Transform allParent;

    // 测试
    public void TestToString()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();

        System.Text.StringBuilder str0 = new System.Text.StringBuilder();
        System.Text.StringBuilder str1 = new System.Text.StringBuilder();
        System.Text.StringBuilder str2 = new System.Text.StringBuilder();

        var itr0 = modulePanels.Keys.GetEnumerator();
        while (itr0.MoveNext())
        {
            str0.Append(itr0.Current + "\n");
            str0.Append("{\n");

            for (int i = 0; i < modulePanels[itr0.Current].Count; i++) 
            {
                if (i == modulePanels[itr0.Current].Count - 1)
                {
                    str0.Append("  " + modulePanels[itr0.Current][i].GetType().Name);
                }
                else
                {
                    str0.Append("  " + modulePanels[itr0.Current][i].GetType().Name + ",\n");
                }
            }

            str0.Append("\n}\n");
        }
        itr0.Dispose();

        var itr1 = panelToModuleTypes.Keys.GetEnumerator();
        while (itr1.MoveNext())
        {
            str1.Append(itr1.Current + "=" + panelToModuleTypes[itr1.Current] + "\n");
        }
        itr1.Dispose();

        var itr2 = panelToModuleTypes.Keys.GetEnumerator();
        while (itr2.MoveNext())
        {
            str2.Append("UIPanelType=" + itr2.Current + "\n");
        }
        itr2.Dispose();

        builder.Append("UIManagerComponet=\n");
        builder.Append("具体模块下的所有Panel=\n" + str0 + "\n");
        builder.Append("Panel的类型对应模块的类型=\n" + str1 + "\n");
        builder.Append("Panel的类型对应Panel的实体=\n" + str2 + "\n");
 
        Debug.Log(builder.ToString());
    }

    #region  Initialize and Cleanup

    public void Initialize()
    {
        allParent = new GameObject("UIManager").transform;
        allParent.localPosition = Vector3.zero;
        allParent.localScale = Vector3.one;
        allParent.localRotation = Quaternion.identity;
        GameObject.DontDestroyOnLoad(allParent.gameObject);

        Vector2 Resolution = new Vector2(1920, 1080);

        // uiCamera
        {
            uiCamera = CreateGameObject("UICamera").AddComponent<Camera>();
            uiCamera.backgroundColor = Color.black;
            uiCamera.cullingMask = LayerMask.GetMask("UI"); ;
            uiCamera.orthographic = true;
            uiCamera.orthographicSize = 8;
            uiCamera.nearClipPlane = -100;
            uiCamera.farClipPlane = 100;
            uiCamera.depth = 10;
            uiCamera.allowHDR = false;
            uiCamera.allowMSAA = false;
        }

        // ScreenSpaceCamera 覆盖模式
        {
            var rootCanvasGo = CreateGameObject("UIRoot");
            rootCanvasGo.layer = LayerMask.NameToLayer("UI");
            var canvas = rootCanvasGo.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = uiCamera;
            canvas.pixelPerfect = false;
            canvas.planeDistance = 0;
            var scaler = rootCanvasGo.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = Resolution;
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 1;
            rootCanvasGo.AddComponent<GraphicRaycaster>();
            rootCanvasGo.AddComponent<UIAdaptor>();

            allCanvas.Add(CanvasType.UIRoot, canvas);
        }

        //创建最上层画布
        {
            var canvasOverlayGo = CreateGameObject("UIOverlay");
            var canvasOverlay = canvasOverlayGo.AddComponent<Canvas>();
            canvasOverlay.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasOverlay.sortingOrder = 10;
            canvasOverlay.planeDistance = 100;
            var scalerOverlay = canvasOverlayGo.AddComponent<CanvasScaler>();
            scalerOverlay.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scalerOverlay.referenceResolution = Resolution;
            scalerOverlay.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scalerOverlay.matchWidthOrHeight = 1;
            canvasOverlayGo.AddComponent<GraphicRaycaster>();
            canvasOverlayGo.AddComponent<UIAdaptor>();

            allCanvas.Add(CanvasType.Overlay, canvasOverlay);
        }


        //创建渲染3dUI的摄像机
        {
            uICamera3D = CreateGameObject("UICamera3D").AddComponent<Camera>();
            uICamera3D.clearFlags = CameraClearFlags.Depth;
            uICamera3D.cullingMask = LayerMask.NameToLayer("3DUI");
            uICamera3D.orthographic = false;
            uICamera3D.fieldOfView = 40;
            uICamera3D.depth = 11;
            uICamera3D.enabled = true;
            uICamera3D.transform.position = new Vector3(0, 0, -100);

            //创建3dUI的Canvas
            var canvas3DGo = CreateGameObject("Canvas3D");
            canvas3DGo.layer = LayerMask.NameToLayer("3DUI");
            var canvas3D = canvas3DGo.AddComponent<Canvas>();
            canvas3D.renderMode = RenderMode.WorldSpace;
            canvas3D.worldCamera = uICamera3D;
            var scaler3D = canvas3DGo.AddComponent<CanvasScaler>();
            canvas3DGo.AddComponent<GraphicRaycaster>();

            allCanvas.Add(CanvasType.World, canvas3D);
        }

        var itr = allCanvas.Keys.GetEnumerator();
        while (itr.MoveNext())
        {
            AddLayer(LayerType.Bottom.ToString(), itr.Current);
            AddLayer(LayerType.Content.ToString(), itr.Current);
            AddLayer(LayerType.TOP.ToString(), itr.Current);
            AddLayer(LayerType.TIP.ToString(), itr.Current);
        }
        itr.Dispose();
    }

    public void Cleanup()
    {
        modulePanels.Clear();
        panelDic.Clear();
        allCanvas.Clear();
        canvasLayers.Clear();

        GameObject.DestroyImmediate(allParent);
    }

    private GameObject CreateGameObject(string objName)
    {
        var go = new GameObject(objName);
        go.transform.SetParent(allParent);
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        go.transform.localRotation = Quaternion.identity;
        return go;
    }

    private RectTransform AddLayer(string name, CanvasType canvasType)
    {
        var go = new GameObject(name);
        go.AddComponent<CanvasRenderer>();
        var rect = go.AddComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.sizeDelta = Vector2.zero;

        rect.SetParent(allCanvas[canvasType].transform, false);

        if (!canvasLayers.ContainsKey(canvasType))
            canvasLayers.Add(canvasType,new List<GameObject>());

        canvasLayers[canvasType].Add(go);
        return rect;
    }

    #endregion

    Transform GetParent(BasePanelComponet basePanel)
    {
        if (canvasLayers.ContainsKey(basePanel.canvasType))
        {
            return canvasLayers[basePanel.canvasType][(int)basePanel.layerType].transform;
        }
        return null;
    }

    public BasePanelComponet CreatePanel(Type type, ModuleBase modelBase)
    {
        if (!panelDic.ContainsKey(type))
        {
            panelDic.Add(type, (BasePanelComponet)Activator.CreateInstance(type));
            if (!modulePanels.ContainsKey(modelBase.GetType()))
            {
                modulePanels.Add(modelBase.GetType(), new List<BasePanelComponet>());
            }
            if (!modulePanels[modelBase.GetType()].Contains(panelDic[type]))
            {
                modulePanels[modelBase.GetType()].Add(panelDic[type]);
            }
        }

        if (panelDic[type].IsOpen())
            return panelDic[type];

        if (panelDic[type].gameObject == null)
        {
            GameObject prefab = Resources.Load<GameObject>(panelDic[type].prefabPath);
            if (prefab == null)
            {
                Debug.LogError("UIPrefab为空! Path=" + panelDic[type].prefabPath);
                return panelDic[type];
            }

            GameObject gameObject = GameObject.Instantiate(prefab);
            gameObject.transform.SetParent(GetParent(panelDic[type]));
            gameObject.transform.localScale = Vector3.one;
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;

            var rect = gameObject.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;
            rect.anchoredPosition = Vector2.zero;

            panelDic[type].SetGameObject(gameObject);
            panelDic[type].Initialize();
            panelDic[type].InitEvent();
        }

        if (!panelToModuleTypes.ContainsKey(type))
            panelToModuleTypes.Add(type, modelBase.GetType());
        panelDic[type].Show(); 
        return panelDic[type];
    }

    public T CreatePanel<T>(ModuleBase modelBase) where T : BasePanelComponet, new()
    {
        return (T)CreatePanel(typeof(T), modelBase);
    }

    public void ShowPanel<T>()where T:BasePanelComponet
    {
        if(panelDic.ContainsKey(typeof(T)))
        {
            if (panelDic[typeof(T)].gameObject != null) 
            {
                panelDic[typeof(T)].Show();
            }
        }
    }

    public bool HasPanel<T>() where T : BasePanelComponet
    {
        return panelDic.ContainsKey(typeof(T));
    }

    public bool IsOpen<T>() where T : BasePanelComponet
    {
        if (GetPanel<T>() != null) 
        {
            return GetPanel<T>().IsOpen();
        }
        return false;
    }

    public T GetPanel<T>() where T : BasePanelComponet
    {
        if (panelDic.ContainsKey(typeof(T)))
        {
            return panelDic[typeof(T)] as T;
        }
        return default(T);
    }

    public void DestroyPanel<T>() where T : BasePanelComponet
    {
        DestroyPanel(typeof(T));
    }

    public void DestroyPanel(Type type)
    {
        if (panelDic.ContainsKey(type))
        {
            panelDic[type].Destroy();

            Type moduleType = null;
            if (panelToModuleTypes.ContainsKey(type))
            {
                moduleType = panelToModuleTypes[type];
                panelToModuleTypes.Remove(type);
            }

            if (modulePanels.ContainsKey(moduleType))
                modulePanels[moduleType].Remove(panelDic[type]);

            if (modulePanels[moduleType].Count <= 0) 
                modulePanels.Remove(moduleType);

            panelDic.Remove(type);
        }
    }

    public void DestroyModuelPanel(Type moduleType)
    {
        if (modulePanels.ContainsKey(moduleType))
        {
            List<BasePanelComponet> basePanels = modulePanels[moduleType];

            for (int i = 0; i < basePanels.Count; i++)
            {
                DestroyPanel(basePanels[i].GetType());
            }
        }
    }

    public void DestroyAllPanel()
    {
        List<Type> allPanelKeys = new List<Type>();
        allPanelKeys.AddRange(panelDic.Keys);

        for (int i = 0; i < allPanelKeys.Count; i++) 
        {
            DestroyPanel(allPanelKeys[i]);
        }
    }

    public void HideAllPanel()
    {
        List<BasePanelComponet> allPanelKeys = new List<BasePanelComponet>();
        allPanelKeys.AddRange(panelDic.Values);

        for (int i = 0; i < allPanelKeys.Count; i++)
        {
            allPanelKeys[i].Hide();
        }
    }

    public void Update()
    {
        var itr = panelDic.Values.GetEnumerator();
        while (itr.MoveNext())
        {
            itr.Current.Update();
        }
        itr.Dispose();
    }

}
