using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class VersionCheckMgr : MonoDontDestroySingleton<VersionCheckMgr>
    {
        /*static private readonly string URL = "https://play.google.com/store/apps/details?id=com.BitmuriCreative.Push_It"; //데이터를 가져올 URL
        static private readonly string MARKET_URL = "market://details?id=com.BitmuriCreative.Push_It"; //마켓오픈 URL
        static private readonly string VERSION_PATTERN = "^[0-9]{1}.[0-9]{1}.[0-9]{1}$";

        private UILabel m_uiVersion;     //버전을 표시할 텍스트

        //유니티 자체에서 bundleIdentifier를 읽을수도 있지만, 이렇게 읽을 수 도 있다.
        public string _bundleIdentifier { get { return URL.Substring(URL.IndexOf("details"), URL.LastIndexOf("details") + 1); } }
        
        public bool m_isSamePlayStoreVersion = false;

        private void Start()
        {
            m_uiVersion = gameObject.GetComponent<UILabel>();
            if(m_uiVersion == null)
            {
                m_uiVersion = gameObject.AddComponent<UILabel>();
                if(m_uiVersion != null)
                {
                    m_uiVersion.text = Application.version;
                    m_uiVersion.color = Color.red;
                    m_uiVersion.transform.localPosition = new Vector3(940f, 320f, 0f);
                }  
            }

            StartCoroutine(PlayStoreVersionCheck());
        }

        /// <summary>
        /// 버전체크를 하여, 강제업데이트를 체크한다.
        /// </summary>
        /// <returns></returns>
        private IEnumerator PlayStoreVersionCheck()
        {
            m_isSamePlayStoreVersion = false;

            WWW www = new WWW(URL);
            yield return www;
            
            //인터넷 연결 에러가 없다면, 
            if (www.error == null)
            {
                int index = www.text.IndexOf("\"htlgb\">"); //<div class=\"BgcNfc\">Current Version</div><div><span class=\"htlgb\"> //"</span></div>"
                if (index == -1) yield return null;
                string versionText = www.text.Substring(index, 30);

                //플레이스토어에 올라간 APK의 버전을 가져온다.
                int softwareVersion = versionText.IndexOf(">");
                string playStoreVersion = versionText.Substring(softwareVersion + 1, Application.version.Length + 1);

                //HtmlDocument doc = new HtmlDocument();
                //doc.LoadHtml(www.url);
                //HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//htlgb");
                //string s = node[0].InnerText;

                //HtmlDocument doc = new HtmlDocument();
                //doc.LoadHtml(www.url);
                //Elements Version = doc.Select(".htlgb");

                //var value = doc.DocumentNode
                //             .SelectNodes("//td/input")
                //             .First()
                //             .Attributes["value"].Value;

                //// With LINQ 
                //var nodes = doc.DocumentNode.Descendants("input").Select(y => y.Descendants()
                // .Where(x => x.Attributes["class"].Value == "box"))
                // .ToList();

                //HtmlAgilityPack
                //Document doc = NSoupClient.Connect(www.url).Get();
                //Elements Version = doc.Select(".htlgb");
                //string VersionMarket = null;
                //for (int i = 0; i < 10; i++)
                //{
                //    VersionMarket = Version.Text;
                //    if (Pattern.matches("^[0-9]{1}.[0-9]{1}.[0-9]{1}$", VersionMarket))
                //    {
                //        break;
                //    }
                //}

                //버전이 같다면,
                if (playStoreVersion.Trim().Equals(Application.version))
                {
                    //게임 씬으로 넘어간다.
                    Debug.LogWarning("true : " + playStoreVersion + " : " + Application.version);

                    //버전이 같다면, 앱을 넘어가도록 한다.
                    m_isSamePlayStoreVersion = true;
                }
                else
                {
                    //버전이 다르므로, 마켓으로 보낸다.
                    Debug.LogWarning("false : " + playStoreVersion + " : " + Application.version);
                    
                    PopupUpdate tempUpdate = PopupUpdate.Open("Update", "최신 버전이 나왔습니다!\n업데이트를 진행해주세요!");
                    tempUpdate._onYes += () =>
                    {
#if (UNITY_ANDROID && UNITY_EDITOR)
                        Application.OpenURL(URL);

#elif (UNITY_ANDROID)
                        Application.OpenURL(m_strMarketUrl);
#endif
                    };

                    tempUpdate._onNo += () =>
                    {
#if (UNITY_ANDROID && UNITY_EDITOR)
                        UnityEditor.EditorApplication.isPlaying = false;

#elif (UNITY_ANDROID)
                        Application.Quit();
#endif
                    };
                }
            }
            else
            {
                //인터넷 연결 에러시
                Debug.LogWarning(www.error);
                PopupConfirm tempConfirm = PopupConfirm.Open("Confirm", "인터넷 연결이 되어 있지 않아요!\n인터넷 연결을 확인하세요!!");
                tempConfirm._onConfirm += () =>
                {
#if (UNITY_ANDROID && UNITY_EDITOR)
                    UnityEditor.EditorApplication.isPlaying = false;

#elif (UNITY_ANDROID)
                    Application.Quit();
#endif
                };
            }
        }

        /// <summary>
        /// 업데이트 팝업에서 업데이트 여부를 체크한다.
        /// </summary>
        public void Call_PlayStoreVersionCheck()
        {
            StartCoroutine(PlayStoreVersionCheck());
        }*/
    }
}
