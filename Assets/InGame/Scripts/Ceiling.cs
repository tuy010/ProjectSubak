using System.Collections;
using System.Collections.Generic;
using Ty.ProjectSubak.Game;
using UnityEngine;

namespace Ty.ProjectSubak.Game
{
    public class Ceiling : MonoBehaviour
    {
        #region PrivateField
        [SerializeField] private List<Unit> units;
        private bool isEnd;
        #endregion

        #region Unity
        private void Start()
        {
            isEnd = false;
            units = new List<Unit>();
        }
        private void Update()
        {
            if (!isEnd)
            {
                foreach (var unit in units)
                {
                    if (unit.CurrentState == Unit.State.Ground)
                    {
                        isEnd = true;
                        Debug.Log("End");
                        GameManager.Instance.OnEnd();
                        break;
                    }
                }
            }

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Unit tmp = collision.GetComponent<Unit>(); //It's Heavy, How to Improve?
            if (tmp != null)
            {
                if (tmp.CurrentState == Unit.State.Ground) GameManager.Instance.OnEnd();
                else units.Add(tmp);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            Unit tmp = collision.GetComponent<Unit>();
            if (tmp != null)
            {
                units.Remove(tmp);
            }
        }
        #endregion
    }
}
