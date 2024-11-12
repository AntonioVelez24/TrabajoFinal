using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void WinGame()
    {
        SceneManager.LoadScene("Victory");
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    public void PauseGame()
    {

    }
}