using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GameDataBase
{
    public class DefaultMinionDataS_L : MonoBehaviour
    {
        public bool bSwitchSave = true;
        private string xmlFilePath;
        private void OnMouseDown()
        {
            if(bSwitchSave)
                SaveData_3();
            else
            {
                LoadData_3();
            }
        }

        private void Awake()
        {
            xmlFilePath = Application.dataPath + "testMinionData.xml";
        }

        private void SaveData_2()
        {
            string basicPath = Application.dataPath;
            Debug.Log("SaveData_2");
            // user
            DefaultGameData.Default_UserData testUser = userSet();
            // unit
            List<DefaultGameData.Default_MinionData> testObj = unitSet();
            // item
            List<DefaultGameData.Default_ItemData> testItem = itemSet();
            // skill
            List<DefaultGameData.Default_SkillData> testSkill = skillSet();
            // minion skill
            List<DefaultGameData.Default_MinionSkillData> testMinionSkill = minionSkillSet();
            // lv exp
            DefaultGameData.LvExpManager lvManager = expManagerSet();

            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(basicPath+DataPath.defaultUserDataPath, FileMode.OpenOrCreate))
            {
                bf.Serialize(fs,testUser);
            }

            using (FileStream fs = new FileStream(basicPath + DataPath.defaultItemDataPath, FileMode.OpenOrCreate))
            {
                bf.Serialize(fs, testItem);
            }

            using (FileStream fs = new FileStream(basicPath + DataPath.defaultSkillDataPath, FileMode.OpenOrCreate))
            {
                bf.Serialize(fs, testSkill);
            }

            using (FileStream fs = new FileStream(basicPath + DataPath.defaultMinionSkillPath, FileMode.OpenOrCreate))
            {
                bf.Serialize(fs, testMinionSkill);
            }

            using (FileStream fs = new FileStream(basicPath + DataPath.defaultMinionDataPath, FileMode.OpenOrCreate))
            {
                bf.Serialize(fs, testObj);
            }

            using (FileStream fs = new FileStream(basicPath + DataPath.defaultExpManagerPath, FileMode.OpenOrCreate))
            {
                bf.Serialize(fs, lvManager);
            }
        }

        private void SaveData_3()
        {
            string basicPath = "";
#if UNITY_STANDALONE_WIN||UNITY_EDITOR
            basicPath = Application.streamingAssetsPath;
#else
            basicPath = "jar:file://" + Application.dataPath + "!/assets";
#endif

            DataArea db = new DataArea();

            Debug.Log("SaveData_3");
            // user
            db.DefaultUserData = userSet();
            // unit
            db.DefaultMinionData = unitSet().ToArray();
            // item
            db.DefaultItemData = itemSet().ToArray();
            // skill
            db.DefaultSkillData = skillSet().ToArray();
            // minion skill
            db.DefaultMinionSkillData = minionSkillSet().ToArray();
            // chapter 
            db.DefaultChapterData = chapterSet().ToArray();
            // lv exp
            db.DefaultExpManager = expManagerSet();

            string Json = JsonUtility.ToJson(db, true);
            File.WriteAllText(basicPath + DataPath.defaultPath, Json);
        }

        private DefaultGameData.Default_UserData userSet()
        {
            //나중에 바꿔야됨
            DefaultGameData.Default_UserData userData = new DefaultGameData.Default_UserData(20,0.01f, 0.01f,2,100,10,100,10);
            //
            return userData;
        }

        private List<DefaultGameData.Default_MinionData> unitSet()
        {
            List<DefaultGameData.Default_MinionData> list = new List<DefaultGameData.Default_MinionData>();
            
            DefaultGameData.Default_MinionData obj = new DefaultGameData.Default_MinionData();
            obj.MinionName = "索米";
            obj.MinionType = MinionType.Warriors;
            obj.BasicStatus = new BasicStatus(10, 5, 1, 1, 5, 0.2f, 0.2f, 0, 0, 2f);

            obj.ItemSlotType[0] = ItemType.Weapon;
            obj.ItemSlotType[1] = ItemType.Armor;
            obj.ItemSlotType[2] = ItemType.Accessory;

            obj.OwnedSkills[0] = 0;
            obj.OwnedSkills[1] = 1;
            obj.OwnedSkills[2] = 2;

            obj.AddStatusByLv = new BasicStatus(1, 0.5f, 0.1f, 0.1f, 0.5f, 0.02f, 0.02f, 0, 0, 0.2f);
            obj.MainImagePath = "Images/Character/" + obj.MinionName + "_0";
            obj.BattleImagePath = "Images/Character/" + obj.MinionName + "_1";
            obj.Explanation = "간단한 테스트용 유닛";
            list.Add(obj);

            obj = new DefaultGameData.Default_MinionData();
            obj.MinionName = "PPS-43";
            obj.MinionType = MinionType.Warriors;
            obj.BasicStatus = new BasicStatus(10, 5, 1, 1, 5, 0.2f, 0.2f, 0, 0, 2f);

            obj.ItemSlotType[0] = ItemType.Weapon;
            obj.ItemSlotType[1] = ItemType.Armor;
            obj.ItemSlotType[2] = ItemType.Accessory;

            obj.OwnedSkills[0] = 0;
            obj.OwnedSkills[1] = 1;
            obj.OwnedSkills[2] = 2;

            obj.AddStatusByLv = new BasicStatus(1, 0.5f, 0.1f, 0.1f, 0.5f, 0.02f, 0.02f, 0, 0, 0.2f);
            obj.MainImagePath = "Images/Character/" + obj.MinionName + "_0";
            obj.BattleImagePath = "Images/Character/" + obj.MinionName + "_1";
            obj.Explanation = "권총 유닛 PPS - 43";
            list.Add(obj);

            return list;
        }

        private List<DefaultGameData.Default_ItemData> itemSet()
        {
            List<DefaultGameData.Default_ItemData> list = new List<DefaultGameData.Default_ItemData>();

            BasicStatus bs = new BasicStatus(0,0,0.5f, 0.2f, 4f,0,0,0,0,0);
            DefaultGameData.Default_ItemData newItem = new DefaultGameData.Default_ItemData("weapon1", ItemType.Weapon, bs, bs * 0.1f, "Images/Item/"+ "weapon1", "무기 아이템");
            list.Add(newItem);

            bs = new BasicStatus(10f, 0,0, 0,0, 0, 3f, 0, 0.1f, 0);
            newItem = new DefaultGameData.Default_ItemData("armor1", ItemType.Weapon, bs, bs*0.2f, "Images/Item/" + "armor1", "방어 아이템");
            list.Add(newItem);
            return list;
        }

        private List<DefaultGameData.Default_SkillData> skillSet()
        {
            List<DefaultGameData.Default_SkillData> list = new List<DefaultGameData.Default_SkillData>();
            BasicStatus bsS = new BasicStatus(0, 0, 0.5f, 0.2f, 4f, 0, 0, 0, 0, 0);
            BasicStatus bsAS = bsS * 0.1f;
            BasicStatus bufS = new BasicStatus();
            BasicStatus bufAS = new BasicStatus();
            DefaultGameData.Default_SkillData newSkill = new DefaultGameData.Default_SkillData("testSkill", bsS, bsAS, SkillType.Active, bufS, bufAS, 0, "Images/Skill/" + "testSkill_0", "Images/Skill/" + "testSkill_1", "간단한 테스트용 스킬");
            list.Add(newSkill);

            bsS = new BasicStatus(1f, 0.2f, 0, 0, 0, 0, 0.1f, 0, 0, 0);
            bsAS = bsS * 0.1f;
            bufS = new BasicStatus();
            bufAS = new BasicStatus();
            newSkill = new DefaultGameData.Default_SkillData("testSkill1", bsS, bsAS, SkillType.Active, bufS, bufAS, 0, "Images/Skill/" + "testSkill1_0", "Images/Skill/" + "testSkill1_1", "간단한 테스트용 스킬");
            list.Add(newSkill);
            return list;
        }

        private List<DefaultGameData.Default_MinionSkillData> minionSkillSet()
        {
            List<DefaultGameData.Default_MinionSkillData> list = new List<DefaultGameData.Default_MinionSkillData>();
            BasicStatus bsS = new BasicStatus(0, 0, 0.5f, 0.2f, 4f, 0, 0, 0, 0, 0);
            BasicStatus bsAS = bsS*0.1f;
            BasicStatus bufS = new BasicStatus();
            BasicStatus bufAS = new BasicStatus();
            DefaultGameData.Default_MinionSkillData newSkill = new DefaultGameData.Default_MinionSkillData(SkillType.Active, "testMinionSkill0", bsS, bsAS, bufS, bufAS, 0, "Images/MinionSkill/" + "testMinionSkill0_0", "Images/MinionSkill/" + "testMinionSkill0_1", "간단한 테스트용 미니언 스킬");
            list.Add(newSkill);

            return list;
        }

        private List<DefaultGameData.Default_ChapterData> chapterSet()
        {
            List<DefaultGameData.Default_ChapterData> result = new List<DefaultGameData.Default_ChapterData>();
            
            DefaultGameData.Default_ChapterData chapter1 = new DefaultGameData.Default_ChapterData();
            chapter1.stage = new List<string>();
            chapter1.stage.Add("시작의 문");
            chapter1.stage.Add("병장기들의 묘지");
            chapter1.stage.Add("첫 번째 문지기");
            result.Add(chapter1);

            DefaultGameData.Default_ChapterData chapter2 = new DefaultGameData.Default_ChapterData();
            chapter2.stage = new List<string>();
            chapter2.stage.Add("시련의 문");
            chapter2.stage.Add("요정들의 호수");
            chapter2.stage.Add("두 번째 문지기");
            result.Add(chapter2);

            return result;
        }

        private DefaultGameData.LvExpManager expManagerSet()
        {
            DefaultGameData.LvExpManager manager = new DefaultGameData.LvExpManager();

            // minion
            manager.UnitRateCount = 5;
            manager.UnitExp = new int[manager.UnitRateCount];
            for (int i = 0; i < manager.UnitRateCount; ++i)
                manager.UnitExp[i] = (i+1)* 100;
            manager.LimitLvOfRate = new int[manager.UnitRateCount];
            manager.LimitLvOfRate[0] = (int)MinionLimitLV.Basic;
            manager.LimitLvOfRate[1] = (int)MinionLimitLV.Second;
            manager.UnitExpChart = UnitExpSet();

            // skill
            manager.SkillRateCount = 5;
            manager.SkillExp = new int[manager.SkillRateCount];
            for(int i = 0; i < manager.SkillRateCount; ++i)
                manager.SkillExp[i] = (i + 1) * 100;
            manager.SkillExpChart = skillExpSet();

            // item
            manager.ItemRateCount = 5;
            manager.ItemExp = new int[manager.ItemRateCount];
            for (int i = 0; i < manager.ItemRateCount; ++i)
                manager.ItemExp[i] = (i + 1) * 100;
            manager.ItemExpChart = itemExpSet();


            manager.UserExpChart = userExpDataSet();

            return manager;
        }
        private DefaultGameData.LvUpNeedExp UnitExpSet()
        {
            DefaultGameData.LvUpNeedExp unitExpChart = new DefaultGameData.LvUpNeedExp();
            unitExpChart.MaxLv = (int)ObjectLvLimit.MinionMax;
            unitExpChart.NeedExp = new int[unitExpChart.MaxLv];
            unitExpChart.NeedGold = new int[unitExpChart.MaxLv];
            int prevExp = 0;
            int prevGold = 0;
            for (int i = 0; i < unitExpChart.MaxLv; ++i)
            {
                if (i > 0)
                {
                    prevExp = unitExpChart.NeedExp[i - 1];
                    prevGold = unitExpChart.NeedGold[i - 1];
                }
                unitExpChart.NeedExp[i] = (i/10+1) * 100 + prevExp;
                unitExpChart.NeedGold[i] = (i/10+1) * 100 + prevGold;
            }
            return unitExpChart;
        }

        private DefaultGameData.LvUpNeedExp skillExpSet()
        {
            DefaultGameData.LvUpNeedExp skillExpChart = new DefaultGameData.LvUpNeedExp();
            skillExpChart.MaxLv = (int)ObjectLvLimit.SkillMax;
            skillExpChart.NeedExp = new int[skillExpChart.MaxLv];
            skillExpChart.NeedGold = new int[skillExpChart.MaxLv];
            int prevExp = 0;
            int prevGold = 0;
            for (int i = 0; i < skillExpChart.MaxLv; ++i)
            {
                if (i > 0)
                {
                    prevExp = skillExpChart.NeedExp[i - 1];
                    prevGold = skillExpChart.NeedGold[i - 1];
                }
                skillExpChart.NeedExp[i] = (i / 10 + 1) * 100 + prevExp;
                skillExpChart.NeedGold[i] = (i / 10 + 1) * 100 + prevGold;
            }
            return skillExpChart;
        }
        private DefaultGameData.LvUpNeedExp itemExpSet()
        {
            DefaultGameData.LvUpNeedExp itemExpChart = new DefaultGameData.LvUpNeedExp();
            itemExpChart.MaxLv = (int)ObjectLvLimit.ItemMax;
            itemExpChart.NeedExp = new int[itemExpChart.MaxLv];
            itemExpChart.NeedGold = new int[itemExpChart.MaxLv];
            int prevExp = 0;
            int prevGold = 0;
            for (int i = 0; i < itemExpChart.MaxLv; ++i)
            {
                if (i > 0)
                {
                    prevExp = itemExpChart.NeedExp[i - 1];
                    prevGold = itemExpChart.NeedGold[i - 1];
                }
                itemExpChart.NeedExp[i] = (i / 10 + 1) * 100 + prevExp;
                itemExpChart.NeedGold[i] = (i / 10 + 1) * 100 + prevGold;
            }
            return itemExpChart;
        }

        private DefaultGameData.LvUpNeedExp userExpDataSet()
        {
            DefaultGameData.LvUpNeedExp expChart = new DefaultGameData.LvUpNeedExp();
            expChart.MaxLv = (int)ObjectLvLimit.UserMax;
            expChart.NeedExp = new int[expChart.MaxLv];
            int prevExp = 0;
            for (int i = 0; i < expChart.MaxLv; ++i)
            {
                if (i > 0)
                {
                    prevExp = expChart.NeedExp[i - 1];
                }
                expChart.NeedExp[i] = (i + 1) * 1000 + prevExp;
            }
            return expChart;
        }
        private void LoadData_2()
        {
            string basicPath = Application.dataPath;
            Debug.Log("Load_2 Start");
            DefaultGameData.Default_UserData testUserData;
            List<DefaultGameData.Default_MinionData> obj;
            // item
            List<DefaultGameData.Default_ItemData> testItem;
            // skill
            List<DefaultGameData.Default_SkillData> testSkill;
            // minionskill
            List<DefaultGameData.Default_MinionSkillData> testMinionSkill;
            // lv exp
            DefaultGameData.LvExpManager lvManager;

            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(basicPath+DataPath.defaultUserDataPath, FileMode.Open))
            {
                testUserData = (DefaultGameData.Default_UserData)bf.Deserialize(fs);
            }
            using (FileStream fs = new FileStream(basicPath + DataPath.defaultMinionDataPath, FileMode.Open))
            {
                obj = (List<DefaultGameData.Default_MinionData>)bf.Deserialize(fs);
            }
            using (FileStream fs = new FileStream(basicPath+DataPath.defaultItemDataPath, FileMode.Open))
            {
                testItem = (List<DefaultGameData.Default_ItemData>)bf.Deserialize(fs);
            }
            using (FileStream fs = new FileStream(basicPath + DataPath.defaultSkillDataPath, FileMode.Open))
            {
                testSkill = (List<DefaultGameData.Default_SkillData>)bf.Deserialize(fs);
            }
            using (FileStream fs = new FileStream(basicPath + DataPath.defaultMinionSkillPath, FileMode.Open))
            {
                testMinionSkill = (List<DefaultGameData.Default_MinionSkillData>)bf.Deserialize(fs);
            }
            using (FileStream fs = new FileStream(basicPath + DataPath.defaultExpManagerPath, FileMode.Open))
            {
                lvManager = (DefaultGameData.LvExpManager)bf.Deserialize(fs);
            }
            Debug.Log("Complete Load");
        }
        private void LoadData_3()
        {
            string basicPath = Application.dataPath;
            Debug.Log("Load_2 Start");
            DataArea db;
            
            string json =  File.ReadAllText(basicPath + DataPath.defaultPath);
            db = JsonUtility.FromJson<DataArea>(json);
            Debug.Log("Complete Load");
        }
    }
}