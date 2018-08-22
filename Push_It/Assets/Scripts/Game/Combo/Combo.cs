using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class Combo : ComboBase
    {
        private UILabel m_Number = null;

        public void SetUp()
        {
            m_Number = gameObject.GetComponentInChildren<UILabel>();
        }

        public void ComboNumber(int _iValue)
        {
            if (m_Number != null) { m_Number.text = _iValue.ToString(); }
        }
    }
}
