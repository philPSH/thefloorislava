﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void StartTheGame()
    {
        // 
        SceneManager.LoadScene("GameScene");
        
    }

    public void EndGame()
    {
        Application.Quit();
        Debug.Log("Application Quit");
 
    }


	



}
