using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SettingsFunctions : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle shrinkToggle;
    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);

        shrinkToggle.isOn = (PlayerPrefs.GetInt("ShrinkToggle") == 1 ? true : false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnVolumeChanged()
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", volumeSlider.value);
    }

    public void OnShrinkToggle()
    {
        PlayerPrefs.SetInt("ShrinkToggle", shrinkToggle.isOn ? 1 : 0);
    }
}
