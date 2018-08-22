using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoDontDestroySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static protected T m_Instance = null;

    static public T Get()
    {
        return (T)m_Instance;
    }

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this as T;

        DontDestroyOnLoad(transform.root.gameObject);
    }
}
