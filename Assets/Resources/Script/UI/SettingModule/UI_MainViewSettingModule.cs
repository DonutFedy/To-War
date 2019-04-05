using GameDataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSetting
{
    public class UI_MainViewSettingModule : UI_SettingModule
    {
        #region contain Object
        [SerializeField]
        Sprite backgroundImage;//배경이미지
        [SerializeField]
        Image backgroundImageObject;//배경이미지 적용대상
        
        public Sprite BackgroundImage { get => backgroundImage; set => backgroundImage = value; }
        public Image BackgroundImageObject { get => backgroundImageObject; set => backgroundImageObject = value; }
        public UI_Button OpenButton { get => openButton; set => openButton = value; }
        public int OpenButtonContentsIndex { get => openButtonContentsIndex; set => openButtonContentsIndex = value; }
        public UI_Button ExitButton { get => exitButton; set => exitButton = value; }
        public int CloseButtonContentsIndex { get => closeButtonContentsIndex; set => closeButtonContentsIndex = value; }


        // Inpomation
        // 유저 레벨및 경험치 관련
        [SerializeField]
        Text idText;
        [SerializeField]
        Text pwText;
        [SerializeField]
        Text lvText;
        [SerializeField]
        Text expText;
        [SerializeField]
        Text minionText;
        [SerializeField]
        Text itemText;


        // 유저 자원? 및 가챠 포인트
        // 이벤트 바

        //Test
        [SerializeField]
        UI_Button openButton = null;
        [SerializeField]
        int openButtonContentsIndex;
        [SerializeField]
        UI_Button exitButton = null;
        [SerializeField]
        int closeButtonContentsIndex;

        [Header("Strengthen")]
        public UI_Button StrengthenButton;
        public int openStrengthenModuleIndex;

        [Header("SelectDec")]
        public UI_Button SelectDecButton;
        public int openSelectDecModuleIndex;

        [Header("BattleSelect")]
        public UI_Button BattleSelectButton;
        public int indexOfOpenBattleSelectModule;


        // button
        // 전투 버튼
        // 상점 버튼
        // 덱 세팅 버튼
        #endregion

        public override void SetUI(UI_Container _container)
        {
            Debug.LogError("MianView set....add script");

            // background set
            backgroundImageObject.sprite = backgroundImage;

            // button set
            if (openButton.clickedFunc == null)
            {
                openButton.clickedFunc = () => { ButtonClicked(_container, OpenButtonContentsIndex); };
            }
            if (ExitButton.clickedFunc == null)
            {
                ExitButton.clickedFunc = () => { ButtonClicked(_container, CloseButtonContentsIndex); };
            }
            if (StrengthenButton.clickedFunc == null)
            {
                StrengthenButton.clickedFunc = () => { ButtonClicked(_container, openStrengthenModuleIndex); };
            }
            if (SelectDecButton.clickedFunc == null)
            {
                SelectDecButton.clickedFunc = () => { ButtonClicked(_container, openSelectDecModuleIndex); };
            }
            if (BattleSelectButton.clickedFunc == null)
            {
                BattleSelectButton.clickedFunc = () => { ButtonClicked(_container, indexOfOpenBattleSelectModule); };
            }


            // info area set
            DataArea db =  Management.MenuGameManager.Instance.DataArea;
            idText.text = db.LoginData.Id;
            pwText.text = db.LoginData.Pw;
            lvText.text = db.LoginData.Lv.ToString();
            expText.text = db.LoginData.CurExp+"/"+db.LoginData.NeedLvExp;
            minionText.text = db.OwnedData.MinionData.Count+"/"+(db.DefaultUserData.BasicMinionCount + db.LoginData.Lv/db.DefaultUserData.AddMinionCountIntervalLv*db.DefaultUserData.AddMinionCount);
            itemText.text = db.OwnedData.ItemData.Count+"/"+ (db.DefaultUserData.BasicItemCount + db.LoginData.Lv / db.DefaultUserData.AddItemCountIntervalLv* db.DefaultUserData.AddItemCount);


        }

        public void ButtonClicked(UI_Container _container, int _index)
        {
            _container.CallContents(_index);
        }
    }
}