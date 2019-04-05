using GameDataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSetting
{
    public abstract class UI_SelectStrengtMatSettingModule : UI_SettingModule
    {
        public abstract void SetType(ViewType _viewType, StrengthSubType _type, List<int> _selectedMatList, int _targetIndex);
    }
}