using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIControl : MonoBehaviour
{
    //[SerializeField] private GameObject pausePanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI healthText;
    private PlayerControl playerControl;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
        //UpdateScoreText();
    }
    //private void SetPausePanel(GameObject pausePanel)
    //{
    //pausePanel.SetActive(true);
    //}
    //private void UpdateScoreText()
    //{
        //scoreText.text = "Score: " + GameManager.Instance.score.ToString();
    //}
    private void UpdateHealthBar()
    {
        healthText.text = "Health: " + playerControl.playerHealth.ToString();
    }
    private void UpdateInventory()
    {

    }
}
