using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSetting
{
    public abstract class UI_SettingModule : MonoBehaviour
    {
        public abstract void SetUI(UI_Container _container);
    }
}