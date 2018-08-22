using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class PopupConfirm : Popup
    {
        static private readonly string PATH = "Prefabs/Popup/PopupConfirm";

        public delegate void _callback();

        public _callback _onConfirm = null;

        public UILabel m_uiMessage = null;

        static public PopupConfirm Open(string _strID, string _strMessage)
        {
            return Open(PATH, _strID, _strMessage);
        }

        static public PopupConfirm Open(string _strPath, string _strID, string _strMessage)
        {
            PopupConfirm temp = Popup.Create<PopupConfirm>(_strPath, _strID);

            if(temp.m_uiMessage != null) { temp.m_uiMessage.text = _strMessage; }

            return temp;
        }

        public void OnConfirm()
        {
            if (_onConfirm != null)
            {
                _onConfirm();
            }

            Close();
        }
    }
}
