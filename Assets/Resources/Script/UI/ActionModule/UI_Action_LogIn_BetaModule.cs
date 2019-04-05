using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDataBase;
using GameDataBase.SettingData;
using GameDataBase.DefaultGameData;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

namespace GameSetting
{

    public class UI_Action_LogIn_BetaModule :UI_ActionModule
    {
        

        public override void ToAction(UI_Container _curContainer, UI_Container _targetContainer, object _data)
        {
            OnClickStart();
            if(_targetContainer)
                _targetContainer.OpenContainer(_curContainer);
            _curContainer.gameObject.SetActive(false);
        }


        public void OnClickStart()
        {
            SettingData();
        }

        private void SettingData()
        {
            Management.MenuGameManager.Instance.DataArea= new DataArea();
            string error = "";
            // default data load
            try
            {
                SetDefaultDataArea(Management.MenuGameManager.Instance.DataArea);
                error += "Complete Default\n";
            }
            catch(Exception ex)
            {
                error += "Not Complete Default\n";
            }

            // login part
            // 대충 간단하게 더미 데이터 넣자
            try
            {
                SetLoginDataArea(Management.MenuGameManager.Instance.DataArea);
                error += "Complete Login\n";
            }
            catch (Exception ex)
            {
                error += "Not Complete Login\n";
            }

            // setting data load
            try
            {
                SetSettingDataArea(Management.MenuGameManager.Instance.DataArea);
                error += "Complete setting\n";
            }
            catch (Exception ex)
            {
                error += "Not Complete setting\n";
            }
            //Management.MenuGameManager.Instance.errorText.text = error;
        }

        private void SetLoginDataArea(DataArea dataArea)
        {
            dataArea.LoginData = new LogInData();
            dataArea.LoginData.Id = "testId";
            dataArea.LoginData.Pw = "testPw";
            dataArea.LoginData.DbIndex = "0123456789";

            dataArea.LoginData.Gold = 1000;
            dataArea.LoginData.UserName = "test";
            dataArea.LoginData.Lv = 1;
            dataArea.LoginData.CurExp = 0;
            dataArea.LoginData.ClearChapter = 0;
            dataArea.LoginData.ClearStage = 0;
            //dec
            dataArea.LoginData.Pay_DecSlotCount = 0;
            dataArea.LoginData.Event_DecSlotCount = 0;
            dataArea.LoginData.AddPlusGatchaRate = 0;
            dataArea.LoginData.AddPlusStrengthenRate = 0;
            dataArea.LoginData.MinionSerializeCodeCount= 0;
            dataArea.LoginData.ItemSerializeCodeCount = 0;
        }

        private void SetSettingDataArea(DataArea dataArea)
        {
            // set by setData
            dataArea.LoginData.NeedLvExp = dataArea.DefaultExpManager.UserExpChart.NeedExp[dataArea.LoginData.Lv - 1];
            dataArea.LoginData.Free_DecSlotCount = dataArea.DefaultUserData.BasicDecCount + dataArea.LoginData.Lv / dataArea.DefaultUserData.OpenFreeDecLvInterval;
            dataArea.LoginData.PlusGatchaRate = dataArea.DefaultUserData.AddGatchaRateByLv * dataArea.LoginData.Lv;
            dataArea.LoginData.PlusStrengthenRate = dataArea.DefaultUserData.AddStrengthenRateByLv * dataArea.LoginData.Lv;

            dataArea.OwnedData = new OwnedListData();

            dataArea.OwnedData.ItemData = new List<SettingItemData>();
            dataArea.OwnedData.MinionData = new List<SettingMinionData>();

            // item
            int lv = 3;
            dataArea.OwnedData.ItemData.Add(new SettingItemData(ItemType.Weapon,dataArea.LoginData.ItemSerializeCodeCount++, 0, lv, 0, dataArea.DefaultExpManager.ItemExpChart.NeedExp[lv - 1], true, 0));
            dataArea.OwnedData.ItemData.Add(new SettingItemData(ItemType.Weapon,dataArea.LoginData.ItemSerializeCodeCount++, 1, lv, 0, dataArea.DefaultExpManager.ItemExpChart.NeedExp[lv - 1], true, 1));
            dataArea.OwnedData.ItemData.Add(new SettingItemData(ItemType.Weapon,dataArea.LoginData.ItemSerializeCodeCount++, 0, lv + 3, 0, dataArea.DefaultExpManager.ItemExpChart.NeedExp[lv + 2], false, -1));
            dataArea.OwnedData.ItemData.Add(new SettingItemData(ItemType.Weapon,dataArea.LoginData.ItemSerializeCodeCount++, 1, lv + 3, 0, dataArea.DefaultExpManager.ItemExpChart.NeedExp[lv + 2], false, -1));
            dataArea.OwnedData.ItemData.Add(new SettingItemData(ItemType.Weapon,dataArea.LoginData.ItemSerializeCodeCount++, 0, lv + 1, 0, dataArea.DefaultExpManager.ItemExpChart.NeedExp[lv], false, -1));
            dataArea.OwnedData.ItemData.Add(new SettingItemData(ItemType.Weapon,dataArea.LoginData.ItemSerializeCodeCount++, 1, lv + 1, 0, dataArea.DefaultExpManager.ItemExpChart.NeedExp[lv], false, -1));
            dataArea.OwnedData.ItemData.Add(new SettingItemData(ItemType.Weapon,dataArea.LoginData.ItemSerializeCodeCount++, 0, lv + 5, 0, dataArea.DefaultExpManager.ItemExpChart.NeedExp[lv + 4], false, -1));
            dataArea.OwnedData.ItemData.Add(new SettingItemData(ItemType.Weapon,dataArea.LoginData.ItemSerializeCodeCount++, 1, lv + 5, 0, dataArea.DefaultExpManager.ItemExpChart.NeedExp[lv + 4], false, -1));

            // minion
            int rate = 1;
            int skillLV = 1;
            int needExp = dataArea.DefaultExpManager.SkillExpChart.NeedExp[skillLV];
            SettingMinionSkillData minionSkill = new SettingMinionSkillData(SkillType.Active,0,skillLV,0, needExp);
            dataArea.OwnedData.MinionData.Add(new SettingMinionData(0, dataArea.LoginData.MinionSerializeCodeCount++, lv, rate, 0, dataArea.DefaultExpManager.UnitExpChart.NeedExp[lv - 1], 3, new int[] { 0 }, 2, new SettingMinionSkillData[] { minionSkill }));
            dataArea.OwnedData.MinionData.Add(new SettingMinionData(1, dataArea.LoginData.MinionSerializeCodeCount++, lv, rate, 10, dataArea.DefaultExpManager.UnitExpChart.NeedExp[lv - 1], 3, new int[] { 1 }, 2, new SettingMinionSkillData[] { minionSkill }));
            dataArea.OwnedData.MinionData.Add(new SettingMinionData(0,dataArea.LoginData.MinionSerializeCodeCount++, lv + 5, rate, 0, dataArea.DefaultExpManager.UnitExpChart.NeedExp[lv + 4], 3, new int[] { }, 2, new SettingMinionSkillData[] { minionSkill }));
            dataArea.OwnedData.MinionData.Add(new SettingMinionData(1,dataArea.LoginData.MinionSerializeCodeCount++, lv + 2, rate, 10, dataArea.DefaultExpManager.UnitExpChart.NeedExp[lv + 1], 3, new int[] { }, 2, new SettingMinionSkillData[] { minionSkill }));
                                                                      
            dataArea.OwnedData.MinionData.Add(new SettingMinionData(0,dataArea.LoginData.MinionSerializeCodeCount++, lv, rate, 0, dataArea.DefaultExpManager.UnitExpChart.NeedExp[lv - 1], 3, new int[] {  }, 2, new SettingMinionSkillData[] { minionSkill }));
            dataArea.OwnedData.MinionData.Add(new SettingMinionData(1,dataArea.LoginData.MinionSerializeCodeCount++, lv, rate, 10, dataArea.DefaultExpManager.UnitExpChart.NeedExp[lv - 1], 3, new int[] {  }, 2, new SettingMinionSkillData[] { minionSkill }));
            dataArea.OwnedData.MinionData.Add(new SettingMinionData(0,dataArea.LoginData.MinionSerializeCodeCount++, lv + 5, rate, 0, dataArea.DefaultExpManager.UnitExpChart.NeedExp[lv + 4], 3, new int[] { }, 2, new SettingMinionSkillData[] { minionSkill }));
            dataArea.OwnedData.MinionData.Add(new SettingMinionData(1, dataArea.LoginData.MinionSerializeCodeCount++, lv + 2, rate, 10, dataArea.DefaultExpManager.UnitExpChart.NeedExp[lv + 1], 3, new int[] { }, 2, new SettingMinionSkillData[] { minionSkill }));


            // skill
            dataArea.OwnedData.SkillData = new List<SettingSkillData>();
            int skillLv = 2;
            dataArea.OwnedData.SkillData.Add(new SettingSkillData(SkillType.Active, 0, skillLv, 0,dataArea.DefaultExpManager.SkillExpChart.NeedExp[skillLv-1]));
            dataArea.OwnedData.SkillData.Add(new SettingSkillData(SkillType.Active, 0, skillLv+2, 0, dataArea.DefaultExpManager.SkillExpChart.NeedExp[skillLv + 1]));
            dataArea.OwnedData.SkillData.Add(new SettingSkillData(SkillType.Active,1, skillLv+5, 100, dataArea.DefaultExpManager.SkillExpChart.NeedExp[skillLv + 4]));
            dataArea.OwnedData.SkillData.Add(new SettingSkillData(SkillType.Active,1, skillLv, 0, dataArea.DefaultExpManager.SkillExpChart.NeedExp[skillLv-1]));

            // dec
            dataArea.OwnedData.DecData = new List<SettingDecData>();

        }

        private void SetDefaultDataArea(DataArea dataArea)
        {
            string basicPath = "";
            string json = "";
#if UNITY_STANDALONE_WIN||UNITY_EDITOR
            basicPath = Application.streamingAssetsPath;
            json = File.ReadAllText(basicPath + DataPath.defaultPath);
            
#else
            basicPath = "jar:file://" + Application.dataPath + "!/assets";
            WWW www = new WWW(basicPath+DataPath.defaultPath);
            json = www.text;
            Debug.LogError(json);
            
#endif
            // load 
            Management.MenuGameManager.Instance.DataArea = JsonUtility.FromJson<DataArea>(json);
            Debug.Log("Complete Load");
        }
        //private void SetDefaultDataArea(DataArea dataArea)
        //{
        //    string basicPath = Application.dataPath;
        //    // load 
        //    BinaryFormatter bf = new BinaryFormatter();
        //    using (FileStream fs = new FileStream(basicPath + DataPath.defaultUserDataPath, FileMode.Open))
        //    {
        //        dataArea.DefaultUserData = (Default_UserData)bf.Deserialize(fs);
        //    }

        //    using (FileStream fs = new FileStream(basicPath + DataPath.defaultItemDataPath, FileMode.Open))
        //    {
        //        //dataArea.DefaultItemData = new Default_ItemData[dataArea.MaxItemCount];
        //        //for (int i = 0; i < dataArea.MaxItemCount; ++i)
        //        //    dataArea.DefaultItemData[i] = (Default_ItemData)bf.Deserialize(fs);
        //        List<Default_ItemData> list = (List<Default_ItemData>)bf.Deserialize(fs);
        //        dataArea.DefaultItemData = list.ToArray();
        //    }

        //    using (FileStream fs = new FileStream(basicPath + DataPath.defaultSkillDataPath, FileMode.Open))
        //    {
        //        dataArea.DefaultSkillData = ((List<Default_SkillData>)bf.Deserialize(fs)).ToArray();
        //    }
        //    using (FileStream fs = new FileStream(basicPath + DataPath.defaultMinionSkillPath, FileMode.Open))
        //    {
        //        dataArea.DefaultMinionSkillData = ((List<Default_MinionSkillData>)bf.Deserialize(fs)).ToArray();
        //    }

        //    using (FileStream fs = new FileStream(basicPath + DataPath.defaultMinionDataPath, FileMode.Open))
        //    {
        //        //dataArea.DefaultMinionData = new Default_MinionData[dataArea.MaxMinionCount];

        //        //for (int i = 0; i < dataArea.MaxSkillCount; ++i)
        //        //   dataArea.DefaultMinionData[i] = (Default_MinionData)bf.Deserialize(fs);
        //        List<Default_MinionData> list = (List<Default_MinionData>)bf.Deserialize(fs);

        //        dataArea.DefaultMinionData = list.ToArray();
        //    }

        //    using (FileStream fs = new FileStream(basicPath + DataPath.defaultExpManagerPath, FileMode.Open))
        //    {
        //        dataArea.DefaultExpManager = (LvExpManager)bf.Deserialize(fs);
        //    }
            
        //}
    }

}