using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class PopupUpdate : PopupYesNo
    {
        static private readonly string PATH = "Prefabs/Popup/PopupUpdate";

        public UILabel m_uiMessage = null;

        static public PopupUpdate Open(string _strID, string _strMessage)
        {
            return Open(PATH, _strID, _strMessage);
        }

        static public PopupUpdate Open(string _strPath, string _strID, string _strMessage)
        {
            PopupUpdate temp = Popup.Create<PopupUpdate>(_strPath, _strID);

            if (temp.m_uiMessage != null) { temp.m_uiMessage.text = _strMessage; }

            return temp;
        }

        public void OnUpdate()
        {
            if (_onYes != null)
            {
                _onYes();
            }

            StartCoroutine(Co_Delay(0.4f));
        }

        public void OnQuit()
        {
            if (_onNo != null)
            {
                _onNo();
            }

            Close();
        }
    }
}
