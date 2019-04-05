using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSetting {

    public class UI_Action_CloseContainerModule : UI_ActionModule
    {
        [SerializeField]
        public delegate void temp();
        public temp deletgateFunc;

        public override void ToAction(UI_Container _curContainer, UI_Container _targetContainer, object _data)
        {
            if (_curContainer.PrevContainer == null)
                return;
            _curContainer.PrevContainer.OpenOtherContainer = false;
            _curContainer.PrevContainer.OpenContainer();
            _curContainer.PrevContainer = null;
            _curContainer.gameObject.SetActive(false);
            if (deletgateFunc != null)
                deletgateFunc.Invoke();
        }
    }
}