using GameDataBase;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameSetting
{
    public class UI_SelectItemSettingModule : UI_SubContainerSettingModule
    {
        [Header("Slot")]
        public GameObject slotPrefab;
        public GameObject slotParent;
        public List<UI_SlotModule> slotList = new List<UI_SlotModule>();
        /// <summary>
        /// 현재 미니언의 아이템 슬롯 index
        /// </summary>
        int curItemSlotIndex;

        public int closeSubContainerModuleIndex;
        int curSelectSlot; // 현재 골라진 슬롯
        int curOwnerMinionIndex;

        [Header("Buttons")]
        public UI_Button closeButton;

        public override void SetUI(UI_Container _container)
        {
            curSelectSlot = -1;

            DataArea db = Management.MenuGameManager.Instance.DataArea;
            int ownedItemCount = db.OwnedData.ItemData.Count;
            int curSlotIndex = 0;
            for(int i = 0; i < ownedItemCount; ++i)
            {
                if (db.OwnedData.ItemData[i].UseItem && db.OwnedData.ItemData[i].OwnerMinionIndexOfSerializeCode !=  db.OwnedData.MinionData[curOwnerMinionIndex].IndexOfSerializeCode ) continue;
                

                var slot = Instantiate(slotPrefab,slotParent.transform).GetComponent<UI_SlotModule>();

                slot.IndexOfSlot = curSlotIndex;
                slot.IndexOfViewType = i;
                slot.nameText.text = db.DefaultItemData[db.OwnedData.ItemData[i].IndexOfType].ItemName;
                slot.lvText.text = db.OwnedData.ItemData[i].Lv.ToString();
                slot.SlotImage.sprite = Resources.Load<Sprite>( db.DefaultItemData[db.OwnedData.ItemData[i].IndexOfType].IconPath);


                if (slot.clickFunc == null)
                    slot.clickFunc = () => { SelectSlot(slot.IndexOfSlot); };

                //EventTrigger.Entry entry = new EventTrigger.Entry();
                //entry.eventID = EventTriggerType.PointerDown;
                //entry.callback.AddListener((eventData)=> { SelectSlot(slot.IndexOfSlot); });
                //slot.EventTrigger.triggers.Add(entry);

                slotList.Add(slot);
                if(db.OwnedData.ItemData[i].UseItem && db.OwnedData.ItemData[i].OwnerMinionIndexOfSerializeCode == db.OwnedData.MinionData[curOwnerMinionIndex].IndexOfSerializeCode)
                {
                    SelectSlot(curSlotIndex);
                }
                ++curSlotIndex;
            }

            if( closeButton.clickedFunc == null)
                closeButton.clickedFunc = ()=> { CloseButton(_container); };
        }

        void ClearList()
        {
            while (slotList.Count > 0)
            {
                UI_SlotModule slot = slotList[0];
                DestroyImmediate(slot.gameObject);
                slotList.RemoveAt(0);
            }
            slotList.Clear();
        }

        private void OnDisable()
        {
            ClearList();
        }

        public void SelectSlot(int _slotIndex)
        {
            if(curSelectSlot == _slotIndex)
            {
                slotList[curSelectSlot].selectedImage.SetActive(false);
                curSelectSlot = -1;
                return;
            }
            curSelectSlot = _slotIndex;
            for(int i = 0; i< slotList.Count; ++i)
            {
                if (i == curSelectSlot) continue;
                slotList[i].selectedImage.SetActive(false);
            }
            slotList[curSelectSlot].selectedImage.SetActive(true);
        }

        public void CloseButton(UI_Container _container)
        {
            DataArea db = Management.MenuGameManager.Instance.DataArea;
            try
            {
                int resultCurSelectSlot = curSelectSlot >= 0 ? db.OwnedData.ItemData[slotList[curSelectSlot].IndexOfViewType].IndexOfSerializeCode : -1;
                ClickedButton(_container, closeSubContainerModuleIndex, new int[] { curItemSlotIndex, resultCurSelectSlot });
            }
            catch (Exception ex)
            {
                Management.MenuGameManager.Instance.errorText.text = ex.Message;
            }
        }

        public void ClickedButton(UI_Container _container, int _moduleIndex, object _data = null)
        {
            _container.CallContents(_moduleIndex, _data);
        }

        public override void SetSubContianer(object _data)
        {
            int[] data = (int[])_data;
            curItemSlotIndex = data[0];
            curOwnerMinionIndex = data[1];
        }
    }
}