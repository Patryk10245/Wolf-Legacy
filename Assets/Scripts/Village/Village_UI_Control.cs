using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Village_UI_Control : MonoBehaviour
{
    public GameObject infoField;

    public GameObject paladinWindow;
    public GameObject barbarianWindow;
    public GameObject rangerWindow;
    public GameObject mageWindow;



    public void ShowPaladinWindow()
    {
        paladinWindow.SetActive(true);
    }
    public void ShowBarbarianWindow()
    {
        barbarianWindow.SetActive(true);
    }
    public void ShowRangerWindow()
    {
        rangerWindow.SetActive(true);
    }
    public void ShowMageWindow()
    {
        mageWindow.SetActive(true);
    }

    public void CloseWindows()
    {
        paladinWindow.SetActive(false);
        barbarianWindow.SetActive(false);
        rangerWindow.SetActive(false);
        mageWindow.SetActive(false);
    }


    public void SetPos()
    {

    }

}
