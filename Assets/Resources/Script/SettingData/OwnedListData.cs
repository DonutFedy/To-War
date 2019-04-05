using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDataBase.SettingData
{
    public class OwnedListData
    {
        // minion list
        List<SettingMinionData> minionData;

        // item list
        List<SettingItemData> itemData;

        // skill list
        List<SettingSkillData> skillData;


        // dec list
        List<SettingDecData> decData;

        public List<SettingMinionData> MinionData { get => minionData; set => minionData = value; }
        public List<SettingItemData> ItemData { get => itemData; set => itemData = value; }
        public List<SettingDecData> DecData { get => decData; set => decData = value; }
        public List<SettingSkillData> SkillData { get => skillData; set => skillData = value; }
    }
}