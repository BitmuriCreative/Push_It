using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class PopupResult : PopupYesNo
    {
        static private readonly string PATH = "Prefabs/Popup/PopupResult";

        public GameObject m_Best        = null;
        public GameObject m_BestSticker = null;
        public GameObject m_NewRecord   = null;

        public UILabel m_uiBest  = null;
        public UILabel m_uiScore = null;
        public UILabel m_uiCombo = null;
        public UILabel m_uiBonus = null;

        static public PopupResult Open(string _strID, int _iBestScore, int _iScore, int _iCombo, int _iBonus, bool _isNewRecord)
        {
            return Open(PATH, _strID, _iBestScore, _iScore, _iCombo, _iBonus, _isNewRecord);
        }

        static public PopupResult Open(string _strPath, string _strID, int _iBestScore, int _iScore, int _iCombo, int _iBonus, bool _isNewRecord)
        {
            PopupResult temp = Popup.Create<PopupResult>(_strPath, _strID);

            if (temp.m_Best        != null) { temp.m_Best.SetActive(!_isNewRecord); }
            if (temp.m_BestSticker != null) { temp.m_BestSticker.SetActive(_isNewRecord); }
            if (temp.m_NewRecord   != null) { temp.m_NewRecord.SetActive(_isNewRecord); }

            if (temp.m_uiBest  != null) { temp.m_uiBest.text = _iBestScore.ToString(); }
            if (temp.m_uiScore != null) { temp.m_uiScore.text = _iScore.ToString(); }
            if (temp.m_uiCombo != null) { temp.m_uiCombo.text = _iCombo.ToString(); }
            if (temp.m_uiBonus != null) { temp.m_uiBonus.text = _iBonus.ToString(); }

            return temp;
        }

        public void OnOkay()
        {
            if (_onYes != null)
            {
                _onYes();
            }

            StartCoroutine(Co_Delay(0.4f));
        }

        public void OnRetry()
        {
            if (_onNo != null)
            {
                _onNo();
            }

            StartCoroutine(Co_Delay(0.4f));
        }
    }
}
