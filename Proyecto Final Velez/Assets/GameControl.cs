using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    void WinGame()
    {
        SceneManager.LoadScene("Victory");
    }
    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    void PauseGame()
    {

    }
}