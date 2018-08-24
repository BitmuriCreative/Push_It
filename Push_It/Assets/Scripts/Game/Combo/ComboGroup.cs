using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class ComboGroup : MonoSingleton<ComboGroup>
    {
        static private readonly string COMBO_PATH      = "Prefabs/Combo/Combo";
        static private readonly string SUPERCOMBO_PATH = "Prefabs/Combo/SuperCombo";

        private Combo[]    m_ComboTexts     = null;
        private SuperCombo m_SuperComboText = null;

        private int        m_iComboCount    = 5;

        protected override void OnAwake()
        {
            if (m_Instance == null) return;

            //콤보문구 5개 미리 만들어두고 돌려쓰려고.
            Combo combo = Resources.Load<Combo>(COMBO_PATH);
            if (combo != null)
            {
                m_ComboTexts = new Combo[m_iComboCount];
                for (int iLoop = 0; iLoop < m_iComboCount; ++iLoop)
                {
                    m_ComboTexts[iLoop] = Instantiate<Combo>(combo);
                    m_ComboTexts[iLoop].transform.parent = m_Instance.transform;
                    m_ComboTexts[iLoop].transform.Reset();
                    m_ComboTexts[iLoop].SetUp();
                }
            }
            SuperCombo super = Resources.Load<SuperCombo>(SUPERCOMBO_PATH);
            if (super != null)
            {
                m_SuperComboText = Instantiate<SuperCombo>(super);
                m_SuperComboText.transform.parent = m_Instance.transform;
                m_SuperComboText.transform.Reset();
            }
        }

        //비활성화 되어있는 콤보객체 찾아서 활성화 시켜준다.
        public void ComboActive(int _iValue)
        {
            if (m_Instance == null) return;
            for (int iLoop = 0; iLoop < 5; ++iLoop)
            {
                if (m_ComboTexts[iLoop] == null) continue;
                if (m_ComboTexts[iLoop].gameObject.activeSelf) continue;
                m_ComboTexts[iLoop].ComboNumber(_iValue);
                m_ComboTexts[iLoop].MyStartCoroutine();
                break;
            }
        }

        public void SuperComboActive()
        {
            if (m_Instance == null) return;
            if (m_SuperComboText == null) return;
            m_SuperComboText.MyStartCoroutine();
        }
    }
}
