using GameDataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUnit
{
    public class Unit_StatusModule : Unit_Module
    {
        [Header("Basic")]
        /// <summary>
        /// 유닛을 판별할때 쓰임
        /// </summary>
        public int          unitCode;
        public int          ownedIndex;
        public string       unitName;
        public DecType      decType;
        public BattleType   battleType;
        public int          nodePos;
        public BasicStatus  mainStatus;
        public BasicStatus  bufStatus;
        public float        curHP;
        public float        curMP;

        public Sprite       curSprite;
        public bool         bEndTurn;
        public bool         bAlive;
        public float        dontMoveCoolTime;
        public float        dontMoveCoolTimeDefault = 1f;
        public float        attackCoolTime = 0;
        public float        attackCoolTimeDefault = 1f;
        // 버프 리스트 추가요망


        [Header("Move")]
        public List<int> moveRoot = new List<int>();

        [Header("EnemyDetect")]
        public UnitController   enemyUnit;
        public bool             bDetectEnemy;

        #region Test

        [Header("Test")]
        public static MapData mapData;
        #endregion


        public override void SetModule(object _data = null)
        {
            Debug.LogError("버프 리스트 추가 요망");
            UnitSetData data = (UnitSetData)_data;

            unitCode    = data.indexOfSerialize;
            battleType  = data.battleType;
            nodePos     = data.indexOfNode;

            ownedIndex = data.indexOfOwnedUnit;
            decType = data.decType;

            DataArea db = Management.MenuGameManager.Instance.DataArea;
            if( data.decType == DecType.Minion)
            {
                unitName = db.DefaultMinionData[db.OwnedData.MinionData[data.indexOfOwnedUnit].IndexOfMinion].MinionName;
                mainStatus = db.DefaultMinionData[db.OwnedData.MinionData[data.indexOfOwnedUnit].IndexOfMinion].BasicStatus + db.OwnedData.MinionData[data.indexOfOwnedUnit].AddStatus;
                curSprite = Resources.Load<Sprite>( db.DefaultMinionData[db.OwnedData.MinionData[data.indexOfOwnedUnit].IndexOfMinion].MainImagePath);
            }
            else
            {
                unitName = db.DefaultSkillData[db.OwnedData.SkillData[data.indexOfOwnedUnit].IndexOfType].SkillName;
                mainStatus = db.DefaultSkillData[db.OwnedData.SkillData[data.indexOfOwnedUnit].IndexOfType].BasicStatus + db.OwnedData.SkillData[data.indexOfOwnedUnit].AddStatus;
                curSprite = Resources.Load<Sprite>(db.DefaultSkillData[db.OwnedData.SkillData[data.indexOfOwnedUnit].IndexOfType].MainImagePath);
            }

            bufStatus = new BasicStatus();

            curHP = mainStatus.Hp + bufStatus.Hp;
            curMP = mainStatus.Mp + bufStatus.Mp;

            moveRoot.Clear();

            bAlive          = true;
            bDetectEnemy    = false;
            bEndTurn        = true;
        }

    }
}