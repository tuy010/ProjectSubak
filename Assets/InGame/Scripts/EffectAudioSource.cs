using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ty.ProjectSubak.Game
{
    public class EffectAudioSource : MonoBehaviour
    {
        private static EffectAudioSource instance;
        public static EffectAudioSource Instance
        {
            get
            {
                if (instance == null)
                    return null;
                else return instance;
            }
        }

        [SerializeField] private AudioSource audioSource;
        public AudioSource AudioSource => audioSource;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                if (!PlayerPrefs.HasKey("EffectVolume")) PlayerPrefs.SetFloat("EffectVolume", 1.0f);
                audioSource.volume = PlayerPrefs.GetFloat("EffectVolume");
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
