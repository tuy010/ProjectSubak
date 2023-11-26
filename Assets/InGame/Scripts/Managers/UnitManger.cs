using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ty.ProjectSubak.Game
{
    public class UnitManger : MonoBehaviour, IObserver
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
        [SerializeField] private UnitSpawner unitSpawner;
        [SerializeField] private Ceiling ceiling;
        #endregion
        #region PrivateField
        private int unitCnt;
        #endregion

        #region PublicMethod
        public void OnEvent(GameState newGameState)
        {

        }
        public void SpawnUnit(bool isHold, int unitLv, Vector2? pos = null)
        {

        }
        #endregion

    }
}

    
