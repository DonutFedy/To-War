using GameDataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameSetting
{
    public class UI_StrengthenModule : MonoBehaviour
    {
        [SerializeField]
        int indexOfViewType;
        [SerializeField]
        ViewType viewType;
        [SerializeField]
        int indexOfSlot;
        [SerializeField]
        Image slotImage;


        public delegate void OnClicked();
        public OnClicked clickFunc;

        public void ClickedButton()
        {
            if (clickFunc != null)
            {
                clickFunc.Invoke();
            }
        }


        public int IndexOfViewType { get => indexOfViewType; set => indexOfViewType = value; }
        public ViewType ViewType { get => viewType; set => viewType = value; }
        public int IndexOfSlot { get => indexOfSlot; set => indexOfSlot = value; }
        public Image SlotImage { get => slotImage; set => slotImage = value; }
    }
}