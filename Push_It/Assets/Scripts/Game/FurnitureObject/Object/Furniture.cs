using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class Furniture : MonoBehaviour
    {
        //가구 공통
        [Tooltip("가구가 위쪽에 위치하는지 밑쪽에 위치하는지 구분")]
        public eAreaPosition m_eAreaPosition = eAreaPosition.top;
    }
}