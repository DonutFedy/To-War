using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDataBase
{
    namespace DefaultGameData
    {

        [Serializable]
        public class Default_MinionData
        {
            public string MinionName;
            public MinionType MinionType;
            public BasicStatus BasicStatus;
            public MinionLimitLV BreakLimit;
            public const int ItemNums = 3; // 아이템 장착 갯수
            public ItemType[] ItemSlotType = new ItemType[ItemNums]; // 착용가능 아이템 Type

            public const int SkillNums = 3; // 스킬 갯수
            public int[] OwnedSkills = new int[SkillNums]; // 소유 스킬 index list

            public BasicStatus AddStatusByLv; // lv업당 추가 스테이터스

            public string MainImagePath; // 일러스트
            public string BattleImagePath; // 전투 이미지

            public string Explanation;

        }
    }
}