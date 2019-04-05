using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDataBase.DefaultGameData
{
    [Serializable]
    public class Default_UserData
    {
        public int      BasicDecCount;
        public int      OpenFreeDecLvInterval;
        public int      MaxDecCount = 20;

        public float    AddGatchaRateByLv;
        public float    AddStrengthenRateByLv;

        public int      BasicMinionCount;
        public int      AddMinionCountIntervalLv;
        public int      AddMinionCount              = 10;
        public int      BasicItemCount;
        public int      AddItemCountIntervalLv;
        public int      AddItemCount                = 10;
        


        public Default_UserData(int openFreeDecLvInterval, float addGatchaRateByLv, float addStrengthenRateByLv, int basicDecCount, int basicMinionCount, int addMinionCountIntervalLv, int basicItemCount, int addItemCountIntervalLv)
        {
            OpenFreeDecLvInterval = openFreeDecLvInterval;
            AddGatchaRateByLv = addGatchaRateByLv;
            AddStrengthenRateByLv = addStrengthenRateByLv;
            BasicDecCount = basicDecCount;
            BasicMinionCount = basicMinionCount;
            AddMinionCountIntervalLv = addMinionCountIntervalLv;
            BasicItemCount = basicItemCount;
            AddItemCountIntervalLv = addItemCountIntervalLv;
        }
    }
}