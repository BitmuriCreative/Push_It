using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class PopupRoot : MonoBehaviour
    {
        static PopupRoot m_PopupRoot = null;

        private void Awake()
        {
            m_PopupRoot = this;
        }

        static public void Add(Popup _popup)
        {
            if (m_PopupRoot == null)
            {
                m_PopupRoot = GameObject.FindObjectOfType<PopupRoot>();

                if (m_PopupRoot == null)
                {
                    m_PopupRoot = Resources.Load<PopupRoot>("UI Root Popup");
                }
            }

            for(int i=0; i< m_PopupRoot.transform.childCount; ++i)
            {
                if(m_PopupRoot.transform.GetChild(i) == null) continue;
                if (_popup == m_PopupRoot.transform.GetChild(i)) return;
            }

            _popup.transform.parent = m_PopupRoot.transform;
            _popup.transform.Reset();
        }
    }
}