using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class BitmuriArea : MonoBehaviour
    {
        static private BitmuriArea m_BitmuriArea = null;

        public Transform m_transStart = null;
        public Transform m_transEnd = null;

        static public void Attach(Transform _transChild)
        {
            _transChild.parent = m_BitmuriArea.transform;
            _transChild.Reset();
        }

        static public Transform GetTransStart { get { return m_BitmuriArea != null ? m_BitmuriArea.m_transStart : null; } }
        static public Transform GetTransEnd { get { return m_BitmuriArea != null ? m_BitmuriArea.m_transEnd : null; } }

        static public void ChildDestroy()
        {
            while (m_BitmuriArea.transform.childCount > 0)
            {
                Transform temp = m_BitmuriArea.transform.GetChild(0);
                temp.parent = null;
                Destroy(temp.gameObject);
            }
        }

        private void Awake()
        {
            m_BitmuriArea = this;
        }
    }
}
