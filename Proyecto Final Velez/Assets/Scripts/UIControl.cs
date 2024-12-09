using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image energyBar;
    private PlayerControl playerControl;
    private bool IsPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
        UpdateScoreText();
        UpdateEnergyBar();
    }
    public void SetPausePanel()
    {
        if (IsPaused == false)
        {
            Game_Manager.Instance.PauseGame();
            playerControl.UnlockCursor();
            pausePanel.SetActive(true);
            IsPaused = true;
        }
        else if(IsPaused == true)
        {
            Game_Manager.Instance.UnPauseGame();
            pausePanel.SetActive(false);
            IsPaused = false;
            playerControl.LockCursor();
        }
    }
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + Game_Manager.Instance.score.ToString();
    }
    private void UpdateHealthBar()
    {
        healthBar.fillAmount = playerControl.playerHealth /15; 
    }
    private void UpdateEnergyBar()
    {
        energyBar.fillAmount = playerControl.energy /30;
    }
    private void UpdateInventory()
    {

    }
}
