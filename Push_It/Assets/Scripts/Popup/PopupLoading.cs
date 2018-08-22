using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class PopupLoading : Popup
    {
        private const string PATH = "Prefabs/Popup/PopupLoading";

        static public PopupLoading Open(string _strID)
        {
            return Open(PATH, _strID);
        }

        static public PopupLoading Open(string strPath, string _strID)
        {
            PopupLoading temp = Popup.Create<PopupLoading>(strPath, _strID);

            return temp;
        }
    }
}
