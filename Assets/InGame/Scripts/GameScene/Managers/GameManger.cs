using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

namespace Ty.ProjectSubak.Game
{
    public class GameManager : MonoBehaviour
    {
        #region Enum
        public enum GameState
        {
            Idle,
            Init,
            GamePlay,
            Setting,
            GameEnd,
            ToLobby,
            ShutDown
        }
        #endregion

        #region StaticField
        static private GameManager instance;
        static public GameManager Instance
        {
            get
            {
                if (instance == null)
                    return null;
                else return instance;
            }
        }
        #endregion

        #region SerializeField
        [Header("Sound")]
        [SerializeField] private AudioClip buttonClip;
        #endregion

        #region PrivateField
        [SerializeField] private GameState currentState = GameState.Idle;
        private GameState _nextState = GameState.Idle;
        private int score;
        private int highScore;
        #endregion

        #region Get/Set
        public GameState nextState
        {
            get
            {
                return _nextState;
            }
            set
            {
                _nextState = value;
                UpdateState();
            }
        }
        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
            }
        }
        #endregion

        #region PublicMethods
        public void UpdateState()
        {
            if (currentState == nextState) return;

            if (currentState == GameState.Idle)
            {
                switch (nextState)
                {
                    case GameState.Init:
                        currentState = nextState;
                        InitGame();
                        break;
                    default:
                        nextState = currentState;
                        break;
                }
            }
            else if (currentState == GameState.Init)
            {
                switch (nextState)
                {
                    case GameState.GamePlay:
                        currentState = nextState;
                        StarGame();
                        break;
                    default:
                        nextState = currentState;
                        break;
                }
            }
            else if (currentState == GameState.GamePlay)
            {
                switch (nextState)
                {
                    case GameState.Setting:
                        currentState = nextState;
                        OpenSetting();
                        break;
                    case GameState.GameEnd:
                        currentState = nextState;
                        EndGame();
                        break;
                    default:
                        nextState = currentState;
                        break;
                }
            }
            else if (currentState == GameState.Setting)
            {
                switch (nextState)
                {
                    case GameState.Idle:
                        currentState = nextState;
                        RestartGame();
                        break;
                    case GameState.GamePlay:
                        currentState = nextState;
                        CloseSetting();
                        break;
                    case GameState.ToLobby:
                        currentState = nextState;
                        ToLobby();
                        break;
                    case GameState.ShutDown:
                        currentState = nextState;
                        ShutDown();
                        break;
                    default:
                        nextState = currentState;
                        break;
                }
            }
            else if (currentState == GameState.GameEnd)
            {
                switch (nextState)
                {
                    case GameState.Idle:
                        currentState = nextState;
                        RestartGame();
                        break;
                    case GameState.ToLobby:
                        currentState = nextState;
                        ToLobby();
                        break;
                    case GameState.ShutDown:
                        currentState = nextState;
                        ShutDown();
                        break;
                    default:
                        nextState = currentState;
                        break;
                }
            }
            else if (currentState == GameState.ToLobby)
            {
                
            }
            else if (currentState == GameState.ShutDown)
            {
                
            }


            return;
        }

        public void OnRestart()
        {
            EffectAudioSource.Instance.AudioSource.clip = buttonClip;
            EffectAudioSource.Instance.AudioSource.Play();
            if (currentState == GameState.Setting || currentState == GameState.GameEnd)
            {
                nextState = GameState.Idle;
            }

        }
        public void OnToLobby()
        {
            EffectAudioSource.Instance.AudioSource.clip = buttonClip;
            EffectAudioSource.Instance.AudioSource.Play();
            if (currentState == GameState.Setting || currentState == GameState.GameEnd) nextState = GameState.ToLobby;
        }
        public void OnEnd()
        {
            nextState = GameState.GameEnd;
        }
        public void OnShutDown()
        {
            EffectAudioSource.Instance.AudioSource.clip = buttonClip;
            EffectAudioSource.Instance.AudioSource.Play();
            if (currentState == GameState.ToLobby || currentState == GameState.Setting) nextState = GameState.ShutDown;
        }
        #endregion

        #region PrivateMethods
        private void InitGame()
        {
            Application.targetFrameRate = 60;
            Score = 0;
            EventManager.Instance.CallEvent(EventType.InitGame);
            nextState = GameState.GamePlay;
        }
        private void StarGame()
        {
            EventManager.Instance.CallEvent(EventType.StartGame);
        }
        private void OpenSetting()
        {
            EventManager.Instance.CallEvent(EventType.OpenSetting);
            Time.timeScale = 0;
        }
        private void CloseSetting()
        {
            EventManager.Instance.CallEvent(EventType.CloseSetting);
            Time.timeScale = 1;
        }
        private void EndGame()
        {
            Time.timeScale = 0;
            EventManager.Instance.CallEvent(EventType.EndGame);
            if (! PlayerPrefs.HasKey("HighScore")) PlayerPrefs.SetInt("HighScore", 0);
            if (PlayerPrefs.GetInt("HighScore") < score) PlayerPrefs.SetInt("HighScore", score);
        }
        private void RestartGame()
        {
            EffectAudioSource.Instance.AudioSource.clip = buttonClip;
            Time.timeScale = 1;
            EventManager.Instance.CallEvent(EventType.RestartGame);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);   
        }
        private void ToLobby()
        {
            Time.timeScale = 1;
            Destroy(BGMAudioSource.Instance.gameObject);
            Destroy(EffectAudioSource.Instance.gameObject);
            SceneManager.LoadScene("Lobby");
        }
        private void ShutDown()
        {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        }
        #endregion

        #region InputSysMethods
        public void OnBackKey()
        {
            Debug.Log("!");
            EffectAudioSource.Instance.AudioSource.clip = buttonClip;
            EffectAudioSource.Instance.AudioSource.Play();
            if (currentState == GameState.GamePlay) nextState = GameState.Setting;
            else if(currentState == GameState.Setting) nextState = GameState.GamePlay;
        }
        #endregion

        #region Unity
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        private void Update()
        {
            if(currentState == GameState.Idle) { nextState = GameState.Init; }
        }
        #endregion

    }
}

