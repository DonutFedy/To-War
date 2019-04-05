using GameDataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameSetting
{
    public class UI_StrenghtenMinionSettingModule : UI_StrengthenSubSettingModule
    {
        [Header("ChangedStatus")]
        public Text lvText;
        public Text expText;
        public Text hpText;
        public Text mpText;
        public Text attackSpeedText;
        public Text attackRangeText;
        public Text attackPowerText;
        public Text criticalChanceText;
        public Text deffenceText;
        public Text deffencePentractionText;
        public Text evasiveText;
        public Text moveSpeedText;

        [Header("Select Material")]
        public int openSelectMatContainerModuleIndex;
        public UI_Button selectMatButton;
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
        public Button strenghtenButton;
        public int strengthenModuleIndex;

        public override void SetUI(UI_Container _container)
        {
            addExp = 0;
            addLv = 0;
            selectTarget = false;
            selectMat = false;
            
            if(selectMatButton.clickedFunc == null)
            {
                selectMatButton.clickedFunc = () => { ButtonClicked(_container, openSelectMatContainerModuleIndex, new StrengthReturnData(ViewType.Minion, StrengthSubType.Material, new List<int>(selectedMatOwnedIndex), slot.IndexOfViewType)); };
            }

            Strengthen();

            if (slot.clickFunc == null)
                slot.clickFunc = () => { ButtonClicked(_container, openSelectMatContainerModuleIndex, new StrengthReturnData(ViewType.Minion, StrengthSubType.Target, new List<int>(selectedMatOwnedIndex), slot.IndexOfViewType)); };

            //EventTrigger.Entry entry = new EventTrigger.Entry();
            //entry.eventID = EventTriggerType.PointerDown;
            //entry.callback.AddListener((eventData) => { ButtonClicked(_container, openSelectMatContainerModuleIndex, new StrengthReturnData(ViewType.Minion, StrengthSubType.Target, new List<int>(selectedMatOwnedIndex), slot.IndexOfViewType)); });
            //slot.EventTrigger.triggers.Add(entry);

            strenghtenButton.onClick.AddListener(() => { ClickedStrengthenButton(_container);});
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
            slot.lvText.text = db.OwnedData.MinionData[_index].Lv.ToString();
            slot.nameText.text = db.DefaultMinionData[db.OwnedData.MinionData[_index].IndexOfMinion].MinionName;
            slot.SlotImage.sprite = Resources.Load<Sprite>(db.DefaultMinionData[db.OwnedData.MinionData[_index].IndexOfMinion].MainImagePath);

            BasicStatus bs = db.DefaultMinionData[db.OwnedData.MinionData[_index].IndexOfMinion].BasicStatus;
            BasicStatus abs = db.OwnedData.MinionData[_index].AddStatus;
            lvText.text = (db.OwnedData.MinionData[_index].Lv).ToString();
            expText.text = (db.OwnedData.MinionData[_index].CurExp).ToString();
            hpText.text = (bs.Hp + abs.Hp).ToString();
            mpText.text = (bs.Mp + abs.Mp).ToString();
            attackSpeedText.text = (bs.AttackSpeed + abs.AttackSpeed).ToString();
            attackRangeText.text = (bs.AttackRange + abs.AttackRange).ToString();
            attackPowerText.text = (bs.AttackPower + abs.AttackPower).ToString();
            criticalChanceText.text = (bs.CriticalChance + abs.CriticalChance).ToString();
            deffenceText.text = (bs.Deffence + abs.Deffence).ToString();
            deffencePentractionText.text = (bs.DeffencePentraction + abs.DeffencePentraction).ToString();
            evasiveText.text = (bs.Evasive + abs.Evasive).ToString();
            moveSpeedText.text = (bs.MoveSpeed + abs.MoveSpeed).ToString();
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
                newSlot.SlotImage.sprite = Resources.Load<Sprite>(db.DefaultMinionData[db.OwnedData.MinionData[selectedList[i]].IndexOfMinion].MainImagePath);
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
            int needExp = _db.OwnedData.MinionData[slot.IndexOfViewType].NeedExp;
            int curExp  = _db.OwnedData.MinionData[slot.IndexOfViewType].CurExp + addExp;
            int lv = _db.OwnedData.MinionData[slot.IndexOfViewType].Lv;
            int prevLv = lv;
            bool changeLv = false;
            while (needExp <= curExp)
            {
                ++lv;
                curExp -= needExp;
                needExp = _db.DefaultExpManager.UnitExpChart.NeedExp[lv];
                changeLv = true;
            }
            ChangeStatus(_db, curExp, lv, prevLv);
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

        private void ChangeStatus(DataArea _db,int _curExp, int _lv, int _prevLv)
        {
            addLv = _lv - _prevLv;
            addExp = _curExp;
            BasicStatus bs = _db.DefaultMinionData[_db.OwnedData.MinionData[slot.IndexOfViewType].IndexOfMinion].BasicStatus;
            BasicStatus abs = _db.OwnedData.MinionData[slot.IndexOfViewType].AddStatus;
            BasicStatus nextAbs = _db.OwnedData.MinionData[slot.IndexOfViewType].AddStatus + _db.DefaultMinionData[_db.OwnedData.MinionData[slot.IndexOfViewType].IndexOfMinion].AddStatusByLv * (_lv - _prevLv);
            expText.text = (_db.OwnedData.MinionData[slot.IndexOfViewType].CurExp) + " >> " + _curExp;
            if( addLv == 0)
            {
                lvText.text = (_db.OwnedData.MinionData[slot.IndexOfViewType].Lv).ToString();
                hpText.text = (bs.Hp + abs.Hp).ToString();
                mpText.text = (bs.Mp + abs.Mp).ToString();
                attackSpeedText.text = (bs.AttackSpeed + abs.AttackSpeed).ToString();
                attackRangeText.text = (bs.AttackRange + abs.AttackRange).ToString();
                attackPowerText.text = (bs.AttackPower + abs.AttackPower).ToString();
                criticalChanceText.text = (bs.CriticalChance + abs.CriticalChance).ToString();
                deffenceText.text = (bs.Deffence + abs.Deffence).ToString();
                deffencePentractionText.text = (bs.DeffencePentraction + abs.DeffencePentraction).ToString();
                evasiveText.text = (bs.Evasive + abs.Evasive).ToString();
                moveSpeedText.text = (bs.MoveSpeed + abs.MoveSpeed).ToString();
            }
            else
            {
                lvText.text = (_db.OwnedData.MinionData[slot.IndexOfViewType].Lv) + " >> " + _lv;
                hpText.text = (bs.Hp + abs.Hp) + " >> " + (bs.Hp + nextAbs.Hp);
                mpText.text = (bs.Mp + abs.Mp) + " >> " + (bs.Mp + nextAbs.Mp);
                attackSpeedText.text = (bs.AttackSpeed + abs.AttackSpeed) + " >> " + (bs.AttackSpeed + nextAbs.AttackSpeed);
                attackRangeText.text = (bs.AttackRange + abs.AttackRange) + " >> " + (bs.AttackRange + nextAbs.AttackRange);
                attackPowerText.text = (bs.AttackPower + abs.AttackPower) + " >> " + (bs.AttackPower + nextAbs.AttackPower);
                criticalChanceText.text = (bs.CriticalChance + abs.CriticalChance) + " >> " + (bs.CriticalChance + nextAbs.CriticalChance);
                deffenceText.text = (bs.Deffence + abs.Deffence) + " >> " + (bs.Deffence + nextAbs.Deffence);
                deffencePentractionText.text = (bs.DeffencePentraction + abs.DeffencePentraction) + " >> " + (bs.DeffencePentraction + nextAbs.DeffencePentraction);
                evasiveText.text = (bs.Evasive + abs.Evasive) + " >> " + (bs.Evasive + nextAbs.Evasive);
                moveSpeedText.text = (bs.MoveSpeed + abs.MoveSpeed) + " >> " + (bs.MoveSpeed + nextAbs.MoveSpeed);
            }
        }

        /// <summary>
        /// 재료 슬롯을 클릭시 취소되게 하는 함수
        /// </summary>
        /// <param name="index"></param>
        public void CancleMatSlot(int _index)
        {
            var slot = matSlotList[_index];
            DataArea db = Management.MenuGameManager.Instance.DataArea;
            
            selectedMatOwnedIndex.Remove(slot.IndexOfViewType);
            DestroyImmediate(slot.gameObject);
            matSlotList.RemoveAt(_index);
            if (matSlotList.Count <= 0)
            {
                addExp = 0;
                addLv = 0;
                lvText.text = (Management.MenuGameManager.Instance.DataArea.OwnedData.MinionData[_index].Lv).ToString();
                expText.text = (Management.MenuGameManager.Instance.DataArea.OwnedData.MinionData[_index].CurExp).ToString();
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
                ButtonClicked(_container, strengthenModuleIndex, new StrengthenData(ViewType.Minion, slot.IndexOfViewType, new List<int>(selectedMatOwnedIndex), addExp, addLv));
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
            hpText.text = "--";
            mpText.text = "--";
            attackSpeedText.text = "--";
            attackRangeText.text = "--";
            attackPowerText.text = "--";
            criticalChanceText.text = "--";
            deffenceText.text = "--";
            deffencePentractionText.text = "--";
            evasiveText.text = "--";
            moveSpeedText.text = "--";


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