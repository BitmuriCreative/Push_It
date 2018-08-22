using UnityEngine;
using UnityEngine.SceneManagement;

public class AddScene : MonoBehaviour
{
    public string[] m_strAddScene = null;

    private void Awake()
    {
        if (m_strAddScene == null) return;

        for (int iLoop = 0; iLoop < m_strAddScene.Length; ++iLoop)
        {
            SceneManager.LoadScene(m_strAddScene[iLoop], LoadSceneMode.Additive);
        }
    }
}
