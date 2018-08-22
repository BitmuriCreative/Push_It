using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Push_It
{
    public class BestStageNumber : MonoBehaviour
    {
        static private readonly string STAGE_STRING_FORMAT = "{0}/100";

        private void Start()
        {
            UILabel label = gameObject.GetComponent<UILabel>();
            if (label == null) return;
            if (SaveDataMgr.Get() != null)
                label.text = string.Format(STAGE_STRING_FORMAT, SaveDataMgr.Current.m_iBestStageNumber);
        }
    }
}