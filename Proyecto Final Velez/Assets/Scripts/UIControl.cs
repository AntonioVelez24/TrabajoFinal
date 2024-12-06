using TMPro;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI energyText;
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
        healthText.text = "Health: " + playerControl.playerHealth.ToString();
    }
    private void UpdateEnergyBar()
    {
        energyText.text = "Energy: " + playerControl.energy.ToString();
    }
    private void UpdateInventory()
    {

    }
}
