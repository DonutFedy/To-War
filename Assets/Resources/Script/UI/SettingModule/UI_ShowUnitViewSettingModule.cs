using GameDataBase;
using GameDataBase.SettingData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameSetting
{

    public class UI_ShowUnitViewSettingModule : UI_SettingModuleHaveSubContainerModule
    {
        #region contain Object
        [Header("Background")]
        [SerializeField]
        Sprite backgroundImage;//배경이미지
        [SerializeField]
        Image backgroundImageObject;//배경이미지 적용대상


        // close module delegate
        [Header("CloseModule")]
        public UI_Action_CloseContainerModule closeModule;

        [Header("ViewType")]
        // choose viewtype buttons
        public int ChooseTypeButtonIndexOfModule;
        public EventTrigger minionTypeB;
        public EventTrigger itemTypeB;
        public EventTrigger skillTypeB;
        public Text curViewTypeText;


        [Header("Slot")]
        // Slot
        [SerializeField]
        int viewDetailContentsIndex;
        [SerializeField]
        ViewType viewType= ViewType.Item;
        [SerializeField]
        GameObject slotPrefab;
        [SerializeField]
        GameObject slotParent;
        [SerializeField]
        List<UI_SlotModule> slotList;
        int prevSlotIndex = -1;

        [Space(20)]

        [Header("ExitButton")]
        [SerializeField]
        public UI_Button exitButton = null;
        [SerializeField]
        int closeButtonContentsIndex;

        [Header("Minion Info")]
        public GameObject minionInfoObj;

        public Image    minionImage;
        public Text     minionName;
        public Text     minionLv;
        public Text     minionExp;
        public Text     minionInfo;
        public Text     minionStatusInfo;

        public Image    skillImage;
        public Text     skillName;
        public Text     skillLv;
        public Text     skillInfo;
        
        public Image    item_0_Image;
        public Text     item_0_Name;
        public Text     item_0_Lv;
        public Text     item_0_Info;

        public Image    item_1_Image;
        public Text     item_1_Name;
        public Text     item_1_Lv;
        public Text     item_1_Info;

        public Image    item_2_Image;
        public Text     item_2_Name;
        public Text     item_2_Lv;
        public Text     item_2_Info;

        [Header("Item Info")]
        public GameObject   itemInfoObj;
        public Image        itemImage;
        public Text         itemName;
        public Text         itemLv;
        public Text         itemExp;
        public Text         itemInfo;
        public Text         itemAddStatus;

        public Sprite BackgroundImage { get => backgroundImage; set => backgroundImage = value; }
        public Image BackgroundImageObject { get => backgroundImageObject; set => backgroundImageObject = value; }
        public int CloseButtonContentsIndex { get => closeButtonContentsIndex; set => closeButtonContentsIndex = value; }
        public GameObject SlotPrefab { get => slotPrefab; set => slotPrefab = value; }
        public GameObject SlotParent { get => slotParent; set => slotParent = value; }
        public List<UI_SlotModule> SlotList { get => slotList; set => slotList = value; }
        public int ViewDetailContentsIndex { get => viewDetailContentsIndex; set => viewDetailContentsIndex = value; }

        [Header("Skill Info")]
        public GameObject   skillTypeInfoObj;
        public Image        skillTypeImage;
        public Text         skillTypeName;
        public Text         skillTypeLv;
        public Text         skillTypeExp;
        public Text         skillTypeInfo;
        public Text         skillTypeAddStatus;



        [Header("ItemSelect Contianer")]
        public List<GameObject> subContainer = new List<GameObject>();
        public List<EventTrigger> subContainerOpenTrigger = new List<EventTrigger>();
        public int openItemSelectSubContainerModuleIndex;

        #endregion

        public override void SetUI(UI_Container _container)
        {
            Debug.LogError("TestView set....add script");

            ClearSlot();

            // close module delegate set
            closeModule.deletgateFunc = ClearSlot;

            // background set
            backgroundImageObject.sprite = backgroundImage;

            // button set
            if (exitButton.clickedFunc==null)
            {
                exitButton.clickedFunc = () => { ButtonClicked(_container, CloseButtonContentsIndex); };
            }
            if (minionTypeB.triggers.Count<= 0)
            {
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerDown;
                entry.callback.AddListener((eventData)=> { ButtonClicked(_container, ChooseTypeButtonIndexOfModule, (int)ViewType.Minion); });
                minionTypeB.triggers.Add(entry);
            }
            if (itemTypeB.triggers.Count <= 0)
            {
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerDown;
                entry.callback.AddListener((eventData) => { ButtonClicked(_container, ChooseTypeButtonIndexOfModule, (int)ViewType.Item); });
                itemTypeB.triggers.Add(entry);
            }
            if (skillTypeB.triggers.Count <= 0)
            {
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerDown;
                entry.callback.AddListener((eventData) => { ButtonClicked(_container, ChooseTypeButtonIndexOfModule, (int)ViewType.Skill); });
                skillTypeB.triggers.Add(entry);
            }

            for (int i = 0; i < subContainerOpenTrigger.Count; ++i)
            {
                if (subContainerOpenTrigger[i].triggers.Count > 0)
                    continue;
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerDown;
                int curIndex = i;
                entry.callback.AddListener((eventoject)=> {ClickItemSlot(_container, curIndex); });
                subContainerOpenTrigger[i].triggers.Add(entry);
            }

            // slot set
            SetSlots(_container,ViewType.Minion);
        }

        public void ClickItemSlot(UI_Container _container, int _itemSlotIndex)
        {
            int minionIndex = prevSlotIndex;
            DataArea db = Management.MenuGameManager.Instance.DataArea;
            switch (_itemSlotIndex)
            {
                case 0:
                    if (db.OwnedData.MinionData[minionIndex].Lv < (int)MinionItemSlotLimitByLv.First)
                        return;
                    break;
                case 1:
                    if (db.OwnedData.MinionData[minionIndex].Lv < (int)MinionItemSlotLimitByLv.Second)
                        return;
                    break;
                case 2:
                    if (db.OwnedData.MinionData[minionIndex].Lv < (int)MinionItemSlotLimitByLv.Third)
                        return;
                    break;
            }

            ButtonClicked(_container, openItemSelectSubContainerModuleIndex, new OpenSubContainerData(openItemSelectSubContainerModuleIndex, new int[] { _itemSlotIndex,prevSlotIndex }));
        }

        public void SetSlots(UI_Container _container,ViewType viewType)
        {
            if (this.viewType == viewType)
                return;
            ClearSlot();



            this.viewType = viewType;
            string basicPath = Application.dataPath;
            int userOwnedCount = 0;
            if (viewType == ViewType.Minion)
            {
                curViewTypeText.text = "미니언 목록";
                userOwnedCount = Management.MenuGameManager.Instance.DataArea.OwnedData.MinionData.Count;
            }
            else if (viewType == ViewType.Item)
            {
                curViewTypeText.text = "아이템 목록";
                userOwnedCount = Management.MenuGameManager.Instance.DataArea.OwnedData.ItemData.Count;
            }
            else
            {
                curViewTypeText.text = "스킬 목록";
                userOwnedCount = Management.MenuGameManager.Instance.DataArea.OwnedData.SkillData.Count;
            }

            for(int i = 0; i < userOwnedCount; ++i)
            {
                string path = string.Empty;
                string name = string.Empty;
                int lv = 1;
                var newSlot = Instantiate(SlotPrefab,SlotParent.transform).GetComponent<UI_SlotModule>();

                slotList.Add(newSlot);

                newSlot.IndexOfSlot = i;
                if (viewType == ViewType.Minion)
                {
                    newSlot.IndexOfViewType = Management.MenuGameManager.Instance.DataArea.OwnedData.MinionData[i].IndexOfMinion;
                    path = Management.MenuGameManager.Instance.DataArea.DefaultMinionData[newSlot.IndexOfViewType].MainImagePath;

                    name = Management.MenuGameManager.Instance.DataArea.DefaultMinionData[newSlot.IndexOfViewType].MinionName;
                    lv = Management.MenuGameManager.Instance.DataArea.OwnedData.MinionData[i].Lv;
                }
                else if (viewType == ViewType.Item)
                {
                    newSlot.IndexOfViewType = Management.MenuGameManager.Instance.DataArea.OwnedData.ItemData[i].IndexOfType;
                    path = Management.MenuGameManager.Instance.DataArea.DefaultItemData[newSlot.IndexOfViewType].IconPath;

                    name = Management.MenuGameManager.Instance.DataArea.DefaultItemData[newSlot.IndexOfViewType].ItemName;
                    lv = Management.MenuGameManager.Instance.DataArea.OwnedData.ItemData[i].Lv;
                }
                else
                {
                    newSlot.IndexOfViewType = Management.MenuGameManager.Instance.DataArea.OwnedData.SkillData[i].IndexOfType;
                    path = Management.MenuGameManager.Instance.DataArea.DefaultSkillData[newSlot.IndexOfViewType].MainImagePath;

                    name = Management.MenuGameManager.Instance.DataArea.DefaultSkillData[newSlot.IndexOfViewType].SkillName;
                    lv = Management.MenuGameManager.Instance.DataArea.OwnedData.SkillData[i].Lv;
                }
                newSlot.ViewType = viewType;


                if (newSlot.clickFunc == null)
                    newSlot.clickFunc = () => {  SelectSlot(newSlot.IndexOfSlot); ButtonClicked(_container, viewDetailContentsIndex, new UI_Slot_Data(viewType, newSlot.IndexOfSlot));  };
                


                // image Set
                Sprite sp = Resources.Load<Sprite>(path);
                newSlot.SlotImage.sprite = sp;

                newSlot.nameText.text = name;
                newSlot.lvText.text = lv.ToString();
            }



            if (viewType == ViewType.Minion)
            {
                SetMinionInfo(0);
            }
            else if (viewType == ViewType.Item)
            {
                SetItemInfo(0);
            }
            else
            {
                SetSkillInfo(0);
            }


            prevSlotIndex = 0;
            SelectSlot(0);

        }

        public void SetMinionInfo(int index)
        {

            DataArea db = Management.MenuGameManager.Instance.DataArea;

            if( db.OwnedData.MinionData.Count <= 0)
            {
                // empty data
                minionInfoObj.SetActive(false);
                return;
            }
            minionInfoObj.SetActive(true);
            skillTypeInfoObj.SetActive(false);
            itemInfoObj.SetActive(false);

            var curMinion = db.OwnedData.MinionData[index];
            int indexMinion = curMinion.IndexOfMinion;
            minionImage.sprite =Resources.Load<Sprite>(db.DefaultMinionData[indexMinion].MainImagePath);
            minionName.text =   db.DefaultMinionData[indexMinion].MinionName;
            minionLv.text =     "Lv : " + curMinion.Lv;
            minionExp.text =    "Exp : " + curMinion.CurExp + "/" + curMinion.NeedExp;
            minionInfo.text =   db.DefaultMinionData[indexMinion].Explanation;
            minionStatusInfo.text = SetStatusMinion(curMinion, indexMinion,db);

            int indexSkill = db.DefaultMinionData[indexMinion].OwnedSkills[0];
            skillImage.sprite = Resources.Load<Sprite>(db.DefaultMinionSkillData[indexSkill].MainImagePath); ;
            skillName.text = db.DefaultMinionSkillData[indexSkill].SkillName;
            skillLv.text    = "Lv : " + curMinion.OwnedSkillList[0].Lv.ToString();
            skillInfo.text  =   db.DefaultMinionSkillData[indexSkill].Explanation;

            int _minionLv = curMinion.Lv;
            if (_minionLv < (int)(MinionItemSlotLimitByLv.First) || curMinion.IndexOfSerializeCodeOwnedItem[0] == -1)
            {
                item_0_Image.sprite = null;
                item_0_Name.text = "-";
                item_0_Lv.text = "Lv : " + "-";
                item_0_Info.text = "-";
            }
            else{
                var item_0 = db.OwnedData.ItemData.Find(x => x.IndexOfSerializeCode == curMinion.IndexOfSerializeCodeOwnedItem[0]);
                item_0_Image.sprite = Resources.Load<Sprite>( db.DefaultItemData[item_0.IndexOfType].IconPath);
                item_0_Name.text = db.DefaultItemData[item_0.IndexOfType].ItemName;
                item_0_Lv.text = "Lv : " + item_0.Lv.ToString();
                item_0_Info.text = db.DefaultItemData[item_0.IndexOfType].Explanation;
            }

            if (_minionLv < (int)(MinionItemSlotLimitByLv.Second) || curMinion.IndexOfSerializeCodeOwnedItem[1] == -1)
            {
                item_1_Image.sprite = null;
                item_1_Name.text = "-";
                item_1_Lv.text = "Lv : " + "-";
                item_1_Info.text = "-";
            }        
            else
            {
                var item_1 = db.OwnedData.ItemData.Find(x => x.IndexOfSerializeCode == curMinion.IndexOfSerializeCodeOwnedItem[1]);
                item_1_Image.sprite = Resources.Load<Sprite>(db.DefaultItemData[item_1.IndexOfType].IconPath);
                item_1_Name.text = db.DefaultItemData[item_1.IndexOfType].ItemName;
                item_1_Lv.text = "Lv : " + item_1.Lv.ToString();
                item_1_Info.text = db.DefaultItemData[item_1.IndexOfType].Explanation;
            }

            if (_minionLv < (int)(MinionItemSlotLimitByLv.Third) || curMinion.IndexOfSerializeCodeOwnedItem[2] == -1)
            {
                item_2_Image.sprite = null;
                item_2_Name.text = "-";
                item_2_Lv.text = "Lv : " + "-";
                item_2_Info.text = "-";
            }        
            else
            {
                var item_2 = db.OwnedData.ItemData.Find(x => x.IndexOfSerializeCode == curMinion.IndexOfSerializeCodeOwnedItem[2]);
                item_2_Image.sprite = Resources.Load<Sprite>(db.DefaultItemData[item_2.IndexOfType].IconPath);
                item_2_Name.text = db.DefaultItemData[item_2.IndexOfType].ItemName;
                item_2_Lv.text = "Lv : " + item_2.Lv.ToString();
                item_2_Info.text = db.DefaultItemData[item_2.IndexOfType].Explanation;
            }
        }

        private string SetStatusMinion(SettingMinionData _ownedMinion, int _defaultIndex, DataArea _db)
        {
            string result = string.Empty;

            BasicStatus resultBS = _db.DefaultMinionData[_defaultIndex].BasicStatus;
            resultBS += _ownedMinion.AddStatus;

            result += "+Hp : " + resultBS.Hp + "\t\t";
            result += "+Mp : " + resultBS.Mp + "\n";
            result += "+공속    : " + resultBS.AttackSpeed+"\t\t";
            result += "+공격범위 : " + resultBS.AttackRange  + "\n";
            result += "+공격력 : " + resultBS.AttackPower+ "\t\t";
            result += "+치명타율 : " + resultBS.CriticalChance + "\n";
            result += "+방어력 : " + resultBS.Deffence+ "\t\t";
            result += "+방어관통력 : " + resultBS.DeffencePentraction + "\n";
            result += "+회피율 : " + resultBS.Evasive+"\t\t";
            result += "+이동속도 : " + resultBS.MoveSpeed;

            return result;
        }

        public void SetItemInfo(int index)
        {
            DataArea db = Management.MenuGameManager.Instance.DataArea;
            if (db.OwnedData.ItemData.Count <= 0)
            {
                // empty data
                itemInfoObj.SetActive(false);
                return;
            }
            itemInfoObj.SetActive(true);
            minionInfoObj.SetActive(false);
            skillTypeInfoObj.SetActive(false);

            int itemIndex = db.OwnedData.ItemData[index].IndexOfType;

            string addStatusStr = CheckItemAddStatus(index,itemIndex, db);
            

            itemImage.sprite = Resources.Load<Sprite>( db.DefaultItemData[itemIndex].IconPath);
            itemName.text = db.DefaultItemData[itemIndex].ItemName;
            itemLv.text = "Lv : "+ db.OwnedData.ItemData[index].Lv;
            itemExp.text = "Exp : "+db.OwnedData.ItemData[index].CurExp + "/" + db.OwnedData.ItemData[index].NeedExp;
            itemAddStatus.text = addStatusStr;
            itemInfo.text = db.DefaultItemData[itemIndex].Explanation;
        }

        public void SetSkillInfo(int index)
        {
            DataArea db = Management.MenuGameManager.Instance.DataArea;
            if (db.OwnedData.SkillData.Count <= 0)
            {
                // empty data
                skillTypeInfoObj.SetActive(false);
                return;
            }
            skillTypeInfoObj.SetActive(true);
            itemInfoObj.SetActive(false);
            minionInfoObj.SetActive(false);

            int skillIndex = db.OwnedData.SkillData[index].IndexOfType;

            string addStatusStr = "스킬 개발 요망";

            skillTypeImage.sprite = Resources.Load<Sprite>(db.DefaultSkillData[skillIndex].MainImagePath);
            skillTypeName.text = db.DefaultSkillData[skillIndex].SkillName;
            skillTypeLv.text = "Lv : " + db.OwnedData.SkillData[index].Lv;
            skillTypeExp.text = "Exp : " + db.OwnedData.SkillData[index].CurExp + "/" + db.OwnedData.SkillData[index].NeedExp;
            skillTypeInfo.text = addStatusStr;
            skillTypeAddStatus.text = db.DefaultSkillData[skillIndex].Explantion;
        }

        private string CheckItemAddStatus(int _ownedIndex,int _defaultIndex, DataArea _db)
        {
            string result = "";
            BasicStatus resultBS = _db.DefaultItemData[_defaultIndex].BasicStatus;
            resultBS += _db.OwnedData.ItemData[_ownedIndex].AddStatus;
            int count = 0;
            if (resultBS.Hp != 0)
            {
                result += "+Hp : "+resultBS.Hp;
                ++count;
                if( count %2 == 0)
                {
                    result += "\n";
                    count = 0;
                }
                else
                {
                    result += "\t";
                }
            }
            if(resultBS.Mp != 0)
            {
                result += "+Mp : " + resultBS.Mp;
                ++count;
                if (count % 2 == 0)
                {
                    result += "\n";
                    count = 0;
                }
                else
                {
                    result += "\t";
                }
            }
            if (resultBS.AttackSpeed != 0)
            {
                result += "+공속 : " + resultBS.AttackSpeed;
                ++count;
                if (count % 2 == 0)
                {
                    result += "\n";
                    count = 0;
                }
                else
                {
                    result += "\t";
                }
            }
            if (resultBS.AttackRange != 0)
            {
                result += "+공격범위 : " + resultBS.AttackRange;
                ++count;
                if (count % 2 == 0)
                {
                    result += "\n";
                    count = 0;
                }
                else
                {
                    result += "\t";
                }
            }
            if (resultBS.AttackPower != 0)
            {
                result += "+공격력 : " + resultBS.AttackPower;
                ++count;
                if (count % 2 == 0)
                {
                    result += "\n";
                    count = 0;
                }
                else
                {
                    result += "\t";
                }
            }
            if (resultBS.CriticalChance != 0)
            {
                result += "+치명타율 : " + resultBS.CriticalChance;
                ++count;
                if (count % 2 == 0)
                {
                    result += "\n";
                    count = 0;
                }
                else
                {
                    result += "\t";
                }
            }
            if (resultBS.Deffence != 0)
            {
                result += "+방어력 : " + resultBS.Deffence;
                ++count;
                if (count % 2 == 0)
                {
                    result += "\n";
                    count = 0;
                }
                else
                {
                    result += "\t";
                }
            }
            if (resultBS.DeffencePentraction != 0)
            {
                result += "+방어관통력 : " + resultBS.DeffencePentraction;
                ++count;
                if (count % 2 == 0)
                {
                    result += "\n";
                    count = 0;
                }
                else
                {
                    result += "\t";
                }
            }
            if (resultBS.Evasive != 0)
            {
                result += "+회피율 : " + resultBS.Evasive;
                ++count;
                if (count % 2 == 0)
                {
                    result += "\n";
                    count = 0;
                }
                else
                {
                    result += "\t";
                }
            }
            if (resultBS.MoveSpeed != 0)
            {
                result += "+이동속도 : " + resultBS.MoveSpeed;
                ++count;
                if (count % 2 == 0)
                {
                    result += "\n";
                    count = 0;
                }
                else
                {
                    result += "\t";
                }
            }





            return result;
        }

        public void ButtonClicked(UI_Container _container, int _index, object _data = null)
        {
            _container.CallContents(_index, _data);
        }

        /// <summary>
        /// 슬롯 선택시 기존 슬롯의 selectedImage를 끄고 현재 슬롯의 selectedImage를 켠다
        /// </summary>
        private void SelectSlot(int _curSlot)
        {
            if(prevSlotIndex != -1 && prevSlotIndex !=_curSlot)
            {
                slotList[prevSlotIndex].selectedImage.SetActive(false);
            }
            slotList[_curSlot].selectedImage.SetActive(true);
            prevSlotIndex = _curSlot;
        }

        public void ClearSlot()
        {
            for(int i = 0; i < SlotList.Count; ++i)
            {
                Destroy(SlotList[i].gameObject);
            }
            SlotList.Clear();
        }

        public override void OpenSubContainer(OpenSubContainerData _subContainerIndex)
        {

        }

        /// <summary>
        /// item select 콘테이너에서 온 정보를 세팅
        /// </summary>
        /// <param name="_data"></param>
        public override void CloseSubContainer(object _data)
        {

            int[] data = (int[])_data; // 0 : itemSlotIndex, 1: ownedItemIndex;
            int ownedItemIndex = data[1];
            DataArea db = Management.MenuGameManager.Instance.DataArea;

            var OwnerMinion = db.OwnedData.MinionData[prevSlotIndex];
            
            int prevItemIndex = OwnerMinion.IndexOfSerializeCodeOwnedItem[data[0]];
            
            if (ownedItemIndex == -1)
            {
                if (prevItemIndex < 0)
                {
                    return;
                }
                // 아이템을 가지고있는데 -1 번째 아이템이 리턴되면 해당아이템 제거
                OwnerMinion.IndexOfSerializeCodeOwnedItem[data[0]] = ownedItemIndex;
                var item = db.OwnedData.ItemData.Find(x => x.IndexOfSerializeCode == prevItemIndex);
                BasicStatus prevItemBs = db.DefaultItemData[item.IndexOfType].BasicStatus + item.AddStatus;
                OwnerMinion.AddStatus -= prevItemBs;
                item.UseItem = false;
                item.OwnerMinionIndexOfSerializeCode = -1;
            }
            // n번째 아이템을 가져왔는데, 그게 현재 미니언의 아이템이면 리턴
            else
            {
                var newitem = db.OwnedData.ItemData.Find(x => x.IndexOfSerializeCode == ownedItemIndex);
                if (newitem.UseItem == true && OwnerMinion.IndexOfSerializeCode == newitem.OwnerMinionIndexOfSerializeCode)
                    return;
               
                OwnerMinion.IndexOfSerializeCodeOwnedItem[data[0]] = newitem.IndexOfSerializeCode;
                if (prevItemIndex >= 0)
                {
                    var item = db.OwnedData.ItemData.Find(x => x.IndexOfSerializeCode == prevItemIndex);
                    BasicStatus prevItemBs = db.DefaultItemData[db.OwnedData.ItemData[prevItemIndex].IndexOfType].BasicStatus + db.OwnedData.ItemData[prevItemIndex].AddStatus;
                    OwnerMinion.AddStatus -= prevItemBs;
                    item.UseItem = false;
                    item.OwnerMinionIndexOfSerializeCode = -1;
                }
                BasicStatus curItemBs = db.DefaultItemData[newitem.IndexOfType].BasicStatus + newitem.AddStatus;
                newitem.OwnerMinionIndexOfSerializeCode = OwnerMinion.IndexOfSerializeCode;
                newitem.UseItem = true;

                OwnerMinion.AddStatus += curItemBs;
            }
            minionStatusInfo.text = SetStatusMinion(OwnerMinion, OwnerMinion.IndexOfMinion,db);
            var showitem = db.OwnedData.ItemData.Find(x => x.IndexOfSerializeCode == ownedItemIndex);
            switch (data[0])
            {
                case 0:
                    if( ownedItemIndex == -1)
                    {
                        item_0_Image.sprite = null;
                        item_0_Name.text = "-";
                        item_0_Lv.text = "Lv : -";
                        item_0_Info.text = "-";
                    }
                    else
                    {
                        item_0_Image.sprite = Resources.Load<Sprite>(db.DefaultItemData[showitem.IndexOfType].IconPath);
                        item_0_Name.text    = db.DefaultItemData[showitem.IndexOfType].ItemName;
                        item_0_Lv.text      = "Lv : " + showitem.Lv.ToString();
                        item_0_Info.text = db.DefaultItemData[showitem.IndexOfType].Explanation;
                    }
                    break;
                case 1:
                    if (ownedItemIndex == -1)
                    {
                        item_1_Image.sprite = null;
                        item_1_Name.text = "-";
                        item_1_Lv.text = "Lv : -";
                        item_1_Info.text = "-";
                    }
                    else
                    {
                        item_1_Image.sprite = Resources.Load<Sprite>(db.DefaultItemData[showitem.IndexOfType].IconPath);
                        item_1_Name.text    = db.DefaultItemData[showitem.IndexOfType].ItemName;
                        item_1_Lv.text      = "Lv : " + showitem.Lv.ToString();
                        item_1_Info.text = db.DefaultItemData[showitem.IndexOfType].Explanation;
                    }
                    break;
                case 2:
                    if (ownedItemIndex == -1)
                    {
                        item_2_Image.sprite = null;
                        item_2_Name.text = "-";
                        item_2_Lv.text = "Lv : -";
                        item_2_Info.text = "-";
                    }
                    else
                    {
                        item_2_Image.sprite = Resources.Load<Sprite>(db.DefaultItemData[showitem.IndexOfType].IconPath);
                        item_2_Name.text    = db.DefaultItemData[showitem.IndexOfType].ItemName;
                        item_2_Lv.text      = "Lv : " + showitem.Lv.ToString();
                        item_2_Info.text = db.DefaultItemData[showitem.IndexOfType].Explanation;
                    }
                    break;
            }

            //Management.MenuGameManager.Instance.SaveData();
        }

        public override void SetSubContianer(object _data)
        {
        }

        private void OnDisable()
        {
            this.viewType = ViewType.Skill;
        }
    }
}