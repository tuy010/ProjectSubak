using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ty.ProjectSubak.Game
{
    public class UnitManger : ObserverBehaviour
    {
        #region StaticField
        private static UnitManger instance;
        public static UnitManger Instance
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
        [Header("Related Objects")]
        [SerializeField] private UnitSpawner unitSpawner;
        [SerializeField] private Ceiling ceiling;

        [Header("Prefabs")]
        [SerializeField] private List<GameObject> unitPrefabs;
        #endregion
        #region PrivateField
        private int unitCnt;
        #endregion

        #region PublicMethod
        public override void OnEvent(EventType eventType)
        {
            switch (eventType)
            {
                case EventType.InitGame:
                    break;
                case EventType.StartGame:
                    break;
                case EventType.OpenSetting:
                    break;
                case EventType.CloseSetting:
                    break;
                case EventType.EndGame:
                    break;
                case EventType.RestartGame:
                    break;
                case EventType.ToLobby:
                    break;
                case EventType.ShutDown:
                    break;
                default:
                    break;
            }
            return;
        }
        public void SpawnUnit(bool isHold, int unitLv, Vector2? pos = null)
        {

        }
        #endregion

        #region PrivateMethod
        private void Init()
        {

        }
        #endregion
    }
}

    
