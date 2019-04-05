using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDataBase.DefaultGameData
{
    [Serializable]
    public class Default_ItemData
    {
        public string ItemName;
        public ItemType ItemType;
        public BasicStatus BasicStatus;
        public BasicStatus AddStatusByLv;
        public string IconPath;
        public string Explanation;

        public Default_ItemData(string itemName, ItemType itemType, BasicStatus basicStatus, BasicStatus addStatusByLv, string iconPath, string explanation)
        {
            ItemName = itemName ?? throw new ArgumentNullException(nameof(itemName));
            ItemType = itemType;
            BasicStatus = basicStatus ?? throw new ArgumentNullException(nameof(basicStatus));
            AddStatusByLv = addStatusByLv ?? throw new ArgumentNullException(nameof(addStatusByLv));
            IconPath = iconPath ?? throw new ArgumentNullException(nameof(iconPath));
            Explanation = explanation ?? throw new ArgumentNullException(nameof(explanation));
        }

    }
}