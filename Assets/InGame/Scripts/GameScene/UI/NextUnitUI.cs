using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ty.ProjectSubak.Game
{
    public class NextUnitUI : MonoBehaviour
    {
        #region SerializeField
        [SerializeField] List<GameObject> units;
        [SerializeField] UnitSpawner unitSpawner;
        #endregion

        #region PrivateField
        [SerializeField] private int unitLV;
        #endregion
        void Start()
        {
            foreach (var unit in units) unit.SetActive(false);
            unitLV = -1;
        }

        // Update is called once per frame
        void Update()
        {
            if (unitLV != unitSpawner.NextUnitLV)
            {
                if (unitLV != -1) units[unitLV].SetActive(false);
                unitLV = unitSpawner.NextUnitLV;
                units[unitLV].SetActive(true);
            }
        }
    }
}
    
