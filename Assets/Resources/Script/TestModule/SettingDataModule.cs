
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDataBase.DefaultGameData;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

namespace GameDataBase
{
    public class SettingDataModule : MonoBehaviour
    {
        [SerializeField]
        DataArea dataArea;
        

        private void OnMouseDown()
        {
            SettingData();
        }

        private void SettingData()
        {
            dataArea = new DataArea();
            // default data load
            SetDefaultDataArea(dataArea);

            // login part
            // 대충 간단하게 더미 데이터 넣자
            SetLoginDataArea(dataArea);

            // setting data load
            SetSettingDataArea(dataArea);
        }

        private void SetLoginDataArea(DataArea dataArea)
        {
            dataArea.LoginData = new SettingData.LogInData();
            dataArea.LoginData.Id = "testId";
            dataArea.LoginData.Pw = "testPw";
            dataArea.LoginData.DbIndex = "0123456789";

            dataArea.LoginData.Gold = 1000;
            dataArea.LoginData.UserName = "test";
            dataArea.LoginData.Lv = 1;
            dataArea.LoginData.CurExp = 0;
            dataArea.LoginData.ClearChapter = 1;
            dataArea.LoginData.ClearStage = 0;
            //dec
            dataArea.LoginData.Pay_DecSlotCount = 0;
            dataArea.LoginData.Event_DecSlotCount = 0;
            dataArea.LoginData.AddPlusGatchaRate = 0;    
            dataArea.LoginData.AddPlusStrengthenRate = 0;
        }

        private void SetSettingDataArea(DataArea dataArea)
        {
            // set by setData
            dataArea.LoginData.NeedLvExp = dataArea.DefaultExpManager.UserExpChart.NeedExp[dataArea.LoginData.Lv - 1];
            dataArea.LoginData.Free_DecSlotCount = dataArea.DefaultUserData.BasicDecCount + dataArea.LoginData.Lv / dataArea.DefaultUserData.OpenFreeDecLvInterval;
            dataArea.LoginData.PlusGatchaRate = dataArea.DefaultUserData.AddGatchaRateByLv * dataArea.LoginData.Lv;
            dataArea.LoginData.PlusStrengthenRate = dataArea.DefaultUserData.AddStrengthenRateByLv * dataArea.LoginData.Lv;

            dataArea.OwnedData = new SettingData.OwnedListData();

            dataArea.OwnedData.ItemData = new List<SettingData.SettingItemData>();
            dataArea.OwnedData.MinionData = new List<SettingData.SettingMinionData>();

            int lv = 1;
            //dataArea.OwnedData.ItemData.Add(new GameDataBase.SettingData.SettingItemData(ItemType.Weapon,0,lv,0,dataArea.DefaultExpManager.ItemExpChart.NeedExp[lv-1]));

            int rate = 1;
            BasicStatus addS = new BasicStatus();
            //dataArea.OwnedData.MinionData.Add(new GameDataBase.SettingData.SettingMinionData(0, lv, rate, 0, dataArea.DefaultExpManager.UnitExpChart.NeedExp[lv], 3, new int[] { 0 },2, null));
        }

        private void SetDefaultDataArea(DataArea dataArea)
        {
            string basicPath = Application.dataPath;
            // load 
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(basicPath + DataPath.defaultUserDataPath , FileMode.Open ))
            {
                dataArea.DefaultUserData = (Default_UserData)bf.Deserialize(fs);
            }

            using (FileStream fs = new FileStream(basicPath + DataPath.defaultItemDataPath, FileMode.Open))
            {
                dataArea.DefaultItemData = new Default_ItemData[dataArea.MaxItemCount];

                for (int i = 0; i < dataArea.MaxItemCount; ++i)
                    dataArea.DefaultItemData[i] = (Default_ItemData)bf.Deserialize(fs);
            }

            using (FileStream fs = new FileStream(basicPath + DataPath.defaultSkillDataPath, FileMode.Open))
            {
                dataArea.DefaultSkillData = new Default_SkillData[dataArea.MaxSkillCount];

                for (int i = 0; i < dataArea.MaxSkillCount; ++i)
                    dataArea.DefaultSkillData[i] = (Default_SkillData)bf.Deserialize(fs);
            }

            using (FileStream fs = new FileStream(basicPath + DataPath.defaultMinionDataPath, FileMode.Open))
            {
                //dataArea.DefaultMinionData = new Default_MinionData[dataArea.MaxMinionCount];

                List<Default_MinionData> list = new List<Default_MinionData>();
                list = (List<Default_MinionData>)bf.Deserialize(fs);
                dataArea.DefaultMinionData = list.ToArray();
                //for (int i = 0; i < dataArea.MaxSkillCount; ++i)
                  //  dataArea.DefaultMinionData[i] = (Default_MinionData)bf.Deserialize(fs);
            }

            using (FileStream fs = new FileStream(basicPath + DataPath.defaultExpManagerPath, FileMode.Open))
            {
                dataArea.DefaultExpManager = (LvExpManager)bf.Deserialize(fs);
            }
        }
        
    }
}