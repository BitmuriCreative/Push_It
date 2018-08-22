using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class ComboArea : MonoBehaviour
    {
        static private readonly string COMBO_PATH = "Prefabs/Combo/Combo";
        static private readonly string SUPERCOMBO_PATH = "Prefabs/Combo/SuperCombo";

        static private ComboArea m_ComboArea = null;
        
        static public void ComboActive(int _iValue)
        {
            if (m_ComboArea == null) return;
            for (int iLoop = 0; iLoop < 5; ++iLoop)
            {
                if (m_ComboArea.m_ComboTexts[iLoop] == null) continue;
                if (m_ComboArea.m_ComboTexts[iLoop].gameObject.activeSelf) continue;
                m_ComboArea.m_ComboTexts[iLoop].ComboNumber(_iValue);
                m_ComboArea.m_ComboTexts[iLoop].MyStartCoroutine();
                break;
            }
        }

        static public void SuperComboActive()
        {
            if (m_ComboArea == null) return;
            if (m_ComboArea.m_SuperComboText == null) return;
            m_ComboArea.m_SuperComboText.MyStartCoroutine();
        }

        private Combo[]    m_ComboTexts     = null;
        private SuperCombo m_SuperComboText = null;

        private void Awake()
        {
            m_ComboArea = this;

            Combo combo = Resources.Load<Combo>(COMBO_PATH);
            if (combo != null)
            {
                m_ComboTexts = new Combo[5];
                for (int iLoop = 0; iLoop < 5; ++iLoop)
                {
                    m_ComboTexts[iLoop] = Instantiate<Combo>(combo);
                    m_ComboTexts[iLoop].transform.parent = m_ComboArea.transform;
                    m_ComboTexts[iLoop].transform.Reset();
                    m_ComboTexts[iLoop].SetUp();
                }
            }
            SuperCombo super = Resources.Load<SuperCombo>(SUPERCOMBO_PATH);
            if(super != null)
            {
                m_SuperComboText = Instantiate<SuperCombo>(super);
                m_SuperComboText.transform.parent = m_ComboArea.transform;
                m_SuperComboText.transform.Reset();
            }
        }
    }
}
