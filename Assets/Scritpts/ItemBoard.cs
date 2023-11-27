using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ItemBoard : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI txtBoard;
    BoardController mParent;
    public BoardController.Data mData;
    [SerializeField] GameObject objText;
    [SerializeField] UnityEngine.UI.Image bg;
    public bool isCanClick = true;

    public void Init(BoardController.Data data, BoardController parent)
    {
        txtBoard.text = data.name;
        mData = data;
        mParent = parent;
        OnFlip(false);
        isCanClick = true;
    }

    public void OnFlip(bool isShow)
    {
        objText.SetActive(isShow);
    }
    public void OnChangeColor(Color col)
    {
        txtBoard.color = col;
    }
    public void ColorImage(Color col)
    {
        bg.color = col;
    }
    public void itemClick()
    {
        //chanclick
        if (isCanClick == false)
        {
            return;
        }
        mParent.OnitemClick(mData);
    }
}















