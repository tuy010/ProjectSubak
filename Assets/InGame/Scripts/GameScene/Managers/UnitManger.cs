using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ty.ProjectSubak.Game
{
    public class UnitManger : ObserverBehaviour
    {
        #region StaticField
        private static UnitManger instance;
        public static UnitManger Instance
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
        [Header("Related Objects")]
        [SerializeField] private UnitSpawner unitSpawner;
        [SerializeField] private Ceiling ceiling;


        [Header("Effect")]
        [SerializeField] private ParticleSystem mergeEffect;

        [Header("Audio")]
        [SerializeField] private AudioClip dropClip;
        [SerializeField] private AudioClip mergeSound;

        [Header("Prefabs")]
        [SerializeField] private List<GameObject> unitPrefabs;
        #endregion
        #region PrivateField
        private int unitCnt;
        #endregion

        #region PublicMethod
        public override void OnEvent(EventType eventType)
        {
            switch (eventType)
            {
                case EventType.InitGame:
                    Init();
                    break;
                case EventType.StartGame:
                    break;
                case EventType.OpenSetting:
                    unitSpawner.IsWorking = false;
                    break;
                case EventType.CloseSetting:
                    unitSpawner.IsWorking = true;
                    break;
                case EventType.EndGame:
                    unitSpawner.IsWorking = false;
                    break;
                case EventType.RestartGame:
                    break;
                case EventType.ToLobby:
                    break;
                case EventType.ShutDown:
                    break;
                default:
                    break;
            }
            return;
        }
        public Unit SpawnUnit(bool isHold, int unitLv, Vector3? pos = null, Vector2? vel = null)
        {
            if(isHold)
            {
                Unit unit = Instantiate(unitPrefabs[unitLv], unitSpawner.UnitSpawnPoint.position, Quaternion.identity).GetComponent<Unit>();
                unit.transform.SetParent(unitSpawner.UnitSpawnPoint);
                unit.Init(isHold, unitCnt++);
                return unit;
            }
            else
            {
                ParticleSystem particle = Instantiate(mergeEffect, pos.Value, Quaternion.identity);
                if (unitLv < 11)
                {
                    Unit unit = Instantiate(unitPrefabs[unitLv], pos.Value, Quaternion.identity).GetComponent<Unit>();
                    unit.Init(isHold, unitCnt++);
                    unit.GetComponent<Rigidbody2D>().velocity = vel.Value;
                }
                GameManager.Instance.Score += unitLv + 1;

                EffectAudioSource.Instance.AudioSource.clip = mergeSound;
                EffectAudioSource.Instance.AudioSource.Play();
                return null;
            }
            
        }
        #endregion

        #region PrivateMethod
        private void Init()
        {
            unitSpawner.nextState = UnitSpawner.State.Init;
            unitSpawner.dropClip = dropClip;
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

    
