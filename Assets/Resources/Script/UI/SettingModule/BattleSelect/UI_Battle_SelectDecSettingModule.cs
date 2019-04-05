using GameDataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSetting
{
    public class UI_Battle_SelectDecSettingModule : UI_SubContainerSettingModule
    {
        [SerializeField]
        int indexOfCurStage = -1;
        [SerializeField]
        int indexOfCurChapter = -1;
        [SerializeField]
        int indexOfCurDex = -1;
        [SerializeField]
        bool bSelected = false;

        [Header("Background")]
        public Image backgroundImage;
        public Sprite backgroundSprite;

        [Header("Dec Slot")]
        public Image scrollBackgroundImage;
        public Sprite scrollBackgroundSprite;
        public GameObject slotPrefab;
        public GameObject slotParent;
        public List<UI_DecSlotModule> decList = new List<UI_DecSlotModule>();

        [Header("Close Button")]
        public UI_Button closeButton;
        public int indexOfCloseModule;

        [Header("Start Battle Button")]
        public UI_Button startBattleButton;
        public int indexOfStartBattleButton;

        public override void SetSubContianer(object _data)
        {
            indexOfCurChapter = ((int[])_data)[0];
            indexOfCurStage = ((int[])_data)[1];
            indexOfCurDex = -1;
        }

        public override void SetUI(UI_Container _container)
        {
            ClearSlot();

            DataArea db = Management.MenuGameManager.Instance.DataArea;

            // set background image
            backgroundImage.sprite = backgroundSprite;

            // set scroll background image
            scrollBackgroundImage.sprite = scrollBackgroundSprite;

            // set dec slot
            int curDecIndex = 0;
            int decIndex = db.LoginData.Free_DecSlotCount + db.LoginData.Event_DecSlotCount + db.LoginData.Pay_DecSlotCount;

            for (int i = 0; i < decIndex; ++i)
            {
                if (db.OwnedData.DecData.Count <= i)
                    break;
                UI_DecSlotModule newDecSlot = Instantiate(slotPrefab, slotParent.transform).GetComponent<UI_DecSlotModule>();

                newDecSlot.indexOfSlot = i;

                decList.Add(newDecSlot);
                
                Sprite image = null;
                string decName = "덱을 세팅하세요";
                // slot Setting
                // 세팅된 덱의 데이터 가 있다면 세팅
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

                newDecSlot.decNameText.text = decName;
                newDecSlot.slotImage.sprite = image;

                // trigger set
                if (newDecSlot.clickFunc == null)
                {
                    newDecSlot.clickFunc = () => { SelectDecSlot(newDecSlot.indexOfSlot); };
                }
            }

            // set buttons function
            if (closeButton.clickedFunc == null)
                closeButton.clickedFunc = () => { ClickedButton(_container, indexOfCloseModule); };
            if (startBattleButton.clickedFunc == null)
            {
                startBattleButton.clickedFunc = () => { ClickedButton(_container, indexOfStartBattleButton); };
                Debug.LogError("게임시작할때 보낼 데이터를 세팅해줘!!!!!!!!!!!");
            }
        }

        #region private Func
        private void ClickedButton(UI_Container _container, int _moduleIndex, object _data = null)
        {
            _container.CallContents(_moduleIndex, _data);
        }

        private void SelectDecSlot(int _decIndex)
        {
            if( indexOfCurDex == _decIndex)
            {
                decList[indexOfCurDex].selectedImage.SetActive(false);
                indexOfCurDex = -1;
                bSelected = false;
            }
            else
            {
                if (indexOfCurDex >= 0)
                    decList[indexOfCurDex].selectedImage.SetActive(false);
                indexOfCurDex = _decIndex;
                decList[indexOfCurDex].selectedImage.SetActive(true);
                bSelected = true;
            }
        }

        private void ClearSlot()
        {
            while (decList.Count > 0)
            {
                DestroyImmediate(decList[0].gameObject);
                decList.RemoveAt(0);
            }
            decList.Clear();

            bSelected = false;
            indexOfCurDex = -1;
        }

        #endregion 
    }
}
