using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class CloseDoorCount : MonoBehaviour
    {
        static private CloseDoorCount m_Instance = null;

        static public void StageCloseDoorCountUpdate()
        {
            if (m_Instance == null) return;
            if (m_Instance.m_uiCloseDoorCount == null) return;
            
            --m_Instance.m_iDoorCount;

            if (m_Instance.m_iDoorCount <= 0)
                m_Instance.m_iDoorCount = 0;

            m_Instance.m_uiCloseDoorCount.text = m_Instance.m_iDoorCount.ToString();
        }

        static public void StageCloseDoorCountReset()
        {
            if (m_Instance == null) return;
            if (m_Instance.m_uiCloseDoorCount == null) return;
            if (GameDataMgr.Get() != null)
                m_Instance.m_iDoorCount = GameDataMgr.Get().GetTouchCount();

            m_Instance.m_uiCloseDoorCount.text = m_Instance.m_iDoorCount.ToString();
        }

        private UILabel m_uiCloseDoorCount = null;
        private int     m_iDoorCount       = 0;

        private void Start()
        {
            m_Instance = this;
            m_uiCloseDoorCount = gameObject.GetComponentInChildren<UILabel>();
            if (GameDataMgr.Get() != null)
                m_iDoorCount = GameDataMgr.Get().GetTouchCount();
        }
    }
}
