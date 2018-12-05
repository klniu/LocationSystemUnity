﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PersonAlarmSearchItem : MonoBehaviour {

    public GameObject ui;
    GameObject currentUI;
    void Start () {
      
    }
	
	// Update is called once per frame
	void Update () {
        GameObject currentUI = EventSystem.current.currentSelectedGameObject;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (ui== currentUI)
            {
                PersonnelAlarmList.Instance.PerAlarmSearchBut_Click();
            }
           
        }

    }
}
