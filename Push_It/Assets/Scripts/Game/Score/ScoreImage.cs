using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class ScoreImage : MonoBehaviour
    {
        static public ScoreImage m_Instance = null;

        public Transform    m_transPos  = null;
        public GameObject[] m_uiNumber  = null;

        public float m_fEnableTime = 0.3f;

        private void Awake()
        {
            m_Instance = this;
        }

        static public void On(Vector3 _vecMousePos, int _iIndex)
        {
            if (m_Instance == null) return;

            float fHalf = 2f;
            Vector3 temp = new Vector3(_vecMousePos.x - Screen.width / fHalf, _vecMousePos.y - Screen.height / fHalf, 0);

            m_Instance.StopAllCoroutines();
            for (int iLoop = 0; iLoop < m_Instance.m_uiNumber.Length; ++iLoop)
            {
                m_Instance.m_uiNumber[iLoop].SetActive(false);
            }

            m_Instance.m_transPos.localPosition = temp;
            m_Instance.m_uiNumber[_iIndex].SetActive(true);
            m_Instance.StartCoroutine(m_Instance.Co_End(_iIndex));
        }

        IEnumerator Co_End(int _iIndex)
        {
            float fMoveTime = 0f;
            Vector3 vecEndPos = m_transPos.localPosition;
            vecEndPos.y += 70f;
            while (fMoveTime <= m_fEnableTime)
            {
                fMoveTime += Time.deltaTime;
                transform.localPosition = Vector3.Lerp(m_transPos.localPosition, vecEndPos, fMoveTime);
                yield return null;
            }

            yield return new WaitForSeconds(m_fEnableTime);
            m_Instance.m_uiNumber[_iIndex].SetActive(false);
        }
    }
}
