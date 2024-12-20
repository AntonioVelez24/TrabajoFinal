using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VolumeValues", menuName = "VolumeValuesSO")]
public class VolumeValuesSO : ScriptableObject
{
    [SerializeField] public float MasterVolume = 0.5f;
    [SerializeField] public float MusicVolume = 0.5f;
    [SerializeField] public float SfxVolume = 0.5f;

}
