using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutSideLibrary : MonoBehaviour
{
    private OutsideLibraryImpl _impl;

    private void Awake()
    {
        CreateImpl();
    }

    private void CreateImpl()
    {
#if !UNITY_EDITOR && UNITY_IOS
        _impl = new OutsideLibraryIOS();
#elif !UNITY_EDITOR && UNITY_ANDROID
        _impl = new OutsideLibraryAndroid();
#else
        _impl = new OutsideLibraryImpl();
#endif
    }

    public int Sum(int a, int b)
    {
        return _impl.Sum(a, b);
    }

}


public class OutsideLibraryImpl
{
    public virtual int Sum(int a, int b)
    {
        Debug.Log("Untiy3d editor can not do anything.");
        return 0;
    }
}

public class OutsideLibraryAndroid : OutsideLibraryImpl
{
    public override int Sum(int a, int b)
    {
        using (AndroidJavaClass ajc = new AndroidJavaClass("org.jsoup"))
        {
            return ajc.CallStatic<int>("testSum", a, b);
        }
    }
}
