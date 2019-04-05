using GameDataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUnit
{
    /// <summary>
    /// 이 객체는 기본적으로 유닛이 작동하는 모듈들을 제어합니다.
    /// </summary>
    public class UnitController : MonoBehaviour
    {

        // (1) Search Road Module
        public Unit_SearchRoadModule        searchRoad_M;

        // (2) Search Enemy Module
        public Unit_SearchEnemyModule       searchEnemy_M;

        // (3) movement Module 이동 모듈
        public Unit_MovementModule          movement_M;

        // (4) attack module 공격 모듈
        public Unit_AttackModule            attack_M;

        // (5) onHit module 피격 모듈
        public Unit_OnHitModule             onHit_M;

        // (6) image module 이미지 관리 모듈
        public Unit_ImageModule             image_M;

        // data module 정보 모듈
        public Unit_StatusModule            status_M;
        // 각 모듈은 정보 모듈을 참조한다

        /// <summary>
        /// 유닛을 세팅한다
        /// </summary>
        /// <param name="_data">세팅할 데이터를 삽입</param>
        public void SetUnit(object _data)
        {
            status_M.onwer = this;
            status_M.SetModule(_data);

            searchRoad_M.onwer = this;
            searchRoad_M.SetModule(status_M);
            searchEnemy_M.onwer = this;
            searchEnemy_M.SetModule(status_M);
            movement_M.onwer = this;
            movement_M.SetModule(status_M);
            attack_M.onwer = this;
            attack_M.SetModule(status_M);
            onHit_M.onwer = this;
            onHit_M.SetModule(status_M);
            image_M.onwer = this;
            image_M.SetModule(status_M);

            // 게임 매니저에게 자신의 정보 전달
            // 리스트에 추가할때 ㅇㅇ
        }

        public void UnitUpdate()
        {
            status_M.bEndTurn = false;
            // (2)
            searchEnemy_M.SearchEnemy();

            // (3)  (4)
            if (status_M.battleType == BattleType.Offence)
            {
                if ( status_M.dontMoveCoolTime > 0)
                {
                    status_M.dontMoveCoolTime -= Time.deltaTime;
                }
                else
                {
                    movement_M.MoveUpdate();
                }
            }
            if (status_M.attackCoolTime > 0f)
            {
                status_M.attackCoolTime -= Time.deltaTime;
            }
            else if (status_M.bDetectEnemy)
            {
                attack_M.AttackEnemy();
            }
        }

        /// <summary>
        /// 피격 당할시에 작동
        /// </summary>
        /// <param name="_data">공격 측의 데이터</param>
        public void OnHitUnit(object _data)
        {
            // (5)
        }
    }
}