using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDataBase.SettingData
{
    public class SettingDecData
    {
        public int decCount;
        public string decName;
        public List<DecInfo> decList = new List<DecInfo>();

        public SettingDecData(int decCount, string decName, List<DecInfo> decList)
        {
            this.decCount = decCount;
            this.decName = decName ;
            this.decList = decList ?? throw new ArgumentNullException(nameof(decList));
        }
        public SettingDecData(SettingDecData _source)
        {
            this.decCount = _source.decCount;
            this.decName = _source.decName;

            this.decList = new List<DecInfo>(_source.decList);
        }
    }
}