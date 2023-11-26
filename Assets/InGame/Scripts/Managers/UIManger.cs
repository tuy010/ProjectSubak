using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ty.ProjectSubak.Game
{
    public class UIManger : MonoBehaviour, IObserver
    {
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

        #region PublicMethod
        public void OnEvent(GameState newGameState)
        {

        }
        #endregion
    }
}

