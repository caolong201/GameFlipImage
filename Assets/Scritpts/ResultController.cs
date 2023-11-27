using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static BoardController;
using UnityEngine.UI;
using static Unity.VisualScripting.Cooldown;

public class ResultController : MonoBehaviour
{
    //[SerializeField] GameObject Setup;

    public class SceneName
    {
        public const string Main = "Main";
    }
    public void Cliick()
    {
        SceneManager.LoadScene(SceneName.Main);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);      
    }

    public void ClickMain()
    {
        SceneManager.LoadScene(SceneName.Main);
    }
    //public void ClickSetup()
    //{ 
    //    Setup.SetActive(true);
    //}
    
}
