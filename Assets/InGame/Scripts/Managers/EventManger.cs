using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Ty.ProjectSubak.Game
{
    #region Enum
    public enum EventType
    {
        InitGame,
        StartGame,
        OpenSetting,
        CloseSetting,
        EndGame,
        RestartGame,
        ToLobby,
        ShutDown
    }
    #endregion

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

        #region SerializeField
        [SerializeField] private List<ObserverBehaviour> obs;
        #endregion

        #region PublicMethod
        public void CallEvent(EventType eventType)
        {
            foreach (ObserverBehaviour observer in obs) observer.OnEvent(eventType);
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

