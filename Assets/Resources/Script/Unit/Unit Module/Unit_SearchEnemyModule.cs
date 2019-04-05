using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUnit
{
    public class Unit_SearchEnemyModule : Unit_Module
    {
        public override void SetModule(object _data = null)
        {
            status_M = (Unit_StatusModule)_data;
        }

        /// <summary>
        /// 매턴 적을 검사, 범위 밖으로 가면 새 적을 찾는다.
        /// 범위 안에 있다면 그대로 넘어간다
        /// </summary>
        public void SearchEnemy()
        {
            float detectRange = status_M.mainStatus.AttackRange + status_M.bufStatus.AttackRange;

            if(status_M.enemyUnit == null || status_M.enemyUnit.status_M.bAlive == false || Vector2.Distance(transform.position, status_M.enemyUnit.transform.position) > detectRange)
            {
                status_M.bDetectEnemy = SearchEnemyUnitArroundAttackRange();
            }
        }

        bool SearchEnemyUnitArroundAttackRange()
        {
            bool result = false;

            //Debug.LogError("나중에 GM으로 바꿔야되~");
            List<UnitController> units = TestMakeUnit.Instance.unitList;

            UnitController detectUnit = null;
            float minDistance = float.MaxValue;
            float curDistance = -1;
            float detectedRange = status_M.mainStatus.AttackRange + status_M.bufStatus.AttackRange;
            for (int i = 0; i < units.Count; ++i)
            {
                if (units[i].status_M.bAlive == false)
                    continue;
                curDistance = Vector2.Distance(transform.position, units[i].transform.position);
                if ( status_M.battleType !=units[i].status_M.battleType && detectedRange >= curDistance && minDistance > curDistance)
                {
                    detectUnit = units[i];
                    minDistance = curDistance;
                    result = true;
                }
            }
            if(detectUnit != null)
            {
                status_M.enemyUnit=detectUnit;
            }

            return result;
        }
    }

}