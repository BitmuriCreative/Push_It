using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class PlayFurniture : Furniture
    {
        [Tooltip("가구에 달린 문들")]
        [SerializeField]
        protected Door[] m_Doors = null;
        public Door[] GetDoors { get { return (m_Doors == null) ? null : m_Doors; } }
        public int GetDoorCount { get { return (m_Doors == null) ? 0 : m_Doors.Length; } }

        public eAreaNumber m_eAreaNumber = eAreaNumber.pos_0;

        //가구 바디 콜라이더.
        protected Collider m_Collider = null;

        protected virtual void Awake()
        {
            m_Collider = gameObject.GetComponent<Collider>();
        }

        //하나라도 닫힌게 있는지 체크.
        public bool CloseDoorCheck()
        {
            for (int iLoop = 0; iLoop < m_Doors.Length; ++iLoop)
            {
                if (!m_Doors[iLoop].IsOpenDoorCheck) return true;
            }

            return false;
        }

        public bool RandomOpenDoor()
        {
            int random = Random.Range(0, m_Doors.Length);
            //닫힌문 열어준다.
            if (!m_Doors[random].IsOpenDoorCheck)
            {
                m_Doors[random].OpenDoor();
                m_Collider.enabled = true;

                return true;
            }

            return false;
        }

        public bool OpenDoor()
        {
            for (int iLoop = 0; iLoop < m_Doors.Length; ++iLoop)
            {
                if (!m_Doors[iLoop].IsOpenDoorCheck)
                {
                    m_Doors[iLoop].OpenDoor();
                    m_Collider.enabled = true;
                    return true;
                }
            }

            return false;
        }

        public void OnPressed()
        {
            SoundMgr.Get().EffectPlay(eEffectSound.door_close, 0.2f);

            //터치 시 순서대로 문 닫아준다.
            for (int iLoop = 0; iLoop < m_Doors.Length; ++iLoop)
            {
                if (m_Doors[iLoop] == null) continue;
                if (!m_Doors[iLoop].IsOpenDoorCheck) continue;
                m_Doors[iLoop].CloseDoor();
                break;
            }

            //문이 다 닫혔는지 체크.
            int temp = 0;
            for (int iLoop = 0; iLoop < m_Doors.Length; ++iLoop)
            {
                if (m_Doors[iLoop] == null) continue;
                if (m_Doors[iLoop].IsOpenDoorCheck) break;
                ++temp;
            }

            if (temp >= m_Doors.Length)
                m_Collider.enabled = false;

            GameMgr.Get().GameUpdate(m_eAreaNumber, m_eAreaPosition);
        }
    }
}