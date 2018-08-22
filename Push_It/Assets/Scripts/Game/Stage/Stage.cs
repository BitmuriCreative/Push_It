using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class Stage : MonoBehaviour
    {
        static public readonly string OBJ_NAME       = "Stage";

        static public readonly string FURNITURE_PATH = "Prefabs/Furniture";
        static public readonly string DECO_PATH      = "Prefabs/DecoFurniture";

        static public readonly string BITMURI_PATH    = "Prefabs/Bitmuri";

        static public Stage Create(int _iFurnitureLevel, int _iDoorCount)
        {
            GameObject obj = new GameObject();
            if (obj == null) return null;
            obj.name = OBJ_NAME;
            obj.transform.SetParent(null);
            obj.transform.Reset();

            Stage stage = obj.AddComponent<Stage>();
            if(stage == null)
            {
                Destroy(obj);
                return null;
            }

            stage.PrefabsLoad();
            stage.InitFurniture(_iFurnitureLevel, _iDoorCount);
            stage.BitmuriSetting();

            return stage;
        }

        private PlayFurniture[] m_prefabsFurniture = null;
        private Bitmuri[]       m_prefabsBitmuri   = null;

        //실제 가구 객체 담아놈.
        private List<PlayFurniture> m_ListPlayFurniture = new List<PlayFurniture>();
        //실제 빗무리 객체 담아놈.
        private List<Bitmuri>       m_ListBitmuri       = new List<Bitmuri>();

        private int m_iStageDoorCount        = 0;   //stage 안에 닫아야하는 문에 개수 체크.
        private int m_iAgainOpenDoorCount    = 0;   //언제부터 다시 문이 열릴것인지.
        private int m_iCurrentCloseDoorCount = 0;   //현재 닫은 문의 개수.
        //private int m_iPlusCount             = 0;   //동물이 열고 간 문 개수.

        private void PrefabsLoad()
        {
            m_prefabsFurniture = Resources.LoadAll<PlayFurniture>(FURNITURE_PATH);
            m_prefabsBitmuri    = Resources.LoadAll<Bitmuri>(BITMURI_PATH);
        }

        public void InitFurniture(int _iFurnitureLevel, int _iDoorCount)
        {
            if (m_prefabsFurniture.Length == 0) return;

            //0:top, 1:bottom
            Dictionary<int, List<PlayFurniture>> tempFurniture = new Dictionary<int, List<PlayFurniture>>();
            if (_iDoorCount == -1) return;

            tempFurniture.Add(0, new List<PlayFurniture>());
            tempFurniture.Add(1, new List<PlayFurniture>());

            foreach (PlayFurniture temp in m_prefabsFurniture)
            {
                if (temp == null) continue;
                if (temp.GetDoorCount > _iDoorCount) continue;
                switch (temp.m_eAreaPosition)
                {
                    case eAreaPosition.top:
                        tempFurniture[0].Add(temp);
                        break;
                    case eAreaPosition.bottom:
                        tempFurniture[1].Add(temp);
                        break;
                    default:
                        break;
                }
            }

            FurnitureSetting(_iFurnitureLevel, tempFurniture);
        }

        //게임용 가구 배치
        private void FurnitureSetting(int _iFurnitureLevel, Dictionary<int, List<PlayFurniture>> _dicFurniture)
        {
            int iMaxCount = 0;          //한 스테이지에 배치되는 전체 가구 개수.
            int iBottomCount = 0;       //밑 부분 가구 배치 개수.
            int iFurnitureMaxCount = 3; //bottom, top 각각 가구 최대 배치 개수.
            switch(_iFurnitureLevel)
            {
                case 4:
                    {
                        iMaxCount = 4;
                        iBottomCount = Random.Range(1, iFurnitureMaxCount + 1);
                    }
                    break;
                case 5:
                    {
                        iMaxCount = 5;
                        iBottomCount = Random.Range(2, iFurnitureMaxCount + 1);
                    }
                    break;
                case 6:
                    {
                        iMaxCount = 6;
                        iBottomCount = iFurnitureMaxCount;
                    }
                    break;
                case 7:
                    {
                        iMaxCount = Random.Range(5, 6 + 1);
                        iBottomCount = (iMaxCount == 5) ? Random.Range(2, iFurnitureMaxCount + 1) : iFurnitureMaxCount;
                    }
                    break;
            }

            //밑에 부터 셋팅.
            FurnitureSetting(_dicFurniture[1], iBottomCount, eAreaPosition.bottom);

            int iTopCount = iMaxCount - iBottomCount;
            FurnitureSetting(_dicFurniture[0], iTopCount, eAreaPosition.top);

            m_iAgainOpenDoorCount = m_iStageDoorCount - 2;
        }

        private void FurnitureSetting(List<PlayFurniture> _listObj, int _iCount, eAreaPosition _ePosition)
        {
            int iIndex = 0;
            for (int iLoop = 0; iLoop < _iCount; ++iLoop)
            {
                int prefabIdx = Random.Range(0, _listObj.Count);
                PlayFurniture obj = Instantiate(_listObj[prefabIdx]);
                if (obj == null) return;

                m_iStageDoorCount += obj.GetDoorCount;

                if (_iCount == 3)
                {
                    FurnitureArea.Get().FixingAreaJoint(obj, iIndex, _ePosition);
                    iIndex += 2;
                    m_ListPlayFurniture.Add(obj);
                    continue;
                }
                else
                {
                    FurnitureArea.Get().RandomAreaJoint(obj, _ePosition);
                    m_ListPlayFurniture.Add(obj);
                }
            }
        }

        private void BitmuriSetting()
        {
            foreach(Bitmuri bitmuri in m_prefabsBitmuri)
            {
                if (bitmuri == null) continue;
                Bitmuri obj = Instantiate(bitmuri);
                BitmuriArea.Attach(obj.transform);
                obj.SetUp(BitmuriArea.GetTransStart, BitmuriArea.GetTransEnd);
                m_ListBitmuri.Add(obj);
            }
        }

        public bool StageUpdate(eAreaNumber _eAreaNumber, eAreaPosition _eAreaPosition)
        {
            ++m_iCurrentCloseDoorCount;
            
            CloseDoorCount.StageCloseDoorCountUpdate();

            if ((GameDataMgr.Get().GetTouchCount() > m_iStageDoorCount)
                && (m_iCurrentCloseDoorCount >= m_iAgainOpenDoorCount))
            {
                RandomOpenDoor(_eAreaNumber, _eAreaPosition);
                return false;
            }

            if (m_iCurrentCloseDoorCount >= (GameDataMgr.Get().GetTouchCount()/* + m_iPlusCount*/))
            {
                m_iCurrentCloseDoorCount = 0;
                m_iStageDoorCount        = 0;
                m_iAgainOpenDoorCount    = 0;
                return true;
            }

            return false;
        }

        private void RandomOpenDoor(eAreaNumber _eAreaNumber, eAreaPosition _eAreaPosition)
        {
            List<PlayFurniture> listTemp = new List<PlayFurniture>();
            List<int> listTemp_index = new List<int>();
            int iCount = 0;
            foreach (PlayFurniture obj in m_ListPlayFurniture)
            {
                if (obj == null) continue;
                if ((obj.m_eAreaNumber == _eAreaNumber) && (obj.m_eAreaPosition == _eAreaPosition)) continue;
                if (!obj.CloseDoorCheck()) continue;
                listTemp.Add(obj);
                listTemp_index.Add(iCount++);
            }

            if (listTemp.Count == 0) return;
            for (int iLoop = 0; iLoop < listTemp_index.Count; ++iLoop)
            {
                int random = Random.Range(0, listTemp_index.Count);
                if (listTemp[listTemp_index[random]].RandomOpenDoor())
                {
                    //문을 열어줬다.
                    ++m_iStageDoorCount;
                    return;
                }

                listTemp_index.RemoveAt(random);
                --iLoop;
            }
        }

        private int m_iTallBitmuriAppearCount  = 0;
        private int m_iSmallBitmuriAppearCount = 0;
        private int m_iFatBitmuriAppearCount   = 0;

        public void BitmuriAppearSetting()
        {
            //등장 할껀지 안할껀지. 0 -> 등장, 1 -> 등장 안함.
            int iRandom = Random.Range(0, 1 + 1);  
            if ((GameDataMgr.Get().m_iCurrentStageLevel >= 10) && (GameDataMgr.Get().m_iCurrentStageLevel <= 25))
            {
                if (GameDataMgr.Get().GetBitmuriAppearCount(0) == -1) return;
                if (GameDataMgr.Get().GetBitmuriAppearCount(0) > m_iTallBitmuriAppearCount)
                {
                    int iAppearCount = GameDataMgr.Get().m_iCurrentStageLevel - GameDataMgr.Get().GetBitmuriAppearCount(0);
                    if ((GameDataMgr.Get().GetBitmuriAppearCount(0) > iAppearCount) || (iRandom == 0))
                    {
                        ++m_iTallBitmuriAppearCount;
                        BitmuriAppear(eBitmuriName.tall_bitmuri);
                    }
                }
            }
            else if ((GameDataMgr.Get().m_iCurrentStageLevel >= 26) && (GameDataMgr.Get().m_iCurrentStageLevel <= 50))
            {
                if (GameDataMgr.Get().GetBitmuriAppearCount(1) == -1) return;
                if (GameDataMgr.Get().GetBitmuriAppearCount(1) > m_iSmallBitmuriAppearCount)
                {
                    int iAppearCount = GameDataMgr.Get().m_iCurrentStageLevel - GameDataMgr.Get().GetBitmuriAppearCount(1);
                    if ((GameDataMgr.Get().GetBitmuriAppearCount(1) > iAppearCount) || (iRandom == 0))
                    {
                        ++m_iSmallBitmuriAppearCount;
                        BitmuriAppear(eBitmuriName.small_bitmuri);
                    }
                }
            }
            else if ((GameDataMgr.Get().m_iCurrentStageLevel >= 51) && (GameDataMgr.Get().m_iCurrentStageLevel <= 100))
            {
                if (GameDataMgr.Get().GetBitmuriAppearCount(2) == -1) return;
                if (GameDataMgr.Get().GetBitmuriAppearCount(2) > m_iFatBitmuriAppearCount)
                {
                    int iAppearCount = GameDataMgr.Get().m_iCurrentStageLevel - GameDataMgr.Get().GetBitmuriAppearCount(2);
                    if ((GameDataMgr.Get().GetBitmuriAppearCount(2) > iAppearCount) || (iRandom == 0))
                    {
                        ++m_iFatBitmuriAppearCount;
                        BitmuriAppear(eBitmuriName.fat_bitmuri);
                    }
                }
            }
        }

        private void BitmuriAppear(eBitmuriName _eBitmuriName)
        {
            Bitmuri bitmuri = null;
            foreach (Bitmuri temp in m_ListBitmuri)
            {
                if (temp == null) continue;
                if (temp.m_eBitmuriName == _eBitmuriName)
                {
                    bitmuri = temp;
                    break;
                }
            }

            //List<PlayFurniture> listTempFurniture = new List<PlayFurniture>();
            //foreach (PlayFurniture obj in m_ListPlayFurniture)
            //{
            //    if (obj == null) continue;
            //    if (obj.m_eAreaPosition != eAreaPosition.bottom) continue;
            //    if (!obj.CloseDoorCheck()) continue;

            //    listTempFurniture.Add(obj);
            //}

            bitmuri.Move();

            //if (listTempFurniture.Count != 0)
            //    StartCoroutine(Co_Destance(bitmuri, listTempFurniture, 0.3f));//0.3
        }

        //IEnumerator Co_Destance(Bitmuri _targetBitmuri, List<PlayFurniture> _targetFurniture, float _destans)
        //{
        //    int iIndex = 0;
        //    bool isTemp = true;
        //    while(isTemp)
        //    {
        //        float destans = Vector3.Distance(_targetBitmuri.transform.position, _targetFurniture[iIndex].transform.position);
        //        if(destans < _destans)//가까워졌다.
        //        {
        //            if(_targetFurniture.Count == 1)//가구 1개일때 닫힌문 있으면 무조건 열어준다.
        //            {
        //                if (_targetFurniture[iIndex].OpenDoor())
        //                {
        //                    ++m_iPlusCount;
        //                    CloseDoorCount.StageCloseDoorCountUpdate(m_iPlusCount);
        //                }

        //                isTemp = false;
        //            }
        //            else
        //            {
        //                //마지막 문일때는 무조건 열어준다.
        //                if (iIndex == (_targetFurniture.Count - 1))
        //                {
        //                    if (_targetFurniture[iIndex].OpenDoor())
        //                    {
        //                        ++m_iPlusCount;
        //                        CloseDoorCount.StageCloseDoorCountUpdate(m_iPlusCount);
        //                    }

        //                    isTemp = false;
        //                }
        //                else//아닐 경우에는 열어주거나 지나가거나.
        //                {
        //                    int random = Random.Range(0, 1 + 1);
        //                    if (random == 0)//열어준다.
        //                    {
        //                        if (_targetFurniture[iIndex].OpenDoor())
        //                        {
        //                            ++m_iPlusCount;
        //                            CloseDoorCount.StageCloseDoorCountUpdate(m_iPlusCount);
        //                            isTemp = false;
        //                        }
        //                    }

        //                    ++iIndex;
        //                }
        //            }
        //        }
        //        yield return null;
        //    }
        //}

        public void BitmuriAppearCountClear()
        {
            m_iTallBitmuriAppearCount  = 0;
            m_iSmallBitmuriAppearCount = 0;
            m_iFatBitmuriAppearCount   = 0;
        }

        public void Clear()
        {
            StopAllCoroutines();

            foreach (Bitmuri bitmuri in m_ListBitmuri)
            {
                if (bitmuri == null) continue;
                bitmuri.Clear();
            }
            
            m_ListPlayFurniture.Clear();
            m_iStageDoorCount        = 0;
            m_iAgainOpenDoorCount    = 0;
            m_iCurrentCloseDoorCount = 0;
            //m_iPlusCount             = 0;
        }
    }
}
