using GameDataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSetting
{
    public class SettingMinionSkillData 
    {
        SkillType skillType;
        int indexOfType;
        int lv;
        BasicStatus addStatus;

        public SettingMinionSkillData(SkillType skillType, int indexOfType, int lv, int curExp, int needExp)
        {
            SkillType = skillType;
            IndexOfType = indexOfType;
            Lv = lv;
            AddStatus = new BasicStatus();
            if (lv > 1)
                addStatus.AddStatus(Management.MenuGameManager.Instance.DataArea.DefaultMinionSkillData[IndexOfType].AddStatusByLv * Lv);
        }

        public SkillType SkillType { get => skillType; set => skillType = value; }
        public int IndexOfType { get => indexOfType; set => indexOfType = value; }
        public int Lv { get => lv; set => lv = value; }
        public BasicStatus AddStatus { get => addStatus; set => addStatus = value; }
    }
}