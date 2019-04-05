using GameDataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSetting
{
    public class UI_Action_CloseStregthenModule : UI_ActionModule
    {
        public override void ToAction(UI_Container _curContainer, UI_Container _targetContainer, object _data = null)
        {
            if (_curContainer.gameObject.activeSelf == false)
                return;
            //여기에 추가할것 넣기
            UI_StrengthenSubSettingModule module = (UI_StrengthenSubSettingModule)_curContainer.PrevContainer.SettingModule;
            StrengthReturnData data = (StrengthReturnData)_data;
            if (data.type == StrengthSubType.Target)
            {
                module.GetTargetIndex((int)data.data);
            }
            else if (data.type == StrengthSubType.Material)
            {
                if(data.data != null)
                    module.GetMatList((List<int>)data.data);
                else
                    module.GetMatList(null);
            }
            //
            if (_curContainer.PrevContainer == null)
                return;
            _curContainer.PrevContainer.OpenOtherContainer = false;
            _curContainer.PrevContainer.OpenContainer();
            _curContainer.PrevContainer = null;
            _curContainer.gameObject.SetActive(false);
        }
    }
}