using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDataBase.DefaultGameData
{
    [Serializable]
    public class Default_MinionSkillData
    {
        public SkillType SkillType;
        public string SkillName;
        public BasicStatus BasicStatus;
        public BasicStatus AddStatusByLv;
        public BasicStatus BufStatus;
        public BasicStatus AddBufStatusByLv;

        public int ModuleIndex;

        public string MainImagePath;
        public string EffectImagePath;
        public string Explanation;



        public Default_MinionSkillData(SkillType skillType, string skillName, BasicStatus basicStatus, BasicStatus addStatusByLv, BasicStatus bufStatus, BasicStatus addBufStatusByLv, int moduleIndex, string mainImagePath, string effectImagePath, string explanation)
        {
            SkillType = skillType;
            SkillName = skillName ?? throw new ArgumentNullException(nameof(skillName));
            BasicStatus = basicStatus ?? throw new ArgumentNullException(nameof(basicStatus));
            AddStatusByLv = addStatusByLv ?? throw new ArgumentNullException(nameof(addStatusByLv));
            BufStatus = bufStatus ?? throw new ArgumentNullException(nameof(bufStatus));
            AddBufStatusByLv = addBufStatusByLv ?? throw new ArgumentNullException(nameof(addBufStatusByLv));
            ModuleIndex = moduleIndex;
            MainImagePath = mainImagePath ?? throw new ArgumentNullException(nameof(mainImagePath));
            EffectImagePath = effectImagePath ?? throw new ArgumentNullException(nameof(effectImagePath));
            Explanation = explanation ?? throw new ArgumentNullException(nameof(explanation));
        }

    }
}