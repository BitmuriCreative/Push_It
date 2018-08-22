using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : FadeBase
{
    public Collider m_FadeCollider = null;
    public Collider m_ClickCollider = null;

    public string m_strAnimName = string.Empty;

    private StartAnim m_Anim = null;

    //false = in, true = out
    private bool m_isFadeCheck = false;

    protected override void Start()
    {
        m_Anim = gameObject.GetComponent<StartAnim>();

        FadeEvent();
    }

    public void FadeEvent()
    {
        if (m_FadeCollider != null)
            m_FadeCollider.enabled = true;

        if (!m_isFadeCheck)
        {
            StartCoroutine(Co_FadeIn());
        }
        else
        {
            if (m_ClickCollider != null)
                m_ClickCollider.enabled = false;

            StartCoroutine(Co_FadeOut());
        }
    }

    protected override IEnumerator Co_FadeIn()
    {
        //FadeIn 
        Color color = m_uiFade.color;
        while (color.a > 0)
        {
            m_fTime += Time.deltaTime / m_fFadeTime;
            color.a = Mathf.Lerp(1, 0, m_fTime);
            m_uiFade.color = color;
            yield return null;
        }
        m_uiFade.alpha = 0;
        m_fTime = 0;
        yield return new WaitForSeconds(m_fNextTime);

        m_isFadeCheck = true;

        if (m_FadeCollider != null)
            m_FadeCollider.enabled = false;

        if (m_Anim != null)
            m_Anim.PlayAnim(m_strAnimName);
    }

    protected override IEnumerator Co_FadeOut()
    {
        //FadeOut 
        Color color = m_uiFade.color;
        while (color.a < 1f)
        {
            m_fTime += Time.deltaTime / m_fFadeTime;
            color.a = Mathf.Lerp(0, 1, m_fTime);
            m_uiFade.color = color;
            yield return null;
        }
        m_uiFade.alpha = 1;
        m_fTime = 0;
        yield return new WaitForSeconds(m_fNextTime);

        m_isFadeCheck = false;

        if (m_FadeCollider != null)
            m_FadeCollider.enabled = false;

        gameObject.SendMessage("NextScene", SendMessageOptions.RequireReceiver);
    }
}
