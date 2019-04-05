using System.Collections;
using System.Collections.Generic;
using GameDataBase;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameSetting
{
    public class UI_SelectStrengthenMatSettingModule : UI_SelectStrengtMatSettingModule
    {
        ViewType curViewType;
        StrengthSubType curType;
        int maxSelectCount = 1;
        int curSelectedCount = 0;
        int targetIndex;

        [Header("Button")]
        public EventTrigger closeButton;
        public int closeModuleIndex;

        [Header("Image")]
        public Image backgroundImage;
        public Sprite backgroundSprite;

        [Header("Slot")]
        public GameObject slotPrefab;
        public List<UI_SlotModule> slotList;
        public GameObject slotParent;

        [Header("Title")]
        public Text title;

        [Header("SelectMat")]
        List<int> prevSelectMatList = new List<int>();
        List<int> selectMatList = new List<int>();

        public override void SetType(ViewType _viewType,StrengthSubType _type, List<int> _selectedMatList,int _targetIndex)
        {
            curViewType = _viewType;
            curType = _type;
            targetIndex = _targetIndex;
            if (curType == StrengthSubType.Target)
            {
                maxSelectCount = 1;
                title.text = "대상 선택";
                prevSelectMatList = _selectedMatList;
            }
            else if (curType == StrengthSubType.Material)
            {
                maxSelectCount = (int)StrengthSubType.SelectMax;
                prevSelectMatList = _selectedMatList;
                title.text = "재료 선택";
                selectMatList = new List<int>(prevSelectMatList);
            }
        }

        public override void SetUI(UI_Container _container)
        {
            curSelectedCount = selectMatList.Count;
            DataArea db = Management.MenuGameManager.Instance.DataArea;
            int slotIndex = 0;
            int count = 0;
            switch (curViewType)
            {
                case ViewType.Minion:
                    count = db.OwnedData.MinionData.Count;
                    break;
                case ViewType.Item:
                    count = db.OwnedData.ItemData.Count;
                    break;
                case ViewType.Skill:
                    count = db.OwnedData.SkillData.Count;
                    break;
            }
            for(int i = 0; i < count; ++i)
            {
                if (curType == StrengthSubType.Material && i == targetIndex) continue;

                if (curType == StrengthSubType.Target && prevSelectMatList.Contains(i)) continue;
                UI_SlotModule newSlot = null;
                switch (curViewType)
                {
                    case ViewType.Minion:
                        newSlot = Instantiate(slotPrefab, slotParent.transform).GetComponent<UI_SlotModule>();
                        newSlot.nameText.text = db.DefaultMinionData[db.OwnedData.MinionData[i].IndexOfMinion].MinionName;
                        newSlot.lvText.text = db.OwnedData.MinionData[i].Lv.ToString();
                        newSlot.SlotImage.sprite = Resources.Load<Sprite>(db.DefaultMinionData[db.OwnedData.MinionData[i].IndexOfMinion].MainImagePath);

                        break;
                    case ViewType.Item:
                        if (curType == StrengthSubType.Material &&db.OwnedData.ItemData[i].UseItem)
                            continue;
                        newSlot = Instantiate(slotPrefab, slotParent.transform).GetComponent<UI_SlotModule>();
                        newSlot.nameText.text = db.DefaultItemData[db.OwnedData.ItemData[i].IndexOfType].ItemName;
                        newSlot.lvText.text = db.OwnedData.ItemData[i].Lv.ToString();
                        newSlot.SlotImage.sprite = Resources.Load<Sprite>(db.DefaultItemData[db.OwnedData.ItemData[i].IndexOfType].IconPath);

                        break;
                    case ViewType.Skill:
                        newSlot = Instantiate(slotPrefab, slotParent.transform).GetComponent<UI_SlotModule>();
                        newSlot.nameText.text = db.DefaultSkillData[db.OwnedData.SkillData[i].IndexOfType].SkillName;
                        newSlot.lvText.text = db.OwnedData.SkillData[i].Lv.ToString();
                        newSlot.SlotImage.sprite = Resources.Load<Sprite>(db.DefaultSkillData[db.OwnedData.SkillData[i].IndexOfType].MainImagePath);

                        break;
                }
                newSlot.IndexOfSlot = slotIndex++;
                newSlot.IndexOfViewType = i;
                //EventTrigger.Entry entry = new EventTrigger.Entry();
                //entry.eventID = EventTriggerType.PointerDown;
                //entry.callback.AddListener((eventData)=> { SelectMat(newSlot.IndexOfSlot, newSlot.IndexOfViewType); });
                //newSlot.EventTrigger.triggers.Add(entry);
                if (newSlot.clickFunc == null)
                    newSlot.clickFunc = () => { SelectMat(newSlot.IndexOfSlot, newSlot.IndexOfViewType); };


                if (curType == StrengthSubType.Target && targetIndex == i)
                {
                    newSlot.selectedImage.SetActive(true);
                    ++curSelectedCount;
                    selectMatList.Add(i);
                }
                if (prevSelectMatList != null && prevSelectMatList.Contains(newSlot.IndexOfViewType))
                {
                    newSlot.selectedImage.SetActive(true);
                }

                slotList.Add(newSlot);
            }

            if (closeButton.triggers.Count <= 0)
            {
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerUp;
                entry.callback.AddListener((eventData) => { CloseButtonClick(_container, closeModuleIndex); });

                closeButton.triggers.Add(entry);
            }
        }

        public void SelectMat(int _slotIndex, int _ownedIndex)
        {
            if( selectMatList.Contains(_ownedIndex) == false)
            {
                if (curSelectedCount == maxSelectCount)
                {
                    return;
                }
                selectMatList.Add(_ownedIndex);
                ++curSelectedCount;
                slotList[_slotIndex].selectedImage.SetActive(true);
            }
            else
            {
                if (targetIndex == _ownedIndex)
                    targetIndex = -1;
                selectMatList.Remove(_ownedIndex);
                --curSelectedCount;
                slotList[_slotIndex].selectedImage.SetActive(false);
            }
        }

        private void CloseButtonClick(UI_Container _container, int _moduleIndex)
        {
            StrengthReturnData data;
            if ( curType == StrengthSubType.Target)
            {
                if(curSelectedCount <= 0)
                {
                    data = new StrengthReturnData(curViewType,curType, -1, -1);
                }
                else
                {
                    data = new StrengthReturnData(curViewType,curType, selectMatList[0], -1);
                }
            }
            else
            {
                if (curSelectedCount <= 0)
                {
                    data = new StrengthReturnData(curViewType, curType, null, -1);
                }
                else
                {
                    data = new StrengthReturnData(curViewType, curType, new List<int>( selectMatList), -1);
                }
            }
            ClickedButton(_container, _moduleIndex, data);
        }

        private void ClickedButton(UI_Container _container,int _moduleIndex, object _data)
        {
            _container.CallContents(_moduleIndex,_data);
        }

        /// <summary>
        /// 컨테이너 종료시 발생
        /// </summary>
        public void CloseContainer()
        {
            for (int i = 0; i < slotList.Count; ++i)
            {
                DestroyImmediate(slotList[i].gameObject);
            }
            prevSelectMatList.Clear();
            slotList.Clear();
            selectMatList.Clear();
        }


        private void OnDisable()
        {
            CloseContainer();
        }

        
    }
}