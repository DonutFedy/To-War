using GameDataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSetting
{
    public class UI_StoryModeSettingModule : UI_SettingModuleHaveSubContainerModule
    {
        [Header("System")]
        UI_Container ownerContainer;

        [Header("Background")]
        public Image mainBackgroundImage;
        public Sprite mainBackgroundSprite;

        [Header("Chapter Slot")]
        int chapterRectT_X_DefaultSize = 2090;
        public RectTransform chapterContainViewerRectT;
        public Image chapterSlotBackgroundImage;
        public Sprite chapterSlotBackgroundSprite;
        public GameObject chapterSlotPrefab;
        public GameObject chapterSlotParent;
        public List<UI_SelectChapterSlotModule> chapterSlotList = new List<UI_SelectChapterSlotModule>();

        [Header("Stage Slot")]
        public int indexOfOpenSelectDecModule;
        int curChapterIndex;
        int stageRectT_Y_DefaultSize = 1040;
        public RectTransform stageContainViewerRectT;
        public Image stageSlotBackgroundImage;
        public Sprite stageSlotBackgroundSprite;
        public GameObject stageSlotPrefab;
        public GameObject stageSlotParent;
        public List<UI_StageSlotModule> stageSlotList = new List<UI_StageSlotModule>();

        public Vector2 fristSlotPos;
        public Vector2 evenNumberSlotOffset;
        public Vector2 oddNumberSlotOffset;


        public override void SetSubContianer(object _data)
        {
        }

        public override void SetUI(UI_Container _container)
        {
            ClearSlot();

            ownerContainer = _container;

            DataArea db = Management.MenuGameManager.Instance.DataArea;
            int clearChapter = db.LoginData.ClearChapter;

            // main backgroundImage set
            mainBackgroundImage.sprite = mainBackgroundSprite;

            // chapter slot background set
            chapterSlotBackgroundImage.sprite = chapterSlotBackgroundSprite;
            // chapter slot set
            for (int i = 0; i < db.DefaultChapterData.Length; ++i)
            {
                UI_SelectChapterSlotModule slot = Instantiate(chapterSlotPrefab,chapterSlotParent.transform).GetComponent<UI_SelectChapterSlotModule>();
                slot.SetSlot(i, clearChapter>= i? true : false);
                slot.clickFunc = () => { ClickedChapterSlot(slot.indexOfSlot); };
                chapterSlotList.Add(slot);
            }

            int chapterSize_X = db.DefaultChapterData.Length * 420+40;
            if (chapterSize_X < chapterRectT_X_DefaultSize)
                chapterSize_X = chapterRectT_X_DefaultSize;
            Vector2 size = chapterContainViewerRectT.anchoredPosition+ Vector2.right*chapterSize_X;
            chapterContainViewerRectT.anchoredPosition = size;


            // stage slot background set
            stageSlotBackgroundImage.sprite = stageSlotBackgroundSprite;
            // stage slot set
            SetStage(clearChapter);







        }

        #region private Func

        private void ClearSlot()
        {
            while(chapterSlotList.Count > 0)
            {
                DestroyImmediate(chapterSlotList[0].gameObject);
                chapterSlotList.RemoveAt(0);
            }
            chapterSlotList.Clear();

            while (stageSlotList.Count > 0)
            {
                DestroyImmediate(stageSlotList[0].gameObject);
                stageSlotList.RemoveAt(0);
            }
            stageSlotList.Clear();
            
        }

        private void SetStage(int _indexOfChapter)
        {
            DataArea db = Management.MenuGameManager.Instance.DataArea;
            curChapterIndex = _indexOfChapter;
            int clearStage = db.LoginData.ClearStage;

            Vector2 curPos = Vector2.zero;
            for (int i = 0; i < db.DefaultChapterData[curChapterIndex].stage.Count; ++i)
            {
                if (i == 0)
                    curPos += fristSlotPos;
                else if (i % 2 == 0)
                    curPos += evenNumberSlotOffset;
                else
                    curPos += oddNumberSlotOffset;

                UI_StageSlotModule slot = Instantiate(stageSlotPrefab, stageSlotParent.transform).GetComponent<UI_StageSlotModule>();
                slot.SetSlot(i, db.DefaultChapterData[curChapterIndex].stage[i] ,clearStage>= i ? true : false);
                slot.clickedFunc = () => { ClickedButton(ownerContainer, indexOfOpenSelectDecModule,new OpenSubContainerData( 0,new int[] { curChapterIndex, slot.indexOfSlot })); };
                slot.thisRectTransform.anchoredPosition = curPos;
                stageSlotList.Add(slot);
            }

            int Size_Y = db.DefaultChapterData[curChapterIndex].stage.Count * 400 + 300;
            if (Size_Y < stageRectT_Y_DefaultSize)
                Size_Y = stageRectT_Y_DefaultSize;
            Vector2 size = stageContainViewerRectT.anchoredPosition + Vector2.up * Size_Y;
            stageContainViewerRectT.anchoredPosition = size;
        }

        private void ClickedChapterSlot(int _slotIndex)
        {
            SetStage(_slotIndex);
        }
        
        private void ClickedButton(UI_Container _container, int _moduleIndex, object _data = null)
        {
            _container.CallContents(_moduleIndex, _data);
        }

        public override void OpenSubContainer(OpenSubContainerData _subContainerIndex)
        {
        }

        public override void CloseSubContainer(object _data)
        {
        }

        #endregion
    }

}