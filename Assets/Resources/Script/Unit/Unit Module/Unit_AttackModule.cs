using GameDataBase;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUnit
{
    public class Unit_AttackModule : Unit_Module
    {
        public static float criticalDamageRate = 1.5f;

        public override void SetModule(object _data = null)
        {
            status_M = (Unit_StatusModule)_data;
        }

        public void AttackEnemy()
        {
            if (status_M.bDetectEnemy == false)
            {
                return;
            }
            status_M.attackCoolTime = 1 / (status_M.mainStatus.AttackSpeed + status_M.bufStatus.AttackSpeed);

            float basicDamage = status_M.mainStatus.AttackPower + status_M.bufStatus.AttackPower;
            float criticalRate = status_M.mainStatus.CriticalChance + status_M.bufStatus.CriticalChance;
            if( (UnityEngine.Random.Range(0,101)/100f) < criticalRate)
            {
                basicDamage *= criticalDamageRate;
            }
            float defencePenetraction = status_M.mainStatus.DeffencePentraction + status_M.bufStatus.DeffencePentraction;

            DamageInfo info = new DamageInfo(onwer, basicDamage , defencePenetraction);
            status_M.enemyUnit.onHit_M.onHit(info);

            status_M.bEndTurn = true;
        }
    }
}