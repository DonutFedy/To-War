using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDataBase.DefaultGameData
{
    [Serializable]
    public class LvUpNeedExp
    {
        public int MaxLv;
        public int[] NeedExp;
        public int[] NeedGold;
    }
}
