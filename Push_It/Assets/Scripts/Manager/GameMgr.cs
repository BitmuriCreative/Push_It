using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class GameMgr : MonoSingleton<GameMgr>
    {
        public delegate void OnNextEvent();
        public event OnNextEvent _onNextEvent;

        public delegate void OnGameOverEvent();
        public event OnGameOverEvent _onGameOverEvent;

        //게임 관리
        [SerializeField]
        private StartAnim m_StartAnim = null;
        public StartAnim GetAnim { get { return (m_Instance == null) ? null : m_Instance.m_StartAnim; } }

        [SerializeField]
        private Result m_ResultPopup = null;
        public Result GetResultPopup { get { return (m_Instance == null) ? null : m_Instance.m_ResultPopup; } }

        private Score m_Score = null;
        public Score GetScore { get { return (m_Instance == null) ? null : m_Instance.m_Score; } }

        private Stage m_Stage = null;
        public Stage GetStage { get { return (m_Instance == null) ? null : m_Instance.m_Stage; } }

        private void Start()
        {
            if (SoundMgr.Get() != null)
                SoundMgr.Get().BGMStop();

            StageCreate();
            ScoreCreate();
        }

        private void StageCreate()
        {
            int iFurnitureCount = GameDataMgr.Get().GetFurnitureCount();
            if (iFurnitureCount == -1) return;
            int iDoorCount = GameDataMgr.Get().GetFurnitureDoorCount();
            if (iDoorCount == -1) return;

            CloseDoorCount.StageCloseDoorCountReset();

            m_Stage = Stage.Create(iFurnitureCount, iDoorCount);
        }

        private void ScoreCreate()
        {
            m_Score = Score.Create();
        }

        public void NextStage()
        {
            if (m_Stage != null)
            {
                CurrentStageNumber.StageNumberUpdate();
                CloseDoorCount.StageCloseDoorCountReset();

                int iFurnitureCount = GameDataMgr.Get().GetFurnitureCount();
                if (iFurnitureCount == -1) return;
                int iDoorCount = GameDataMgr.Get().GetFurnitureDoorCount();
                if (iDoorCount == -1) return;

                m_Stage.InitFurniture(iFurnitureCount, iDoorCount);
            }
        }

        public void GameUpdate(eAreaNumber _eAreaNumber, eAreaPosition _eAreaPosition)
        {
            if (m_Score == null) return;
            if (m_Stage == null) return;

            m_Score.AddScore();
            
            if (m_Stage.StageUpdate(_eAreaNumber, _eAreaPosition))
            {
                m_Score.ClearScore();
                m_Stage.Clear();

                FurnitureArea.Get().AreaReset();
                ++GameDataMgr.Get().m_iCurrentStageLevel;

                if (GameDataMgr.Get().m_iCurrentStageLevel >= (int)eStageLevel.Level_101)
                {
                    GameDataMgr.Get().m_iCurrentStageLevel = 100;

                    if (_onGameOverEvent != null)
                        _onGameOverEvent();

                    if (m_ResultPopup != null)
                        m_ResultPopup.ResultPopupOpen();

                    return;
                }

                if (_onNextEvent != null)
                    _onNextEvent();
            }
        }

        public void BitmuriAppear()
        {
            if (m_Stage == null) return;
            m_Stage.BitmuriAppearSetting();
        }
    }
}
