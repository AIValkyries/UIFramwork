using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 提示框参数
/// </summary>
public class MsgData
{
    public string title;
    public string desc;
    public Action callBackConfirm;
    public Action callBackCancel;
    public string confirmTxt;
    public string cancelTxt;
    public float autoExit;
}

public class MsgBox : BasePanelComponet
{
    public override CanvasType canvasType { get { return CanvasType.Overlay; } }

    public override LayerType layerType { get { return LayerType.TIP; } }

    public override string prefabPath { get { return "Message/MsgBox"; } }

    public override bool showInInitialization { get { return false; } }

    bool _isStopTime;
    RectTransform _contentRect;
    CanvasGroup _canvasGroup;
    Image _bg;
    Image _icon;
    Text _title;
    Text _desc;
    GameObject _confirmCenter;
    GameObject _confirmRight;
    GameObject _cancel;

    Text _centerText;
    Text _rightText;
    Text _cancelTect;

    MsgData _data;

    public override void Initialize()
    {
        _contentRect = transform.Find("Content").GetComponent<RectTransform>();
        _canvasGroup = _contentRect.GetComponent<CanvasGroup>();
        _bg = _contentRect.GetComponent<Image>();
        _icon = transform.Find("Content/Icon").GetComponent<Image>();
        _title = transform.Find("Content/Title").GetComponent<Text>();
        _desc = transform.Find("Content/Desc").GetComponent<Text>();
        _confirmCenter = transform.Find("Content/ConfirmCenter").gameObject;
        _confirmRight = transform.Find("Content/ConfirmRight").gameObject;
        _cancel = transform.Find("Content/Cancel").gameObject;

        _centerText = _confirmCenter.transform.Find("Text").GetComponent<Text>();
        _rightText = _confirmRight.transform.Find("Text").GetComponent<Text>();
        _cancelTect = _cancel.transform.Find("Text").GetComponent<Text>();
    }

    public override void InitEvent()
    {
        _confirmCenter.GetComponent<Button>().onClick.AddListener(()=>
        {
            Hide();
            _isStopTime = true;
            if (_data.callBackConfirm != null) _data.callBackConfirm();
        });

        _confirmRight.GetComponent<Button>().onClick.AddListener(() =>
        {
            Hide();
            _isStopTime = true;
            if (_data.callBackConfirm != null) _data.callBackConfirm();
        });

        _cancel.GetComponent<Button>().onClick.AddListener(() =>
        {
            Hide();
            _isStopTime = true;
            if (_data.callBackCancel != null) _data.callBackCancel();
        });

    }

    protected override void OnShow()
    {
        _data = (MsgData)ModuleBase.Blackboard.GetobjectVariables(BlackboardKey.MSG_DATA_KEY);
        if (_data == null) return;

        setData();
    }

    private void setData()
    {
        if (!string.IsNullOrEmpty(_data.title))
        {
            _title.text = _data.title;
        }

        _desc.text = _data.desc;

        bool btnShow = _data.callBackCancel == null;
        _confirmCenter.SetActive(btnShow);
        _confirmRight.SetActive(!btnShow);
        _cancel.SetActive(!btnShow);

        if (!string.IsNullOrEmpty(_data.confirmTxt))
        {
            if (_confirmCenter.activeSelf)
            {
                _centerText.text = _data.confirmTxt;
            }
            else
            {
                _rightText.text = _data.confirmTxt;
            }
        }

        if (!string.IsNullOrEmpty(_data.cancelTxt))
        {
            _cancelTect.text = _data.cancelTxt;
        }

        ModuleBase.Coroutine.StartCoroutine(ChangeTime());
    }

    private IEnumerator ChangeTime()
    {
        while (_data.autoExit > 0 && !_isStopTime)
        {
            yield return new WaitForSeconds(1);
            _data.autoExit--;
            _centerText.text = _data.confirmTxt + "\n" + _data.autoExit + "秒";
        }
        if (_data.callBackConfirm != null && !_isStopTime)
            _data.callBackConfirm();
    }

    private void show()
    {
        _canvasGroup.alpha = 1f;
        transform.SetAsLastSibling();
        gameObject.SetActive(true);
    }


}
