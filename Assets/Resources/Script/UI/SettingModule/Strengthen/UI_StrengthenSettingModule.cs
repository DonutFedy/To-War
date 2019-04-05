using GameDataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSetting
{
    public class UI_StrengthenSettingModule : UI_SettingModuleHaveSubContainerModule
    {
        [Header("Image")]
        public Image backgroundImage;
        public Sprite backgroundSprite;

        [Header("Buttons")]
        public Button minionTypeButton;
        public Button itemTypeButton;
        public Button skillTypeButton;

        [Header("ExitButton")]
        public Button exitButton;
        public int closeModuleIndex;

        [Header("Title")]
        public Text titleText;

        [Header("OpenSubContainer")]
        public int openSubContainer_type_Minion_ModuleIndex;
        public GameObject minionSubContainer;
        public int openSubContainer_type_Item_ModuleIndex;
        public GameObject itemSubContainer;
        public int openSubContainer_type_Skill_ModuleIndex;
        public GameObject skillSubContainer;

        public override void SetUI(UI_Container _container)
        {
            // background
            backgroundImage.sprite = backgroundSprite;

            // button
            exitButton.onClick.AddListener(()=> { OnButtonClicked(_container, closeModuleIndex); });

            minionTypeButton.onClick.AddListener(()=> { OnButtonClicked(_container, openSubContainer_type_Minion_ModuleIndex, new OpenSubContainerData(openSubContainer_type_Minion_ModuleIndex, null)); });
            itemTypeButton.onClick.AddListener(()=> { OnButtonClicked(_container, openSubContainer_type_Item_ModuleIndex, new OpenSubContainerData(openSubContainer_type_Item_ModuleIndex, null)); });
            skillTypeButton.onClick.AddListener(()=> { OnButtonClicked(_container, openSubContainer_type_Skill_ModuleIndex,new OpenSubContainerData( openSubContainer_type_Skill_ModuleIndex,null)); });

            // first subcontainer
            OnButtonClicked(_container,openSubContainer_type_Minion_ModuleIndex, new OpenSubContainerData(openSubContainer_type_Minion_ModuleIndex, null));
        }

        public override void OpenSubContainer(OpenSubContainerData _subContainerIndex)
        {
            if( _subContainerIndex.SubContainerIndex == openSubContainer_type_Minion_ModuleIndex)
            {
                titleText.text = "미니언 강화";
                itemSubContainer.SetActive(false);
                skillSubContainer.SetActive(false);
            }
            else if (_subContainerIndex.SubContainerIndex == openSubContainer_type_Item_ModuleIndex)
            {
                titleText.text = "아이템 강화";
                minionSubContainer.SetActive(false);
                skillSubContainer.SetActive(false);
            }
            else if (_subContainerIndex.SubContainerIndex == openSubContainer_type_Skill_ModuleIndex)
            {
                titleText.text = "스킬 강화";
                minionSubContainer.SetActive(false);
                itemSubContainer.SetActive(false);
            }
            curSubContainer = _subContainerIndex.SubContainerIndex;
        }

        private void OnButtonClicked(UI_Container _container,int _moduleIndex, object _data = null)
        {
            _container.CallContents(_moduleIndex, _data);
        }

        public override void CloseSubContainer(object _data)
        {
        }

        public override void SetSubContianer(object _data)
        {
        }
    }
}