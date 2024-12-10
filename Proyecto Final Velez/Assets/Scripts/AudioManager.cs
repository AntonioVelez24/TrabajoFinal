using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private VolumeValuesSO volumeSO;   
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    // Start is called before the first frame update
    void Start()
    {
        musicSlider.value = volumeSO.MasterVolume;
        sfxSlider.value = volumeSO.MusicVolume;
        masterSlider.value = volumeSO.SfxVolume;

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMasterVolume(float volume)
    {
        volumeSO.MasterVolume = volume;
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }
    public void SetMusicVolume(float volume)
    {
        volumeSO.MusicVolume = volume;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }
    public void SetSfxVolume(float volume)
    {
        volumeSO.SfxVolume = volume;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
    void ToggleMute()
    {
        
    }
}
