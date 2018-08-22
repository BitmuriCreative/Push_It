using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class GameDataMgr : MonoSingleton<GameDataMgr>
    {
        public class StageLevelFurnitureData
        {
            public int m_iFurnitureCount = 0;
            public int m_iDoorCount      = 0;

            public StageLevelFurnitureData() { }

            public StageLevelFurnitureData(int _iFurnitureCount, int _iDoorCount)
            {
                m_iFurnitureCount = _iFurnitureCount;
                m_iDoorCount = _iDoorCount;
            }
        }

        private Dictionary<int, StageLevelFurnitureData> m_StageLevelFurnitureData = new Dictionary<int, StageLevelFurnitureData>();
        private Dictionary<int, int> m_StageLevelTouchCount = new Dictionary<int, int>();
        private Dictionary<int, float> m_StageLevelTimeSpeed = new Dictionary<int, float>();

        /// <summary>
        /// Stage Level에 맞는 StageLevelFurnitureData 정보 넘겨주기.
        /// </summary>
        public StageLevelFurnitureData GetFurnitureData()
        {
            if (m_StageLevelFurnitureData.ContainsKey(m_iCurrentStageLevel))
            {
                return m_StageLevelFurnitureData[m_iCurrentStageLevel];
            }

            return null;
        }

        /// <summary>
        /// Stage Level에 맞는 FurnitureCount 정보 넘겨주기.
        /// </summary>
        public int GetFurnitureCount()
        {
            if (m_StageLevelFurnitureData.ContainsKey(m_iCurrentStageLevel))
            {
                return m_StageLevelFurnitureData[m_iCurrentStageLevel].m_iFurnitureCount;
            }

            return -1;
        }

        /// <summary>
        /// Stage Level에 맞는 FurnitureDoorCount 정보 넘겨주기.
        /// </summary>
        public int GetFurnitureDoorCount()
        {
            if (m_StageLevelFurnitureData.ContainsKey(m_iCurrentStageLevel))
            {
                return m_StageLevelFurnitureData[m_iCurrentStageLevel].m_iDoorCount;
            }

            return -1;
        }

        /// <summary>
        /// Stage Level에 맞는 TouchCount 정보 넘겨주기.
        /// </summary>
        public int GetTouchCount()
        {
            if (m_StageLevelTouchCount.ContainsKey(m_iCurrentStageLevel))
            {
                return m_StageLevelTouchCount[m_iCurrentStageLevel];
            }

            return -1;
        }

        /// <summary>
        /// Stage Level에 맞는 TimeSpeed 정보 넘겨주기.
        /// </summary>
        public float GetTimeSpeed()
        {
            if (m_StageLevelTimeSpeed.ContainsKey(m_iCurrentStageLevel))
            {
                return m_StageLevelTimeSpeed[m_iCurrentStageLevel];
            }

            return -1f;
        }

        [Tooltip("현재 Stage")]
        public int m_iCurrentStageLevel = 1;

        [Tooltip("토탈 점수")]
        public int m_iTotalScore = 0;

        [Tooltip("토탈 콤보 카운트")]
        public int m_iTotalComboCount = 0;

        [Tooltip("시간 보너스 점수")]
        public int m_iBounsTimeScore = 0;

        private List<int> m_listBitmuriAppearCount = new List<int>();

        public int GetBitmuriAppearCount(int _iIndex)
        {
            return (m_listBitmuriAppearCount != null) ? m_listBitmuriAppearCount[_iIndex] : -1;
        }

        private void Start()
        {
            StageLevelDataSetting();
            BitmuriAppearCountSetting();
        }

        private void StageLevelDataSetting()
        {
            StageLevelFurnitureSetting();
            StageLevelTouchCountSetting();
            StageLevelTimeSpeedSetting();
        }

        private void StageLevelFurnitureSetting()
        {
            //1 - 5    f4     문1
            //6 - 15   f5     문1 문2
            //16 - 25  f5 - 6 문1 문2
            //26 - 50  f6     문1 문2 문3
            //51 - 100 f5 - 6 전체 가구
            StageLevelFurnitureData data = new StageLevelFurnitureData(4, 1);
            for (int iLoop = 1; iLoop <= 5; ++iLoop)
            {
                m_StageLevelFurnitureData.Add(iLoop, data);
            }

            data = new StageLevelFurnitureData(5, 2);
            for (int iLoop = 6; iLoop <= 15; ++iLoop)
            {
                m_StageLevelFurnitureData.Add(iLoop, data);
            }

            data = new StageLevelFurnitureData(7, 2);
            for (int iLoop = 16; iLoop <= 25; ++iLoop)
            {
                m_StageLevelFurnitureData.Add(iLoop, data);
            }

            data = new StageLevelFurnitureData(6, 3);
            for (int iLoop = 26; iLoop <= 50; ++iLoop)
            {
                m_StageLevelFurnitureData.Add(iLoop, data);
            }

            data = new StageLevelFurnitureData(7, 4);//전체
            for (int iLoop = 51; iLoop <= 100; ++iLoop)
            {
                m_StageLevelFurnitureData.Add(iLoop, data);
            }
        }

        private void StageLevelTouchCountSetting()
        {
            //  1~10 stage 터치 횟수 - 20번
            //  11~20 stage 터치 횟수 - 22번
            //  21~30 stage 터치 횟수 - 25번
            //  31~100 stage 터치 횟수 - 30번
            for (int iLoop = 1; iLoop <= 10; ++iLoop)
            {
                m_StageLevelTouchCount.Add(iLoop, 20);
            }
            for (int iLoop = 11; iLoop <= 20; ++iLoop)
            {
                m_StageLevelTouchCount.Add(iLoop, 22);
            }
            for (int iLoop = 21; iLoop <= 30; ++iLoop)
            {
                m_StageLevelTouchCount.Add(iLoop, 25);
            }
            for (int iLoop = 31; iLoop <= 100; ++iLoop)
            {
                m_StageLevelTouchCount.Add(iLoop, 30);
            }
        }

        private void StageLevelTimeSpeedSetting()
        {
            //  21 stage 부터 1.25배 빨라짐
            //  31 stage 부터 1.5배 빨라짐
            //  41 stage 부터 2배 빨라짐
            for (int iLoop = 1; iLoop <= 20; ++iLoop)
            {
                m_StageLevelTimeSpeed.Add(iLoop, 1f);
            }
            for (int iLoop = 21; iLoop <= 30; ++iLoop)
            {
                m_StageLevelTimeSpeed.Add(iLoop, 1.25f);
            }
            for (int iLoop = 31; iLoop <= 40; ++iLoop)
            {
                m_StageLevelTimeSpeed.Add(iLoop, 1.5f);
            }
            for (int iLoop = 41; iLoop <= 100; ++iLoop)
            {
                m_StageLevelTimeSpeed.Add(iLoop, 2f);
            }
        }

        private void BitmuriAppearCountSetting()
        {
            m_listBitmuriAppearCount.Add(7);
            m_listBitmuriAppearCount.Add(12);
            m_listBitmuriAppearCount.Add(25);
        }
    }
}
