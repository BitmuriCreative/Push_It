using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class SaveDataMgr : MonoDontDestroySingleton<SaveDataMgr>
    {
        //저장 해둬야할 것들 여기에 모아둠.
        static private readonly string SAVEDATA_PATH_FORMAT = "{0}/{1}";
        static private readonly string SAVEDATA_FILE        = @"/userdata.ini";

        static private readonly string SAVEDATA_SECTION              = "SAVEDATA";
        static private readonly string SAVEDATA_KEY_BESTTOTAL_SCORE  = "bestTotalScore";
        static private readonly string SAVEDATA_KEY_BESTSTAGE_NUMBER = "bestStageNumber";

        public class SaveData
        {
            public int m_iBestTotalScore  = 0;
            public int m_iBestStageNumber = 0;
        }

        private SaveData m_SaveData = new SaveData();
        static public SaveData Current
        {
            get
            {
                if (m_Instance == null)
                    return null;

                if (m_Instance.m_SaveData == null)
                    m_Instance.m_SaveData = new SaveData();

                return m_Instance.m_SaveData;
            }
        }

        public bool BestTotalScore(int _iCurrentScore)
        {
            if (m_Instance == null) return false;
            if (m_Instance.m_SaveData == null) return false;

            if (m_SaveData.m_iBestTotalScore < _iCurrentScore)
            {
                m_SaveData.m_iBestTotalScore = _iCurrentScore;
                return true;
            }
            return false;
        }

        public void BestStageNumber(int _iCurrentStage)
        {
            if (m_Instance == null) return;
            if (m_Instance.m_SaveData == null) return;

            if (m_SaveData.m_iBestStageNumber < _iCurrentStage)
                m_SaveData.m_iBestStageNumber = _iCurrentStage;
        }

        /// <summary>
        /// Config Load
        /// </summary>
        public void Load()
        {
            bool isNeedSave = false;

            string path = string.Format(SAVEDATA_PATH_FORMAT, Application.persistentDataPath, SAVEDATA_FILE);

            SharpConfig.Configuration config = null;
            //지정된 파일이 있는지 여부를 확인
            if (System.IO.File.Exists(path))
            {
                config = SharpConfig.Configuration.LoadFromFile(path);
            }

            if (config == null)
            {
                config = new SharpConfig.Configuration();
            }

            if (!config.Contains(SAVEDATA_SECTION))
            {
                m_SaveData.m_iBestTotalScore  = 0;
                m_SaveData.m_iBestStageNumber = 1;
                isNeedSave = true;
            }
            else
            {
                var userdata = config[SAVEDATA_SECTION];
                m_SaveData.m_iBestTotalScore  = userdata[SAVEDATA_KEY_BESTTOTAL_SCORE].IntValue;
                m_SaveData.m_iBestStageNumber = userdata[SAVEDATA_KEY_BESTSTAGE_NUMBER].IntValue;
            }

            if (isNeedSave)
            {
                Save();
            }
        }

        /// <summary>
        /// Config Save
        /// </summary>
        public void Save()
        {
            var config = new SharpConfig.Configuration();

            config[SAVEDATA_SECTION][SAVEDATA_KEY_BESTTOTAL_SCORE].IntValue  = m_SaveData.m_iBestTotalScore;
            config[SAVEDATA_SECTION][SAVEDATA_KEY_BESTSTAGE_NUMBER].IntValue = m_SaveData.m_iBestStageNumber;

            config.SaveToFile(string.Format(SAVEDATA_PATH_FORMAT, Application.persistentDataPath, SAVEDATA_FILE));
        }
    }
}