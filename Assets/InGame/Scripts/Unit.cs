using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    #region Enum
    public enum State
    {
        Idle,
        Hold,
        Drop,
        Ground
    }
    #endregion

    #region SerializeField
    [SerializeField] private int _level;
    public int Level => _level;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D cd;
    #endregion
    #region PrivateField
    private int _unitNum;
    public int unitNum => _unitNum;
    [SerializeField] private State currentState = State.Idle;
    [SerializeField] private State _nextState = State.Idle;
    public State nextState
    {
        get { return _nextState; }
        set{
            _nextState = value;
            UpdateState();
        }
    }
    #endregion

    #region PublicMethod
    public void Init(bool isHold, int unitNum)
    {
        _unitNum = unitNum;
        if(isHold)
        {
            nextState = State.Hold;
        }
        else
        {
            nextState = State.Ground;
        }   
    }
    #endregion

    #region PrivateMethod
    private void UpdateState()
    {
        if (currentState == nextState) return;

        if (currentState == State.Idle)
        {
            switch (nextState)
            {               
                case State.Hold:
                    currentState = nextState;
                    break;
                case State.Ground:
                    currentState = nextState;
                    break;
                default:
                    break;
            }
        }
        else if (currentState == State.Hold)
        {
            switch (nextState)
            {
                case State.Drop:
                    currentState = nextState;
                    Drop();
                    break;
                default:
                    break;
            }
        }
        else if (currentState == State.Drop)
        {
            switch (nextState)
            {
                case State.Ground:
                    currentState = nextState;
                    break;
                default:
                    break;
            }
        }
        else if (currentState == State.Ground)
        {

        }


        }
    private void Drop()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        cd.isTrigger = false;
        Vector3 tmp = transform.position;
        transform.SetParent(null, true);
        transform.position = tmp;
    }
    #endregion
}
