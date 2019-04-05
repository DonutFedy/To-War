using GameDataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSetting
{
    public class UI_Action_OpenSubContainer_StrengthModule : UI_ActionModule
    {
        public override void ToAction(UI_Container _curContainer, UI_Container _targetContainer, object _data = null)
        {
            StrengthReturnData data = (StrengthReturnData)_data;
            UI_SelectStrengtMatSettingModule module = (UI_SelectStrengtMatSettingModule)_targetContainer.SettingModule;
            module.SetType(data.viewType, data.type, (List<int>)data.data, data.targetIndex);
            _targetContainer.OpenContainer(_curContainer);
        }
    }
}