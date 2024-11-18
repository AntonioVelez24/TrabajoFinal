using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class MenuControl : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera menuCamera;
    [SerializeField] private CinemachineVirtualCamera startCamera;
    [SerializeField] private CinemachineVirtualCamera virtualCamera1;
    [SerializeField] private CinemachineVirtualCamera virtualCamera2;

    [SerializeField] private GameObject audioPanel;
    [SerializeField] private GameObject exitPanel;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject startText;

    private bool startingGame = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Startmenu();
    }
    private void Startmenu()
    {
        if (startingGame == false)
        {
            if (Input.anyKey)
            {
                startText.SetActive(false);
                mainMenu.SetActive(true);
                startingGame = true;
                startCamera.Priority = 0;
                menuCamera.Priority = 100;
            }
        }
    }
    public void OpenExitPanel()
    {
        exitPanel.SetActive(true);
        mainMenu.SetActive(false);
        menuCamera.Priority = 0;
        virtualCamera2.Priority = 100;
    }
    public void CloseExitPanel()
    {
        exitPanel.SetActive(false);
        mainMenu.SetActive(true);
        menuCamera.Priority = 100;
        virtualCamera2.Priority = 0;
    }
    public void OpenAudioPanel()
    {
        audioPanel.SetActive(true);
        mainMenu.SetActive(false);
        menuCamera.Priority = 0;
        virtualCamera1.Priority = 100;
    }
    public void CloseAudioPanel()
    {
        audioPanel.SetActive(false);
        mainMenu.SetActive(true);
        menuCamera.Priority = 100;
        virtualCamera1.Priority = 0;
    }
}
