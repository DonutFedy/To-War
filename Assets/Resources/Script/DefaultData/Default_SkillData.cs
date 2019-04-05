using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameDataBase.DefaultGameData
{
    [Serializable]
    public class Default_SkillData
    {
        public string SkillName;

        public BasicStatus BasicStatus;
        public BasicStatus BufStatus;
        public SkillType SkillType;
        public BasicStatus AddStatusByLv;
        public BasicStatus AddBufStatusByLv;

        public int ModuleIndex;

        public string MainImagePath;
        public string EffectImagePath;
        public string Explantion;

        public Default_SkillData(string skillName, BasicStatus basicStatus, BasicStatus bufStatus1, SkillType skillType, BasicStatus addStatusByLv, BasicStatus addBufStatusByLv, int moduleIndex, string mainImagePath, string effectImagePath, string explantion)
        {
            SkillName = skillName ?? throw new ArgumentNullException(nameof(skillName));
            BasicStatus = basicStatus ?? throw new ArgumentNullException(nameof(basicStatus));
            BufStatus = bufStatus1 ?? throw new ArgumentNullException(nameof(bufStatus1));
            SkillType = skillType;
            AddStatusByLv = addStatusByLv ?? throw new ArgumentNullException(nameof(addStatusByLv));
            AddBufStatusByLv = addBufStatusByLv ?? throw new ArgumentNullException(nameof(addBufStatusByLv));
            ModuleIndex = moduleIndex;
            MainImagePath = mainImagePath ?? throw new ArgumentNullException(nameof(mainImagePath));
            EffectImagePath = effectImagePath ?? throw new ArgumentNullException(nameof(effectImagePath));
            Explantion = explantion ?? throw new ArgumentNullException(nameof(explantion));
        }

    }
}