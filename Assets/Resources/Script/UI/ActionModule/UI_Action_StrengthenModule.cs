using GameDataBase;
using GameDataBase.SettingData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameSetting
{
    public class UI_Action_StrengthenModule : UI_ActionModule
    {
        public override void ToAction(UI_Container _curContainer, UI_Container _targetContainer, object _data = null)
        {
            StrengthenData data = (StrengthenData)_data;

            ((UI_StrengthenSubSettingModule)_curContainer.SettingModule).Strengthen();

            DataArea db = Management.MenuGameManager.Instance.DataArea;
            switch (data.type)
            {
                case ViewType.Minion:
                    SettingMinionData target = db.OwnedData.MinionData[data.ownedTargetIndex];
                    target.Lv += data.addLv;
                    target.CurExp += data.addExp;
                    BasicStatus addStatus = target.AddStatus + db.DefaultMinionData[db.OwnedData.MinionData[data.ownedTargetIndex].IndexOfMinion].AddStatusByLv * (data.addLv);
                    target.AddStatus = addStatus;
                    target.NeedExp = db.DefaultExpManager.UnitExpChart.NeedExp[target.Lv];
                    
                    List<SettingMinionData> newList = new List<SettingMinionData>(db.OwnedData.MinionData);
                    int listCount = 0;
                    for (int i = 0; i < db.OwnedData.MinionData.Count; ++i)
                    {
                        if (data.ownedMaterialIndex.Contains(i))
                        {
                            for (int j = 0; j < db.OwnedData.MinionData[i].IndexOfSerializeCodeOwnedItem.Length; ++j)
                            {
                                var ownedItem = db.OwnedData.ItemData.Find(x => x.IndexOfSerializeCode == db.OwnedData.MinionData[i].IndexOfSerializeCodeOwnedItem[j]);
                                if (ownedItem != null)
                                {
                                    ownedItem.UseItem = false;
                                    ownedItem.OwnerMinionIndexOfSerializeCode = -1;
                                }
                            }
                            // dec에서 삭제
                            DeleteInDecByIndex(db, DecType.Minion, i);
                            // 리스트에서 삭제
                            newList.RemoveAt(listCount);
                        }
                        else
                        {
                            // dec에서 현재 타입이 같고 index가 i와 같은 녀석을 찾아서, listCount로 고쳐준다
                            SearchDecByIndex(db,DecType.Minion,i,listCount);

                            ++listCount;
                        }
                    }
                    db.OwnedData.MinionData = newList;

                    for(int i = 0; i < db.OwnedData.MinionData.Count; ++i)
                    {
                        for (int j = 0; j < db.OwnedData.MinionData[i].IndexOfSerializeCodeOwnedItem.Length; ++j)
                        {
                            var curItem = db.OwnedData.ItemData.Find(x => x.IndexOfSerializeCode == db.OwnedData.MinionData[i].IndexOfSerializeCodeOwnedItem[j]);
                            if (curItem != null)
                            {
                                curItem.OwnerMinionIndexOfSerializeCode = db.OwnedData.MinionData[i].IndexOfSerializeCode;
                            }
                        }
                    }

                    break;
                case ViewType.Item:
                    SettingItemData targetItem = db.OwnedData.ItemData[data.ownedTargetIndex];
                    targetItem.Lv += data.addLv;
                    targetItem.CurExp += data.addExp;
                    BasicStatus addStatusItem = targetItem.AddStatus + db.DefaultItemData[db.OwnedData.ItemData[data.ownedTargetIndex].IndexOfType].AddStatusByLv * (data.addLv);

                    // 소유 미니언의 추가스텟 보정
                    if(targetItem.OwnerMinionIndexOfSerializeCode >= 0)
                    {
                        var owner = db.OwnedData.MinionData.Find(x => x.IndexOfSerializeCode == targetItem.OwnerMinionIndexOfSerializeCode);
                        if( owner != null)
                        {
                            owner.AddStatus -= targetItem.AddStatus;
                            targetItem.AddStatus = addStatusItem;
                            owner.AddStatus += targetItem.AddStatus;
                        }
                    }
                    targetItem.NeedExp = db.DefaultExpManager.UnitExpChart.NeedExp[targetItem.Lv];

                    List<SettingItemData> newListItem = new List<SettingItemData>(db.OwnedData.ItemData);
                    for (int i = 0; i < data.ownedMaterialIndex.Count; ++i)
                    {
                        newListItem.Remove(db.OwnedData.ItemData[data.ownedMaterialIndex[i]]);
                    }
                    db.OwnedData.ItemData = newListItem;

                    break;
                case ViewType.Skill:
                    SettingSkillData targetSkill = db.OwnedData.SkillData[data.ownedTargetIndex];
                    targetSkill.Lv += data.addLv;
                    targetSkill.CurExp += data.addExp;
                    BasicStatus addStatusSkill = targetSkill.AddStatus + db.DefaultSkillData[db.OwnedData.SkillData[data.ownedTargetIndex].IndexOfType].AddStatusByLv * (data.addLv);

                    targetSkill.NeedExp = db.DefaultExpManager.UnitExpChart.NeedExp[targetSkill.Lv];

                    List<SettingSkillData> newListSkill = new List<SettingSkillData>(db.OwnedData.SkillData);
                    //for (int i = 0; i < data.ownedMaterialIndex.Count; ++i)
                    //{
                    //    DeleteInDecByIndex(db,DecType.Skill, data.ownedMaterialIndex[i]);
                    //    newListSkill.Remove(db.OwnedData.SkillData[data.ownedMaterialIndex[i]]);
                    //}
                    int listCountSkill = 0;
                    for (int i = 0; i < db.OwnedData.SkillData.Count; ++i)
                    {
                        if (data.ownedMaterialIndex.Contains(i))
                        {

                            // dec에서 삭제
                            DeleteInDecByIndex(db, DecType.Skill, i);
                            // 리스트에서 삭제
                            newListSkill.Remove(db.OwnedData.SkillData[i]);
                        }
                        else
                        {
                            // dec에서 현재 타입이 같고 index가 i와 같은 녀석을 찾아서, listCount로 고쳐준다
                            SearchDecByIndex(db, DecType.Skill, i, listCountSkill);

                            ++listCountSkill;
                        }
                    }
                    db.OwnedData.SkillData = newListSkill;
                    break;
            }

            //Management.MenuGameManager.Instance.SaveData();

        }

        private void DeleteInDecByIndex(DataArea _db, DecType type, int _index)
        {
            for(int i = 0; i < _db.OwnedData.DecData.Count; ++i)
            {
                for(int j = 0; j < _db.OwnedData.DecData[i].decList.Count; ++j)
                {
                    if(_db.OwnedData.DecData[i].decList[j].type == type && _db.OwnedData.DecData[i].decList[j].ownedIndex == _index)
                    {
                        _db.OwnedData.DecData[i].decList.RemoveAt(j);
                        _db.OwnedData.DecData[i].decCount = _db.OwnedData.DecData[i].decList.Count;
                        if (_db.OwnedData.DecData[i].decCount <= 0)
                        {
                            _db.OwnedData.DecData.RemoveAt(i);
                            --i;
                            break;
                        }
                        --j;
                    }
                }
            }
        }

        private void SearchDecByIndex(DataArea _db, DecType type, int _index, int _newIndex)
        {
            for (int i = 0; i < _db.OwnedData.DecData.Count; ++i)
            {
                for (int j = 0; j < _db.OwnedData.DecData[i].decList.Count; ++j)
                {
                    if (_db.OwnedData.DecData[i].decList[j].type == type && _db.OwnedData.DecData[i].decList[j].ownedIndex == _index)
                    {
                        _db.OwnedData.DecData[i].decList[j].ownedIndex = _newIndex;
                    }
                }
            }
        }
    }
}

