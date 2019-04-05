using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSetting
{
    public class UI_Container : MonoBehaviour
    {
        // setting module
        [SerializeField]
        UI_SettingModule settingModule;
        // contents list
        [SerializeField]
        List<UI_Contents> uiContents = new List<UI_Contents>();

        [SerializeField]
        UI_Container prevContainer = null;

        bool openOtherContainer = false;

        public List<UI_Contents> UiContents { get => uiContents; set => uiContents = value; }
        public UI_Container PrevContainer { get => prevContainer; set => prevContainer = value; }
        public UI_SettingModule SettingModule { get => settingModule; set => settingModule = value; }
        public bool OpenOtherContainer { get => openOtherContainer; set => openOtherContainer = value; }

        private void OnEnable()
        {
            // call setting module
            settingModule.SetUI(this);
        }

        /// <summary>
        /// 다른 Container가 열려고 할때 시도.
        /// </summary>
        /// <param name="_container"></param>
        public void OpenContainer(UI_Container _container = null)
        {
            OpenOtherContainer = false;
            if (_container) // << 이거 빼면 로그인창으로 안가짐
                PrevContainer = _container;
            gameObject.SetActive(true);
        }


        public void CallContents(int _contentsIndex, object _data = null)
        {
            if( _contentsIndex>= uiContents.Count)
            {
                Debug.LogError("wrong access");
                return;
            }
            // call contents by index
            UiContents[_contentsIndex].ActionModule.ToAction(this, UiContents[_contentsIndex].TargetContainer, _data);
        }

    }
}