using System.Collections;
using System.Collections.Generic;
using Ty.ProjectSubak.Game;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static Ty.ProjectSubak.Game.GameManager;

namespace Ty.ProjectSubak.Game
{
    public class UnitSpawner : MonoBehaviour
    {
        #region Enum
        public enum State
        {
            Idle,
            Init,
            Hold,
            Drop
        }
        #endregion

        #region SerializeField
        [Header("Position Data")]
        [SerializeField] private Transform initPos;
        [SerializeField] private Transform unitSpawnPoint;
        [SerializeField] private Transform boundaryL;
        [SerializeField] private Transform boundaryR;

        [Header("Etc")]
        [SerializeField] private float speed;
        [SerializeField] private int minSpawnUnitLV = 0;
        [SerializeField] private int maxSpawnUnitLV = 4;
        #endregion

        #region PrivateField
        private bool isWorking;
        private Unit unitHold;
        private State currentState;
        private State _nextState;
        public State nextState
        {
            get { return _nextState; }
            set
            {
                _nextState = value;
                UpdateState();
            }
        }
        private int nowUnitLV;
        private int nextUnitLV;
        private float moveDir;
        #endregion

        #region PublicField
        public AudioClip dropClip;
        #endregion

        #region Get/Set
        public Transform UnitSpawnPoint => unitSpawnPoint;
        public int NextUnitLV => nextUnitLV;
        public bool IsWorking
        {
            get { return isWorking; }
            set { isWorking = value; }
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
                    case State.Init:
                        currentState = nextState;
                        Init();
                        break;
                    default:
                        break;
                }
            }
            else if (currentState == State.Init)
            {
                switch (nextState)
                {
                    case State.Hold:
                        currentState = nextState;
                        HoldUnit();
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
                        DropUnit();
                        break;
                    default:
                        break;
                }
            }
            else if (currentState == State.Drop)
            {
                switch (nextState)
                {
                    case State.Hold:
                        currentState = nextState;
                        HoldUnit();
                        break;
                    default:
                        break;
                }
            }
            return;
        }
        private void Init()
        {
            if (unitHold != null) Destroy(unitHold.gameObject);
            nextUnitLV = Random.Range(minSpawnUnitLV, maxSpawnUnitLV + 1);
            transform.position = initPos.position;
            isWorking = true;
            nextState = State.Hold;
        }
        private void HoldUnit()
        {
            unitHold = UnitManger.Instance.SpawnUnit(true, nextUnitLV);
            nowUnitLV = nextUnitLV;
            nextUnitLV = Random.Range(minSpawnUnitLV, maxSpawnUnitLV + 1);
        }
        private void DropUnit()
        {
            if (currentState != State.Drop || unitHold == null)
            {
                Debug.Log("DROP FALSE");
                return;
            }
            unitHold.NextState = Unit.State.Drop;
        }
        #endregion

        #region InputSysMethods
        private void OnMove(InputValue value)
        {
            if (!isWorking) return;
            float input = value.Get<float>();
            moveDir = input;
        }
        private void OnDrop()
        {
            if (!isWorking) return;
            if (currentState == State.Hold)
            {
                nextState = State.Drop;
                EffectAudioSource.Instance.AudioSource.clip = dropClip;
                EffectAudioSource.Instance.AudioSource.Play();
            }
        }
        #endregion

        #region Unity
        private void Update()
        {
            if (isWorking)
            {
                switch (currentState)
                {
                    case State.Idle:
                        break;
                    case State.Init:
                        Init();
                        break;
                    case State.Hold:
                        if (moveDir != 0)
                        {
                            float x = Mathf.Clamp(gameObject.transform.position.x + moveDir * speed * Time.deltaTime, boundaryL.position.x, boundaryR.position.x);
                            gameObject.transform.position = new Vector3(x, gameObject.transform.position.y, gameObject.transform.position.z);
                        }
                        break;
                    case State.Drop:
                        if (moveDir != 0)
                        {
                            float x = Mathf.Clamp(gameObject.transform.position.x + moveDir * speed * Time.deltaTime, boundaryL.position.x, boundaryR.position.x);
                            gameObject.transform.position = new Vector3(x, gameObject.transform.position.y, gameObject.transform.position.z);
                        }
                        if (unitHold == null) nextState = State.Hold;
                        else if (unitHold.CurrentState == Unit.State.Ground) nextState = State.Hold;
                        break;
                    default:
                        break;
                }
                ;
            }
        }
        #endregion
    }
}