using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ty.ProjectSubak.Game
{
    public class VoiumeSetting : MonoBehaviour
    {
        [SerializeField] private Slider BGMSlider;
        [SerializeField] private Slider EffectSlider;

        public void Start ()
        {
            if(!PlayerPrefs.HasKey("BGMVolume")) PlayerPrefs.SetFloat("BGMVolume", 1.0f);
            BGMSlider.value = Mathf.RoundToInt(PlayerPrefs.GetFloat("BGMVolume")*100);

            if (!PlayerPrefs.HasKey("BGMVolume")) PlayerPrefs.SetFloat("BGMVolume", 1.0f);
            BGMSlider.value = Mathf.RoundToInt(PlayerPrefs.GetFloat("BGMVolume") * 100);
        }

        public void OnChangeVolume()
        {
            float tmp = Mathf.RoundToInt(BGMSlider.value) / 100f;
            PlayerPrefs.SetFloat("BGMVolume", tmp);
            BGMAudioSource.Instance.AudioSource.volume = tmp;

            tmp = Mathf.RoundToInt(EffectSlider.value) / 100f;
            PlayerPrefs.SetFloat("EffectVolume", tmp);
            EffectAudioSource.Instance.AudioSource.volume = tmp;
        }
    }
}


