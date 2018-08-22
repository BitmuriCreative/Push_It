using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class TvDrawer : PlayFurniture
    {
        public GameObject[] m_Deco = null;

        private void Start()
        {
            if (m_Deco == null) return;
            int random = Random.Range(0, m_Deco.Length);
            m_Deco[random].SetActive(true);
        }
    }
}
