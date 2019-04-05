using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUnit
{
    public class TestNode : MonoBehaviour
    {
        public TestMakeUnit onwer;
        public int curNodeIndex;

        private void OnMouseDown()
        {
            onwer.OnMouseDownTest(this.transform.position,curNodeIndex);
        }
    }
}