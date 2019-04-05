using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameSetting
{
    public class UI_Action_OpenMainContainerModule : UI_ActionModule
    {
        public override void ToAction(UI_Container _curContainer, UI_Container _targetContainer, object _data)
        {
            if (_curContainer.OpenOtherContainer == true)
                return;
            _curContainer.OpenOtherContainer = true;
            _targetContainer.OpenContainer(_curContainer);
            _curContainer.gameObject.SetActive(false);
            var prevC = _curContainer.PrevContainer;
            while(prevC!= null)
            {
                prevC.transform.gameObject.SetActive(false);
                prevC = prevC.PrevContainer;
            }
        }
    }
}