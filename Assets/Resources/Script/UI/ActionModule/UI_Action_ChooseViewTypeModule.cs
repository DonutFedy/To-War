using GameDataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSetting
{
    public class UI_Action_ChooseViewTypeModule : UI_ActionModule
    {
        
        public override void ToAction(UI_Container _curContainer, UI_Container _targetContainer, object _data)
        {
            ViewType viewType = (ViewType)_data;
            ((UI_ShowUnitViewSettingModule)_curContainer.SettingModule).SetSlots(_curContainer,viewType);
        }
    }
}