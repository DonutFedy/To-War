using GameDataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameSetting
{
    public class UI_Action_ChooseSlotModule : UI_ActionModule
    {
        public override void ToAction(UI_Container _curContainer, UI_Container _targetContainer, object _data = null)
        {
            UI_Slot_Data data = (UI_Slot_Data)_data;
            UI_ShowUnitViewSettingModule module = ((UI_ShowUnitViewSettingModule)_curContainer.SettingModule);
            if (data.type == ViewType.Minion)
                module.SetMinionInfo(data.index);
            else if (data.type == ViewType.Item)
                module.SetItemInfo(data.index);
            else
                module.SetSkillInfo(data.index);
        }
    }
}