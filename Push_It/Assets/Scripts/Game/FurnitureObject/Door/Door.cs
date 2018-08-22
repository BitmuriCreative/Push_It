using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class Door : MonoBehaviour
    {
        public enum eDoor
        {
            Open,
            Close,
        }

        [Tooltip("Index 0:열린문 1:닫힌문")]
        public GameObject[] m_Door = null;

        //열려 있으면 true 닫혀있으면 false.
        private bool m_isOpen = false;
        public bool IsOpenDoorCheck { get { return m_isOpen; } }

        private void Start()
        {
            OpenDoor();
        }

        public void OpenDoor()
        {
            m_isOpen = true;
            DoorActive(true);
        }

        public void CloseDoor()
        {
            m_isOpen = false;
            DoorActive(false);
        }

        private void DoorActive(bool _isEnble)
        {
            m_Door[(int)eDoor.Open].SetActive(_isEnble);
            m_Door[(int)eDoor.Close].SetActive(!_isEnble);
        }
    }
}