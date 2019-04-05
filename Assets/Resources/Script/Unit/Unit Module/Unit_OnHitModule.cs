using System;
using System.Collections;
using System.Collections.Generic;
using GameDataBase;
using UnityEngine;

namespace GameUnit
{
    public class Unit_OnHitModule : Unit_Module
    {
        public override void SetModule(object _data = null)
        {
            status_M = (Unit_StatusModule)_data;
        }

        internal void onHit(DamageInfo info)
        {
            float EvasiveRate = status_M.mainStatus.Evasive+status_M.bufStatus.Evasive;
            if((UnityEngine.Random.Range(0, 101) / 100f) < EvasiveRate)
            {
                //회피!
                Debug.Log("회피");
                return;
            }
            // 방어력은 1당 1퍼씩 데미지 감소
            float defence = status_M.mainStatus.Deffence + status_M.bufStatus.Deffence - info.defencePenetration;
            info.damage *= ((int)(100 - defence)) / 100f;


            string damageOwnerName = info.ownerUnit.status_M.unitName;
            Debug.Log(damageOwnerName + "은 "+ status_M.unitName+"에게 ["+ info.damage + "]의 데미지를 주었다\n"+ status_M.unitName + "의 HP : "+ status_M.curHP + " >> " + (status_M.curHP - info.damage));
            status_M.curHP -= info.damage;
            status_M.dontMoveCoolTime = status_M.dontMoveCoolTimeDefault;
            if (status_M.curHP<= 0)
            {
                Debug.LogError(status_M.unitName + "가 사망\n나중에 GM에 사망신고 보내야됨");
                status_M.bAlive = false;
                gameObject.SetActive(false);
            }
        }
    }
}