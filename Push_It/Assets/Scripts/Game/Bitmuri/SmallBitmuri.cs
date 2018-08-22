using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class SmallBitmuri : Bitmuri
    {
        public override void SetUp(Transform _transStart, Transform _transEnd)
        {
            base.SetUp(_transStart, _transEnd);

            m_fMoveSpeed = 0.3f;
            gameObject.SetActive(false);
        }
    }
}
