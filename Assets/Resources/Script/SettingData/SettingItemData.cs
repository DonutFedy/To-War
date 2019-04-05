using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDataBase.SettingData
{
    public class SettingItemData 
    {
        ItemType itemType;
        int indexOfType;
        int indexOfSerializeCode;
        int lv;
        int curExp;
        int needExp;
        BasicStatus addStatus;
        bool useItem;
        int ownerMinionIndexOfSerializeCode;

        public SettingItemData(ItemType itemType,int indexOfSerializeCode, int indexOfType, int lv, int curExp, int needExp, bool useItem, int ownerMinionIndex = -1)
        {
            ItemType = itemType;
            IndexOfType = indexOfType;
            IndexOfSerializeCode = indexOfSerializeCode;
            Lv = lv;
            CurExp = curExp;
            NeedExp = needExp;
            AddStatus = Management.MenuGameManager.Instance.DataArea.DefaultItemData[IndexOfType].AddStatusByLv * (Lv-1);
            UseItem = useItem;
            OwnerMinionIndexOfSerializeCode = ownerMinionIndex;
        }

        public ItemType ItemType { get => itemType; set => itemType = value; }
        public int IndexOfType { get => indexOfType; set => indexOfType = value; }
        public int Lv { get => lv; set => lv = value; }
        public int CurExp { get => curExp; set => curExp = value; }
        public int NeedExp { get => needExp; set => needExp = value; }
        public BasicStatus AddStatus { get => addStatus; set => addStatus = value; }
        public int OwnerMinionIndexOfSerializeCode { get => ownerMinionIndexOfSerializeCode; set => ownerMinionIndexOfSerializeCode = value; }
        public bool UseItem { get => useItem; set => useItem = value; }
        public int IndexOfSerializeCode { get => indexOfSerializeCode; set => indexOfSerializeCode = value; }
    }
}