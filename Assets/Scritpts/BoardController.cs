using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;
public enum EGameLevel
{
    Lv1 /*= 1*/,
    Lv2,
    Lv3,
    Lv4,
    Lv5,
    Lv6,
    Lv7,
    Lv8,
    Lv9,
    Lv10,
    Lv11,    
    Lv12,
    Lv13,    
    Lv14,
    Lv15,
    Lv16
}
public class BoardController : MonoBehaviour
{
    public class Data
    {
        public string name;
        public int itemValue;
        public int index;
    }
    [SerializeField] GameObject itemSetup;
    [SerializeField] GameObject itemWinlv;
    [SerializeField] Transform container;
    [SerializeField] GameObject itemPref;
    private List<ItemBoard> listItems = new List<ItemBoard>();
    private ItemBoard item1, item2;
    private int maxItem, currItem;
    private bool isChecking = false;
    private int levelCount;
    private GridLayoutGroup gridLayoutGroup;
    private bool isInit = false;
    public LvBoardController lvBoardController;

    private List<Data> cheatdata()
    {
        gridLayoutGroup = container.GetComponent<GridLayoutGroup>();
        switch (UserData.gameLever)
        {
            case EGameLevel.Lv1:
                levelCount = 6;
                gridLayoutGroup.constraintCount = 3;
                break;

            case EGameLevel.Lv2:
                levelCount = 10;
                gridLayoutGroup.constraintCount = 4;
                break;

            case EGameLevel.Lv3:
                levelCount = 12;
                gridLayoutGroup.constraintCount = 4;
                break;

            case EGameLevel.Lv4:
                levelCount = 16;
                gridLayoutGroup.constraintCount = 4;
                break;

            case EGameLevel.Lv5:
                levelCount = 16;
                gridLayoutGroup.constraintCount = 4;
                break;

            case EGameLevel.Lv6:
                levelCount = 6;
                gridLayoutGroup.constraintCount = 3;
                break;
            case EGameLevel.Lv7:
                levelCount = 10;
                gridLayoutGroup.constraintCount = 4;
                break;
            case EGameLevel.Lv8:
                levelCount = 10;
                gridLayoutGroup.constraintCount = 4;
                break;
            case EGameLevel.Lv9:
                levelCount = 10;
                gridLayoutGroup.constraintCount = 4;
                break;
            case EGameLevel.Lv10:
                levelCount = 10;
                gridLayoutGroup.constraintCount = 4;
                break;
            case EGameLevel.Lv11:
                levelCount = 10;
                gridLayoutGroup.constraintCount = 4;
                break;
            case EGameLevel.Lv12:
                levelCount = 10;
                gridLayoutGroup.constraintCount = 4;
                break;
            case EGameLevel.Lv13:
                levelCount = 10;
                gridLayoutGroup.constraintCount = 4;
                break;
            case EGameLevel.Lv14:
                levelCount = 10;
                gridLayoutGroup.constraintCount = 4;
                break;
            case EGameLevel.Lv15:
                levelCount = 10;
                gridLayoutGroup.constraintCount = 4;
                break;
            case EGameLevel.Lv16:
                levelCount = 10;
                gridLayoutGroup.constraintCount = 4;
                break;
           
        }
        var datas = new List<Data>();
        int count = -1;
        for (int i = 0; i < levelCount; i++)
        {
            count++;
            Data dt1 = new Data();
            dt1.name = (i + 1) + "";
            dt1.itemValue = i + 1;
            dt1.index = count;

            count++;
            Data dt2 = new Data();
            dt2.name = (i + 1) + "";
            dt2.itemValue = i + 1;
            dt2.index = count;

            datas.Add(dt1);
            datas.Add(dt2);
        }
        return datas;
    }
    private void Start()
    {
        //itemSetup.gameObject.SetActive(false);
        itemWinlv.gameObject.SetActive(false);
        currItem = 0;
        List<Data> datas = cheatdata();
        for (int i = 0; i < datas.Count; i++)
        {
            //Dao item
            Data temp = datas[i];
            int rand = UnityEngine.Random.Range(i, datas.Count);
            datas[i] = datas[rand];
            datas[rand] = temp;

            GameObject newContent = Instantiate(itemPref, container);
            newContent.SetActive(true);
            var item = newContent.GetComponent<ItemBoard>();
            item.Init(datas[i], this);
            listItems.Add(item);
        }
        maxItem = datas.Count;
    }   

    public void OnitemClick(Data dt)
    {
        //chanclick
        if (isChecking == true)
        {
            return;
        }
        Debug.Log(dt.itemValue);

        if (item1 == null)
        {
            item1 = FindItem(dt);
            item1.OnFlip(true);
            item1.isCanClick = false;
        }
        else
        {
            item2 = FindItem(dt);
            item2.OnFlip(true);
            item2.isCanClick = false;
        }

        if (item1 != null && item2 != null)
        {
            if (item1.mData.itemValue == item2.mData.itemValue)
            {
                currItem += 2;
                if (currItem >= maxItem)
                {
                    StartCoroutine(_DelayAction(0.5f, () =>
                    {
                        //Chienthang
                        //itemSetup.gameObject.SetActive(true);
                        itemWinlv.gameObject.SetActive(true);

                        UserData.UpdateLevel((int)UserData.gameLever,LvBoardController.ItemState.Completed);

                        //epkieu
                        int intLevel = (int)UserData.gameLever;
                        intLevel++;
                        if (intLevel > 5)
                        {
                            intLevel = 0;
                        }
                        UserData.gameLever = (EGameLevel)intLevel;
                        Debug.Log(UserData.gameLever);
                        UserData.UpdateLevel((int)UserData.gameLever, LvBoardController.ItemState.Play);

                    }));
                }
                item1.OnChangeColor(Color.green);
                item2.OnChangeColor(Color.green);
                item1.ColorImage(Color.gray);
                item2.ColorImage(Color.gray);
                item1 = null; item2 = null;
            }
            else
            {
                Debug.Log("gg");
                //chan click
                isChecking = true;
                StartCoroutine(_DelayAction(1f, () =>
                {
                    isChecking = false;
                    item1.OnFlip(false);
                    item2.OnFlip(false);
                    item1.isCanClick = true;
                    item2.isCanClick = true;
                    item1 = null; item2 = null;
                }));
            }
        }
    }

    private ItemBoard FindItem(Data dt)
    {
        for (int i = 0; i < listItems.Count; i++)
        {
            if (listItems[i].mData.index == dt.index)
            {
                return listItems[i];
            }
        }
        return null;
    }
    //delay cho
    private IEnumerator _DelayAction(float delayTime, Action callback)
    {
        yield return new WaitForSeconds(delayTime);
        callback?.Invoke();
    }
}






