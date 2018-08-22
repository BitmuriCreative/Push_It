using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class PopupQuit : PopupYesNo
    {
        static private readonly string PATH = "Prefabs/Popup/PopupQuit";

        static public PopupQuit Open(string _strID)
        {
            return Open(PATH, _strID);
        }

        static public PopupQuit Open(string _strPath, string _strID)
        {
            PopupQuit temp = Popup.Create<PopupQuit>(_strPath, _strID);
            return temp;
        }

        public void OnYes()
        {
            if (_onYes != null)
            {
                _onYes();
            }
            
            Close();
        }

        public void OnNo()
        {
            if (_onNo != null)
            {
                _onNo();
            }

            //Close();
            StartCoroutine(Co_Delay(0.4f));
        }
    }
}
