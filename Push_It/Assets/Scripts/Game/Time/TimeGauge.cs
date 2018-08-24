using System;
using UnityEngine;
using System.Collections;

namespace Push_It
{
    public class TimeGauge : MonoBehaviour
    {
        static private readonly string PLAY_GAMEOVER_ANIM = "GameOver";
        static private readonly string TIMEBAR_BLUE       = "time_bar_blue";
        static private readonly string TIMEBAR_RED        = "time_bar_red";
        
        public UISlider m_uiTimebar      = null;
        public UISprite m_uiTimebarColor = null;

        public  float m_fMaxTime     = 10.0f;
        private float m_fCurrentTime = 0.0f;
        private float m_fCutValue    = 0.0f;

        private bool m_isStop      = true;
        private bool m_isCondition = false;

        private void Start()
        {
            if (GameMgr.Get() != null)
                GameMgr.Get()._onNextEvent += new GameMgr.OnNextEvent(NextStage);

            if (GameMgr.Get() != null)
                GameMgr.Get()._onGameOverEvent += new GameMgr.OnGameOverEvent(TimeBonusScore);

            Bitmuri._onEvent += new Bitmuri.OnBonusEvent(TimeBonus);

            FSM_SendMessage._onEvent += new FSM_SendMessage.OnFSMEvent(GameStart);
        }

        private void OnDestroy()
        {
            if (GameMgr.Get() != null)
                GameMgr.Get()._onNextEvent -= new GameMgr.OnNextEvent(NextStage);

            if (GameMgr.Get() != null)
                GameMgr.Get()._onGameOverEvent -= new GameMgr.OnGameOverEvent(TimeBonusScore);

            Bitmuri._onEvent -= new Bitmuri.OnBonusEvent(TimeBonus);

            FSM_SendMessage._onEvent -= new FSM_SendMessage.OnFSMEvent(GameStart);
        }

        private void GameStart()
        {
            m_uiTimebar.value = 1;
            m_isStop = false;
            m_isCondition = false;
        }

        private void TimeBonus(float _bonus)
        {
            m_fCurrentTime -= _bonus;
            if (m_fCurrentTime < 0)
                m_fCurrentTime = 0;
        }

        private void Update()
        {
            if (m_isStop) return;

            if (m_uiTimebar.value > 0f)
            {
                //Current Time / Max Time
                m_fCurrentTime += Time.deltaTime / m_fMaxTime;
                m_fCutValue = 1 - (m_fCurrentTime * GameDataMgr.Get().GetTimeSpeed());

                if (m_fCutValue <= 0.5f && !m_isCondition)
                {
                    m_isCondition = true;
                    GameMgr.Get().BitmuriAppear();
                }

                if (m_fCutValue < 0.25f && m_uiTimebarColor.spriteName == TIMEBAR_BLUE)
                    m_uiTimebarColor.spriteName = TIMEBAR_RED;
                else if(m_fCutValue > 0.25f && m_uiTimebarColor.spriteName == TIMEBAR_RED)
                    m_uiTimebarColor.spriteName = TIMEBAR_BLUE;

                m_uiTimebar.value = m_fCutValue;
            }
            else
            {
                //Game Over
                m_fCurrentTime    = 0.0f;
                m_fCutValue       = 0.0f;
                m_uiTimebar.value = 0;
                m_isStop          = true;

                m_uiTimebarColor.spriteName = TIMEBAR_BLUE;

                GameMgr.Get().GetScore.ClearScore();

                GameMgr.Get().GetAnim.PlayAnim(PLAY_GAMEOVER_ANIM);
            }
        }

        private void NextStage()
        {
            m_isStop = true;
            
            TimeBonusScore();

            //값 초기화.
            m_uiTimebarColor.spriteName = TIMEBAR_BLUE;
            m_uiTimebar.value = 1;
            m_fCurrentTime    = 0.0f;
        }

        private void TimeBonusScore()
        {
            double temp = Math.Round(m_fCurrentTime, 1);
            GameDataMgr.Get().m_iBounsTimeScore += Convert.ToInt32(m_fMaxTime - (temp * m_fMaxTime));
        }
    }
}
