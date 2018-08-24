using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Push_It
{
    public class SoundMgr : MonoDontDestroySingleton<SoundMgr>
    {
        public AudioSource m_sourceBgm        = null;
        public AudioSource m_sourceEffect     = null;

        public AudioClip[] m_clipEffectSounds = null;

        public void EffectPlay(eEffectSound _eEffectSound, float _fVolume = 1f, bool _isLoop = false)
        {
            m_sourceEffect.clip   = m_clipEffectSounds[(int)_eEffectSound];
            m_sourceEffect.loop   = _isLoop;
            m_sourceEffect.volume = _fVolume;
            m_sourceEffect.Play();
        }

        public void BGMPlay()
        {
            m_sourceBgm.Play();
        }

        public void BGMStop()
        {
            m_sourceBgm.Stop();
        }
    }
}