using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ty.ProjectSubak.Game
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

    public class GameManager : MonoBehaviour
    {
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

            bool isUpdated = true;
            if (nowState == GameState.Idle)
            {
                switch (nextState)
                {
                    case GameState.Init:
                        InitGame();
                        break;
                    default:
                        isUpdated = false;
                        break;
                }
            }
            else if (nowState == GameState.Init)
            {
                switch (nextState)
                {
                    case GameState.GamePlay:
                        StarGame();
                        break;
                    default:
                        isUpdated = false;
                        break;
                }
            }
            else if (nowState == GameState.GamePlay)
            {
                switch (nextState)
                {
                    case GameState.Setting:
                        OpenSetting();
                        break;
                    case GameState.GameEnd:
                        EndGame();
                        break;
                    default:
                        isUpdated = false;
                        break;
                }
            }
            else if (nowState == GameState.Setting)
            {
                switch (nextState)
                {
                    case GameState.GamePlay:
                        CloseSetting();
                        break;
                    case GameState.ToLobby:
                        ToLobby();
                        break;
                    case GameState.ShutDown:
                        ShutDown();
                        break;
                    default:
                        isUpdated = false;
                        break;
                }
            }
            else if (nowState == GameState.GameEnd)
            {
                switch (nextState)
                {
                    case GameState.Idle:
                        RestartGame();
                        break;
                    case GameState.ToLobby:
                        ToLobby();
                        break;
                    case GameState.ShutDown:
                        ShutDown();
                        break;
                    default:
                        isUpdated = false;
                        break;
                }
            }
            else if (nowState == GameState.ToLobby)
            {
                isUpdated = false;
            }
            else if (nowState == GameState.ShutDown)
            {
                isUpdated = false;
            }

            if (isUpdated) nowState = nextState;
            else nextState = nowState;

            return;
        }
        #endregion

        #region PrivateMethods
        private void InitGame()
        {

        }
        private void StarGame()
        {

        }
        private void OpenSetting()
        {

        }
        private void CloseSetting()
        {

        }
        private void EndGame()
        {

        }
        private void RestartGame()
        {

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

