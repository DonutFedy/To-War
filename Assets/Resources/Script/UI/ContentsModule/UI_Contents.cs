using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSetting
{
    public class UI_Contents : MonoBehaviour
    {
        // ui action moudle
        [SerializeField]
        UI_ActionModule actionModule;
        // ui container
        [SerializeField]
        UI_Container targetContainer;

        public UI_ActionModule ActionModule { get => actionModule; set => actionModule = value; }
        public UI_Container TargetContainer { get => targetContainer; set => targetContainer = value; }
    }
}