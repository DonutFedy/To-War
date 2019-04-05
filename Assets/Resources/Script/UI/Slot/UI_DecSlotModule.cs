using GameDataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameSetting
{
    public class UI_DecSlotModule : MonoBehaviour
    {
        [Header("Slot Info")]
        /// <summary>
        /// owned Index
        /// </summary>
        public int indexOfType;
        /// <summary>
        /// slot Index
        /// </summary>
        public int indexOfSlot;

        [Header("Image and Text")]
        public Image slotImage;
        public GameObject selectedImage;
        public Text decNameText;
        public Text decCountText;

        public delegate void OnClicked();
        public OnClicked clickFunc;

        public void ClickedButton()
        {
            if (clickFunc != null)
            {
                clickFunc.Invoke();
            }
        }

        public UI_DecSlotModule(int indexOfType, int indexOfSlot, Sprite image, string decName, int decCount, bool selected = false)
        {
            this.indexOfType = indexOfType;
            this.indexOfSlot = indexOfSlot;
            slotImage.sprite = image;
            decNameText.text = decName;
            decCountText.text = decCount + "/" + DecMax.UnitMax;
        }
    }
}