using GameDataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSetting
{
    public abstract class UI_SettingModuleHaveSubContainerModule : UI_SubContainerSettingModule
    {
        public int curSubContainer = -1;

        public abstract void OpenSubContainer(OpenSubContainerData _subContainerIndex);
        public abstract void CloseSubContainer(object _data);
    }
}