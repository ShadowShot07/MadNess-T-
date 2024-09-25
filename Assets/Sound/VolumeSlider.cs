using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [Header("Type")]
    [SerializeField] private AudioManager.BusType volumeType;

    private Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = GetComponent<Slider>();
        volumeSlider.value = .8f;
        volumeSlider.onValueChanged.AddListener(OnVolumeValueChanged);
    }

    private void Start()
    {
        OnVolumeValueChanged(volumeSlider.value);
    }

    private void Update()
    {
        if (isActiveAndEnabled)
        switch (volumeType)
        {
            case AudioManager.BusType.MASTER:
                volumeSlider.value = AudioManager.instance.masterVolume;
                break;
            case AudioManager.BusType.MUSIC:
                volumeSlider.value = AudioManager.instance.musicVolume;
                break;
            case AudioManager.BusType.SFX:
                volumeSlider.value = AudioManager.instance.sfxVolume;
                break;
            default:
                Debug.Log("Bus Type not supported: " + volumeType);
                break;
        }
    }

    public void OnVolumeValueChanged(float newVolume)
    {
        AudioManager.instance.ChangeBusVolume(volumeType, newVolume);
    }

}
