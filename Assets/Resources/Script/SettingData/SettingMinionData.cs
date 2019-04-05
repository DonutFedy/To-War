using GameSetting;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDataBase.SettingData
{
    public class SettingMinionData
    {
        int indexOfMinion;
        int indexOfSerializeCode;
        int lv;
        int rate;
        int curExp;
        int needExp;
        BasicStatus addStatus;

        int maxItemSlotCount;
        int[] indexOfSerializeCodeOwnedItem; // owned item use "MinionItemSlotType"

        int maxSkillSlotCount;
        SettingMinionSkillData[] ownedSkillList; // setting skill data


        public SettingMinionData(int indexOfMinion,int indexOfSerializeCode, int lv, int rate, int curExp, int needExp,  int maxItemSlotCount, int[] indexOfOwnedItem, int maxSkillSlotCount, SettingMinionSkillData[] ownedSkillList)
        {
            DataArea db = Management.MenuGameManager.Instance.DataArea;
            IndexOfMinion = indexOfMinion;
            IndexOfSerializeCode = indexOfSerializeCode;
            Lv = lv;
            Rate = rate;
            CurExp = curExp;
            NeedExp = needExp;
            AddStatus = new BasicStatus();
            MaxItemSlotCount = maxItemSlotCount;
            IndexOfSerializeCodeOwnedItem = new int[MaxItemSlotCount];
            AddStatus.AddStatus(Management.MenuGameManager.Instance.DataArea.DefaultMinionData[IndexOfMinion].AddStatusByLv*(Lv-1));
            for (int i = 0; i < MaxItemSlotCount; ++i)
            {
                if (indexOfOwnedItem.Length > i)
                {
                    IndexOfSerializeCodeOwnedItem[i] = indexOfOwnedItem[i];

                    // 아이템에 따른 addStatus 추가
                    //AddStatus.AddStatus(Management.MenuGameManager.Instance.DataArea.DefaultItemData[IndexOfOwnedItem[i]].AddStatusByLv * (LvOfOwnedItem[i]-1));
                    var ownedItemData = db.OwnedData.ItemData.Find(x=>x.IndexOfSerializeCode == IndexOfSerializeCodeOwnedItem[i]);
                    if(ownedItemData != null)
                    {
                        int defaultIndex = ownedItemData.IndexOfType;
                        AddStatus.AddStatus(db.DefaultItemData[defaultIndex].BasicStatus + ownedItemData.AddStatus);
                    }

                }
                else
                {
                    IndexOfSerializeCodeOwnedItem[i] = -1;
                }
            }
            MaxSkillSlotCount = maxSkillSlotCount;

            OwnedSkillList = ownedSkillList ;
        }

        public int IndexOfMinion { get => indexOfMinion; set => indexOfMinion = value; }
        public int Lv { get => lv; set => lv = value; }
        public int Rate { get => rate; set => rate = value; }
        public int CurExp { get => curExp; set => curExp = value; }
        public int NeedExp { get => needExp; set => needExp = value; }
        public BasicStatus AddStatus { get => addStatus; set => addStatus = value; }
        public int MaxItemSlotCount { get => maxItemSlotCount; set => maxItemSlotCount = value; }
        public int[] IndexOfSerializeCodeOwnedItem { get => indexOfSerializeCodeOwnedItem; set => indexOfSerializeCodeOwnedItem = value; }
        public int MaxSkillSlotCount { get => maxSkillSlotCount; set => maxSkillSlotCount = value; }
        public SettingMinionSkillData[] OwnedSkillList { get => ownedSkillList; set => ownedSkillList = value; }
        public int IndexOfSerializeCode { get => indexOfSerializeCode; set => indexOfSerializeCode = value; }
    }
}