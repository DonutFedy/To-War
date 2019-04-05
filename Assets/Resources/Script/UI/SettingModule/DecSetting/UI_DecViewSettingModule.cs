using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameDataBase;
using GameDataBase.SettingData;
using UnityEngine.EventSystems;

namespace GameSetting
{
    public class UI_DecViewSettingModule : UI_SubContainerSettingModule
    {
        [Header("Background")]
        public Image backgroundImage;
        public Sprite backgroundSrpite;

        [Header("Title")]
        public Text titleText;
        public Text subTitleText;

        [Header("Buttons")]
        public EventTrigger closeButton;
        public int closeModuleIndex;

        public EventTrigger saveButton;
        public EventTrigger typeChangeButton;


        [Header("System")]
        public DecType curType;
        public int curOwnedDecIndex;
        public SettingDecData decData;
        UI_Container ownerConatiner;

        [Header("Slot Part")]
        public GameObject slotPrefab;
        public GameObject slotParent;
        public List<UI_SlotModule> slotList = new List<UI_SlotModule>();

        public override void SetSubContianer(object _data)
        {
            curOwnedDecIndex = (int)_data;
        }

        public override void SetUI(UI_Container _container)
        {
            ownerConatiner = _container;
            curType = DecType.Minion;
            SetTitle();

            if (closeButton.triggers.Count <= 0)
            {
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerDown;
                entry.callback.AddListener((eventData) => { ClickedButton(_container, closeModuleIndex); });
                closeButton.triggers.Add(entry);
            }
            if (typeChangeButton.triggers.Count <= 0)
            {
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerDown;
                entry.callback.AddListener((eventData)=> { TypeChange(); });
                typeChangeButton.triggers.Add(entry);
            }
            if (saveButton.triggers.Count <= 0)
            {
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerDown;
                entry.callback.AddListener((eventData) => { SaveData(); });
                saveButton.triggers.Add(entry);
            }



            SetSlot(_container);
        }

        private void SetTitle()
        {
            string minionType = "미니언 선택";
            string skillType = "스킬 선택";
            if(curType == DecType.Minion)
            {
                titleText.text = minionType;
                subTitleText.text = skillType;
            }
            else
            {
                titleText.text = skillType;
                subTitleText.text = minionType;
            }
        }

        private void SetSlot(UI_Container _container)
        {
            ClearSlot();

            DataArea db = Management.MenuGameManager.Instance.DataArea;

            // 덱이없다면 생성한다.(저장은 하지않는다.)
            if( db.OwnedData.DecData.Count <= curOwnedDecIndex)
            {
                decData = new SettingDecData(0, "새로운 덱 " + curOwnedDecIndex, new List<DecInfo>());
            }
            else
            {
                decData = new SettingDecData(db.OwnedData.DecData[curOwnedDecIndex]);
            }

            // 현재 뷰 타입에 맞게 슬롯을 생성
            int countMax = 0;
            if( curType == DecType.Minion)
                countMax = db.OwnedData.MinionData.Count;
            else
                countMax = db.OwnedData.SkillData.Count;

            for(int i = 0; i < countMax; ++i)
            {
                UI_SlotModule newSlot = Instantiate(slotPrefab, slotParent.transform).GetComponent<UI_SlotModule>();

                int curDefaultIndex = -1;
                string imagePath = string.Empty;
                string name = string.Empty;
                string lv = string.Empty;
                if(curType == DecType.Minion)
                {
                    curDefaultIndex = db.OwnedData.MinionData[i].IndexOfMinion;
                    imagePath = db.DefaultMinionData[curDefaultIndex].MainImagePath;
                    name = db.DefaultMinionData[curDefaultIndex].MinionName;
                    lv = db.OwnedData.MinionData[i].Lv.ToString();
                }
                else
                {
                    curDefaultIndex = db.OwnedData.SkillData[i].IndexOfType;
                    imagePath = db.DefaultSkillData[curDefaultIndex].MainImagePath;
                    name = db.DefaultSkillData[curDefaultIndex].SkillName;
                    lv = db.OwnedData.SkillData[i].Lv.ToString();
                }

                newSlot.IndexOfViewType = i;
                newSlot.IndexOfSlot = i;
                newSlot.SlotImage.sprite = Resources.Load<Sprite>(imagePath);
                newSlot.nameText.text = name;
                newSlot.lvText.text = lv;

                if (SearchSame(i) != null)
                {
                    newSlot.selectedImage.SetActive(true);
                }


                if (newSlot.clickFunc == null)
                    newSlot.clickFunc = () => { SelectSlot(newSlot.IndexOfViewType); };

                //EventTrigger.Entry entry = new EventTrigger.Entry();
                //entry.eventID = EventTriggerType.PointerDown;
                //entry.callback.AddListener((eventData)=> { SelectSlot(newSlot.IndexOfViewType); });
                //newSlot.EventTrigger.triggers.Add(entry);

                slotList.Add(newSlot);
            }

        }

        /// <summary>
        /// 슬롯 선택시
        /// </summary>
        /// <param name="_slotIndex"></param>
        public void SelectSlot(int _slotIndex)
        {
            DataArea db = Management.MenuGameManager.Instance.DataArea;
            DecInfo curUnit = null;
            // 덱에 세팅되있는 값이면 취소하고
            if ((curUnit = SearchSame(_slotIndex)) != null)
            {
                --decData.decCount;
                decData.decList.Remove(curUnit);
                slotList[_slotIndex].selectedImage.SetActive(false);
            }
            // 없으면 덱에 넣고 selected
            else if(decData.decCount< (int)DecMax.UnitMax)
            {
                ++decData.decCount;
                decData.decList.Add(new DecInfo(curType, _slotIndex));
                slotList[_slotIndex].selectedImage.SetActive(true);
            }
        }

        public void ClickedButton(UI_Container _container, int _moduleIndex, object _data= null)
        {
            _container.CallContents(_moduleIndex, _data);
        }

        private DecInfo SearchSame(int searchIndex)
        {
            DecInfo result = null;

            for(int i = 0; i < decData.decList.Count; ++i)
            {
                if( decData.decList[i].type == curType && decData.decList[i].ownedIndex == searchIndex)
                {
                    result = decData.decList[i];
                    break;
                }
            }

            return result;
        }
        /// <summary>
        /// 현재 덱의 정보를 저장
        /// </summary>
        public void SaveData()
        {
            DataArea db = Management.MenuGameManager.Instance.DataArea;
            


            if (db.OwnedData.DecData.Count > curOwnedDecIndex)
            {
                db.OwnedData.DecData[curOwnedDecIndex].decList = new List<DecInfo>();

                for(int i = 0; i < decData.decList.Count; ++i)
                {
                    db.OwnedData.DecData[curOwnedDecIndex].decList.Add(decData.decList[i]);
                }

                db.OwnedData.DecData[curOwnedDecIndex].decCount = decData.decList.Count;
                if( decData.decCount<= 0)
                {
                    db.OwnedData.DecData.RemoveAt(curOwnedDecIndex);
                }
            }
            else
            {
                db.OwnedData.DecData.Add(new SettingDecData(decData.decList.Count, "새로운 덱 " + curOwnedDecIndex, new List<DecInfo>(decData.decList)));
                curOwnedDecIndex = db.OwnedData.DecData.Count - 1;
            }
        }

        public void TypeChange()
        {
            if (curType == DecType.Minion)
                curType = DecType.Skill;
            else
                curType = DecType.Minion;

            SetTitle();

            SetSlot(ownerConatiner);
        }
        private void ClearSlot()
        {
            while (slotList.Count > 0)
            {
                DestroyImmediate(slotList[0].gameObject);
                slotList.RemoveAt(0);
            }
        }
    }
}