using System.Collections;
using UnityEngine;

namespace Push_It
{
    public class Result : MonoBehaviour
    {
        static private readonly string GAME_START_ANIM = "GameStart";
        static private readonly string RESULT_POPUP    = "Result";

        [Tooltip("0. StageBg_1, 1 StageBg_2")]
        public UI2DSprite[] m_uiInitBg  = null;
        public Sprite[]     m_uiStageBg = null;

        private int m_iTimePlusScore     = 20; //타임 보너스 점수. 
        private int m_iUnityAdsShowCount = 3;  //몇번 만에 광고를 띄울건지 횟수 제한.

        public void ResultPopupOpen()
        {
            //광고.
            UnityAdsMgr tempUnityAdsMgr = UnityAdsMgr.Get();
            if (tempUnityAdsMgr != null)
            {
                ++tempUnityAdsMgr.m_iActiveCount;
                if (tempUnityAdsMgr.m_iActiveCount == m_iUnityAdsShowCount)
                {
                    tempUnityAdsMgr.m_iActiveCount = 0;
                    tempUnityAdsMgr.ShowRewardedAd();
                    return;
                }
            }

            ResultPopupSetting();
        }

        public void ResultPopupSetting()
        {
            GameMgr tempGameMgr = GameMgr.Get();
            if (tempGameMgr == null) return;
            GameDataMgr tempDataMgr = GameDataMgr.Get();
            if (GameMgr.Get() == null) return;
            SaveDataMgr tempSaveDataMgr = SaveDataMgr.Get();
            if (tempSaveDataMgr == null) return;

            tempGameMgr.GetStage.Clear();
            tempGameMgr.GetStage.BitmuriAppearCountClear();

            int iBonus = tempDataMgr.m_iBounsTimeScore * m_iTimePlusScore;
            int iTotal = tempDataMgr.m_iTotalScore + iBonus;

            bool isNewRecord = tempSaveDataMgr.BestTotalScore(iTotal);
            tempSaveDataMgr.BestStageNumber(tempDataMgr.m_iCurrentStageLevel);
            tempSaveDataMgr.Save();

            int iBestScore       = SaveDataMgr.Current.m_iBestTotalScore;
            int iTotalComboCount = tempDataMgr.m_iTotalComboCount;

            //팝업 띄움.
            if(Popup.Find(RESULT_POPUP) == null)
            {
                PopupResult result = PopupResult.Open(RESULT_POPUP, iBestScore, iTotal, iTotalComboCount, iBonus, isNewRecord);

                result._onYes += () =>
                {
                    StartCoroutine(Co_OkayDelay());
                };

                result._onNo += () =>
                {
                    StartCoroutine(Co_RetryDelay());
                };
            }
        }

        IEnumerator Co_OkayDelay()
        {
            yield return new WaitForSeconds(0.5f);
            gameObject.SendMessage("FadeEvent", SendMessageOptions.RequireReceiver);

            if (SoundMgr.Get() != null)
                SoundMgr.Get().BGMPlay();
        }

        IEnumerator Co_RetryDelay()
        {
            yield return new WaitForSeconds(0.4f);
            GameMgr.Get().GetAnim.PlayAnim(GAME_START_ANIM);

            //방 배경 셋팅
            m_uiInitBg[0].sprite2D = m_uiStageBg[(int)eStageBackgound.stage_room1];
            m_uiInitBg[1].sprite2D = m_uiStageBg[(int)eStageBackgound.stage_room2];

            //값 초기화
            FurnitureArea.Get().AreaReset();
            GameDataMgr.Get().m_iBounsTimeScore    = 0;
            GameDataMgr.Get().m_iTotalScore        = 0;
            GameDataMgr.Get().m_iTotalComboCount   = 0;
            GameDataMgr.Get().m_iCurrentStageLevel = 1;
            CurrentStageNumber.StageNumberUpdate();

            //스테이지 다시 시작
            GameMgr.Get().NextStage();
        }
    }
}
