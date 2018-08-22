using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class BestScoreLabel : MonoBehaviour
    {
        static private readonly string SCORE_STRING_FORMAT = "{0:n0}";

        private void Start()
        {
            UILabel label = gameObject.GetComponent<UILabel>();
            if (label == null) return;
            if (SaveDataMgr.Get() != null)
                label.text = string.Format(SCORE_STRING_FORMAT, SaveDataMgr.Current.m_iBestTotalScore);
        }
    }
}
