using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class Bitmuri : MonoBehaviour
    {
        public delegate void OnBonusEvent(float _fBonus);
        static public event OnBonusEvent _onEvent;

        public eBitmuriName           m_eBitmuriName      = eBitmuriName.none;
        public Sprite                 m_uiTouchSprite     = null;
        public Sprite                 m_uiNoneClockSprite = null;
        public Sprite[]               m_uiNoneClockFrames = null;

        protected UI2DSprite          m_ui2dSprite        = null;
        protected UI2DSpriteAnimation m_ui2dSpriteAnim    = null;
        protected Sprite[]            m_ui2dDefaultFrames = null;
        protected Collider            m_Collider          = null;
        protected Transform           m_transStart        = null;
        protected Transform           m_transEnd          = null;

        protected float               m_fMove          = 0.0f;
        protected float               m_fMoveSpeed     = 0.0f;
        protected float               m_fTimeBonus     = 0.15f;

        public virtual void SetUp(Transform _transStart, Transform _transEnd)
        {
            m_ui2dSpriteAnim = gameObject.GetComponent<UI2DSpriteAnimation>();
            m_ui2dSprite     = gameObject.GetComponent<UI2DSprite>();
            m_Collider       = gameObject.GetComponent<Collider>();

            m_ui2dDefaultFrames = m_ui2dSpriteAnim.frames;

            m_transStart = _transStart;
            m_transEnd   = _transEnd;

            transform.localPosition = m_transStart.localPosition;
        }

        public void OnPressed()
        {
            if (_onEvent != null)
                _onEvent(m_fTimeBonus);

            if(m_ui2dSprite != null)
            {
                StopAllCoroutines();
                StartCoroutine(Co_Delay());
            }

            if (m_Collider != null)
                m_Collider.enabled = false;
        }

        public void Move()
        {
            if (m_Collider != null)
                m_Collider.enabled = true;

            m_ui2dSpriteAnim.frames = m_ui2dDefaultFrames;

            gameObject.SetActive(true);
            StartCoroutine(Co_Move());
        }

        protected IEnumerator Co_Move()
        {
            while (m_fMove <= 1f)
            {
                m_fMove += Time.deltaTime * m_fMoveSpeed;
                transform.localPosition = Vector3.Lerp(m_transStart.localPosition, m_transEnd.localPosition, m_fMove);
                yield return null;
            }

            Clear();
        }

        protected IEnumerator Co_Delay()
        {
            m_ui2dSprite.sprite2D = m_uiTouchSprite;
            m_ui2dSpriteAnim.Pause();
            yield return new WaitForSeconds(0.5f);
            m_ui2dSpriteAnim.frames = m_uiNoneClockFrames;
            m_ui2dSpriteAnim.Play();
            StartCoroutine(Co_Move());
        }

        public void Clear()
        {
            StopAllCoroutines();
            m_fMove = 0.0f;
            transform.localPosition = m_transStart.localPosition;
            gameObject.SetActive(false);
        }
    }
}
