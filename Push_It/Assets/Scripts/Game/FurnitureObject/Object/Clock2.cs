using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class Clock2 : PlayFurniture
    {
        protected override void Awake()
        {
            base.Awake();
            if (m_Collider == null)
                m_Collider = gameObject.GetComponentInChildren<Collider>();
        }
    }
}
