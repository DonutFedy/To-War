using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSetting
{
    public abstract class UI_ActionModule : MonoBehaviour
    {
        public abstract void ToAction(UI_Container _curContainer, UI_Container _targetContainer, object _data = null);
    }
}