using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUnit
{
    public abstract class Unit_Module : MonoBehaviour
    {
        public Unit_StatusModule status_M;
        public UnitController onwer;
        public abstract void SetModule(object _data = null);
    }
}