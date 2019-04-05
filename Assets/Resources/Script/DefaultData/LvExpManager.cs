using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDataBase.DefaultGameData
{
    [Serializable]
    public class LvExpManager
    {
        public int UnitRateCount;
        public int[] UnitExp;
        public LvUpNeedExp UnitExpChart;
        public int[] LimitLvOfRate;

        public int SkillRateCount;
        public int[] SkillExp;
        public LvUpNeedExp SkillExpChart;

        public int ItemRateCount;
        public int[] ItemExp;
        public LvUpNeedExp ItemExpChart;

        public LvUpNeedExp UserExpChart;

    }
}