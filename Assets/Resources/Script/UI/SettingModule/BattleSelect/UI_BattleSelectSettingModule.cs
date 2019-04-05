using System.Collections;
using System.Collections.Generic;
using GameDataBase;
using UnityEngine;
using UnityEngine.UI;

namespace GameSetting
{
    public class UI_BattleSelectSettingModule : UI_SettingModuleHaveSubContainerModule
    {
        [Header("System")]
        BattleSelectMode curMode;

        [Header("Select Mode Button")]
        public int indexOfOpenStoryMode;
        public UI_Button storyModeButton;
        public int indexOfOpenMultiMode;
        public UI_Button multiModeButton;


        [Header("Close Button")]
        public int indexOfCloseModule;
        public UI_Button closeButton;

        [Header("Title")]
        public Text titleText;

        public override void SetUI(UI_Container _container)
        {
            if(storyModeButton.clickedFunc == null)
            {
                storyModeButton.clickedFunc = () => { ChangeMode(BattleSelectMode.StoryMode); ClickedButton(_container, indexOfOpenStoryMode); };
            }

            if (multiModeButton.clickedFunc == null)
            {
                multiModeButton.clickedFunc = () => { ChangeMode(BattleSelectMode.MultiMode); ClickedButton(_container, indexOfOpenMultiMode); };
            }

            if (closeButton.clickedFunc == null)
            {
                closeButton.clickedFunc = () => { ClickedButton(_container, indexOfCloseModule); };
            }

            storyModeButton.clickedFunc.Invoke();
        }

        public override void CloseSubContainer(object _data)
        {
        }

        public override void OpenSubContainer(OpenSubContainerData _subContainerIndex)
        {
        }

        public override void SetSubContianer(object _data)
        {
        }

        public void ClickedButton(UI_Container _container, int _moduleIndex, object _data = null)
        {
            _container.CallContents(_moduleIndex, _data);
        }


        #region private Func

        private void ChangeMode(BattleSelectMode _mode)
        {
            curMode = _mode;
            if (curMode == BattleSelectMode.StoryMode)
            {
                titleText.text = "스토리 대전";
            }
            else if (curMode == BattleSelectMode.MultiMode)
            {
                titleText.text = "유저 대전";
            }
        }


        #endregion

    }
}