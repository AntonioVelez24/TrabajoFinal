using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private VolumeValuesSO volumeSO;   
    [SerializeField] private AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volumeSO.MasterVolume) * 20);
    }
    public void setMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(volumeSO.MusicVolume) * 20);
    }
    public void setSfxVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volumeSO.SfxVolume) * 20);
    }
}
