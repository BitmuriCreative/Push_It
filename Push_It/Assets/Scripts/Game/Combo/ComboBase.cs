using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class ComboBase : MonoBehaviour
    {
        protected Vector3 m_vecNormal = Vector3.zero;
        protected float m_fEnableTime = 0.5f;

        protected void Awake()
        {
            m_vecNormal = transform.localPosition;
            gameObject.SetActive(false);
        }

        public void MyStartCoroutine()
        {
            gameObject.SetActive(true);
            StartCoroutine(Co_UnActive());
        }

        protected IEnumerator Co_UnActive()
        {
            float fMoveTime = 0f;
            Vector3 vecEndPos = transform.localPosition;
            vecEndPos.y += 150f;

            while (fMoveTime <= m_fEnableTime)
            {
                fMoveTime += Time.deltaTime;
                transform.localPosition = Vector3.Lerp(transform.localPosition, vecEndPos, fMoveTime);
                yield return null;
            }
            
            transform.localPosition = m_vecNormal;
            gameObject.SetActive(false);
        }
    }
}
