using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSetting
{
    public abstract class UI_StrengthenSubSettingModule : UI_SettingModuleHaveSubContainerModule
    {
        public abstract void GetMatList(List<int> selectedList);
        public abstract void GetTargetIndex(int _index);
        public abstract void Strengthen();
    }
}