using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Push_It
{
    public class SceneMgr : MonoBehaviour
    {
        static private readonly string STAGE_SCENE = "3-1.Stage";
        public string m_strScene = string.Empty;
        
        private Scene m_Scene;

        private void Start()
        {
            if (Screen.sleepTimeout != SleepTimeout.NeverSleep)
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
                Screen.SetResolution(1920, 1080, true);
            }

            if (SaveDataMgr.Get() != null)
                SaveDataMgr.Get().Load();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(Popup.Find("quit") == null)
                {
                    //시간 멈춤.
                    Time.timeScale = 0;

                    PopupQuit quit = PopupQuit.Open("quit");
                    quit._onYes += () =>
                    {
#if (UNITY_ANDROID && UNITY_EDITOR)
                        UnityEditor.EditorApplication.isPlaying = false;

#elif (UNITY_ANDROID)
                    Application.Quit();
#endif
                    };

                    quit._onNo += () =>
                    {
                        //원상 복구.
                        Time.timeScale = 1;
                    };
                }
            }
        }

        public void MoveScene(string _strScene)
        {
            m_strScene = _strScene;
            NextScene();
        }

        public void NextScene()
        {
            m_Scene = SceneManager.GetActiveScene();
            SceneManager.sceneLoaded += Loaded;
            if (m_strScene != STAGE_SCENE)
            {
                SceneManager.LoadScene(m_strScene, LoadSceneMode.Additive);
            }
            else
            {
                StartCoroutine(Co_Loading());
            }
        }

        void Loaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (m_strScene == scene.name)
            {
                SceneManager.SetActiveScene(scene);
                SceneManager.UnloadSceneAsync(m_Scene);
                SceneManager.sceneLoaded -= Loaded;
            }
        }

        IEnumerator Co_Loading()
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(m_strScene, LoadSceneMode.Additive);
            async.allowSceneActivation = false;
            if (Popup.Find("Loading Scene") == null)
            {
                PopupLoading.Open("Prefabs/Popup/PopupLoading", "Loading Scene");
            }

            float fTime = 0;
            while (!async.isDone)
            {
                fTime += Time.deltaTime;
                if (async.progress >= 0.9f)
                {
                    if(fTime > 3f)
                    {
                        PopupLoading.Close("Loading Scene");
                        async.allowSceneActivation = true;
                    }
                }

                yield return null;
            }
        }
    }
}
