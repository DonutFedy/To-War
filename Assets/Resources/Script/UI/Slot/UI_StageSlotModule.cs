using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSetting
{
    public class UI_StageSlotModule : MonoBehaviour
    {
        public Button thisButton;
        public RectTransform thisRectTransform;
        public delegate void ClickedButton();
        public ClickedButton clickedFunc;

        public Text stageNameText;
        public int indexOfSlot;
        public Image closeImage;

        public void SetSlot(int _indexOfSlot, string _name,bool _enable)
        {
            indexOfSlot = _indexOfSlot;
            stageNameText.text = _name;
            if (_enable)
            {
                closeImage.gameObject.SetActive(false);
                thisButton.interactable = true;
            }
            else
            {
                closeImage.gameObject.SetActive(true);
                thisButton.interactable = false;
            }
        }

        public void Clicked()
        {
            if (clickedFunc != null)
                clickedFunc.Invoke();
        }


    }
}