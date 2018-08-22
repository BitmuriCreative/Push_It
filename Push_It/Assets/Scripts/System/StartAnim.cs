using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnim : MonoBehaviour
{
    public string m_strAnimName = string.Empty;
    public Animator m_Anim = null;

    public void PlayAnim(string _strAnimName)
    {
        m_strAnimName = _strAnimName;
        OnPlayAnim();
    }

    public void OnPlayAnim()
    {
        if (m_Anim == null) return;
        m_Anim.Play(m_strAnimName);
    }

    public void OnStopAnim()
    {
        if (m_Anim == null) return;
        m_Anim.StopPlayback();
    }
}
