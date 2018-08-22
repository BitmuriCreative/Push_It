using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class FurnitureArea : MonoSingleton<FurnitureArea>
    {
        public Transform[] m_transBottom = null;
        public Transform[] m_transTop    = null;

        private List<int> m_ListBottomIndex = new List<int>();
        private List<int> m_ListTopIndex    = new List<int>();
        
        private void Start()
        {
            InitIndex();
        }

        private void InitIndex()
        {
            m_ListBottomIndex.Clear();
            m_ListTopIndex.Clear();

            for (int iLoop = 0; iLoop < m_transBottom.Length; ++iLoop)
            {
                m_ListBottomIndex.Add(iLoop);
            }

            for (int iLoop = 0; iLoop < m_transTop.Length; ++iLoop)
            {
                m_ListTopIndex.Add(iLoop);
            }
        }

        public int GetFurnitureCount(eAreaPosition _ePosition)
        {
            int count = 0;
            switch (_ePosition)
            {
                case eAreaPosition.top:
                    {
                        foreach (Transform trans in m_transTop)
                        {
                            if (trans == null) continue;
                            if (trans.childCount == 0) continue;
                            ++count;
                        }
                    }
                    break;
                case eAreaPosition.bottom:
                    {
                        foreach (Transform trans in m_transBottom)
                        {
                            if (trans == null) continue;
                            if (trans.childCount == 0) continue;
                            ++count;
                        }
                    }
                    break;
            }

            return count;
        }

        //랜덤 인덱스
        public void RandomAreaJoint(PlayFurniture _object, eAreaPosition _ePosition)
        {
            switch (_ePosition)
            {
                case eAreaPosition.top:
                    RandomAreaJoint(_object, m_transTop, m_ListTopIndex);
                    break;
                case eAreaPosition.bottom:
                    RandomAreaJoint(_object, m_transBottom, m_ListBottomIndex);
                    break;
            }
        }

        private void RandomAreaJoint(PlayFurniture _object, Transform[] _transParent, List<int> _listIndex)
        {
            int random = Random.Range(0, _listIndex.Count);
            int trueIndex = 0;

            Vector3 vecOldPosition = _object.transform.position;
            _object.transform.SetParent(_transParent[_listIndex[random]]);
            _object.transform.OldReset(vecOldPosition);
            _object.m_eAreaNumber = (eAreaNumber)_listIndex[random];
            trueIndex = _listIndex[random];
            _listIndex.RemoveAt(random);

            switch (trueIndex)
            {
                case 0:
                    if (_listIndex.Contains(1))
                        _listIndex.RemoveAt(_listIndex.FindIndex(idx => (idx == 1)));
                    break;
                case 1:
                    if (_listIndex.Contains(0))
                        _listIndex.RemoveAt(_listIndex.FindIndex(idx => (idx == 0)));
                    if (_listIndex.Contains(2))
                        _listIndex.RemoveAt(_listIndex.FindIndex(idx => (idx == 2)));
                    break;
                case 2:
                    if (_listIndex.Contains(1))
                        _listIndex.RemoveAt(_listIndex.FindIndex(idx => (idx == 1)));
                    if (_listIndex.Contains(3))
                        _listIndex.RemoveAt(_listIndex.FindIndex(idx => (idx == 3)));
                    break;
                case 3:
                    if (_listIndex.Contains(2))
                        _listIndex.RemoveAt(_listIndex.FindIndex(idx => (idx == 2)));
                    if (_listIndex.Contains(4))
                        _listIndex.RemoveAt(_listIndex.FindIndex(idx => (idx == 4)));
                    break;
                case 4:
                    if (_listIndex.Contains(3))
                        _listIndex.RemoveAt(_listIndex.FindIndex(idx => (idx == 3)));
                    break;
            }
        }

        //고정 인덱스
        public void FixingAreaJoint(PlayFurniture _object, int _iIndex, eAreaPosition _ePosition)
        {
            switch (_ePosition)
            {
                case eAreaPosition.top:
                    FixingAreaJoint(_object, _iIndex, m_transTop, m_ListTopIndex);
                    break;
                case eAreaPosition.bottom:
                    FixingAreaJoint(_object, _iIndex, m_transBottom, m_ListBottomIndex);
                    break;
            }
        }

        private void FixingAreaJoint(PlayFurniture _object, int _iIndex, Transform[] _transParent, List<int> _listIndex)
        {
            Vector3 vecOldPosition = _object.transform.position;
            _object.transform.SetParent(_transParent[_listIndex[_iIndex]]);
            _object.transform.OldReset(vecOldPosition);
            _object.m_eAreaNumber = (eAreaNumber)_listIndex[_iIndex];
        }

        public void AreaReset()
        {
            InitIndex();

            foreach (Transform trans in m_transBottom)
            {
                if (trans == null) continue;
                if (trans.childCount == 0) continue;
                Destroy(trans.GetChild(0).gameObject);
            }

            foreach (Transform trans in m_transTop)
            {
                if (trans == null) continue;
                if (trans.childCount == 0) continue;
                Destroy(trans.GetChild(0).gameObject);
            }
        }
    }
}
