using System.Collections;
using System.Collections.Generic;
using GameDataBase;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameSetting
{
    public class UI_SelectDecSettingModule : UI_SettingModuleHaveSubContainerModule
    {
        [Header("Background")]
        public Image background;
        public Sprite backgroundSprite;

        [Header("CloseButton")]
        public Button closeButton;
        public int closeModuleIndex;

        [Header("Slot")]
        public GameObject slotParent;
        public GameObject slotPrefab;
        public List<UI_DecSlotModule> decList;
        public int openSubContainerModuleIndex;

        UI_Container ownerContainer;


        public override void SetUI(UI_Container _container)
        {
            ownerContainer = _container;
            closeButton.onClick.AddListener(()=> { ClickedButton(_container, closeModuleIndex); });

            SetDec();
        }

        public void SetDec()
        {
            ClearSlot();

            // dec Setting
            int maxDec = (int)DecMax.OwnedMax;
            DataArea db = Management.MenuGameManager.Instance.DataArea;
            int decIndex = db.LoginData.Free_DecSlotCount + db.LoginData.Event_DecSlotCount + db.LoginData.Pay_DecSlotCount;
            for (int i = 0; i < maxDec; ++i)
            {
                UI_DecSlotModule newDecSlot = Instantiate(slotPrefab, slotParent.transform).GetComponent<UI_DecSlotModule>();

                newDecSlot.indexOfSlot = i;

                decList.Add(newDecSlot);

                // 소유 가능한 덱보다 작으면 세팅
                if (i >= decIndex)
                    continue;

                Sprite image = null;
                string decName = "덱을 세팅하세요";
                // slot Setting
                // 세팅된 덱의 데이터 가 있다면 세팅
                if (db.OwnedData.DecData.Count > i)
                {
                    newDecSlot.indexOfType = i;
                    int unitIndex = db.OwnedData.DecData[i].decList[0].ownedIndex;
                    DecType type = db.OwnedData.DecData[i].decList[0].type;
                    string path = "";
                    if (type == DecType.Minion)
                    {
                        path = db.DefaultMinionData[db.OwnedData.MinionData[unitIndex].IndexOfMinion].MainImagePath;
                    }
                    else
                    {
                        path = db.DefaultSkillData[db.OwnedData.SkillData[unitIndex].IndexOfType].MainImagePath;
                    }
                    image = Resources.Load<Sprite>(path);
                    decName = db.OwnedData.DecData[i].decName;
                    newDecSlot.decCountText.text = db.OwnedData.DecData[i].decCount + "/" + (int)DecMax.UnitMax;
                }

                newDecSlot.decNameText.text = decName;
                newDecSlot.slotImage.sprite = image;

                // trigger set
                if(newDecSlot.clickFunc == null)
                {
                    newDecSlot.clickFunc = () => { ClickedButton(ownerContainer, openSubContainerModuleIndex, new OpenSubContainerData(openSubContainerModuleIndex, newDecSlot.indexOfSlot)); };
                }
            }
        }


        public override void CloseSubContainer(object _data)
        {
            Debug.LogError("DecSet!!");
            SetDec();
        }

        public override void OpenSubContainer(OpenSubContainerData _subContainerIndex)
        {
        }

        public override void SetSubContianer(object _data)
        {
        }

        public void ClickedButton(UI_Container _contianer, int _moduleIndex, object _data = null)
        {
            Debug.LogError("Click Button!");
            _contianer.CallContents(_moduleIndex, _data);
        }

        private void ClearSlot()
        {
            while (decList.Count>0)
            {
                DestroyImmediate(decList[0].gameObject);
                decList.RemoveAt(0);
            }
        }
    }
}