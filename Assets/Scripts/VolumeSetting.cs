using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolumeSetting : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Slider volumnSlider;
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume",1);
            Load();
        }
        else
        {
            Load();
        }
    }

    // Update is called once per frame

    public void ChangeVolume()
    {
        AudioListener.volume = volumnSlider.value;
        Save();
    }

    public void Load()
    {
        volumnSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumnSlider.value);
    }
}
