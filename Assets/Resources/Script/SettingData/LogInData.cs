using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDataBase.SettingData
{
    public class LogInData
    {
        // server db
        string  id;
        string  pw;
        string  dbIndex;

// user data
        int     gold;
        string  userName;
        int     lv;
        int     needLvExp;
        int     curExp;
        int     clearChapter;
        int     clearStage;

        int     free_DecSlotCount;
        int     pay_DecSlotCount;
        int     event_DecSlotCount;

        float   plusGatchaRate;         // 가챠 추가 확률
        float   addPlusGatchaRate;      // 이벤트나 횟수를 통한 추가 확률

        float   plusStrengthenRate;     // 강화 추가 확률
        float   addPlusStrengthenRate;  // 이벤트 등을 이용한 강화 추가 확률

        /// <summary>
        /// 현재까지 획득한 미니언수
        /// </summary>
        int minionSerializeCodeCount = 0;
        /// <summary>
        /// 현재까지 획득한 아이템수
        /// </summary>
        int itemSerializeCodeCount = 0;

        public void Id_PwSet(string _id, string _pw)
        {
            Id = _id;
            Pw = _pw;
        }

        public void DataSet(int _gold, string _userName, int _lv, int _needLvExp, 
            int _curExp, int _clearChapter, int _clearStage,
            int _pay_DecSlotCount, int _event_DecSlotCount, float _addPlusGatchaRate, float _addPlusStrengthenRate,int _minionSerializeCodeCount, int _itemSerializeCodeCount)
        {
            gold = _gold;
            userName = _userName;
            lv = _lv;
            needLvExp = _needLvExp;
            curExp = _curExp;
            clearChapter = _clearChapter;
            clearStage = _clearStage;

            pay_DecSlotCount = _pay_DecSlotCount;
            event_DecSlotCount = _event_DecSlotCount;

            addPlusGatchaRate = _addPlusGatchaRate;
            addPlusStrengthenRate = _addPlusStrengthenRate;
            MinionSerializeCodeCount = _minionSerializeCodeCount;
            ItemSerializeCodeCount = _itemSerializeCodeCount;
            }

        public string   Id { get => id; set => id = value; }
        public string   Pw { get => pw; set => pw = value; }
        public string   DbIndex { get => dbIndex; set => dbIndex = value; }
        public int      Gold { get => gold; set => gold = value; }
        public string UserName { get => userName; set => userName = value; }
        public int Lv { get => lv; set => lv = value; }
        public int NeedLvExp { get => needLvExp; set => needLvExp = value; }
        public int CurExp { get => curExp; set => curExp = value; }
        public int ClearChapter { get => clearChapter; set => clearChapter = value; }
        public int ClearStage { get => clearStage; set => clearStage = value; }
        public int Free_DecSlotCount { get => free_DecSlotCount; set => free_DecSlotCount = value; }
        public int Pay_DecSlotCount { get => pay_DecSlotCount; set => pay_DecSlotCount = value; }
        public int Event_DecSlotCount { get => event_DecSlotCount; set => event_DecSlotCount = value; }
        public float PlusGatchaRate { get => plusGatchaRate; set => plusGatchaRate = value; }
        public float AddPlusGatchaRate { get => addPlusGatchaRate; set => addPlusGatchaRate = value; }
        public float PlusStrengthenRate { get => plusStrengthenRate; set => plusStrengthenRate = value; }
        public float AddPlusStrengthenRate { get => addPlusStrengthenRate; set => addPlusStrengthenRate = value; }
        public int MinionSerializeCodeCount { get => minionSerializeCodeCount; set => minionSerializeCodeCount = value; }
        public int ItemSerializeCodeCount { get => itemSerializeCodeCount; set => itemSerializeCodeCount = value; }
        //각종 가챠 포인트
    }
}