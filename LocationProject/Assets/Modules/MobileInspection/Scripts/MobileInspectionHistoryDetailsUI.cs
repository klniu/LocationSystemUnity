﻿using Location.WCFServiceReferences.LocationServices;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileInspectionHistoryDetailsUI : MonoBehaviour {

    public static MobileInspectionHistoryDetailsUI Instance;
    /// <summary>
    /// 窗体
    /// </summary>
    public GameObject window;

    public PatrolPoint info;//工作票信息
    public List<PatrolPointItem> PatrolPointItemList;
    public PatrolPointItem PatrolPointItems;

    public Text TxtPersonnelNum;//巡检人工号
    public Text TxtPerson;//巡检人
    public Text devText;//巡检设备名称

    public Text Title;
    public GameObject ItemPrefab;//措施单项
    public VerticalLayoutGroup Grid;//措施列表

    public Button closeBtn;//关闭

    public Sprite Singleline;
    public Sprite DoubleLine;
    // Use this for initialization
    void Start()
    {
        Instance = this;
        closeBtn.onClick.AddListener(CloseBtn_OnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show(PatrolPoint infoT)
    {
        PatrolPointItemList.Clear();
        info = infoT;
        PatrolPointItemList.AddRange (info.Checks);
        UpdateData();
        CreateMeasuresItems();
        SetWindowActive(true);
        if (MobileInspectionInfoManage.Instance.window.activeInHierarchy)
        {
            MobileInspectionInfoManage.Instance.CloseWindow();
        }
   
    }

    /// <summary>
    /// 刷新数据
    /// </summary>
    public void UpdateData()
    {
        if (PatrolPointItems.StaffCode==null)
        {
            TxtPersonnelNum.text = "";
        }
        else
        {
            TxtPersonnelNum.text = PatrolPointItems.StaffCode.ToString();
        }
        if (MobileInspectionInfoManage.Instance .DevName ==null)
        {
            devText.text  = "";
        }
        else
        {
            devText.text = MobileInspectionInfoManage.Instance.DevName.ToString();
        }
        if (MobileInspectionInfoManage.Instance.PersonnelName ==null)
        {
            TxtPerson.text = "";
        }
        else
        {
            TxtPerson.text = MobileInspectionInfoManage.Instance.PersonnelName.ToString();
        }
            Title.text = MobileInspectionDetailsUI.Instance.TitleText + PatrolPointItems.Id.ToString();

    }
    int i = 0;
    /// <summary>
    /// 创建措施列表
    /// </summary>
    public void CreateMeasuresItems()
    {
        ClearMeasuresItems();
        if (PatrolPointItemList == null || PatrolPointItemList.Count  == 0) return;
        foreach (PatrolPointItem sm in PatrolPointItemList)
        {
            i = i + 1;
            GameObject itemT = CreateMeasuresItem();
            Text[] ts = itemT.GetComponentsInChildren<Text>();
            if (ts.Length > 0)
            {
                ts[0].text = sm.CheckId .ToString();
            }
            if (ts.Length > 1)
            {
                ts[1].text = sm.CheckItem;
            }
            if (ts.Length > 2)
            {
               
                    if (sm.dtCheckTime == null)
                {
                    ts[2].text = "";
                }
                    else
                {
                    DateTime timeT = Convert.ToDateTime(sm.dtCheckTime);
                    ts[2].text = timeT.ToString("yyyy/MM/dd HH:mm");
                }
  
            }
            if (i % 2 == 0)
            {
                itemT.transform.gameObject.GetComponent<Image>().sprite = DoubleLine;
            }
            else
            {
                itemT.transform.gameObject.GetComponent<Image>().sprite = Singleline;
            }

        }
    }

    /// <summary>
    /// 创建措施项
    /// </summary>
    public GameObject CreateMeasuresItem()
    {
        GameObject itemT = Instantiate(ItemPrefab);
        itemT.transform.SetParent(Grid.transform);
        itemT.transform.localPosition = Vector3.zero;
        itemT.transform.localScale = Vector3.one;
     //   LayoutElement layoutElement = itemT.GetComponent<LayoutElement>();
      //  layoutElement.ignoreLayout = false;
        itemT.SetActive(true);
        return itemT;
    }

    /// <summary>
    /// 清空措施列表
    /// </summary>
    public void ClearMeasuresItems()
    {
        int childCount = Grid.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(Grid.transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// 是否显示传统
    /// </summary>
    public void SetWindowActive(bool isActive)
    {
        window.SetActive(isActive);
    }

    /// <summary>
    /// 关闭按钮
    /// </summary>
    public void CloseBtn_OnClick()
    {
        SetWindowActive(false);
      //  MobileInspectionHistory_N.Instance.SetContentActive(true);
    }
}