using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance;

    public int score;

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
    public void Play()
    {
        SceneManager.LoadScene("Game");
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
        Time.timeScale = 0f;
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1f;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
