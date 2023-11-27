using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemLv : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI txtLv;
    public LvBoardController.Data mData;
    LvBoardController mParent;
    [SerializeField] GameObject imgGear;
    [SerializeField] UnityEngine.UI.Image bg;
    private Image itemImage;
    public LvBoardController lvBoardController;
    public class SceneName
    {
        public const string Lv1 = "Lv1";
    }
    private void Awake()
    {
        itemImage = GetComponent<Image>();
    }
    public void Init(LvBoardController.Data data,LvBoardController parent)
    {
        txtLv.text = data.name;
        mData = data;
        mParent = parent;
        UpdateItemColor(data.state);

    }
    public void ColorImage(Color col)
    {
        bg.color = col;
    }
    public void ColorImages(Color color)
    {
        itemImage.color = color;
    }
    public void ClickLV()
    {
        if (mData.state == LvBoardController.ItemState.Locked) return;
        mParent.OnitemClickLv(mData);
    }
    public void ClickGear()
    {
        imgGear.SetActive(true);
    } 
    public void ClickXgear()
    {
        imgGear.SetActive(false);
    }
    public void UpdateColorBasedOnState()
    {
        UpdateItemColor(mData.state);
    }
    private void UpdateItemColor(LvBoardController.ItemState state)
    {
        switch (state)
        {
            case LvBoardController.ItemState.Completed:
                itemImage.color = Color.white;
                break;
            case LvBoardController.ItemState.Play:
                itemImage.color = Color.gray;
                break;
            case LvBoardController.ItemState.Locked:
                itemImage.color = Color.black;
                break;
           
        }
    }
}
