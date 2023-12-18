using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ty.ProjectSubak.Game
{
    public class BGMAudioSource : MonoBehaviour
    {
        private static BGMAudioSource instance;
        public static BGMAudioSource Instance
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
        [SerializeField] private AudioClip BGMClip;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);

                if(!PlayerPrefs.HasKey("BGMVolume")) PlayerPrefs.SetFloat("BGMVolume", 1.0f);
                audioSource.volume = PlayerPrefs.GetFloat("BGMVolume");
                audioSource.clip = BGMClip;
                audioSource.Play();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}

