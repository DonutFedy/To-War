using GameDataBase.DefaultGameData;
using GameDataBase.SettingData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDataBase
{
    [Serializable]
    public class DataArea
    {
        // default area
        public Default_UserData        DefaultUserData;
        public int MaxItemCount;
        public Default_ItemData[]      DefaultItemData;
        public int MaxSkillCount;
        public Default_SkillData[]     DefaultSkillData;
        public int MaxMinionSkillCount;
        public Default_MinionSkillData[] DefaultMinionSkillData;
        public int MaxMinionCount;
        public Default_MinionData[]    DefaultMinionData;
        public Default_ChapterData[]    DefaultChapterData;
        public LvExpManager            DefaultExpManager;
        // stage area


        // setting area
        public LogInData               LoginData;
        public OwnedListData           OwnedData;

    }
}