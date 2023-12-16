using System.Collections;
using System.Collections.Generic;
using Ty.ProjectSubak.Game;
using UnityEngine;

public class Unit : MonoBehaviour
{
    #region Enum
    public enum State
    {
        Idle,
        Hold,
        Drop,
        Ground,
        Merge
    }
    #endregion

    #region SerializeField
    [SerializeField] private int _level;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D cd;
    #endregion
    #region PrivateField
    [SerializeField] private int _unitNum;
    public int UnitNum => _unitNum;
    private State _currentState = State.Idle;
    private State _nextState = State.Idle;
    #endregion

    #region Get/Set
    public int Level => _level;
    public State CurrentState
    {
        get { return _currentState; }
        set { _currentState = value; }
    }
    public State NextState
    {
        get { return _nextState; }
        set
        {
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
            NextState = State.Hold;
        }
        else
        {
            NextState = State.Ground;
            rb.bodyType = RigidbodyType2D.Dynamic;
            cd.isTrigger = false;
            Vector3 tmp = transform.position;
            transform.SetParent(null, true);
            transform.position = tmp;
        }   
    }
    #endregion

    #region PrivateMethod
    private void UpdateState()
    {
        if (CurrentState == NextState) return;

        if (CurrentState == State.Idle)
        {
            switch (NextState)
            {               
                case State.Hold:
                    CurrentState = NextState;
                    break;
                case State.Ground:
                    CurrentState = NextState;
                    break;
                default:
                    break;
            }
        }
        else if (CurrentState == State.Hold)
        {
            switch (NextState)
            {
                case State.Drop:
                    CurrentState = NextState;
                    Drop();
                    break;
                default:
                    break;
            }
        }
        else if (CurrentState == State.Drop)
        {
            switch (NextState)
            {
                case State.Ground:
                    CurrentState = NextState;
                    break;
                default:
                    break;
            }
        }
        else if (CurrentState == State.Ground)
        {

        }


        }
    private void Drop()
    {
        float randX = Random.Range(-0.01f,0.01f);
        rb.bodyType = RigidbodyType2D.Dynamic;
        cd.isTrigger = false;
        Vector3 tmp = transform.position + new Vector3(randX, 0, 0);
        transform.SetParent(null, true);
        transform.position = tmp;
    }
    private void MergeUnit(Unit unit)
    {
        Vector2 thisV = transform.position;
        Vector2 inputV = unit.gameObject.transform.position;
        Vector2 outputV = new Vector2((thisV.x + inputV.x) / 2, (thisV.y + inputV.y) / 2);

        Vector2 thisVel = rb.velocity;
        Vector2 inputVel = unit.GetComponent<Rigidbody2D>().velocity;
        Vector2 outputVel = (thisVel + inputVel) / 2;

        /*
        float thisVelX = rb.velocity.x;
        float inputVelX = unit.GetComponent<Rigidbody2D>().velocity.x;
        Vector2 outputVel = new Vector2((thisVelX + inputVelX) / 2, 0);
        */
        if (this.Level != 10) UnitManger.Instance.SpawnUnit(false, this.Level + 1, outputV, outputVel);
        else GameManager.Instance.Score += 11;
        
        Destroy(unit.gameObject);
        Destroy(gameObject);
    }
    #endregion

    #region Unity
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (CurrentState == State.Drop)
        {
            if(col.gameObject.tag == "Unit")
            {
                NextState = State.Ground;
                Unit tmp = col.gameObject.GetComponent<Unit>();
                if (tmp.Level == this.Level)
                {
                    if(tmp.UnitNum < this.UnitNum)
                    {
                        this.CurrentState = State.Merge;
                        tmp.CurrentState = State.Merge;
                        MergeUnit(tmp);
                    }
                }
                else NextState = State.Ground;
            }
            else if(col.gameObject.tag == "Ground")
            {
                NextState = State.Ground;
            }
        }
        if (CurrentState == State.Ground)
        {
            if (col.gameObject.tag == "Unit")
            {
                Unit tmp = col.gameObject.GetComponent<Unit>();
                if (tmp.Level == this.Level && tmp.CurrentState != State.Merge)
                {
                    if (tmp.UnitNum < this.UnitNum)
                    {
                        this.CurrentState = State.Merge;
                        tmp.CurrentState = State.Merge;
                        MergeUnit(tmp);
                    }
                }
            }
        }
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        if(CurrentState == State.Drop && NextState == CurrentState)
        {
            if (col.gameObject.tag == "Unit")
            {
                NextState = State.Ground;
                Unit tmp = col.gameObject.GetComponent<Unit>();
                if (tmp.Level == this.Level)
                {
                    if (tmp.UnitNum < this.UnitNum)
                    {
                        this.CurrentState = State.Merge;
                        tmp.CurrentState = State.Merge;
                        MergeUnit(tmp);
                    }
                }
                else NextState = State.Ground;
            }
        }
    }
    #endregion
}
