using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class Score : MonoBehaviour
    {
        static private readonly string OBJ_NAME = "Score";

        private const int m_iBasicScore         = 10;   //기본점수.
        private const int m_iNormalComboScore   = 50;   //기본 터치 5번이상부터 주는 점수.
        private const int m_iFiverComboScore    = 1000; //콤보 5번 이어지면 주는 점수.

        private const int m_iContinuityMaxCount = 5;    //연속 터치 체크
        
        private int m_iCurrentScore      = 0;           //현재 점수 체크
        private int m_iCurrentComboCount = 0;           //현재 몇 콤보 체크 5의 배수이면 콤보 점수 줌

        public float m_fEnableTime = 0.5f;

        private bool m_isTimeCheck = false;

        static public Score Create()
        {
            GameObject obj = new GameObject();
            if (obj == null) return null;
            obj.name = OBJ_NAME;
            obj.transform.SetParent(null);
            obj.transform.Reset();

            Score score = obj.AddComponent<Score>();
            if (score == null)
            {
                Destroy(obj);
                return null;
            }

            return score;
        }

        //점수 추가.
        public void AddScore()
        {
            if (!m_isTimeCheck)
            {
                StartCoroutine(Co_EnableTime());
#if(UNITY_ANDROID && UNITY_EDITOR)
                ScoreImage.On(Input.mousePosition, 0);

#elif (UNITY_ANDROID)
                    if(Input.touchCount > 0)    
                        ScoreImage.On(Input.GetTouch(0).position, 0);
#endif
                m_iCurrentScore += m_iBasicScore;
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(Co_EnableTime());
                ++m_iCurrentComboCount;

                if(GameDataMgr.Get() != null)
                    ++GameDataMgr.Get().m_iTotalComboCount;   

                if (m_iCurrentComboCount >= m_iContinuityMaxCount)
                {
                    //피버 콤보! 여기들어오면 문구 띄어줄 예정.
                    if (((m_iCurrentComboCount % 5) == 0) && (m_iCurrentComboCount != 5))
                    {
#if (UNITY_ANDROID && UNITY_EDITOR)
                        ScoreImage.On(Input.mousePosition, 2);
#elif (UNITY_ANDROID)
                       if(Input.touchCount > 0)    
                        ScoreImage.On(Input.GetTouch(0).position, 2);
#endif
                        AddFiverComboScore();
                        return;
                    }
                    //일반 콤보.
#if (UNITY_ANDROID && UNITY_EDITOR)
                    ScoreImage.On(Input.mousePosition, 1);
#elif (UNITY_ANDROID)
                    if(Input.touchCount > 0)    
                        ScoreImage.On(Input.GetTouch(0).position, 1);
# endif
                    AddNormalComboScore();
                    return;
                }

#if (UNITY_ANDROID && UNITY_EDITOR)
                ScoreImage.On(Input.mousePosition, 0);

#elif (UNITY_ANDROID)
                    if(Input.touchCount > 0)    
                        ScoreImage.On(Input.GetTouch(0).position, 0);
#endif
                m_iCurrentScore += m_iBasicScore;
            }
        }

        //터치 5번 이상 연속했을때.
        private void AddNormalComboScore()
        {
            m_iCurrentScore += m_iNormalComboScore;
            ComboArea.ComboActive(m_iCurrentComboCount);
        }

        //콤보 시작부터 5번 연속 터치 했을 때.
        private void AddFiverComboScore()
        {
            m_iCurrentScore += m_iFiverComboScore;
            ComboArea.SuperComboActive();
        }

        //다음 스테이지로 넘어 갈 시.
        public void ClearScore()
        {
            if (GameDataMgr.Get() != null)
            {
                GameDataMgr.Get().m_iTotalScore += m_iCurrentScore;
            }

            m_iCurrentScore      = 0;
            m_iCurrentComboCount = 0;
        }

        IEnumerator Co_EnableTime()
        {
            m_isTimeCheck = true;
            yield return new WaitForSeconds(m_fEnableTime);
            m_isTimeCheck = false;
            m_iCurrentComboCount = 0;
        }
    }
}
