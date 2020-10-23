using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicHandler : MonoBehaviour 
{
    [SerializeField]private AudioClip[] m_AudioClips;
    [SerializeField]private AudioSource m_AudioSource;
    [SerializeField]private Image m_BGImage;
    [SerializeField]private Text m_MusicTitle;

    [Header("Options")]
    [SerializeField]private bool m_Off;
    [SerializeField]private bool m_Shuffle;
    [SerializeField]private float m_InfoTime;

    int m_CurrentClip;

    bool m_NewClip;
    float m_InfoTimeC;

    Color m_ImgColor;
    Color m_TextColor;

    void Start()
    {
        if (!m_Off)
        {
            if (m_Shuffle)
            {
                m_CurrentClip = Random.Range(0, m_AudioClips.Length);
                StartClip(m_CurrentClip);
            }
            else
            {
                StartClip(0);
            }
            m_ImgColor = m_BGImage.color;
            m_TextColor = m_MusicTitle.color;
            m_InfoTimeC = m_InfoTime;
            m_NewClip = true;
        }
    }
	
	void Update () 
    {
        if (!m_AudioSource.isPlaying && !m_Off)
        {
            if (m_CurrentClip != m_AudioClips.Length - 2)
            {
                if (m_Shuffle)
                {
                    m_CurrentClip = Random.Range(0, m_AudioClips.Length);
                }
                else
                {
                    m_CurrentClip += 1;
                }
                StartClip(m_CurrentClip);
                m_NewClip = true;
            }
        }

        if (m_NewClip)
        {
            m_ImgColor.a += 0.2f * Time.deltaTime;
            m_TextColor.a += 0.3f * Time.deltaTime;
            m_BGImage.rectTransform.sizeDelta = new Vector2(m_AudioClips[m_CurrentClip].name.Length * 22, 50);

            m_InfoTimeC -= 1 * Time.deltaTime;
            if (m_InfoTimeC <= 0)
            {
                m_NewClip = false;
                m_InfoTimeC = m_InfoTime;
            }
        }
        else
        {
            if (m_ImgColor.a > 0)
            {
                m_ImgColor.a -= 0.2f * Time.deltaTime;
            }
            if (m_TextColor.a > 0)
            {
                m_TextColor.a -= 0.3f * Time.deltaTime;
            }
        }

        m_MusicTitle.color = m_TextColor;
        m_BGImage.color = m_ImgColor;
        m_MusicTitle.text = m_AudioClips[m_CurrentClip].name;
	}

    void StartClip(int clipID)
    {
        m_AudioSource.clip = m_AudioClips[clipID];
        m_AudioSource.Play();
    }
}
