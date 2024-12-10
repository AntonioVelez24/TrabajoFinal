using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject objectList;
    [SerializeField] private GameObject listText;
    private PlayerControl playerControl;
    private bool IsPaused = false;
    private bool ListActive = false;
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
    public void UpdateInventory()
    {
        if(ListActive == false)
        {
            objectList.SetActive(true);
            listText.SetActive(false);
            ListActive = true;
        }
        else if (ListActive == true)
        {
            objectList.SetActive(false);
            listText.SetActive(true);
            ListActive = false;
        }
    }
}
