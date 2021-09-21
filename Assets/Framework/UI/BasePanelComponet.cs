using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanelComponet : IPanelComponent
{
    GameObject _go;
    Transform _transform;
    bool _isOpen;

    public GameObject gameObject { get { return _go; } }
    public Transform transform { get { return _transform; } }

    public abstract CanvasType canvasType { get; }
    public abstract LayerType layerType { get; }
    public abstract string prefabPath { get; }
    public virtual bool showInInitialization { get { return true; } }

    public void SetGameObject(GameObject go)
    {
        _go = go;
        _transform = go.transform;
        _go.SetActive(false);
    }

    public virtual void Initialize()
    {

    }

    public virtual void InitEvent()
    {

    }

    public bool IsOpen()
    {
        return _isOpen;
    }

    /// <summary>
    /// 这个函数只会销毁Gameobject，但是该类依旧会存在
    /// </summary>
    public void Destroy()
    {
        if (_go == null) return;

        _isOpen = false;

        GameObject.Destroy(_go);

        OnDestroy();
    }

    public void Hide()
    {
        if (_go == null) return;

        _go.SetActive(false);
        _isOpen = false;
        OnTweenOut();
        OnHide();
    }

    public void Show()
    {
        if (_go == null) return;

        _isOpen = true;
        _go.SetActive(true);
        var tran = _go.GetComponent<RectTransform>();
        tran.SetAsLastSibling();

        OnTweenIn();
        OnShow();
    }

    public virtual void Update()
    {

    }

    protected virtual void OnTweenIn()
    {
        
    }

    protected virtual void OnTweenOut()
    {
        
    }

    protected virtual void OnShow()
    {

    }

    protected virtual void OnHide()
    {

    }

    protected virtual void OnDestroy()
    {

    }


}
