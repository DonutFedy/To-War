using GameDataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSetting
{
    public class UI_Action_OpenSubContainerModule : UI_ActionModule
    {
        public override void ToAction(UI_Container _curContainer, UI_Container _targetContainer, object _data)
        {
            UI_SettingModuleHaveSubContainerModule module = ((UI_SettingModuleHaveSubContainerModule)_curContainer.SettingModule);
            if( _data != null)
            {
                if (module.curSubContainer == ((OpenSubContainerData)_data).SubContainerIndex)
                {
                    return;
                }
                ((UI_SubContainerSettingModule)_targetContainer.SettingModule).SetSubContianer(((OpenSubContainerData)_data).Data);
                module.OpenSubContainer((OpenSubContainerData)_data);
            }

            _targetContainer.OpenContainer(_curContainer);
        }
    }
}