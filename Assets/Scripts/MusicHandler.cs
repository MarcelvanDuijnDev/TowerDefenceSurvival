using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicHandler : MonoBehaviour 
{
    [Header("Refs")]
    [SerializeField]private AudioClip[] _AudioClips = null;
    [SerializeField]private AudioSource _AudioSource = null;
    [SerializeField]private Image _BGImage = null;
    [SerializeField]private Text _MusicTitle = null;

    [Header("Options")]
    [SerializeField]private bool _Off = false;
    [SerializeField]private bool _Shuffle = true;
    [SerializeField]private float _InfoTime = 3;

    private int _CurrentClip;
    private bool _NewClip;
    private float _InfoTimeC;
    private Color _ImgColor;
    private Color _TextColor;

    void Start()
    {
        if (!_Off)
        {
            if (_Shuffle)
            {
                _CurrentClip = Random.Range(0, _AudioClips.Length);
                StartClip(_CurrentClip);
            }
            else
            {
                StartClip(0);
            }
            _ImgColor = _BGImage.color;
            _TextColor = _MusicTitle.color;
            _InfoTimeC = _InfoTime;
            _NewClip = true;
        }
    }
	
	void Update () 
    {
        if (!_AudioSource.isPlaying && !_Off)
        {
            if (_CurrentClip != _AudioClips.Length - 2)
            {
                if (_Shuffle)
                {
                    _CurrentClip = Random.Range(0, _AudioClips.Length);
                }
                else
                {
                    _CurrentClip += 1;
                }
                StartClip(_CurrentClip);
                _NewClip = true;
            }
        }

        if (_NewClip)
        {
            _ImgColor.a += 0.2f * Time.deltaTime;
            _TextColor.a += 0.3f * Time.deltaTime;
            _BGImage.rectTransform.sizeDelta = new Vector2(_AudioClips[_CurrentClip].name.Length * 22, 50);

            _InfoTimeC -= 1 * Time.deltaTime;
            if (_InfoTimeC <= 0)
            {
                _NewClip = false;
                _InfoTimeC = _InfoTime;
            }
        }
        else
        {
            if (_ImgColor.a > 0)
            {
                _ImgColor.a -= 0.2f * Time.deltaTime;
            }
            if (_TextColor.a > 0)
            {
                _TextColor.a -= 0.3f * Time.deltaTime;
            }
        }

        _MusicTitle.color = _TextColor;
        _BGImage.color = _ImgColor;
        _MusicTitle.text = _AudioClips[_CurrentClip].name;
	}

    void StartClip(int clipID)
    {
        _AudioSource.clip = _AudioClips[clipID];
        _AudioSource.Play();
    }
}
