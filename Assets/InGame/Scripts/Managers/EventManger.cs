using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Ty.ProjectSubak.Game
{
    public class EventManager : MonoBehaviour
    {
        #region StaticField
        static private EventManager instance;
        static public EventManager Instance
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
        private List<IObserver> obs;
        #endregion

        #region PublicMethod
        public void CallEvent(GameState nextGameSate)
        {
            foreach (IObserver observer in obs) observer.OnEvent(nextGameSate);
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

        }
        #endregion

    }
}

