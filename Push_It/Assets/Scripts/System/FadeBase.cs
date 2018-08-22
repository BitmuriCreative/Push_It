using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBase : MonoBehaviour
{
    public UISprite m_uiFade;
    
    protected float m_fTime     = 0f;
    protected float m_fFadeTime = 1f;

    protected float m_fDuration = 1f;
    protected float m_fNextTime = 0.3f;

    protected virtual void Start()
    {
        StartCoroutine(Co_FadeIn());
    }

    protected virtual IEnumerator Co_FadeIn()
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

        yield return new WaitForSeconds(m_fDuration);

        StartCoroutine(Co_FadeOut());
    }

    protected virtual IEnumerator Co_FadeOut()
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

        gameObject.SendMessage("NextScene", SendMessageOptions.RequireReceiver);
    }
}
