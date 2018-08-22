using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class PopupYesNo : Popup
    {
        public delegate void _callback();

        public _callback _onYes = null;
        public _callback _onNo = null;

        protected IEnumerator Co_Delay(float _time)
        {
            yield return new WaitForSeconds(_time);
            Close();
        }
    }
}
