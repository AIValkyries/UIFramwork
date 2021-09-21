using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : BasePanelComponet
{
    public override CanvasType canvasType { get { return CanvasType.Overlay; } }

    public override LayerType layerType { get { return LayerType.Bottom; } }
    /// <summary>
    /// 在Resources路径下
    /// </summary>
    public override string prefabPath { get { return "PlayerPanel"; } }

    Button _submitButton;
    Button _updatePlayerName;
    Button _updatePlayerAge;
    Button _updateAll;
    Button _callTestModule;
    Button _showMsgBox;

    Text _name1;
    Text _name2;
    Text _age;
    Text _hometown;
    Text _mail;
    Text _sex;
    List<Text> _hobby;  // 爱好

    public override void Initialize()
    {
        _hobby = new List<Text>();
        _submitButton = transform.Find("Button").GetComponent<Button>();
        _updatePlayerName = transform.Find("Button (1)").GetComponent<Button>();
        _updatePlayerAge = transform.Find("Button (2)").GetComponent<Button>();
        _updateAll = transform.Find("Button (3)").GetComponent<Button>();
        _callTestModule = transform.Find("Button (4)").GetComponent<Button>();
        _showMsgBox = transform.Find("Button (5)").GetComponent<Button>();

        _name1 = transform.Find("Text/Value").GetComponent<Text>();
        _name2 = transform.Find("Text (1)/Value").GetComponent<Text>();
        _age = transform.Find("Text (2)/Value").GetComponent<Text>();
        _hometown = transform.Find("Text (3)/Value").GetComponent<Text>();
        _mail = transform.Find("Text (4)/Value").GetComponent<Text>();
        _sex = transform.Find("Text (5)/Value").GetComponent<Text>();

        Transform parent = transform.Find("Text (6)");

        for (int i = 0; i < parent.childCount; i++) 
        {
            _hobby.Add(parent.GetChild(i).GetComponent<Text>());
        }
    }

    public override void InitEvent()
    {
        _submitButton.onClick.AddListener(SubmitButtonEvent);
        _updatePlayerName.onClick.AddListener(UpdatePlayerName);
        _updatePlayerAge.onClick.AddListener(UpdatePlayerAge);
        _updateAll.onClick.AddListener(UpdateAll);
        _callTestModule.onClick.AddListener(CallTestModule);
        _showMsgBox.onClick.AddListener(ShowMsgBox);
    }

    public void ShowPlayerInfo(PlayerDataComponent playerData)
    {
        _age.text = playerData.Age.ToString();
        _hometown.text = playerData.Hometown;
        _mail.text = playerData.Mail;
        _sex.text = playerData.Sex;

        for (int i = 0; i < playerData.Hobby.Count; i++) 
        {
            _hobby[i].text = playerData.Hobby[i];
        }
    }

    public void UpdatePlayerName1(PlayerGlobalDataComponent playerGlobal)
    {
        _name1.text = playerGlobal.Name;
    }

    public void UpdatePlayerName2(PlayerGlobalDataComponent playerGlobal)
    {
        _name2.text = playerGlobal.Name;
    }

    public void UpdatePlayerAge(PlayerDataComponent playerData)
    {
        _age.text = playerData.Age.ToString();
    }

    #region button event

    void ShowMsgBox()
    {
        MsgData msgData = new MsgData()
        {
            title = "测试title",
            desc = "测试desc",
            callBackConfirm = () => { Debug.Log("确定毁掉"); },
            confirmTxt = "确定",
            autoExit = 5
        };

        CommonModule.ShowMsgBox(msgData);
    }


    void UpdatePlayerName()
    {
        ModuleBase.EventCall.CallEvent(EventID.UpdatePlayerName);
    }

    void UpdatePlayerAge()
    {
        ModuleBase.EventCall.CallEvent(EventID.UpdatePlayerAge);
    }

    void UpdateAll()
    {
        ModuleBase.EventCall.CallEvent(EventID.UpdateAll);
    }

    void CallTestModule()
    {
        ModuleBase.EventCall.CallEvent(EventID.CallTestModule);
    }

    void SubmitButtonEvent()
    {
        ModuleBase.EventCall.CallEvent(EventID.SubmitInfo, new object[] { "我是参数" });
    }

    #endregion

    #region  重写方法

    protected override void OnShow()
    {
        Debug.Log("OnShow");
    }

    protected override void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }

    protected override void OnHide()
    {
        Debug.Log("OnHide");
    }

    protected override void OnTweenIn()
    {
        Debug.Log("OnTweenIn");
    }

    protected override void OnTweenOut()
    {
        Debug.Log("OnTweenOut");
    }

    public override void Update()
    {
        //Debug.Log("Update");
    }

    #endregion

}


public class PlayerPanel2 : BasePanelComponet
{
    public override CanvasType canvasType { get { return CanvasType.Overlay; } }

    public override LayerType layerType { get { return LayerType.Bottom; } }
    /// <summary>
    /// 在Resources路径下
    /// </summary>
    public override string prefabPath { get { return "PlayerPanel"; } }

    public override bool showInInitialization { get { return false; } }

    public override void Initialize()
    {
        Debug.Log("我是 PlayerPanel2");
    }
}

public class PlayerPanel3 : BasePanelComponet
{
    public override CanvasType canvasType { get { return CanvasType.Overlay; } }

    public override LayerType layerType { get { return LayerType.Bottom; } }
    /// <summary>
    /// 在Resources路径下
    /// </summary>
    public override string prefabPath { get { return "PlayerPanel"; } }

    public override bool showInInitialization { get { return false; } }

    public override void Initialize()
    {
        Debug.Log("我是 PlayerPanel3");
    }
}