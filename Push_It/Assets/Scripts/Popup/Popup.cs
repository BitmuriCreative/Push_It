using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class Popup : MonoBehaviour
    {
        public delegate void OnClose();

        static protected Dictionary<string, Popup> m_Popup = new Dictionary<string, Popup>();

        public OnClose onClose = null;
        public string m_strID  = string.Empty;

        static protected T Create<T>(string _strPath, string _strID) where T : Popup
        {
            T temp = Resources.Load<T>(_strPath);

            m_Popup[_strID] = Instantiate<T>(temp);
            m_Popup[_strID].m_strID = _strID;

            PopupRoot.Add(m_Popup[_strID]);

            return m_Popup[_strID] as T;
        }

        static public Popup Find(string _strID)
        {
            if (m_Popup.ContainsKey(_strID))
            {
                return m_Popup[_strID];
            }

            return null;
        }

        static public void Close(string _strID)
        {
            if (m_Popup.ContainsKey(_strID))
            {
                m_Popup[_strID].Close();
            }
        }

        static public void CloseAll()
        {
            List<Popup> tempPopup = new List<Popup>(m_Popup.Values);

            for (int i = 0; i < tempPopup.Count; ++i)
            {
                tempPopup[i].Close();
            }

            m_Popup.Clear();
        }

        public void Close()
        {
            if (onClose != null)
            {
                onClose();
            }

            Popup temp = m_Popup[m_strID];
            m_Popup.Remove(m_strID);
            Destroy(temp.gameObject);
        }
    }
}
