using GameDataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameSetting
{
    public class UI_LogInViewSettingModule : UI_SettingModule
    {
        [SerializeField]
        Image background;
        [SerializeField]
        Sprite backgroundImage;
        [SerializeField]
        EventTrigger loginButton;


        public Text ts;

        public override void SetUI(UI_Container _container)
        {
            string basicPath = string.Empty;
#if UNITY_STANDALONE_WIN||UNITY_EDITOR
            basicPath = Application.streamingAssetsPath;
#else
            basicPath = "jar:file://" + Application.dataPath + "!/assets";
#endif
            if(ts)  
                ts.text = basicPath+DataPath.defaultPath;
            background.sprite = backgroundImage;

            if (loginButton.triggers.Count <= 0)
            {
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerUp;
                entry.callback.AddListener((eventData) => { _container.CallContents(0); });
                loginButton.triggers.Add(entry);
            }

        }


    }
}