using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class CurrentStageNumber : MonoBehaviour
    {
        static private readonly string STAGE_STRING_FORMAT = "{0} Stage";

        static private CurrentStageNumber m_Instance = null;

        private UILabel m_uiStageNumber = null;

        static public void StageNumberUpdate()
        {
            if (m_Instance == null) return;
            if (m_Instance.m_uiStageNumber == null) return;
            if (GameDataMgr.Get() != null)
                m_Instance.m_uiStageNumber.text = string.Format(STAGE_STRING_FORMAT, GameDataMgr.Get().m_iCurrentStageLevel);
        }

        private void Start()
        {
            m_Instance = this;
            m_uiStageNumber = gameObject.GetComponent<UILabel>();
            m_Instance.m_uiStageNumber.text = string.Format(STAGE_STRING_FORMAT, 1);
        }
    }
}
