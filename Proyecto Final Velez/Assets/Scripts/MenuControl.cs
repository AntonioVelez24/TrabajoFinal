using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using DG.Tweening;

public class MenuControl : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera menuCamera;
    [SerializeField] private CinemachineVirtualCamera startCamera;
    [SerializeField] private CinemachineVirtualCamera virtualCamera1;
    [SerializeField] private CinemachineVirtualCamera virtualCamera2;

    [SerializeField] private GameObject audioPanel;
    [SerializeField] private GameObject exitPanel;
    [SerializeField] private GameObject startText;

    [SerializeField] private CanvasGroup mainMenu;
    [SerializeField] private CanvasGroup darkPanel;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource thunderSource;


    [SerializeField] private Light stormLight;
    [SerializeField] private AnimationCurve customCurve;

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
                mainMenu.DOFade(1, 2f).SetUpdate(true);
                startingGame = true;
                startCamera.Priority = 0;
                menuCamera.Priority = 100;
                musicSource.Play();
                StartThunder();
            }
        }
    
    }
    public void OpenExitPanel()
    {
        mainMenu.DOFade(0, 0.5f).SetUpdate(true);
        MoveOpenPanel(exitPanel.GetComponent<RectTransform>());
        darkPanel.DOFade(1, 0.5f).SetUpdate(true);
        menuCamera.Priority = 0;
        virtualCamera2.Priority = 100;
    }
    public void CloseExitPanel()
    {
        mainMenu.DOFade(1, 0.5f).SetUpdate(true);
        MoveClosePanel(exitPanel.GetComponent<RectTransform>());
        darkPanel.DOFade(0, 0.5f).SetUpdate(true);
        menuCamera.Priority = 100;
        virtualCamera2.Priority = 0;
    }
    public void OpenAudioPanel()
    {
        mainMenu.DOFade(0, 0.5f).SetUpdate(true);
        MoveOpenPanel(audioPanel.GetComponent<RectTransform>());
        menuCamera.Priority = 0;
        virtualCamera1.Priority = 100;
    }
    public void CloseAudioPanel()
    {
        mainMenu.DOFade(1, 0.5f).SetUpdate(true);
        MoveClosePanel(audioPanel.GetComponent <RectTransform>());  
        menuCamera.Priority = 100;
        virtualCamera1.Priority = 0;
    }
    private void MoveOpenPanel(RectTransform rect)
    {
        rect.DOAnchorPosY(0, 1f);
    }
    private void MoveClosePanel(RectTransform rect)
    {
        rect.DOAnchorPosY(1140, 1f);
    }
    private void StartThunder()
    {
        stormLight.intensity = 0f;
        Sequence stormSequence = DOTween.Sequence();
        stormSequence.Append(stormLight.DOIntensity(150f, 0.4f).SetEase(customCurve));
        stormSequence.Append(stormLight.DOIntensity(0f, 0.4f).SetEase(customCurve));
        thunderSource.Play();
        stormSequence.Play();
    }
}
