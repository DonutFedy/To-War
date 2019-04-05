using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameSetting {
    public class UI_Action_CloseSubContainerModule : UI_ActionModule
    {
        public override void ToAction(UI_Container _curContainer, UI_Container _targetContainer, object _data = null)
        {

            if (_curContainer.PrevContainer == null)
                return;
            ((UI_SettingModuleHaveSubContainerModule)_curContainer.PrevContainer.SettingModule).CloseSubContainer(_data);
            _curContainer.PrevContainer.OpenOtherContainer = false;
            _curContainer.PrevContainer = null;
            _curContainer.gameObject.SetActive(false);
        }
    }
}
