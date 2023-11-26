using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ty.ProjectSubak.Game
{
    public interface IObserver 
    {
        public void OnEvent(GameState nextGameState);
    }
}
