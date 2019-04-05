using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSetting
{
    public class UI_Button : MonoBehaviour
    {
        public delegate void ClickButton();
        public ClickButton clickedFunc;


        public void Clicked()
        {
            if (clickedFunc != null)
            {
                clickedFunc.Invoke();
            }
        }

    }
}