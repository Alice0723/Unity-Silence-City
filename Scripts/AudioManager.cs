using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    // 获取声音播放组件
    private AudioSource audioSource;

    public AudioClip[] AudioClips;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 播放音频
    /// </summary>
    /// <param name="audioClip"></param>
    public void PlayAudio(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
