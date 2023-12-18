using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ty.ProjectSubak.Lobby
{
    public class Lobby : MonoBehaviour
    {
        #region enum
        enum State
        {
            Init,
            ShowLogo,
            MoveLogo,
            ShowButton,
            Fin
        }
        #endregion

        #region SerializeField
        [Header("UI")]
        [SerializeField] private UIAnimation logoUI;
        [SerializeField] private UIAnimation startButtonUI;
        [SerializeField] private UIAnimation endButtonUI;
        #endregion

        #region PrivateField
        [SerializeField] private State currentState;
        private State _nextState;
        private State nextState
        {
            get { return _nextState; }
            set
            {
                _nextState = value;
                UpdateState();
            }
        }
        private bool isWaitAnimation = false;
        #endregion

        #region PubliceMethod
        public void OnStartGame()
        {
            if (currentState != State.Fin) return;

            SceneManager.LoadScene("GameScene");
        }
        public void OnExitGame()
        {
            if (currentState != State.Fin) return;

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        }
        #endregion

        #region PrivateMethod
        private void UpdateState()
        {
            if (currentState == nextState) return;

            if (currentState == State.Init)
            {
                switch (nextState)
                {
                    case State.ShowLogo:                                    
                        logoUI.CallEffect(0);
                        Debug.Log("1" + logoUI.isWorking);
                        currentState = nextState;
                        isWaitAnimation = true;
                        break;
                    default:
                        nextState = currentState;
                        break;
                }
            }
            else if (currentState == State.ShowLogo)
            {
                switch (nextState)
                {
                    case State.MoveLogo:                                             
                        logoUI.CallEffect(1);
                        currentState = nextState;
                        isWaitAnimation = true;
                        break;
                    default:
                        nextState = currentState;
                        break;
                }
            }
            else if(currentState == State.MoveLogo)
            {
                switch (nextState)
                {
                    case State.ShowButton:
                        currentState = nextState;
                        startButtonUI.CallEffect(0);
                        endButtonUI.CallEffect(0);
                        isWaitAnimation = true;
                        break;
                    default:
                        nextState = currentState;
                        break;
                }
            }
            else if(currentState == State.ShowButton)
            {
                switch (nextState)
                {
                    case State.Fin:
                        currentState = nextState;
                        break;
                    default:
                        nextState = currentState;
                        break;
                }
            }
            else if(currentState == State.Fin)
            {
                switch (nextState)
                {
                    default:
                        nextState = currentState;
                        break;
                }
            }
        }
        #endregion

        #region Unity
        void Start()
        {
            Application.targetFrameRate = 60;
            nextState = State.ShowLogo;
        }

        void Update()
        {
            if (isWaitAnimation)
            {
                switch (currentState)
                {
                    case State.ShowLogo:
                        if (!logoUI.isWorking)
                        {
                            isWaitAnimation = false;
                            nextState = State.MoveLogo;
                        }
                        break;
                    case State.MoveLogo:
                        if (!logoUI.isWorking)
                        {
                            isWaitAnimation = false;
                            nextState = State.ShowButton;
                        }
                        break;
                    case State.ShowButton:
                        if (!startButtonUI.isWorking)
                        {
                            isWaitAnimation = false;
                            nextState = State.Fin;
                        }
                        break;
                    default:
                        break;
                }
            }
           
        }
        #endregion
    }
}


