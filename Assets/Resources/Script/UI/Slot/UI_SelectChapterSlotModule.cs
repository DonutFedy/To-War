using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSetting
{
    public class UI_SelectChapterSlotModule : MonoBehaviour
    {
        public int indexOfSlot;
        public Text slotText;
        public Button thisButton;

        public delegate void ClickButton();

        public ClickButton clickFunc;

        public void SetSlot(int _index, bool _enable)
        {
            indexOfSlot = _index;
            slotText.text = "Ch - " + indexOfSlot;
            if(_enable)
            {
                thisButton.interactable = true;
            }
            else
            {
                thisButton.interactable = false;
            }
        }


        public void Clicked()
        {
            if (clickFunc != null)
                clickFunc.Invoke();
        }
    }
}