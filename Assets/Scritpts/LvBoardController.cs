using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemLv;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;


public class LvBoardController : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] GameObject itemPref;

    private ItemBoard item;
    private List<ItemLv> itemLvs = new List<ItemLv>();
    private ItemLv selectedItemClick = null;

    public class Data
    {
        public string name;
        public int itemValue;
        public int index;
        public ItemState state;
    }
    public enum ItemState
    {
        Completed,
        Play,
        Locked
    }
    private void Start()
    {
       
        List<Data> datas = UserData.GetLevelData();
        for (int i = 0; i < datas.Count; i++)
        {
            GameObject newContent = Instantiate(itemPref, container);
            newContent.SetActive(true);
            var item = newContent.GetComponent<ItemLv>();
            item.Init(datas[i], this);
            itemLvs.Add(item);       
        }
    }
    public void OnitemClickLv(Data dt) 
    {
        UserData.gameLever = (EGameLevel)dt.itemValue;
        SaveLevelData(dt);
        SceneManager.LoadScene(SceneName.Lv1);
    }
    private void SaveLevelData(Data dt)
    {
        PlayerPrefs.SetInt("SelectedLevelIndex", dt.index);
        PlayerPrefs.Save();
        //Debug.Log("Saved Level Index: " + dt.index);
    }
    private ItemLv FindItems(Data dt)
    {
        for (int i = 0; i < itemLvs.Count; i++)
        {
            if (itemLvs[i].mData.index == dt.index)
            {
                return itemLvs[i];
            }
        }
        return null;
    }

}




