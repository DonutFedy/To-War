using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDataBase.SettingData
{
    public class SettingSkillData
    {
        SkillType skillType;
        int indexOfType;
        int lv;
        int curExp;
        int needExp;
        BasicStatus addStatus;

        public SettingSkillData(SkillType skillType, int indexOfType, int lv, int curExp, int needExp)
        {
            SkillType = skillType;
            IndexOfType = indexOfType;
            Lv = lv;
            CurExp = curExp;
            NeedExp = needExp;
            AddStatus = new BasicStatus();
            if (lv > 1)
                addStatus.AddStatus(Management.MenuGameManager.Instance.DataArea.DefaultSkillData[IndexOfType].AddStatusByLv * Lv);
        }

        public SkillType SkillType { get => skillType; set => skillType = value; }
        public int IndexOfType { get => indexOfType; set => indexOfType = value; }
        public int Lv { get => lv; set => lv = value; }
        public int CurExp { get => curExp; set => curExp = value; }
        public int NeedExp { get => needExp; set => needExp = value; }
        public BasicStatus AddStatus { get => addStatus; set => addStatus = value; }
    }
}

