using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameUnit
{
    public class Unit_ImageModule : Unit_Module
    {
        public SpriteRenderer unitImage;
        public Sprite curUnitSprite;

        public override void SetModule(object _data = null)
        {
            status_M = (Unit_StatusModule)_data;
            curUnitSprite = status_M.curSprite;//나중에 배열로 받아서 실시간으로 움직이게 ㅇㅇ
            unitImage.sprite = curUnitSprite;
        }
    }
}