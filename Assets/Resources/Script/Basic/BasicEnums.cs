using GameUnit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDataBase
{
    public enum DisplaySize
    {
        Row = 1440/2, Col = 2560/2
    }


    #region Data Part


    public abstract class DataPath
    {
        static public string defaultUserDataPath = "/Resources/Data/DefaultUser.json";
        static public string defaultItemDataPath = "/Resources/Data/DefaultItem.json";
        static public string defaultSkillDataPath = "/Resources/Data/DefaultSkill.json";
        static public string defaultMinionSkillPath = "/Resources/Data/DefaultMinionSkill.json";
        static public string defaultMinionDataPath = "/Resources/Data/DefaultMinion.json";
        static public string defaultExpManagerPath = "/Resources/Data/DefaultExpManager.json";
        static public string defaultPath = "/DefaultData.json";
    }

    public enum BattleSelectMode
    {
        StoryMode, MultiMode
    }

    public enum MinionType
    {
        Warriors, Magician, Witch,
    }

    public enum ItemType
    {
        Weapon, Armor, Accessory, Cloak,
    }

    public enum SkillType
    {
        Pessive, Active
    }
    [Flags]
    public enum MinionLimitLV
    {
        Basic = 10, Second = 20,
    }
    public enum ObjectLvLimit
    {
        UserMax = 100, MinionMax = 100, SkillMax = 100, ItemMax = 100
    }

    public enum MinionItemSlotType
    {
        MianItem, SubItem, Accessory
    }

    public enum ViewType
    {
        Minion, Item, Skill
    }

    public enum DecType
    {
        Minion, Skill
    }

    public enum MinionItemSlotLimitByLv
    {
        First = 1, Second = 30, Third = 50
    }

    public enum DecMax
    {
        UnitMax = 30, OwnedMax = 20
    }

    public enum StrengthSubType
    {
        Target = 0, Material = 1, SelectMax = 20
    }

    public class UI_Slot_Data
    {
        public ViewType type;
        public int index;

        public UI_Slot_Data(ViewType type, int index)
        {
            this.type = type;
            this.index = index;
        }
    }

    public class DecInfo
    {
        public DecType type;
        public int ownedIndex;

        public DecInfo(DecType type, int ownedIndex)
        {
            this.type = type;
            this.ownedIndex = ownedIndex;
        }
    }

    public class StrengthReturnData
    {
        public ViewType viewType;
        public StrengthSubType type;
        public object data;
        public int targetIndex;

        public StrengthReturnData(ViewType viewType, StrengthSubType type, object data, int targetIndex)
        {
            this.viewType = viewType;
            this.type = type;
            this.data = data;
            this.targetIndex = targetIndex;
        }
    }

    public class StrengthenData
    {
        public ViewType type;
        public int ownedTargetIndex;
        public List<int> ownedMaterialIndex;
        public int addExp;
        public int addLv;

        public StrengthenData(ViewType type, int ownedTargetIndex, List<int> ownedMaterialIndex, int addExp, int addLv)
        {
            this.type = type;
            this.ownedTargetIndex = ownedTargetIndex;
            this.ownedMaterialIndex = ownedMaterialIndex ?? throw new ArgumentNullException(nameof(ownedMaterialIndex));
            this.addExp = addExp;
            this.addLv = addLv;
        }
    }

    public class OpenSubContainerData
    {
        public int SubContainerIndex;
        public object Data;

        public OpenSubContainerData(int subContainerIndex, object data)
        {
            SubContainerIndex = subContainerIndex;
            Data = data;
        }
    }
    #endregion


    #region Unit Part

    public enum UnitMoveStatus
    {
        SearchRoad, SearchEnemy, Attack, Movement
    }
    public enum BattleType
    {
        Defence, Offence
    }


    /// <summary>
    /// 유닛을 생성하기 위해 필요한 데이터
    /// </summary>
    public class UnitSetData
    {
        public DecType decType;
        public BattleType battleType;
        public int indexOfNode;
        public int indexOfOwnedUnit;
        public int indexOfSerialize; // 현재 씬에서의 구분자

        public UnitSetData(DecType decType, BattleType battleType, int indexOfNode, int indexOfOwnedUnit, int indexOfSerialize)
        {
            this.decType = decType;
            this.battleType = battleType;
            this.indexOfNode = indexOfNode;
            this.indexOfOwnedUnit = indexOfOwnedUnit;
            this.indexOfSerialize = indexOfSerialize;
        }
    }

    public class DamageInfo
    {
        public UnitController   ownerUnit;
        public float            damage;
        public float            defencePenetration;

        public DamageInfo(UnitController ownerUnit, float damage, float defencePenetration)
        {
            this.ownerUnit = ownerUnit ;
            this.damage = damage;
            this.defencePenetration = defencePenetration;
        }
    }


    #endregion

}