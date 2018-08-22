using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class NextStage : MonoBehaviour 
    {
        static private readonly string PLAY_FADEOUT_ANIM = "SpeedStageFadeOut";
        static private readonly string PLAY_MOVE_ANIM    = "ChapterClear";

        public UI2DSprite m_ui2dCurrentBg = null;
        public UI2DSprite m_ui2dAfterBg   = null;

        public Sprite[] m_uiStageBg       = null;
        public Sprite[] m_uiStageEndBg    = null;

        private void Start()
        {
            if (GameMgr.Get() != null)
                GameMgr.Get()._onNextEvent += new GameMgr.OnNextEvent(PrepareToTheNextStage);
        }

        private void OnDestroy()
        {
            if (GameMgr.Get() != null)
                GameMgr.Get()._onNextEvent -= new GameMgr.OnNextEvent(PrepareToTheNextStage);
        }

        private void PrepareToTheNextStage()
        {
            switch ((eStageLevel)GameDataMgr.Get().m_iCurrentStageLevel)
            {
                case eStageLevel.Level_11:
                case eStageLevel.Level_21:
                case eStageLevel.Level_31:
                case eStageLevel.Level_41:
                case eStageLevel.Level_51:
                case eStageLevel.Level_61:
                case eStageLevel.Level_71:
                case eStageLevel.Level_81:
                case eStageLevel.Level_91:
                    {
                        //백프레임이 밑으로 내려가는 애니 틀어주기.
                        GameMgr.Get().GetAnim.PlayAnim(PLAY_MOVE_ANIM);
                    }
                    break;
                default:
                    {
                        //패이드아웃호출.
                        GameMgr.Get().GetAnim.PlayAnim(PLAY_FADEOUT_ANIM);
                    }
                    break;
            }
        }

        //방 이동시 배경 변경
        private void ChangeBackgroundWhenMovingStage()
        {
            switch ((eStageLevel)GameDataMgr.Get().m_iCurrentStageLevel)
            {
                case eStageLevel.Level_11:
                    m_ui2dCurrentBg.sprite2D = m_uiStageBg[(int)eStageBackgound.stage_room2];
                    m_ui2dAfterBg.sprite2D   = m_uiStageBg[(int)eStageBackgound.stage_room3];
                    break;
                case eStageLevel.Level_21:
                    m_ui2dCurrentBg.sprite2D = m_uiStageBg[(int)eStageBackgound.stage_room3];
                    m_ui2dAfterBg.sprite2D   = m_uiStageBg[(int)eStageBackgound.stage_room4];
                    break;
                case eStageLevel.Level_31:
                    m_ui2dCurrentBg.sprite2D = m_uiStageBg[(int)eStageBackgound.stage_room4];
                    m_ui2dAfterBg.sprite2D   = m_uiStageBg[(int)eStageBackgound.stage_room5];
                    break;
                case eStageLevel.Level_41:
                    m_ui2dCurrentBg.sprite2D = m_uiStageBg[(int)eStageBackgound.stage_room5];
                    m_ui2dAfterBg.sprite2D   = m_uiStageBg[(int)eStageBackgound.stage_room6];
                    break;
                case eStageLevel.Level_51:
                    m_ui2dCurrentBg.sprite2D = m_uiStageBg[(int)eStageBackgound.stage_room6];
                    m_ui2dAfterBg.sprite2D   = m_uiStageBg[(int)eStageBackgound.stage_room7];
                    break;
                case eStageLevel.Level_61:
                    m_ui2dCurrentBg.sprite2D = m_uiStageBg[(int)eStageBackgound.stage_room7];
                    m_ui2dAfterBg.sprite2D   = m_uiStageBg[(int)eStageBackgound.stage_room8];
                    break;
                case eStageLevel.Level_71:
                    m_ui2dCurrentBg.sprite2D = m_uiStageBg[(int)eStageBackgound.stage_room8];
                    m_ui2dAfterBg.sprite2D   = m_uiStageBg[(int)eStageBackgound.stage_room9];
                    break;
                case eStageLevel.Level_81:
                    m_ui2dCurrentBg.sprite2D = m_uiStageBg[(int)eStageBackgound.stage_room9];
                    m_ui2dAfterBg.sprite2D   = m_uiStageBg[(int)eStageBackgound.stage_room10];
                    break;
                case eStageLevel.Level_91:
                    m_ui2dCurrentBg.sprite2D = m_uiStageBg[(int)eStageBackgound.stage_room10];
                    break;
            }
        }

        private void ToTheNextStage()
        {
            ChangeBockgroundEndStage();
            GameMgr.Get().NextStage();
        }

        //한 챕터마다 마지막방 배경 변경.
        private void ChangeBockgroundEndStage()
        {
            switch ((eStageBackgound)GameDataMgr.Get().m_iCurrentStageLevel)
            {
                case eStageBackgound.stage_room1_end:
                    m_ui2dCurrentBg.sprite2D = m_uiStageEndBg[0];
                    break;
                case eStageBackgound.stage_room2_end:
                    m_ui2dCurrentBg.sprite2D = m_uiStageEndBg[1];
                    break;
                case eStageBackgound.stage_room3_end:
                    m_ui2dCurrentBg.sprite2D = m_uiStageEndBg[2];
                    break;
                case eStageBackgound.stage_room4_end:
                    m_ui2dCurrentBg.sprite2D = m_uiStageEndBg[3];
                    break;
                case eStageBackgound.stage_room5_end:
                    m_ui2dCurrentBg.sprite2D = m_uiStageEndBg[4];
                    break;
                case eStageBackgound.stage_room6_end:
                    m_ui2dCurrentBg.sprite2D = m_uiStageEndBg[5];
                    break;
                case eStageBackgound.stage_room7_end:
                    m_ui2dCurrentBg.sprite2D = m_uiStageEndBg[6];
                    break;
                case eStageBackgound.stage_room8_end:
                    m_ui2dCurrentBg.sprite2D = m_uiStageEndBg[7];
                    break;
                case eStageBackgound.stage_room9_end:
                    m_ui2dCurrentBg.sprite2D = m_uiStageEndBg[8];
                    break;
                case eStageBackgound.stage_room10_end:
                    m_ui2dCurrentBg.sprite2D = m_uiStageEndBg[9];
                    break;
            }
        }
    }
}
