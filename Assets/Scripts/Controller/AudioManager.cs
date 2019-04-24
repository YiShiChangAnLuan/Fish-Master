using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private static AudioManager _instance;
    public static AudioManager Instance{
        get{
            return _instance;
        }
    }
    // 是否静音
    private bool isMute = true;

    // 游戏中的音效

    // 背景音
    public AudioSource bgmAudioSource;
    // 水波转场
    public AudioClip seaWaveClip;
    // 获得金币
    public AudioClip goldClip;
    // 获得奖励
    public AudioClip rewardClip;
    // 开火
    public AudioClip fireClip;
    // 换枪
    public AudioClip exchangeClip;
    // 升级
    public AudioClip lvUpClip;


	// 提供静音功能
	public void SwitchMuteState(bool isOn)
    {
        // isMute:是否静音，所以要取个反
        isMute = !isOn;
        DoMute();
    }

    public void PlayEffectSound(AudioClip clip){
        if(!isMute){
            // 音效播放地点，在Camera附近
            AudioSource.PlayClipAtPoint(clip, new Vector3(0, 0, 0));
        }

    }
    // 提供一个get方法，得到私有变量
    public bool IsMute{
        get{
            return isMute;
        }
    }

	private void Awake()
	{
        _instance = this;
        // 改写，如果之前禁了音，进行判断禁不禁音
        isMute = (PlayerPrefs.GetInt("mute", 0) == 0) ? false : true;
        DoMute();
	}

    void DoMute(){
        if (isMute)
        {
            // 静音
            bgmAudioSource.Pause();
        }
        else
        {
            // 取消静音
            bgmAudioSource.Play();
        }
        
    }
}
