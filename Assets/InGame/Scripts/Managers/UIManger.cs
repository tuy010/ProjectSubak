using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ty.ProjectSubak.Game
{
    public class UIManger : ObserverBehaviour
    {
        #region Struct
        [System.Serializable]
        private struct UIPrefabs
        {
            public GameObject settingUI;
            public GameObject gameoverUI;
            public GameObject scoreUI;
            public GameObject nextUnitUI;
        }
        #endregion

        #region StaticField
        private static UIManger instance;
        public static UIManger Instance
        {
            get
            {
                if(instance == null)
                    return null;
                else return instance;
            }
        }
        #endregion
        #region SerializeField
        [Header("UI prefabs")]
        [SerializeField] private UIPrefabs uIPrefabs;
        #endregion

        #region PublicMethod
        public override void OnEvent(EventType eventType)
        {
            switch (eventType)
            {
                case EventType.InitGame:
                    Init();
                    break;
                case EventType.StartGame:
                    break;
                case EventType.OpenSetting:
                    OpenSetting();
                    break;
                case EventType.CloseSetting:
                    CloseSetting();
                    break;
                case EventType.EndGame:
                    OpenGameOverUI();
                    break;
                case EventType.RestartGame:
                    break;
                case EventType.ToLobby:
                    break;
                case EventType.ShutDown:
                    break;
                default:
                    break;
            } return;
        }
        #endregion

        #region PrivateMethod
        private void Init()
        {
            uIPrefabs.settingUI.SetActive(false);
            uIPrefabs.gameoverUI.SetActive(false);
        }
        private void OpenSetting()
        {
            uIPrefabs.settingUI.SetActive(true);
        }
        private void CloseSetting()
        {
            uIPrefabs.settingUI.SetActive(false);
        }
        private void OpenGameOverUI()
        {
            StartCoroutine(ShowGameOverUI());
        }
        #endregion

        #region Coroutine
        IEnumerator ShowGameOverUI()
        {
            yield return new WaitForSecondsRealtime(1.5f);
            uIPrefabs.gameoverUI.SetActive(true);
            yield break;
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
        #endregion
    }
}

