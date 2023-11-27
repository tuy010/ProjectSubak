using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        #region PrivateField
        private GameState nowState = GameState.Idle;
        private GameState nextState = GameState.Idle;
        public GameState NextState
        {
            set
            {
                nextState = value;
                UpdateState();
            }
        }
        #endregion
        #region PublicMethods
        public void UpdateState()
        {
            if (nowState == nextState) return;

            if (nowState == GameState.Idle)
            {
                switch (nextState)
                {
                    case GameState.Init:
                        nowState = nextState;
                        InitGame();
                        break;
                    default:
                        nextState = nowState;
                        break;
                }
            }
            else if (nowState == GameState.Init)
            {
                switch (nextState)
                {
                    case GameState.GamePlay:
                        nowState = nextState;
                        StarGame();
                        break;
                    default:
                        nextState = nowState;
                        break;
                }
            }
            else if (nowState == GameState.GamePlay)
            {
                switch (nextState)
                {
                    case GameState.Setting:
                        nowState = nextState;
                        OpenSetting();
                        break;
                    case GameState.GameEnd:
                        nowState = nextState;
                        EndGame();
                        break;
                    default:
                        nextState = nowState;
                        break;
                }
            }
            else if (nowState == GameState.Setting)
            {
                switch (nextState)
                {
                    case GameState.GamePlay:
                        nowState = nextState;
                        CloseSetting();
                        break;
                    case GameState.ToLobby:
                        nowState = nextState;
                        ToLobby();
                        break;
                    case GameState.ShutDown:
                        nowState = nextState;
                        ShutDown();
                        break;
                    default:
                        nextState = nowState;
                        break;
                }
            }
            else if (nowState == GameState.GameEnd)
            {
                switch (nextState)
                {
                    case GameState.Idle:
                        nowState = nextState;
                        RestartGame();
                        break;
                    case GameState.ToLobby:
                        nowState = nextState;
                        ToLobby();
                        break;
                    case GameState.ShutDown:
                        nowState = nextState;
                        ShutDown();
                        break;
                    default:
                        nextState = nowState;
                        break;
                }
            }
            else if (nowState == GameState.ToLobby)
            {
                
            }
            else if (nowState == GameState.ShutDown)
            {
                
            }


            return;
        }
        #endregion

        #region PrivateMethods
        private void InitGame()
        {
            EventManager.Instance.CallEvent(EventType.InitGame);
        }
        private void StarGame()
        {
            EventManager.Instance.CallEvent(EventType.StartGame);
        }
        private void OpenSetting()
        {
            EventManager.Instance.CallEvent(EventType.OpenSetting);
        }
        private void CloseSetting()
        {
            EventManager.Instance.CallEvent(EventType.CloseSetting);
        }
        private void EndGame()
        {
            EventManager.Instance.CallEvent(EventType.EndGame);
        }
        private void RestartGame()
        {
            EventManager.Instance.CallEvent(EventType.RestartGame);
        }
        private void ToLobby()
        {

        }
        private void ShutDown()
        {
            
        }
        #endregion

        #region Unity
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                NextState = GameState.Init;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Update()
        {

        }
        #endregion

    }
}

