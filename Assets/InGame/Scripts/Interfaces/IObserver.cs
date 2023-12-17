using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ty.ProjectSubak.Game
{
    public abstract class ObserverBehaviour : MonoBehaviour
    {
        public abstract void OnEvent(EventType eventType);
    }
}
