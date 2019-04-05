using System.Collections;
using System.Collections.Generic;
using GameDataBase;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameSetting
{
    public class UI_StrenghtenSkillSettingModule : UI_StrengthenSubSettingModule
    {
        [Header("ChangedStatus")]
        public Text lvText;
        public Text expText;
        public List<Text> addStatusTextList;

        [Header("Select Material")]
        public int openSelectMatContainerModuleIndex;
        public EventTrigger selectMatButton;
        public List<UI_StrengthenModule> matSlotList = new List<UI_StrengthenModule>();
        public List<int> selectedMatOwnedIndex = new List<int>();
        public GameObject matSlotParent;
        public GameObject matSlotPrefab;

        [Header("SubContainer List")]
        List<GameObject> subContainerList = new List<GameObject>();

        [Header("Select Target Slot")]
        public UI_SlotModule slot;
        public bool selectTarget;
        public bool selectMat;
        int addExp = 0;
        int addLv = 0;

        [Header("Strengthen")]
        public EventTrigger strenghtenButton;
        public int strengthenModuleIndex;

        public override void SetUI(UI_Container _container)
        {
            addExp = 0;
            addLv = 0;
            selectTarget = false;
            selectMat = false;

            if (selectMatButton.triggers.Count <= 0)
            {
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerDown;
                entry.callback.AddListener((eventData) => {Debug.LogError("Skill_SelectMat"); ButtonClicked(_container, openSelectMatContainerModuleIndex, new StrengthReturnData(ViewType.Skill, StrengthSubType.Material, new List<int>(selectedMatOwnedIndex), slot.IndexOfViewType)); });
                selectMatButton.triggers.Add(entry);
            }
            Strengthen();

            if( slot.clickFunc == null)
                slot.clickFunc = () => { Debug.LogError("Skill_Target"); ButtonClicked(_container, openSelectMatContainerModuleIndex, new StrengthReturnData(ViewType.Skill, StrengthSubType.Target, new List<int>(selectedMatOwnedIndex), slot.IndexOfViewType)); };


            //if (slot.EventTrigger.triggers.Count<= 0)
            //{
            //    EventTrigger.Entry entry = new EventTrigger.Entry();
            //    entry.eventID = EventTriggerType.PointerDown;
            //    entry.callback.AddListener((eventData) => { Debug.LogError("Skill_Target"); ButtonClicked(_container, openSelectMatContainerModuleIndex, new StrengthReturnData(ViewType.Skill, StrengthSubType.Target, new List<int>(selectedMatOwnedIndex), slot.IndexOfViewType)); });
            //    slot.EventTrigger.triggers.Add(entry);
            //}

            if(strenghtenButton.triggers.Count<= 0)
            {
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerDown;
                entry.callback.AddListener((eventData) => { Debug.LogError("Skill_Strenghten"); ClickedStrengthenButton(_container); });
                strenghtenButton.triggers.Add(entry);
            }
        }


        #region mat

        /// <summary>
        /// 서브 메뉴 호출
        /// </summary>
        public void ClickSelectMatButton()
        {
            // subContainer open
        }

        /// <summary>
        /// 서브 콘테이너에게 인덱스를 받아 세팅
        /// </summary>
        /// <param name="_index"></param>
        public override void GetTargetIndex(int _index)
        {
            if (_index >= 0)
                selectTarget = true;
            else
            {
                Strengthen();
                return;
            }

            DataArea db = Management.MenuGameManager.Instance.DataArea;
            slot.IndexOfViewType = _index;
            slot.lvText.text = db.OwnedData.SkillData[_index].Lv.ToString();
            slot.nameText.text = db.DefaultSkillData[db.OwnedData.SkillData[_index].IndexOfType].SkillName;
            slot.SlotImage.sprite = Resources.Load<Sprite>(db.DefaultSkillData[db.OwnedData.SkillData[_index].IndexOfType].MainImagePath);

            BasicStatus bs = db.DefaultSkillData[db.OwnedData.SkillData[_index].IndexOfType].BasicStatus;
            BasicStatus abs = db.OwnedData.SkillData[_index].AddStatus;
            lvText.text = (db.OwnedData.SkillData[_index].Lv).ToString();
            expText.text = (db.OwnedData.SkillData[_index].CurExp).ToString();
        }

        /// <summary>
        /// 서브 콘테이너에게 리스트를 받아 세팅
        /// </summary>
        /// <param name="selectedList"></param>
        public override void GetMatList(List<int> selectedList)
        {
            DataArea db = Management.MenuGameManager.Instance.DataArea;

            if (selectedList == null)
            {
                ClearMatList(true);
                return;
            }
            ClearMatList(false);
            selectedMatOwnedIndex = selectedList;
            int count = selectedList.Count;

            selectMat = true;


            for (int i = 0; i < count; ++i)
            {
                UI_StrengthenModule newSlot = Instantiate(matSlotPrefab, matSlotParent.transform).GetComponent<UI_StrengthenModule>();
                // image set
                newSlot.SlotImage.sprite = Resources.Load<Sprite>(db.DefaultSkillData[db.OwnedData.SkillData[selectedList[i]].IndexOfType].MainImagePath);
                newSlot.IndexOfViewType = selectedList[i];
                // button triggerset
                if (newSlot.clickFunc == null)
                    newSlot.clickFunc = () => { CancleMatSlot(newSlot.IndexOfSlot); };
                //EventTrigger.Entry entry = new EventTrigger.Entry();
                //entry.eventID = EventTriggerType.PointerDown;
                //entry.callback.AddListener((eventData) => { CancleMatSlot(newSlot.IndexOfSlot); });
                //newSlot.EventTrigger.triggers.Add(entry);
                // add list
                matSlotList.Add(newSlot);
                // add selectedList
            }
            addExp = OperateExp(db);
            SetTextBySelectedMat(db);
        }

        private void SetTextBySelectedMat(DataArea _db)
        {
            int needExp = _db.OwnedData.SkillData[slot.IndexOfViewType].NeedExp;
            int curExp = _db.OwnedData.SkillData[slot.IndexOfViewType].CurExp + addExp;
            int lv = _db.OwnedData.SkillData[slot.IndexOfViewType].Lv;
            int prevLv = lv;
            bool changeLv = false;
            while (needExp <= curExp)
            {
                ++lv;
                curExp -= needExp;
                needExp = _db.DefaultExpManager.UnitExpChart.NeedExp[lv];
                changeLv = true;
            }
            addLv = lv - prevLv;
            addExp = curExp;
            BasicStatus bs = _db.DefaultSkillData[_db.OwnedData.SkillData[slot.IndexOfViewType].IndexOfType].BasicStatus;
            BasicStatus abs = _db.OwnedData.SkillData[slot.IndexOfViewType].AddStatus;
            BasicStatus nextAbs = _db.OwnedData.SkillData[slot.IndexOfViewType].AddStatus + _db.DefaultSkillData[_db.OwnedData.SkillData[slot.IndexOfViewType].IndexOfType].AddStatusByLv * (lv - prevLv);
            if (addLv > 0)
                lvText.text = (_db.OwnedData.SkillData[slot.IndexOfViewType].Lv) + " >> " + lv;
            else
                lvText.text = prevLv.ToString();
            expText.text = (_db.OwnedData.SkillData[slot.IndexOfViewType].CurExp) + " >> " + curExp;
        }

        private int OperateExp(DataArea _db)
        {
            int addExp = 0;


            Debug.Log("Rate 추가해서 고쳐야됨");
            for (int i = 0; i < selectedMatOwnedIndex.Count; ++i)
            {
                //_db.DefaultMinionData[_db.OwnedData.MinionData[selectedMatOwnedIndex[i]].IndexOfMinion];
                addExp += _db.DefaultExpManager.UnitExp[2];
            }


            return addExp;
        }



        /// <summary>
        /// 재료 슬롯을 클릭시 취소되게 하는 함수
        /// </summary>
        /// <param name="index"></param>
        public void CancleMatSlot(int _index)
        {
            DataArea db = Management.MenuGameManager.Instance.DataArea;
            var slot = matSlotList[_index];
            matSlotList.RemoveAt(_index);
            selectedMatOwnedIndex.Remove(slot.IndexOfViewType);
            DestroyImmediate(slot.gameObject);
            if (matSlotList.Count <= 0)
            {
                lvText.text = (Management.MenuGameManager.Instance.DataArea.OwnedData.SkillData[_index].Lv).ToString();
                expText.text = (Management.MenuGameManager.Instance.DataArea.OwnedData.SkillData[_index].CurExp).ToString();
                ClearMatList(true);
                return;
            }
            addExp = OperateExp(db);
            SetTextBySelectedMat(db);
            for (int i = 0; i < matSlotList.Count; ++i)
            {
                matSlotList[i].IndexOfSlot = i;
            }
        }

        #endregion

        private void ButtonClicked(UI_Container _container, int _moduleIndex, object _data = null)
        {
            if (_moduleIndex == openSelectMatContainerModuleIndex && ((StrengthReturnData)_data).type == StrengthSubType.Material && selectTarget == false)
                return;
            _container.CallContents(_moduleIndex, _data);
        }

        public override void OpenSubContainer(OpenSubContainerData _subContainerIndex)
        {
            for (int i = 0; i < subContainerList.Count; ++i)
            {
                if (i != _subContainerIndex.SubContainerIndex)
                    subContainerList[i].SetActive(false);
            }
        }

        /// <summary>
        /// 컨테이너 종료시 발생
        /// </summary>
        public void CloseContainer()
        {
            for (int i = 0; i < matSlotList.Count; ++i)
            {
                DestroyImmediate(matSlotList[i].gameObject);
            }
            matSlotList.Clear();
        }

        public void ClickedStrengthenButton(UI_Container _container)
        {
            if (selectTarget && selectMat)
                ButtonClicked(_container, strengthenModuleIndex, new StrengthenData(ViewType.Skill, slot.IndexOfViewType, new List<int>(selectedMatOwnedIndex), addExp, addLv));
        }

        private void OnDisable()
        {
            CloseContainer();
        }
        private void ClearMatList(bool bAllClear)
        {
            for (int i = 0; i < matSlotList.Count; ++i)
                DestroyImmediate(matSlotList[i].gameObject);

            matSlotList.Clear();
            if (bAllClear == true)
            {
                selectedMatOwnedIndex.Clear();
                addExp = 0;
                addLv = 0;
            }
            selectMat = false;
        }

        public override void Strengthen()
        {
            lvText.text = "--";
            expText.text = "--";

            selectTarget = false;
            slot.IndexOfViewType = -1;
            slot.nameText.text = "대상을 골라주세요";
            slot.lvText.text = "--";
            slot.SlotImage.sprite = null;
            ClearMatList(true);
        }
        public override void CloseSubContainer(object _data)
        {
        }

        public override void SetSubContianer(object _data)
        {
        }
    }
}