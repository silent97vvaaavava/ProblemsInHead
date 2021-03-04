using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;


public class SceneController : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void MenuGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    //private void LateUpdate()
    //{
    //    if (Input.GetKeyUp(KeyCode.Escape))
    //    {
    //        //Time.timeScale = 0;

    //        MenuGame();
    //    }
    //}
}
