using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Ty.ProjectSubak.Game.GameManager;

public class UnitSpawner : MonoBehaviour
{
    #region Enum
    enum State
    {
        Idle,
        Init,
        Hold,
        Drop
    }
    #endregion

    #region SerializeField
    [Header("Init Data")]
    [SerializeField] private Transform initPos;
    [Header("Spawn Point")]
    [SerializeField] private Transform unitSpawnPoint;
    #endregion
    #region PrivateField
    private bool isWorking;
    private Unit unitHold;
    private State nowState;
    private State nextState;
    #endregion

    #region PublicMethod
    public void UpdateState()
    {
        if (nowState == nextState) return;

        if (nowState == State.Idle)
        {
            switch (nextState)
            {
                case State.Init:
                    nowState = nextState;
                    Init();
                    break;
                default:
                    break;
            }
        }
        else if (nowState == State.Init)
        {
            switch (nextState)
            {
                case State.Hold:
                    nowState = nextState;
                    HoldUnit();
                    break;
                default:
                    break;
            }
        }
        else if (nowState == State.Hold)
        {
            switch (nextState)
            {
                case State.Drop:
                    nowState = nextState;
                    DropUnit();
                    break;
                default:
                    break;
            }
        }
        else if (nowState == State.Drop)
        {
            switch (nextState)
            {
                case State.Hold:
                    nowState = nextState;
                    HoldUnit();
                    break;
                default:
                    break;
            }
        }
        return;
    }

    #endregion

    #region PrivateMethod
    public void Init()
    {
        if (unitHold != null) Destroy(unitHold.gameObject);
        transform.position = initPos.position;
        isWorking = true;
        nextState = State.Hold;
    }
    public void HoldUnit()
    {

    }
    public void DropUnit()
    {
        if (nowState != State.Drop || unitHold == null) return;
        nextState = State.Hold;
    }
    #endregion
}
