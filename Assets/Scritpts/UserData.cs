using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static BoardController;
using static LvBoardController;

public class UserData : MonoBehaviour
{
    public static EGameLevel gameLever = EGameLevel.Lv1;
    static List<LvBoardController.Data> levelData = null;
    public static List<LvBoardController.Data> GetLevelData()
    {
        if (levelData == null) levelData = Cheatdata();
        return levelData;
    }
    public static void UpdateLevel(int index, ItemState state)
    {   
        for (int i = 0; i < levelData.Count; i++)
        {
            if (i == index)
            {
                levelData[i].state = state;
                break;
            }
        }
    }
    private static List<LvBoardController.Data> Cheatdata()
    {   
        var datas = new List<LvBoardController.Data>();
        int selectedLevelIndex = PlayerPrefs.GetInt("SelectedLevelIndex", 0);
        Debug.Log("Loaded Level Index: " + selectedLevelIndex);
        for (int i = 0; i < 16; i++)
        {
            LvBoardController.Data dt = new LvBoardController.Data();
            dt.name = (i + 1) + "";
            dt.itemValue = i;
            dt.index = i;

            if (i == selectedLevelIndex) dt.state = ItemState.Play;
            else if (i < selectedLevelIndex) dt.state = ItemState.Completed;
            else
            {
                dt.state = ItemState.Locked;
            }
           

            datas.Add(dt);
        
    }
        return datas;
    }

}
